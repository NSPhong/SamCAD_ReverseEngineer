using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SamSoarII.Utility.Collection;

namespace SamSoarII.Polyline.Entity.Module;

internal class CornerOptimizeModule : IDisposable
{
	internal enum PolylineStatus
	{
		None = 0,
		HasStart = 1,
		HasEnd = 2,
		IsCycle = 4,
		IsCycleTargeted1 = 8,
		IsCycleTargeted2 = 0x10
	}

	internal enum TangentResultStatus
	{
		None = 0,
		GroupAny_Back = 1,
		GroupAny_Next = 2,
		Entity_Back = 4,
		Entity_Next = 8
	}

	internal class GroupData : IDisposable
	{
		private bool isdisposed = false;

		private PolylineStatus status;

		private List<PolylineMeaning> means;

		private PolylineMeaning[] results;

		private PolylineMeaning[] targets;

		public bool IsDisposed => isdisposed;

		public PolylineStatus Status
		{
			get
			{
				return status;
			}
			set
			{
				status = value;
			}
		}

		public List<PolylineMeaning> Means => means;

		public IList<PolylineMeaning> Results => results;

		public IList<PolylineMeaning> Targets => targets;

		public GroupData(PolylineStatus _status)
		{
			status = _status;
			means = new List<PolylineMeaning>();
			results = new PolylineMeaning[2];
			targets = new PolylineMeaning[2];
		}

		public void Dispose()
		{
			if (!isdisposed)
			{
				isdisposed = true;
				means.Clear();
				means = null;
				results = null;
				targets = null;
			}
		}
	}

	internal class ModuleData : IDisposable
	{
		private bool isdisposed = false;

		private List<PolylineMeaning> items;

		private int activecount;

		public bool IsDisposed => isdisposed;

		public List<PolylineMeaning> Items => items;

		public int ActiveCount => activecount;

		public ModuleData()
		{
			items = new List<PolylineMeaning>();
			activecount = 0;
		}

		public void Dispose()
		{
			if (!isdisposed)
			{
				isdisposed = true;
				items.Clear();
				items = null;
				activecount = 0;
			}
		}

		public void Add(PolylineMeaning item)
		{
			items.Add(item);
			activecount += (item.IsActive ? 1 : 0);
		}

		public void Active(PolylineMeaning item)
		{
			if (!item.IsActive)
			{
				item.IsActive = true;
				activecount++;
			}
		}

		public void Unactive(PolylineMeaning item)
		{
			if (item.IsActive)
			{
				item.IsActive = false;
				activecount--;
			}
		}
	}

	internal class PoleData : IDisposable
	{
		private bool isdisposed = false;

		private CornerOptimizeModule parent;

		private double angle;

		private double sin;

		private double cos;

		private double min;

		private double max;

		private ModuleData[] data;

		public bool IsDisposed => isdisposed;

		public CornerOptimizeModule Parent => parent;

		public IPolylineImage Image => parent.Image;

		public double Angle => angle;

		public double Sin => sin;

		public double Cos => cos;

		public double Min => min;

		public double Max => max;

		public IList<ModuleData> Data => data;

		public PoleData(CornerOptimizeModule _parent, double _angle)
		{
			parent = _parent;
			angle = _angle;
			sin = Math.Sin(angle);
			cos = Math.Cos(angle);
			Point p = default(Point);
			Point p2 = default(Point);
			if (angle <= Math.PI / 2.0)
			{
				p.X = 0.0 - Image.Width;
				p.Y = 0.0 - Image.Height;
				p2.X = Image.Width;
				p2.Y = Image.Height;
			}
			else
			{
				p.X = Image.Width;
				p.Y = 0.0 - Image.Height;
				p2.X = 0.0 - Image.Width;
				p2.Y = Image.Height;
			}
			double offset = GetOffset(p);
			double offset2 = GetOffset(p2);
			min = Math.Min(offset, offset2);
			max = Math.Max(offset, offset2);
			data = new ModuleData[parent.Height];
		}

		public void Dispose()
		{
			if (!isdisposed)
			{
				isdisposed = true;
				parent = null;
				data = null;
			}
		}

		public double GetOffset(Point p)
		{
			Vector vector = new Vector(0.0 - cos, sin);
			Vector vector2 = new Vector(p.X, p.Y);
			Vector vector3 = default(Vector);
			double num = Vector.CrossProduct(vector2, vector);
			if (num > 0.0)
			{
				vector3 = new Vector(0.0 - vector.Y, vector.X);
				vector3 *= num / vector2.Length;
			}
			else
			{
				vector3 = new Vector(vector.Y, 0.0 - vector.X);
				vector3 *= (0.0 - num) / vector2.Length;
			}
			vector3 = p + vector3 - new Point(0.0, 0.0);
			return Vector.CrossProduct(new Vector(1.0, 0.0), vector3);
		}

		public int GetIndex(Point p)
		{
			double offset = GetOffset(p);
			int val = (int)((offset - min) / (max - min) * (double)parent.Height);
			val = Math.Min(val, parent.Height - 1);
			return Math.Max(val, 0);
		}

		public void Add(PolylineMeaning item)
		{
			int index = GetIndex(item.Point);
			if (data[index] == null)
			{
				data[index] = new ModuleData();
			}
			data[index].Add(item);
		}

		public void Active(PolylineMeaning item)
		{
			int index = GetIndex(item.Point);
			data[index]?.Active(item);
		}

		public void Unactive(PolylineMeaning item)
		{
			int index = GetIndex(item.Point);
			data[index]?.Unactive(item);
		}
	}

	internal class TangentResult
	{
		private Vector tangent;

		private IPolylineEntity entity;

		private TangentResultStatus status;

		public Vector Tangent => tangent;

		public IPolylineEntity Entity => entity;

		public TangentResultStatus Status => status;

		public TangentResult(Vector _tangent, IPolylineEntity _entity, TangentResultStatus _status)
		{
			tangent = _tangent;
			entity = _entity;
			status = _status;
		}
	}

	private bool isdisposed = false;

	private List<GroupData> list;

	private List<PoleData> pole;

	private QuickSet qset;

	private PolylineMeaning zero;

	private PolylineMeaning zeroto;

	private IPolylineImage image;

	private int height;

	private int anglecount;

	public bool IsDisposed => isdisposed;

	public IPolylineImage Image => image;

	public int Height => height;

	public int AngleCount => anglecount;

	public CornerOptimizeModule(IPolylineImage _image, int _height = 100, int _anglecount = 90)
	{
		image = _image;
		height = _height;
		anglecount = _anglecount;
		list = new List<GroupData>();
		pole = new List<PoleData>();
		qset = new QuickSet();
		zero = new PolylineMeaning(null, new Point(0.0, 0.0), Meaning.ZeroPoint);
		zeroto = null;
		for (int i = 0; i < _anglecount; i++)
		{
			double angle = Math.PI * (double)i / (double)anglecount;
			PoleData item = new PoleData(this, angle);
			pole.Add(item);
		}
	}

	public void Dispose()
	{
		if (isdisposed)
		{
			return;
		}
		isdisposed = true;
		foreach (PoleData item in pole)
		{
			item.Dispose();
		}
		list?.Clear();
		pole?.Clear();
		qset?.Dispose();
		list = null;
		pole = null;
		image = null;
		qset = null;
	}

	protected int GetAngleIndex(Vector v)
	{
		double num;
		for (num = Vector.AngleBetween(new Vector(1.0, 0.0), v); num < 0.0; num += 180.0)
		{
		}
		while (num > 180.0)
		{
			num -= 180.0;
		}
		return (int)(num * (double)anglecount / 180.0);
	}

	protected void Handle(PolylineMeaning item, Action<PoleData, PolylineMeaning> action)
	{
		switch (item.Meaning)
		{
		case Meaning.GroupStart:
			if (item.Aid1 == -1)
			{
				item.Aid1 = GetAngleIndex(item.Entity.Tangent);
			}
			action(pole[item.Aid1], item);
			break;
		case Meaning.GroupEnd:
			if (item.Aid2 == -1)
			{
				item.Aid2 = GetAngleIndex(item.Entity.TangentBack);
			}
			action(pole[item.Aid2], item);
			break;
		case Meaning.GroupAny:
		{
			IPolylineEntity entity = item.Entity;
			IPolylineGroup polylineGroup = entity.Group;
			IPolylineEntity polylineEntity = ((entity.ID + 1 >= polylineGroup.Start + polylineGroup.Count) ? image.Items[polylineGroup.Start] : image.Items[entity.ID + 1]);
			if (item.Aid1 == -1)
			{
				item.Aid1 = GetAngleIndex(entity.TangentBack);
			}
			if (item.Aid2 == -1)
			{
				item.Aid2 = GetAngleIndex(polylineEntity.Tangent);
			}
			action(pole[item.Aid1], item);
			action(pole[item.Aid2], item);
			break;
		}
		case Meaning.ZeroPoint:
		{
			foreach (PoleData item2 in pole)
			{
				action(item2, item);
			}
			break;
		}
		}
	}

	protected void DataAdd(PolylineMeaning item)
	{
		Handle(item, delegate(PoleData pd, PolylineMeaning i)
		{
			pd.Add(i);
		});
	}

	protected void Active(PolylineMeaning item)
	{
		Handle(item, delegate(PoleData pd, PolylineMeaning i)
		{
			pd.Active(i);
		});
	}

	protected void Unactive(PolylineMeaning item)
	{
		Handle(item, delegate(PoleData pd, PolylineMeaning i)
		{
			pd.Unactive(i);
		});
	}

	protected bool AssertResult(PolylineMeaning item1, PolylineMeaning item2, int rangeangle, int rangeoffset)
	{
		if (item1 == null || item2 == null || item1 == item2)
		{
			return false;
		}
		if (!item1.IsActive || !item2.IsActive)
		{
			return false;
		}
		if (item1.Meaning != Meaning.ZeroPoint && item1.GID == -1)
		{
			return false;
		}
		if (item2.Meaning != Meaning.ZeroPoint && item2.GID == -1)
		{
			return false;
		}
		if (item1.Meaning != Meaning.ZeroPoint && item2.Meaning != Meaning.ZeroPoint && item1.GID == item2.GID)
		{
			return false;
		}
		if (qset.IsSameRoot(item1, item2))
		{
			return false;
		}
		if (rangeangle < anglecount / 2)
		{
		}
		return true;
	}

	protected void DesideResult(PolylineMeaning item1, PolylineMeaning item2)
	{
	}

	public IList<IPolylineEntity> Sort()
	{
		List<PolylineMeaning> list = new List<PolylineMeaning>();
		List<PolylineMeaning> list2 = new List<PolylineMeaning>();
		List<IPolylineEntity> list3 = new List<IPolylineEntity>();
		foreach (IPolylineGroup group in image.Groups)
		{
			IPolylineEntity polylineEntity = group.FirstOrDefault();
			IPolylineEntity polylineEntity2 = group.LastOrDefault();
			Point point = polylineEntity.From;
			Point to = polylineEntity2.To;
			PolylineMeaning polylineMeaning = null;
			GroupData groupData = null;
			if (point.Equals(to))
			{
				groupData = new GroupData(PolylineStatus.IsCycle);
				for (int i = group.Start; i < group.Start + group.Count; i++)
				{
					IPolylineEntity polylineEntity3 = image.Items[i];
					polylineMeaning = new PolylineMeaning(polylineEntity3, polylineEntity3.To, Meaning.GroupAny);
					groupData.Means.Add(polylineMeaning);
					DataAdd(polylineMeaning);
				}
			}
			else
			{
				groupData = new GroupData((PolylineStatus)3);
				polylineMeaning = new PolylineMeaning(polylineEntity, point, Meaning.GroupStart);
				groupData.Means.Add(polylineMeaning);
				DataAdd(polylineMeaning);
				polylineMeaning = new PolylineMeaning(polylineEntity2, to, Meaning.GroupEnd);
				groupData.Means.Add(polylineMeaning);
				DataAdd(polylineMeaning);
			}
			this.list.Add(groupData);
		}
		foreach (GroupData item in this.list)
		{
			for (int j = 1; j < item.Means.Count(); j++)
			{
				qset.Add(item.Means[j], item.Means[j - 1]);
			}
		}
		for (int k = 0; k < Math.Max(height, anglecount); k++)
		{
			int num = Math.Min(k, height);
			int num2 = Math.Min(k, anglecount);
			for (int l = 0; l < anglecount; l++)
			{
				for (int m = 0; m < height; m++)
				{
					if (pole[l].Data[m] == null || pole[l].Data[m].ActiveCount == 0)
					{
						continue;
					}
					list.Clear();
					list.AddRange(pole[l].Data[m].Items.Where((PolylineMeaning polylineMeaning3) => polylineMeaning3.IsActive));
					foreach (PolylineMeaning item2 in list)
					{
						if (!item2.IsActive)
						{
							continue;
						}
						list2.Clear();
						for (int num3 = Math.Max(l - num2, 0); num3 < Math.Min(l + num2 + 1, anglecount); num3++)
						{
							int index = pole[num3].GetIndex(item2.Point);
							int num4 = Math.Max(index - num, 0);
							int num5 = Math.Max(index + num, height - 1);
							if (num3 == l - num2 || num3 == l + num2)
							{
								for (int num6 = num4; num6 <= num5; num6++)
								{
									if (pole[num3].Data[num6] != null && pole[num3].Data[num6].ActiveCount != 0)
									{
										list2.AddRange(pole[num3].Data[num6].Items.Where((PolylineMeaning polylineMeaning3) => polylineMeaning3.IsActive));
									}
								}
								continue;
							}
							if (num4 == index - num && pole[num3].Data[num4] != null && pole[num3].Data[num4].ActiveCount > 0)
							{
								list2.AddRange(pole[num3].Data[num4].Items.Where((PolylineMeaning polylineMeaning3) => polylineMeaning3.IsActive));
							}
							if (num5 == index + num && pole[num3].Data[num5] != null && pole[num3].Data[num5].ActiveCount > 0)
							{
								list2.AddRange(pole[num3].Data[num5].Items.Where((PolylineMeaning polylineMeaning3) => polylineMeaning3.IsActive));
							}
						}
						double num7 = double.MaxValue;
						PolylineMeaning polylineMeaning2 = null;
						foreach (PolylineMeaning item3 in list2)
						{
							if (AssertResult(item2, item3, num2, num))
							{
								double lengthSquared = (item3.Point - item2.Point).LengthSquared;
								if (lengthSquared < num7)
								{
									num7 = lengthSquared;
									polylineMeaning2 = item3;
								}
							}
						}
						if (polylineMeaning2 != null)
						{
							DesideResult(item2, polylineMeaning2);
						}
					}
				}
			}
		}
		return null;
	}
}

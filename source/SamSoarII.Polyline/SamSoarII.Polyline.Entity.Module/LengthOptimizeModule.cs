using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using SamSoarII.Utility.Collection;

namespace SamSoarII.Polyline.Entity.Module;

internal class LengthOptimizeModule : IDisposable
{
	internal enum PolylineStatus
	{
		None = 0,
		HasStart = 1,
		HasEnd = 2,
		IsCycle = 4,
		IsCycleTargeted = 8
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

	internal class ModuleVector
	{
		private int x;

		private int y;

		private int lengthsquared;

		public int X => x;

		public int Y => y;

		public int LengthSquared => lengthsquared;

		public ModuleVector(int _x, int _y)
		{
			x = _x;
			y = _y;
			lengthsquared = x * x + y * y;
		}
	}

	private bool isdisposed = false;

	private ModuleData[,] data;

	private List<GroupData> list;

	private List<ModuleVector> mvecs;

	private QuickSet qset;

	private PolylineMeaning zero;

	private PolylineMeaning zeroto;

	private IPolylineImage image;

	private int width;

	private int height;

	public bool IsDisposed => isdisposed;

	public IPolylineImage Image => image;

	public int Width => width;

	public int Height => height;

	public LengthOptimizeModule(IPolylineImage _image, int _width = 100, int _height = 100)
	{
		image = _image;
		width = _width;
		height = _height;
		data = new ModuleData[width, height];
		list = new List<GroupData>();
		qset = new QuickSet();
		mvecs = new List<ModuleVector>();
		zero = new PolylineMeaning(null, new Point(0.0, 0.0), Meaning.ZeroPoint);
		zeroto = null;
		DataAdd(zero);
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				mvecs.Add(new ModuleVector(i, j));
			}
		}
		mvecs.Sort((ModuleVector v1, ModuleVector v2) => v1.LengthSquared.CompareTo(v2.LengthSquared));
	}

	public void Dispose()
	{
		if (isdisposed)
		{
			return;
		}
		isdisposed = true;
		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				if (data[i, j] != null)
				{
					data[i, j].Dispose();
					data[i, j] = null;
				}
			}
		}
		list?.Clear();
		mvecs?.Clear();
		qset?.Dispose();
		data = null;
		list = null;
		mvecs = null;
		image = null;
		qset = null;
	}

	protected int GetX(Point p)
	{
		int val = (int)((p.X - image.Left) / image.Width * (double)width);
		val = Math.Min(val, width - 1);
		return Math.Max(val, 0);
	}

	protected int GetY(Point p)
	{
		int val = (int)((p.Y - image.Top) / image.Height * (double)height);
		val = Math.Min(val, height - 1);
		return Math.Max(val, 0);
	}

	protected void DataAdd(PolylineMeaning item)
	{
		int x = GetX(item.Point);
		int y = GetY(item.Point);
		if (data[x, y] == null)
		{
			data[x, y] = new ModuleData();
		}
		data[x, y].Add(item);
	}

	protected ModuleData DataGet(PolylineMeaning item)
	{
		int x = GetX(item.Point);
		int y = GetY(item.Point);
		return data[x, y];
	}

	protected void DecideResultSide(PolylineMeaning item, PolylineMeaning that, GroupData gdata)
	{
		switch (item.Meaning)
		{
		case Meaning.ZeroPoint:
			zeroto = that;
			break;
		case Meaning.GroupStart:
			gdata.Targets[0] = item;
			gdata.Results[0] = that;
			gdata.Status &= (PolylineStatus)(-2);
			break;
		case Meaning.GroupEnd:
			gdata.Targets[1] = item;
			gdata.Results[1] = that;
			gdata.Status &= (PolylineStatus)(-3);
			break;
		case Meaning.GroupAny:
			if ((gdata.Status & PolylineStatus.IsCycleTargeted) == 0)
			{
				gdata.Status |= PolylineStatus.IsCycleTargeted;
				gdata.Targets[0] = item;
				gdata.Results[0] = that;
				{
					foreach (PolylineMeaning item2 in gdata.Means)
					{
						ModuleData moduleData = DataGet(item2);
						if (item2 == item)
						{
							moduleData.Active(item2);
						}
						else
						{
							moduleData.Unactive(item2);
						}
					}
					break;
				}
			}
			gdata.Targets[1] = item;
			gdata.Results[1] = that;
			break;
		}
	}

	protected bool AssertResult(PolylineMeaning item1, PolylineMeaning item2)
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
		return true;
	}

	protected bool DecideResult(PolylineMeaning item1, PolylineMeaning item2, ModuleData mdata1 = null, ModuleData mdata2 = null, GroupData gdata1 = null, GroupData gdata2 = null)
	{
		if (!AssertResult(item1, item2))
		{
			return false;
		}
		if (gdata1 == null && item1.Meaning != Meaning.ZeroPoint)
		{
			gdata1 = list[item1.GID];
		}
		if (gdata2 == null && item2.Meaning != Meaning.ZeroPoint)
		{
			gdata2 = list[item2.GID];
		}
		if (mdata1 == null)
		{
			mdata1 = DataGet(item1);
		}
		if (mdata2 == null)
		{
			mdata2 = DataGet(item2);
		}
		mdata1.Unactive(item1);
		mdata2.Unactive(item2);
		DecideResultSide(item1, item2, gdata1);
		DecideResultSide(item2, item1, gdata2);
		qset.Add(item1, item2);
		return true;
	}

	protected void DecideResultList(IList<PolylineMeaning> means)
	{
		for (int i = 0; i < means.Count(); i++)
		{
			if (!means[i].IsActive)
			{
				continue;
			}
			double num = double.MaxValue;
			PolylineMeaning polylineMeaning = null;
			for (int j = i + 1; j < means.Count(); j++)
			{
				PolylineMeaning polylineMeaning2 = means[j];
				if (AssertResult(means[i], polylineMeaning2))
				{
					double lengthSquared = (polylineMeaning2.Point - means[i].Point).LengthSquared;
					if (lengthSquared < num)
					{
						num = lengthSquared;
						polylineMeaning = polylineMeaning2;
					}
				}
			}
			if (polylineMeaning != null)
			{
				DecideResult(means[i], polylineMeaning);
			}
		}
	}

	public IList<IPolylineEntity> Sort()
	{
		List<PolylineMeaning> list = new List<PolylineMeaning>();
		List<IPolylineEntity> list2 = new List<IPolylineEntity>();
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
		foreach (ModuleVector mvec in mvecs)
		{
			for (int k = 0; k < width; k++)
			{
				for (int l = 0; l < height; l++)
				{
					if (data[k, l] == null || data[k, l].ActiveCount == 0)
					{
						continue;
					}
					if (mvec.X == 0 && mvec.Y == 0)
					{
						list.Clear();
						list.AddRange(data[k, l].Items.Where((PolylineMeaning polylineMeaning5) => polylineMeaning5.IsActive));
						DecideResultList(list);
						continue;
					}
					for (int num = -1; num <= 1; num += 2)
					{
						for (int num2 = -1; num2 <= 1; num2 += 2)
						{
							int num3 = k + mvec.X * num;
							int num4 = l + mvec.Y * num2;
							if (num3 >= 0 && num3 < width && num4 >= 0 && num4 < height && data[num3, num4] != null && data[num3, num4].ActiveCount > 0)
							{
								list.Clear();
								list.AddRange(data[k, l].Items.Where((PolylineMeaning polylineMeaning5) => polylineMeaning5.IsActive));
								list.AddRange(data[num3, num4].Items.Where((PolylineMeaning polylineMeaning5) => polylineMeaning5.IsActive));
								DecideResultList(list);
							}
						}
					}
				}
			}
		}
		if (zeroto == null)
		{
			for (int num5 = 0; num5 < width; num5++)
			{
				for (int num6 = 0; num6 < height; num6++)
				{
					if (data[num5, num6] != null && data[num5, num6].ActiveCount != 0)
					{
						PolylineMeaning polylineMeaning2 = data[num5, num6].Items.FirstOrDefault((PolylineMeaning d) => d.IsActive && d.Meaning != Meaning.ZeroPoint);
						if (polylineMeaning2 != null)
						{
							DecideResult(zero, polylineMeaning2);
						}
					}
				}
			}
		}
		if (zeroto == null)
		{
			return null;
		}
		IPolylineGroup polylineGroup = image.Groups[zeroto.GID];
		GroupData groupData2 = this.list[polylineGroup.GID];
		PolylineMeaning polylineMeaning3 = zeroto;
		PolylineMeaning polylineMeaning4 = zero;
		List<int> list3 = new List<int>();
		while (polylineMeaning3 != null && polylineGroup != null && groupData2 != null)
		{
			list3.Add(polylineGroup.GID);
			IPolylineEntity polylineEntity4 = null;
			polylineEntity4 = ((polylineGroup.Start <= 0) ? new PolylineLine(image, list2.Count(), polylineMeaning3.Point, _isreal: false) : image.Items[polylineGroup.Start - 1].Clone());
			polylineEntity4.To = polylineMeaning3.Point;
			list2.Add(polylineEntity4);
			switch (polylineMeaning3.Meaning)
			{
			case Meaning.GroupStart:
			{
				for (int num10 = polylineGroup.Start; num10 < polylineGroup.Start + polylineGroup.Count; num10++)
				{
					IPolylineEntity polylineEntity8 = image.Items[num10].Clone();
					polylineEntity8.ID = list2.Count();
					list2.Add(polylineEntity8);
				}
				polylineMeaning4 = polylineMeaning3;
				polylineMeaning3 = groupData2.Results[1];
				break;
			}
			case Meaning.GroupEnd:
			{
				for (int num9 = polylineGroup.Start + polylineGroup.Count - 1; num9 >= polylineGroup.Start; num9--)
				{
					IPolylineEntity polylineEntity7 = image.Items[num9].Reverse();
					polylineEntity7.ID = list2.Count();
					list2.Add(polylineEntity7);
				}
				polylineMeaning4 = polylineMeaning3;
				polylineMeaning3 = groupData2.Results[0];
				break;
			}
			case Meaning.GroupAny:
			{
				for (int num7 = polylineMeaning3.Entity.ID + 1; num7 < polylineGroup.Start + polylineGroup.Count; num7++)
				{
					IPolylineEntity polylineEntity5 = image.Items[num7].Clone();
					polylineEntity5.ID = list2.Count();
					list2.Add(polylineEntity5);
				}
				for (int num8 = polylineGroup.Start; num8 <= polylineMeaning3.Entity.ID; num8++)
				{
					IPolylineEntity polylineEntity6 = image.Items[num8].Clone();
					polylineEntity6.ID = list2.Count();
					list2.Add(polylineEntity6);
				}
				if (polylineMeaning4.GID == groupData2.Results[0].GID)
				{
					polylineMeaning4 = polylineMeaning3;
					polylineMeaning3 = groupData2.Results[1];
				}
				else
				{
					polylineMeaning4 = polylineMeaning3;
					polylineMeaning3 = groupData2.Results[0];
				}
				break;
			}
			}
			if (polylineMeaning3 != null)
			{
				polylineGroup = image.Groups[polylineMeaning3.GID];
				groupData2 = this.list[polylineMeaning3.GID];
			}
			else
			{
				polylineGroup = null;
				groupData2 = null;
			}
		}
		return list2;
	}
}

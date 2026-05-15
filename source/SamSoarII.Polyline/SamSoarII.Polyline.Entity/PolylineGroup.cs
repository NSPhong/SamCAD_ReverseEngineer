using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using SamSoarII.Core.Files;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Entity;

public class PolylineGroup : PolylineEntity, IPolylineGroup, IPolylineEntity, INotifyPropertyChanged, IDisposable, IGridPenningEntity, IEnumerable<IPolylineEntity>, IEnumerable
{
	private int gid;

	private int start;

	private int count;

	private bool isexpand;

	public override PolylineType Type => PolylineType.Group;

	public int GID
	{
		get
		{
			return gid;
		}
		set
		{
			gid = value;
			InvokePropertyChanged("GID");
		}
	}

	public int Start
	{
		get
		{
			return start;
		}
		set
		{
			start = value;
			InvokePropertyChanged("Start");
		}
	}

	public int Count
	{
		get
		{
			return count;
		}
		set
		{
			count = value;
			InvokePropertyChanged("Count");
		}
	}

	public bool IsExpand
	{
		get
		{
			return isexpand;
		}
		set
		{
			isexpand = value;
			InvokePropertyChanged("IsExpand");
		}
	}

	public IPolylineEntity this[int id] => base.Parent.Items[start + id];

	public PolylineGroup(IPolylineImage _parent, int _id, int _gid, int _start, int _count)
		: base(_parent, _id, default(Point))
	{
		gid = _gid;
		start = _start;
		count = _count;
	}

	protected override string _GetName()
	{
		return $"Group {gid}";
	}

	public IEnumerator<IPolylineEntity> GetEnumerator()
	{
		for (int i = start; i < start + count; i++)
		{
			yield return base.Parent.Items[i];
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	public void Save(PolylineGroupHeader header)
	{
		FileFormat.AllocHeaderString(header, 0, HasRealName() ? base.Name : null);
		header.dwStart = start;
		header.dwCount = count;
	}

	public void Load(PolylineGroupHeader header)
	{
		base.Name = FileFormat.GetString(header.spName);
		start = header.dwStart;
		count = header.dwCount;
	}

	public bool IsLeftInside()
	{
		double num = 0.0;
		double num2 = double.NegativeInfinity;
		bool result = false;
		for (int i = 0; i < Count; i++)
		{
			num += this[i].To.X;
		}
		num /= (double)Count;
		for (int j = 0; j < Count; j++)
		{
			IPolylineEntity polylineEntity = this[j];
			Point point = polylineEntity.From;
			Point point2 = polylineEntity.To;
			if (point.X == num && point2.X == num)
			{
				continue;
			}
			if (point.X <= num && point2.X >= num)
			{
				double num3 = point.Y + (point2.Y - point.Y) * (num - point.X) / (point2.X - point.X);
				if (num3 > num2)
				{
					num2 = num3;
					result = false;
				}
			}
			else if (point.X >= num && point2.X <= num)
			{
				double num4 = point.Y + (point2.Y - point.Y) * (num - point2.X) / (point.X - point2.X);
				if (num4 > num2)
				{
					num2 = num4;
					result = true;
				}
			}
		}
		return result;
	}
}

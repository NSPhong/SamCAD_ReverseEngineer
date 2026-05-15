using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using SamSoarII.Polyline.Entity;

namespace SamSoarII.Polyline.Control;

public class PolylineDataGridItem : INotifyPropertyChanged, IDisposable
{
	public static readonly string[] _Types_S = new string[8] { "Dotted line", "Straight line", "Complete circle", "顺when针圆弧", "逆when针圆弧", "顺when针圆弧（长弧）", "逆when针圆弧（长弧）", "Punching holes" };

	private Point center = default(Point);

	private bool isclockwise = false;

	private bool islarge = false;

	private PolylineDataGridWindow parent;

	private IPolylineEntity core;

	private List<IPolylineDataGridControl> views;

	private DataGridRow row;

	public PolylineDataGridWindow Parent => parent;

	public IPolylineEntity Core
	{
		get
		{
			return core;
		}
		set
		{
			if (core != null)
			{
				core.PropertyChanged -= OnCorePropertyChanged;
			}
			core = value;
			if (core != null)
			{
				core.PropertyChanged += OnCorePropertyChanged;
			}
		}
	}

	public int ID => core.ID;

	public double X
	{
		get
		{
			return core.To.X;
		}
		set
		{
			Point to = core.To;
			core.To = new Point(value, core.To.Y);
			IPolylineAction action = new PolylineAction(core.ID, ChangedTarget.To, to, core.To);
			parent?.InvokeEntityChange(action);
		}
	}

	public double Y
	{
		get
		{
			return core.To.Y;
		}
		set
		{
			Point to = core.To;
			core.To = new Point(core.To.X, value);
			IPolylineAction action = new PolylineAction(core.ID, ChangedTarget.To, to, core.To);
			parent?.InvokeEntityChange(action);
		}
	}

	public double R
	{
		get
		{
			return (core is IPolylineCircle) ? ((IPolylineCircle)core).Radius : 0.0;
		}
		set
		{
			if (core is IPolylineCircle)
			{
				IPolylineCircle polylineCircle = (IPolylineCircle)core;
				double radius = polylineCircle.Radius;
				polylineCircle.Radius = value;
				IPolylineAction action = new PolylineAction(core.ID, ChangedTarget.Radius, radius, polylineCircle.Radius);
				parent?.InvokeEntityChange(action);
			}
		}
	}

	public double CenterX
	{
		get
		{
			return (core is IPolylineCircle) ? ((IPolylineCircle)core).Center.X : 0.0;
		}
		set
		{
			if (core is IPolylineCircle)
			{
				IPolylineCircle polylineCircle = (IPolylineCircle)core;
				Point point = polylineCircle.Center;
				polylineCircle.Center = new Point(value, point.Y);
				IPolylineAction action = new PolylineAction(core.ID, ChangedTarget.Center, ChangedFlags.OnlyX, point, polylineCircle.Center);
				parent?.InvokeEntityChange(action);
			}
		}
	}

	public double CenterY
	{
		get
		{
			return (core is IPolylineCircle) ? ((IPolylineCircle)core).Center.Y : 0.0;
		}
		set
		{
			if (core is IPolylineCircle)
			{
				IPolylineCircle polylineCircle = (IPolylineCircle)core;
				Point point = polylineCircle.Center;
				polylineCircle.Center = new Point(point.X, value);
				IPolylineAction action = new PolylineAction(core.ID, ChangedTarget.Center, ChangedFlags.OnlyY, point, polylineCircle.Center);
				parent?.InvokeEntityChange(action);
			}
		}
	}

	public PolylineDataGridType Type
	{
		get
		{
			if (core is IPolylineArch)
			{
				IPolylineArch polylineArch = (IPolylineArch)core;
				if (polylineArch.IsClockwise)
				{
					return polylineArch.IsLarge ? PolylineDataGridType.ClockLongArch : PolylineDataGridType.ClockArch;
				}
				return polylineArch.IsLarge ? PolylineDataGridType.CoClockLongArch : PolylineDataGridType.CoClockArch;
			}
			if (core is IPolylineCircle)
			{
				IPolylineCircle polylineCircle = (IPolylineCircle)core;
				return PolylineDataGridType.Circle;
			}
			if (core is IPolylineLine)
			{
				IPolylineLine polylineLine = (IPolylineLine)core;
				return polylineLine.IsReal ? PolylineDataGridType.Line : (polylineLine.IsSlot ? PolylineDataGridType.Slot : PolylineDataGridType.Virtual);
			}
			return PolylineDataGridType.Line;
		}
		set
		{
			PolylineDataGridType type = Type;
			IPolylineEntity polylineEntity = null;
			IPolylineCircle polylineCircle = ((core is IPolylineCircle) ? ((IPolylineCircle)core) : null);
			IPolylineArch polylineArch = ((core is IPolylineArch) ? ((IPolylineArch)core) : null);
			if (polylineCircle != null)
			{
				center = polylineCircle.Center;
				isclockwise = polylineCircle.IsClockwise;
			}
			if (polylineArch != null)
			{
				islarge = polylineArch.IsLarge;
			}
			if (value == PolylineDataGridType.Line || value == PolylineDataGridType.Virtual || value == PolylineDataGridType.Slot)
			{
				polylineEntity = new PolylineLine(core.Parent, core.ID, core.To, value == PolylineDataGridType.Line);
				polylineEntity.IsSlot = value == PolylineDataGridType.Slot;
			}
			else if (value == PolylineDataGridType.Circle)
			{
				polylineEntity = new PolylineCircle(core.Parent, core.ID, core.To, center, isclockwise);
			}
			else
			{
				if ((type == PolylineDataGridType.ClockArch && value == PolylineDataGridType.CoClockArch) || (type == PolylineDataGridType.ClockLongArch && value == PolylineDataGridType.CoClockLongArch) || (type == PolylineDataGridType.CoClockArch && value == PolylineDataGridType.ClockArch) || (type == PolylineDataGridType.CoClockLongArch && value == PolylineDataGridType.ClockLongArch) || (type == PolylineDataGridType.ClockArch && value == PolylineDataGridType.ClockLongArch) || (type == PolylineDataGridType.ClockLongArch && value == PolylineDataGridType.ClockArch) || (type == PolylineDataGridType.CoClockArch && value == PolylineDataGridType.CoClockLongArch) || (type == PolylineDataGridType.CoClockLongArch && value == PolylineDataGridType.CoClockArch))
				{
					center = center + (core.From - center) + (core.To - center);
				}
				polylineEntity = new PolylineArch(core.Parent, core.ID, core.To, center, value == PolylineDataGridType.ClockArch || value == PolylineDataGridType.ClockLongArch, value == PolylineDataGridType.ClockLongArch || value == PolylineDataGridType.CoClockLongArch);
			}
			IPolylineAction action = core.Parent.Replace(core.ID, 1, new IPolylineEntity[1] { polylineEntity });
			core = polylineEntity;
			InvokeProp("Type");
			InvokeProp("Type_I");
			parent?.InvokeEntityReplace(action);
		}
	}

	public int Type_I
	{
		get
		{
			return (int)Type;
		}
		set
		{
			Type = (PolylineDataGridType)value;
		}
	}

	public string[] Types_S => _Types_S;

	public IList<IPolylineDataGridControl> Views => views;

	public DataGridRow Row
	{
		get
		{
			return row;
		}
		set
		{
			if (row != null)
			{
				row.Selected -= OnRowSelected;
				row.Unselected -= OnRowUnselected;
			}
			row = value;
			if (row == null)
			{
				return;
			}
			row.Selected += OnRowSelected;
			row.Unselected += OnRowUnselected;
			foreach (IPolylineDataGridControl view in views)
			{
				if (row.IsSelected)
				{
					view.Select();
				}
				else
				{
					view.Unselect();
				}
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public PolylineDataGridItem(PolylineDataGridWindow _parent)
	{
		parent = _parent;
		views = new List<IPolylineDataGridControl>();
	}

	public void Dispose()
	{
		Core = null;
		parent = null;
	}

	public void AddView(IPolylineDataGridControl _view)
	{
		if (row != null)
		{
			if (row.IsSelected)
			{
				_view.Select();
			}
			else
			{
				_view.Unselect();
			}
		}
		views.Add(_view);
	}

	public void RemoveView(IPolylineDataGridControl _view)
	{
		views.Remove(_view);
	}

	protected void InvokeProp(string propname)
	{
		if (propname.Equals("Center"))
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CenterX"));
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CenterY"));
		}
		else
		{
			this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
		}
	}

	protected void InvokeAll()
	{
		InvokeProp("Core");
	}

	private void OnCorePropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		InvokeProp(e.PropertyName);
	}

	private void OnRowSelected(object sender, RoutedEventArgs e)
	{
		foreach (IPolylineDataGridControl view in views)
		{
			view.Select();
		}
	}

	private void OnRowUnselected(object sender, RoutedEventArgs e)
	{
		foreach (IPolylineDataGridControl view in views)
		{
			view.Unselect();
		}
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SamSoarII.Polyline.Control.TreeView;

public class PolylineSelectTreeArrow : UserControl, IComponentConnector
{
	protected static readonly double Scale;

	protected static readonly Geometry Geometry_Collapse;

	protected static readonly Geometry Geometry_Expand;

	protected static readonly Brush Brush_Normal;

	protected static readonly Brush Brush_MouseOver;

	protected static readonly Brush Brush_Selected;

	protected static readonly DependencyProperty IsExpandProperty;

	protected static readonly DependencyProperty IsSelectedProperty;

	internal Path UI_Path;

	private bool _contentLoaded;

	public bool IsExpand
	{
		get
		{
			return (bool)GetValue(IsExpandProperty);
		}
		set
		{
			SetValue(IsExpandProperty, value);
		}
	}

	public bool IsSelected
	{
		get
		{
			return (bool)GetValue(IsSelectedProperty);
		}
		set
		{
			SetValue(IsSelectedProperty, value);
		}
	}

	static PolylineSelectTreeArrow()
	{
		Scale = 1.5;
		Brush_Normal = new SolidColorBrush(new Color
		{
			A = byte.MaxValue,
			R = 32,
			G = 32,
			B = 32
		});
		Brush_MouseOver = new SolidColorBrush(new Color
		{
			A = byte.MaxValue,
			R = 96,
			G = 96,
			B = 160
		});
		Brush_Selected = new SolidColorBrush(new Color
		{
			A = byte.MaxValue,
			R = 240,
			G = 240,
			B = 240
		});
		IsExpandProperty = DependencyProperty.Register("IsExpand", typeof(bool), typeof(PolylineSelectTreeArrow), new PropertyMetadata(false, OnPropertyChanged_IsExpand));
		IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(PolylineSelectTreeArrow), new PropertyMetadata(false, OnPropertyChanged_IsSelected));
		StreamGeometry streamGeometry = null;
		streamGeometry = new StreamGeometry();
		using (StreamGeometryContext streamGeometryContext = streamGeometry.Open())
		{
			streamGeometryContext.BeginFigure(new Point(5.0 * Scale, 0.0), isFilled: true, isClosed: true);
			streamGeometryContext.LineTo(new Point(5.0 * Scale, 5.0 * Scale), isStroked: true, isSmoothJoin: true);
			streamGeometryContext.LineTo(new Point(0.0, 5.0 * Scale), isStroked: true, isSmoothJoin: true);
			streamGeometryContext.LineTo(new Point(5.0 * Scale, 0.0), isStroked: true, isSmoothJoin: true);
		}
		Geometry_Expand = streamGeometry;
		streamGeometry = new StreamGeometry();
		using (StreamGeometryContext streamGeometryContext2 = streamGeometry.Open())
		{
			streamGeometryContext2.BeginFigure(new Point(0.0, 0.0), isFilled: true, isClosed: true);
			streamGeometryContext2.LineTo(new Point(3.536 * Scale, 3.536 * Scale), isStroked: true, isSmoothJoin: true);
			streamGeometryContext2.LineTo(new Point(0.0, 7.072 * Scale), isStroked: true, isSmoothJoin: true);
			streamGeometryContext2.LineTo(new Point(0.0, 0.0), isStroked: true, isSmoothJoin: true);
		}
		Geometry_Collapse = streamGeometry;
	}

	public PolylineSelectTreeArrow()
	{
		InitializeComponent();
		if (base.IsMouseOver)
		{
			UI_Path.Fill = (IsSelected ? Brush_Selected : Brushes.Transparent);
			UI_Path.Stroke = (IsSelected ? Brush_Selected : Brush_MouseOver);
		}
		else
		{
			UI_Path.Fill = Brushes.Transparent;
			UI_Path.Stroke = (IsSelected ? Brush_Selected : Brush_Normal);
		}
		if (IsExpand)
		{
			UI_Path.Data = Geometry_Expand;
		}
		else
		{
			UI_Path.Data = Geometry_Collapse;
		}
	}

	private static void OnPropertyChanged_IsExpand(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_IsSelected(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
	}

	protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		base.OnPropertyChanged(e);
		if (e.Property == UIElement.IsMouseOverProperty || e.Property == IsExpandProperty || e.Property == IsSelectedProperty)
		{
			if (base.IsMouseOver)
			{
				UI_Path.Fill = (IsSelected ? Brush_Selected : Brushes.Transparent);
				UI_Path.Stroke = (IsSelected ? Brush_Selected : Brush_MouseOver);
			}
			else
			{
				UI_Path.Fill = Brushes.Transparent;
				UI_Path.Stroke = (IsSelected ? Brush_Selected : Brush_Normal);
			}
			if (IsExpand)
			{
				UI_Path.Data = Geometry_Expand;
			}
			else
			{
				UI_Path.Data = Geometry_Collapse;
			}
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamSoarII.Polyline;component/control/treeview/polylineselecttreearrow.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		if (connectionId == 1)
		{
			UI_Path = (Path)target;
		}
		else
		{
			_contentLoaded = true;
		}
	}
}

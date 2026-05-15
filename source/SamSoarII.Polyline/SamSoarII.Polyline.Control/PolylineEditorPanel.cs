using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SamSoarII.Polyline.Entity;
using SamSoarII.Shell;

namespace SamSoarII.Polyline.Control;

public class PolylineEditorPanel : UserControl
{
	private static readonly Brush Brush_Real = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 128,
		G = byte.MaxValue,
		B = 128
	});

	private static readonly Brush Brush_NotReal = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 128,
		G = byte.MaxValue,
		B = 128
	});

	private static readonly Brush Brush_Auxility = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 128,
		G = 128,
		B = 128
	});

	private static readonly Brush Brush_MouseOver = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = byte.MaxValue,
		G = byte.MaxValue,
		B = byte.MaxValue
	});

	private static readonly Brush Brush_Bounding = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 64,
		G = 144,
		B = 216
	});

	private static readonly double Thickness_Real = 4.0;

	private static readonly double Thickness_NotReal = 2.0;

	private static readonly double Thickness_Auxility = 0.75;

	private static readonly double Thickness_MouseOver = 4.0;

	private static readonly DoubleCollection DashArray_Real = new DoubleCollection();

	private static readonly DoubleCollection DashArray_NotReal = new DoubleCollection { 2.0, 2.0 };

	private static readonly DoubleCollection DashArray_Auxility = new DoubleCollection { 1.0, 3.0 };

	private static readonly DoubleCollection DashArray_MouseOver = new DoubleCollection();

	private static readonly BlurEffect Effect_Real = new BlurEffect
	{
		Radius = 4.0,
		RenderingBias = RenderingBias.Performance
	};

	private static readonly BlurEffect Effect_MouseOver = new BlurEffect
	{
		Radius = 4.0,
		RenderingBias = RenderingBias.Performance
	};

	private static readonly BlurEffect Effect_Zone = new BlurEffect
	{
		Radius = 1.0,
		RenderingBias = RenderingBias.Performance
	};

	private static readonly PointCollection Points_ZoneStart = new PointCollection
	{
		new Point(12.0, 6.0),
		new Point(0.0, 0.0),
		new Point(0.0, 12.0)
	};

	private static readonly double Radius_ZoneEnd = 12.0;

	private static readonly ImageSource Image_ZoneTile = new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Polyline;component/Resources/Icon/ZoneTile.png"));

	private static readonly Brush Fill_ZoneTile = new ImageBrush
	{
		TileMode = TileMode.Tile,
		Stretch = Stretch.Uniform,
		ViewportUnits = BrushMappingMode.Absolute,
		AlignmentX = AlignmentX.Center,
		AlignmentY = AlignmentY.Center,
		Viewport = new Rect(0.0, 0.0, 32.0, 32.0),
		ImageSource = Image_ZoneTile,
		Opacity = 0.5
	};

	private static readonly double[] ShiftAngles = new double[17]
	{
		-180.0, -150.0, -135.0, -120.0, -90.0, -60.0, -45.0, -30.0, 0.0, 30.0,
		45.0, 60.0, 90.0, 120.0, 135.0, 150.0, 180.0
	};

	protected static readonly DependencyProperty NewEntityProperty = DependencyProperty.Register("NewEntity", typeof(IPolylineEntity), typeof(PolylineEditorPanel), new PropertyMetadata(null, OnPropertyChanged_NewEntity));

	protected static readonly DependencyProperty NexEntityProperty = DependencyProperty.Register("NexEntity", typeof(IPolylineEntity), typeof(PolylineEditorPanel), new PropertyMetadata(null, OnPropertyChanged_NexEntity));

	protected static readonly DependencyProperty MouEntityProperty = DependencyProperty.Register("MouEntity", typeof(IPolylineEntity), typeof(PolylineEditorPanel), new PropertyMetadata(null, OnPropertyChanged_MouEntity));

	protected static readonly DependencyProperty SelectionsProperty = DependencyProperty.Register("Selections", typeof(ObservableCollection<IPolylineEntity>), typeof(PolylineEditorPanel), new PropertyMetadata(null, OnPropertyChanged_Selections));

	protected static readonly DependencyProperty SelectionFreezedProperty = DependencyProperty.Register("SelectionFreezed", typeof(bool), typeof(PolylineEditorPanel), new PropertyMetadata(false, OnPropertyChanged_SelectionFreezed));

	private PolylineEditor editor;

	private Canvas canvas;

	private MouseStatus status;

	private MouseStatus oldstatus;

	private List<PolylineEditorPointView> pviews;

	private List<IPolylineEntity> pvitems;

	private int pvusedcount;

	private List<IPolylineEntity> selentities;

	private Path newpath;

	private Path nexpath;

	private Path moupath;

	private Path mulpath;

	private StreamGeometry newgeo;

	private StreamGeometry nexgeo;

	private StreamGeometry mougeo;

	private StreamGeometry mulgeo;

	private List<Path> auxpaths;

	private List<StreamGeometry> auxgeos;

	private Polygon zonestart;

	private Ellipse zoneend;

	private Rectangle zonebound;

	private PolylineEditorPointView movepoint;

	private PointPaintLabel uil_point;

	private RadiusPaintLabel uil_radius;

	private MirrorLabel uil_mirror;

	protected IPolylineEntity NewEntity
	{
		get
		{
			return (IPolylineEntity)GetValue(NewEntityProperty);
		}
		set
		{
			SetValue(NewEntityProperty, value);
		}
	}

	protected IPolylineEntity NexEntity
	{
		get
		{
			return (IPolylineEntity)GetValue(NexEntityProperty);
		}
		set
		{
			SetValue(NexEntityProperty, value);
		}
	}

	protected IPolylineEntity MouEntity
	{
		get
		{
			return (IPolylineEntity)GetValue(MouEntityProperty);
		}
		set
		{
			SetValue(MouEntityProperty, value);
		}
	}

	public ObservableCollection<IPolylineEntity> Selections
	{
		get
		{
			return (ObservableCollection<IPolylineEntity>)GetValue(SelectionsProperty);
		}
		protected set
		{
			SetValue(SelectionsProperty, value);
		}
	}

	public bool SelectionFreezed
	{
		get
		{
			return (bool)GetValue(SelectionFreezedProperty);
		}
		set
		{
			SetValue(SelectionFreezedProperty, value);
		}
	}

	public PolylineEditor Editor
	{
		get
		{
			if (editor != null)
			{
				return editor;
			}
			DependencyObject parent = base.Parent;
			while (parent is FrameworkElement)
			{
				FrameworkElement frameworkElement = (FrameworkElement)parent;
				if (frameworkElement is PolylineEditor)
				{
					editor = (PolylineEditor)frameworkElement;
					return editor;
				}
				parent = frameworkElement.Parent;
			}
			return null;
		}
	}

	public bool IsEditing => status != MouseStatus.None && status != MouseStatus.Selected;

	protected PolylineEditorPointView MovePoint
	{
		get
		{
			return movepoint;
		}
		set
		{
			_setMovePoint(value);
		}
	}

	public IPolylineImage SelectedImage => Editor?.SelectedImage;

	public GridPenning Penning => Editor?.UI_Penning;

	public IGridPenningSourceEX Source => SelectedImage;

	internal event MousePositionEventHandler MousePositionMove;

	internal event MouseActionEventHandler MouseActionStart;

	internal event MouseActionEventHandler MouseActionDone;

	internal event MouseActionEventHandler MouseActionEscape;

	public PolylineEditorPanel()
	{
		canvas = new Canvas();
		NewEntity = null;
		NexEntity = null;
		selentities = new List<IPolylineEntity>();
		newpath = new Path();
		nexpath = new Path();
		moupath = new Path();
		mulpath = new Path();
		newgeo = new StreamGeometry();
		nexgeo = new StreamGeometry();
		mougeo = new StreamGeometry();
		mulgeo = new StreamGeometry();
		auxpaths = new List<Path>();
		auxgeos = new List<StreamGeometry>();
		zonestart = new Polygon();
		zoneend = new Ellipse();
		zonebound = new Rectangle();
		status = MouseStatus.None;
		oldstatus = MouseStatus.None;
		pviews = new List<PolylineEditorPointView>();
		pvitems = new List<IPolylineEntity>();
		pvusedcount = 0;
		uil_point = new PointPaintLabel();
		uil_radius = new RadiusPaintLabel();
		uil_mirror = new MirrorLabel(this);
		newpath.Data = newgeo;
		nexpath.Data = nexgeo;
		moupath.Data = mougeo;
		mulpath.Data = mulgeo;
		newpath.Effect = Effect_Real;
		nexpath.Effect = Effect_Real;
		mulpath.Effect = Effect_Real;
		moupath.Effect = Effect_MouseOver;
		zonestart.Visibility = Visibility.Hidden;
		zonestart.Fill = Brush_Real;
		zonestart.Width = 12.0;
		zonestart.Height = 12.0;
		zonestart.Points = Points_ZoneStart;
		zonestart.Effect = Effect_Zone;
		zoneend.Visibility = Visibility.Hidden;
		zoneend.Fill = Brush_Real;
		zoneend.Width = Radius_ZoneEnd;
		zoneend.Height = Radius_ZoneEnd;
		zoneend.Effect = Effect_Zone;
		zonebound.Visibility = Visibility.Hidden;
		zonebound.Stroke = Brush_Bounding;
		zonebound.StrokeThickness = 0.75;
		zonebound.RadiusX = 4.0;
		zonebound.RadiusY = 4.0;
		zonebound.Fill = Fill_ZoneTile;
		uil_point.Submit += UIL_Point_Submit;
		uil_point.Escape += UIL_Point_Escape;
		uil_radius.Submit += UIL_Radius_Submit;
		uil_radius.Escape += UIL_Radius_Escape;
		for (int i = 0; i < 8; i++)
		{
			Path path = new Path();
			StreamGeometry item = (StreamGeometry)(path.Data = new StreamGeometry());
			path.Stroke = Brush_Auxility;
			path.StrokeDashArray = DashArray_Auxility;
			path.StrokeThickness = Thickness_Auxility;
			auxpaths.Add(path);
			auxgeos.Add(item);
			canvas.Children.Add(path);
		}
		canvas.Children.Add(newpath);
		canvas.Children.Add(nexpath);
		canvas.Children.Add(moupath);
		canvas.Children.Add(mulpath);
		canvas.Children.Add(zonestart);
		canvas.Children.Add(zoneend);
		canvas.Children.Add(zonebound);
		canvas.Children.Add(uil_point);
		canvas.Children.Add(uil_radius);
		canvas.Children.Add(uil_mirror);
		base.Content = canvas;
		base.Focusable = true;
		Selections = new ObservableCollection<IPolylineEntity>();
	}

	private static void OnPropertyChanged_NewEntity(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineEditorPanel)
		{
			((PolylineEditorPanel)d).OnNewEntityChanged(e);
		}
	}

	protected virtual void OnNewEntityChanged(DependencyPropertyChangedEventArgs e)
	{
		if (e.OldValue is IPolylineEntity)
		{
			IPolylineEntity polylineEntity = (IPolylineEntity)e.OldValue;
			polylineEntity.PropertyChanged -= OnNewEntityPropertyChanged;
		}
		if (e.NewValue is IPolylineEntity)
		{
			IPolylineEntity polylineEntity2 = (IPolylineEntity)e.NewValue;
			polylineEntity2.PropertyChanged += OnNewEntityPropertyChanged;
		}
	}

	private static void OnPropertyChanged_NexEntity(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineEditorPanel)
		{
			((PolylineEditorPanel)d).OnNexEntityChanged(e);
		}
	}

	protected virtual void OnNexEntityChanged(DependencyPropertyChangedEventArgs e)
	{
		if (e.OldValue is IPolylineEntity)
		{
			IPolylineEntity polylineEntity = (IPolylineEntity)e.OldValue;
			polylineEntity.PropertyChanged -= OnNexEntityPropertyChanged;
		}
		if (e.NewValue is IPolylineEntity)
		{
			IPolylineEntity polylineEntity2 = (IPolylineEntity)e.NewValue;
			polylineEntity2.PropertyChanged += OnNexEntityPropertyChanged;
		}
	}

	private static void OnPropertyChanged_MouEntity(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineEditorPanel)
		{
			((PolylineEditorPanel)d).OnMouEntityChanged(e);
		}
	}

	protected virtual void OnMouEntityChanged(DependencyPropertyChangedEventArgs e)
	{
		if (e.OldValue is IPolylineEntity)
		{
			IPolylineEntity polylineEntity = (IPolylineEntity)e.OldValue;
			polylineEntity.PropertyChanged -= OnMouEntityPropertyChanged;
		}
		if (e.NewValue is IPolylineEntity)
		{
			IPolylineEntity polylineEntity2 = (IPolylineEntity)e.NewValue;
			polylineEntity2.PropertyChanged += OnMouEntityPropertyChanged;
			moupath.Visibility = Visibility.Visible;
			DrawingPath(polylineEntity2, moupath, mougeo);
			CtrlUpdate();
		}
		else
		{
			moupath.Visibility = Visibility.Hidden;
			CtrlUpdate();
		}
	}

	private static void OnPropertyChanged_Selections(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineEditorPanel)
		{
			((PolylineEditorPanel)d).OnSelectionsChanged(e);
		}
	}

	protected virtual void OnSelectionsChanged(DependencyPropertyChangedEventArgs e)
	{
		if (e.OldValue is ObservableCollection<IPolylineEntity>)
		{
			ObservableCollection<IPolylineEntity> observableCollection = (ObservableCollection<IPolylineEntity>)e.OldValue;
			observableCollection.CollectionChanged -= OnSelectionsCollectionChanged;
		}
		if (e.NewValue is ObservableCollection<IPolylineEntity>)
		{
			ObservableCollection<IPolylineEntity> observableCollection2 = (ObservableCollection<IPolylineEntity>)e.NewValue;
			observableCollection2.CollectionChanged += OnSelectionsCollectionChanged;
		}
	}

	private static void OnPropertyChanged_SelectionFreezed(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineEditorPanel)
		{
			((PolylineEditorPanel)d).OnSelectionFreezedChanged(e);
		}
	}

	protected virtual void OnSelectionFreezedChanged(DependencyPropertyChangedEventArgs e)
	{
		if (!SelectionFreezed)
		{
			PathUpdate();
		}
	}

	protected void _setMovePoint(PolylineEditorPointView value)
	{
		if (movepoint != null)
		{
			movepoint.IsNearest = false;
		}
		movepoint = value;
		if (movepoint != null)
		{
			movepoint.IsNearest = true;
		}
	}

	public Point GetLogicalPosition(MouseEventArgs e)
	{
		if (Source == null)
		{
			return default(Point);
		}
		Point position = e.GetPosition(this);
		return new Point(SelectedImage.Left + Penning.HorizontalOffset + position.X / base.ActualWidth * Penning.ViewportWidth, SelectedImage.Top + Penning.VerticalOffset + Penning.ViewportHeight - position.Y / base.ActualHeight * Penning.ViewportHeight);
	}

	public Point GetVisualPosition(Point p)
	{
		if (Source == null)
		{
			return default(Point);
		}
		return new Point((p.X - (Penning.HorizontalOffset + SelectedImage.Left)) / Penning.ViewportWidth * base.ActualWidth, (Penning.VerticalOffset + SelectedImage.Top + Penning.ViewportHeight - p.Y) / Penning.ViewportHeight * base.ActualHeight);
	}

	protected void DrawingPath(IPolylineEntity entity, Path path, StreamGeometryContext ctx)
	{
		if (path == moupath && status == MouseStatus.SetRoundPoint)
		{
			entity = SelectedImage?.EntityRoundDemo(MouEntity, uil_radius.Radius);
		}
		if (path == moupath && status == MouseStatus.SetBevelPoint)
		{
			entity = SelectedImage?.EntityBevelDemo(MouEntity, uil_radius.Radius);
		}
		if (path == moupath && status == MouseStatus.SetSharpPoint)
		{
			entity = SelectedImage?.EntitySharpDemo(MouEntity);
		}
		if (entity == null)
		{
			return;
		}
		Point visualPosition = GetVisualPosition(entity.From);
		Point visualPosition2 = GetVisualPosition(entity.To);
		if (path == moupath && status == MouseStatus.SetBreakPoint)
		{
			visualPosition = GetVisualPosition(uil_point.Point);
		}
		if (entity is IPolylineLine)
		{
			ctx.BeginFigure(visualPosition, isFilled: false, isClosed: false);
			ctx.LineTo(visualPosition2, isStroked: true, isSmoothJoin: false);
			return;
		}
		if (entity is IPolylineArch && entity is IGridPenningArch)
		{
			IPolylineArch polylineArch = (IPolylineArch)entity;
			IGridPenningArch gridPenningArch = (IGridPenningArch)entity;
			Point visualPosition3 = GetVisualPosition(polylineArch.Center);
			double width = gridPenningArch.Radius * base.ActualWidth / Penning.ViewportWidth;
			double height = gridPenningArch.Radius * base.ActualHeight / Penning.ViewportHeight;
			ctx.BeginFigure(visualPosition2, isFilled: false, isClosed: false);
			ctx.ArcTo(visualPosition, new Size(width, height), 0.0, polylineArch.IsLarge, (!polylineArch.IsClockwise) ? SweepDirection.Clockwise : SweepDirection.Counterclockwise, isStroked: true, isSmoothJoin: false);
			return;
		}
		if (entity is IPolylineCircle && entity is IGridPenningCircle)
		{
			IPolylineCircle polylineCircle = (IPolylineCircle)entity;
			IGridPenningCircle gridPenningCircle = (IGridPenningCircle)entity;
			Point visualPosition4 = GetVisualPosition(polylineCircle.Center);
			double width2 = gridPenningCircle.Radius * base.ActualWidth / Penning.ViewportWidth;
			double height2 = gridPenningCircle.Radius * base.ActualHeight / Penning.ViewportHeight;
			ctx.BeginFigure(visualPosition, isFilled: false, isClosed: false);
			ctx.ArcTo(visualPosition4 - (visualPosition - visualPosition4), new Size(width2, height2), 0.0, isLargeArc: true, SweepDirection.Clockwise, isStroked: true, isSmoothJoin: false);
			ctx.ArcTo(visualPosition, new Size(width2, height2), 0.0, isLargeArc: true, SweepDirection.Clockwise, isStroked: true, isSmoothJoin: false);
			return;
		}
		if (entity is IPolylineEllipse && entity is IGridPenningEllipse)
		{
			IPolylineEllipse polylineEllipse = (IPolylineEllipse)entity;
			Point visualPosition5 = GetVisualPosition(polylineEllipse.Center);
			double num = Vector.AngleBetween(new Vector(1.0, 0.0), polylineEllipse.Direction);
			num *= Math.PI / 180.0;
			double longRadius = polylineEllipse.LongRadius;
			double shortRadius = polylineEllipse.ShortRadius;
			longRadius = Math.Pow(longRadius * Math.Cos(num) * base.ActualWidth / Penning.ViewportWidth, 2.0) + Math.Pow(longRadius * Math.Sin(num) * base.ActualHeight / Penning.ViewportHeight, 2.0);
			longRadius = Math.Sqrt(longRadius);
			shortRadius = Math.Pow(shortRadius * Math.Sin(num) * base.ActualWidth / Penning.ViewportWidth, 2.0) + Math.Pow(shortRadius * Math.Cos(num) * base.ActualHeight / Penning.ViewportHeight, 2.0);
			shortRadius = Math.Sqrt(shortRadius);
			num *= 180.0 / Math.PI;
			if (entity is IPolylineEllipseArch)
			{
				IPolylineEllipseArch polylineEllipseArch = (IPolylineEllipseArch)entity;
				visualPosition = GetVisualPosition(polylineEllipseArch.From);
				visualPosition2 = GetVisualPosition(polylineEllipseArch.To);
				ctx.BeginFigure(visualPosition, isFilled: false, isClosed: false);
				ctx.ArcTo(visualPosition2, new Size(longRadius, shortRadius), 0.0 - num, polylineEllipseArch.IsLarge, polylineEllipseArch.IsClockwise ? SweepDirection.Clockwise : SweepDirection.Counterclockwise, isStroked: true, isSmoothJoin: false);
			}
			else
			{
				visualPosition = visualPosition5 - polylineEllipse.Direction / polylineEllipse.Direction.Length * longRadius;
				visualPosition2 = visualPosition5 - (visualPosition - visualPosition5);
				visualPosition.X = visualPosition5.X - (visualPosition.X - visualPosition5.X);
				visualPosition2.X = visualPosition5.X - (visualPosition2.X - visualPosition5.X);
				ctx.BeginFigure(visualPosition, isFilled: false, isClosed: false);
				ctx.ArcTo(visualPosition2, new Size(longRadius, shortRadius), 0.0 - num, isLargeArc: true, SweepDirection.Clockwise, isStroked: true, isSmoothJoin: false);
				ctx.ArcTo(visualPosition, new Size(longRadius, shortRadius), 0.0 - num, isLargeArc: true, SweepDirection.Clockwise, isStroked: true, isSmoothJoin: false);
			}
			return;
		}
		if (entity is IPolylineBSpline)
		{
			IPolylineBSpline polylineBSpline = (IPolylineBSpline)entity;
			ctx.BeginFigure(GetVisualPosition(polylineBSpline.From), isFilled: false, isClosed: false);
			{
				foreach (Point node in polylineBSpline.Nodes)
				{
					ctx.LineTo(GetVisualPosition(node), isStroked: true, isSmoothJoin: false);
				}
				return;
			}
		}
		if (entity is IPolylineB2Spline)
		{
			IPolylineB2Spline polylineB2Spline = (IPolylineB2Spline)entity;
			ctx.BeginFigure(GetVisualPosition(polylineB2Spline.From), isFilled: false, isClosed: false);
			for (int i = 1; i < polylineB2Spline.Points.Count; i += 2)
			{
				Point visualPosition6 = GetVisualPosition((i == 1) ? polylineB2Spline.From : polylineB2Spline.Points[i - 2]);
				Point visualPosition7 = GetVisualPosition(polylineB2Spline.Points[i - 1]);
				Point visualPosition8 = GetVisualPosition(polylineB2Spline.Points[i]);
				ctx.BezierTo(visualPosition6, visualPosition7, visualPosition8, isStroked: true, isSmoothJoin: false);
			}
		}
		else if (entity is IPolylineRect)
		{
			IPolylineRect polylineRect = (IPolylineRect)entity;
			ctx.BeginFigure(GetVisualPosition(polylineRect.Rect.TopLeft), isFilled: false, isClosed: true);
			ctx.LineTo(GetVisualPosition(polylineRect.Rect.TopRight), isStroked: true, isSmoothJoin: false);
			ctx.LineTo(GetVisualPosition(polylineRect.Rect.BottomRight), isStroked: true, isSmoothJoin: false);
			ctx.LineTo(GetVisualPosition(polylineRect.Rect.BottomLeft), isStroked: true, isSmoothJoin: false);
		}
		else if (entity is IPolylineFreeRect)
		{
			IPolylineFreeRect polylineFreeRect = (IPolylineFreeRect)entity;
			switch (status)
			{
			case MouseStatus.SetFreeRectStart:
				break;
			case MouseStatus.SetFreeRectHeight:
				ctx.BeginFigure(GetVisualPosition(polylineFreeRect.From), isFilled: false, isClosed: false);
				ctx.LineTo(GetVisualPosition(polylineFreeRect.P1), isStroked: true, isSmoothJoin: false);
				break;
			case MouseStatus.SetFreeRectWidth:
			{
				Point p = polylineFreeRect.From + (polylineFreeRect.P1 - polylineFreeRect.From) + (polylineFreeRect.P2 - polylineFreeRect.From);
				p = GetVisualPosition(p);
				ctx.BeginFigure(GetVisualPosition(polylineFreeRect.From), isFilled: false, isClosed: true);
				ctx.LineTo(GetVisualPosition(polylineFreeRect.P1), isStroked: true, isSmoothJoin: false);
				ctx.LineTo(p, isStroked: true, isSmoothJoin: false);
				ctx.LineTo(GetVisualPosition(polylineFreeRect.P2), isStroked: true, isSmoothJoin: false);
				break;
			}
			}
		}
		else if (entity is IPolylineCorner)
		{
			IPolylineCorner polylineCorner = (IPolylineCorner)entity;
			Point visualPosition9 = GetVisualPosition(polylineCorner.Corner);
			ctx.BeginFigure(visualPosition, isFilled: false, isClosed: false);
			ctx.LineTo(visualPosition9, isStroked: true, isSmoothJoin: false);
			ctx.LineTo(visualPosition2, isStroked: true, isSmoothJoin: false);
		}
	}

	protected void DrawingPath(IPolylineEntity entity, Path path, StreamGeometry geo)
	{
		if (path == newpath || path == nexpath || path == mulpath)
		{
			path.Stroke = (entity.IsReal ? Brush_Real : Brush_NotReal);
			path.StrokeThickness = (entity.IsReal ? Thickness_Real : Thickness_NotReal);
			path.StrokeDashArray = (entity.IsReal ? DashArray_Real : DashArray_NotReal);
		}
		else if (path == moupath)
		{
			path.Stroke = Brush_MouseOver;
			path.StrokeThickness = Thickness_MouseOver;
			path.StrokeDashArray = DashArray_MouseOver;
		}
		using StreamGeometryContext ctx = geo.Open();
		DrawingPath(entity, path, ctx);
	}

	protected void DrawingPaths(IEnumerable<IPolylineEntity> entities, Path path, StreamGeometry geo)
	{
		if (path == newpath || path == nexpath || path == mulpath)
		{
			path.Stroke = Brush_Real;
			path.StrokeThickness = Thickness_Real;
			path.StrokeDashArray = DashArray_Real;
		}
		else if (path == moupath)
		{
			path.Stroke = Brush_MouseOver;
			path.StrokeThickness = Thickness_MouseOver;
			path.StrokeDashArray = DashArray_MouseOver;
		}
		using StreamGeometryContext ctx = geo.Open();
		foreach (IPolylineEntity entity in entities)
		{
			DrawingPath(entity, path, ctx);
		}
	}

	protected int DrawingAux(IPolylineEntity entity, int start)
	{
		if (entity is IPolylineLine)
		{
			return 0;
		}
		if (entity is IPolylineArch)
		{
			IPolylineArch polylineArch = (IPolylineArch)entity;
			Point visualPosition = GetVisualPosition(polylineArch.From);
			Point visualPosition2 = GetVisualPosition(polylineArch.To);
			Point visualPosition3 = GetVisualPosition(polylineArch.Center);
			double length = (visualPosition3 - visualPosition).Length;
			using (StreamGeometryContext streamGeometryContext = auxgeos[start].Open())
			{
				streamGeometryContext.BeginFigure(visualPosition, isFilled: false, isClosed: false);
				streamGeometryContext.LineTo(visualPosition3, isStroked: true, isSmoothJoin: false);
			}
			using (StreamGeometryContext streamGeometryContext2 = auxgeos[start + 1].Open())
			{
				streamGeometryContext2.BeginFigure(visualPosition2, isFilled: false, isClosed: false);
				streamGeometryContext2.LineTo(visualPosition3, isStroked: true, isSmoothJoin: false);
			}
			using (StreamGeometryContext streamGeometryContext3 = auxgeos[start + 2].Open())
			{
				Point point = visualPosition + (visualPosition2 - visualPosition) / 2.0;
				Vector vector = point - visualPosition3;
				if (vector.Length > 1E-10)
				{
					vector /= vector.Length;
					vector *= 1000.0;
					streamGeometryContext3.BeginFigure(visualPosition3 + vector, isFilled: false, isClosed: false);
					streamGeometryContext3.LineTo(visualPosition3 - vector, isStroked: true, isSmoothJoin: false);
				}
			}
			return 3;
		}
		if (entity is IPolylineEllipse)
		{
			IPolylineEllipse polylineEllipse = (IPolylineEllipse)entity;
			Point visualPosition4 = GetVisualPosition(polylineEllipse.Center);
			using (StreamGeometryContext streamGeometryContext4 = auxgeos[start].Open())
			{
				Point p = polylineEllipse.Center + polylineEllipse.Direction * polylineEllipse.LongRadius / polylineEllipse.Direction.Length;
				p = GetVisualPosition(p);
				streamGeometryContext4.BeginFigure(visualPosition4 - (p - visualPosition4), isFilled: false, isClosed: false);
				streamGeometryContext4.LineTo(p, isStroked: true, isSmoothJoin: false);
			}
			using (StreamGeometryContext streamGeometryContext5 = auxgeos[start + 1].Open())
			{
				Vector vector2 = new Vector(0.0 - polylineEllipse.Direction.Y, polylineEllipse.Direction.X);
				Point p2 = polylineEllipse.Center + vector2 * polylineEllipse.ShortRadius / vector2.Length;
				p2 = GetVisualPosition(p2);
				streamGeometryContext5.BeginFigure(visualPosition4 - (p2 - visualPosition4), isFilled: false, isClosed: false);
				streamGeometryContext5.LineTo(p2, isStroked: true, isSmoothJoin: false);
			}
			if (entity is IPolylineEllipseArch)
			{
				IPolylineEllipseArch polylineEllipseArch = (IPolylineEllipseArch)entity;
				double num = Vector.AngleBetween(new Vector(1.0, 0.0), polylineEllipse.Direction);
				num *= Math.PI / 180.0;
				double longRadius = polylineEllipse.LongRadius;
				double shortRadius = polylineEllipse.ShortRadius;
				longRadius = Math.Pow(longRadius * Math.Cos(num) * base.ActualWidth / Penning.ViewportWidth, 2.0) + Math.Pow(longRadius * Math.Sin(num) * base.ActualHeight / Penning.ViewportHeight, 2.0);
				longRadius = Math.Sqrt(longRadius);
				shortRadius = Math.Pow(shortRadius * Math.Sin(num) * base.ActualWidth / Penning.ViewportWidth, 2.0) + Math.Pow(shortRadius * Math.Cos(num) * base.ActualHeight / Penning.ViewportHeight, 2.0);
				shortRadius = Math.Sqrt(shortRadius);
				num *= 180.0 / Math.PI;
				Point visualPosition5 = GetVisualPosition(polylineEllipseArch.From);
				Point point2 = visualPosition4 - (visualPosition5 - visualPosition4);
				using (StreamGeometryContext streamGeometryContext6 = auxgeos[start + 2].Open())
				{
					streamGeometryContext6.BeginFigure(visualPosition5, isFilled: false, isClosed: false);
					streamGeometryContext6.ArcTo(point2, new Size(longRadius, shortRadius), 0.0 - num, isLargeArc: true, polylineEllipseArch.IsClockwise ? SweepDirection.Clockwise : SweepDirection.Counterclockwise, isStroked: true, isSmoothJoin: false);
					streamGeometryContext6.ArcTo(visualPosition5, new Size(longRadius, shortRadius), 0.0 - num, isLargeArc: true, polylineEllipseArch.IsClockwise ? SweepDirection.Clockwise : SweepDirection.Counterclockwise, isStroked: true, isSmoothJoin: false);
				}
				return 3;
			}
			return 2;
		}
		if (entity is IPolylineB2Spline)
		{
			IPolylineB2Spline polylineB2Spline = (IPolylineB2Spline)entity;
			using (StreamGeometryContext streamGeometryContext7 = auxgeos[start].Open())
			{
				streamGeometryContext7.BeginFigure(GetVisualPosition(polylineB2Spline.From), isFilled: false, isClosed: false);
				for (int i = 0; i < polylineB2Spline.Points.Count; i++)
				{
					Point visualPosition6 = GetVisualPosition(polylineB2Spline.Points[i]);
					streamGeometryContext7.LineTo(visualPosition6, isStroked: true, isSmoothJoin: false);
				}
			}
			return 1;
		}
		return 0;
	}

	public void PathShow()
	{
		newpath.Visibility = ((NewEntity == null) ? Visibility.Hidden : Visibility.Visible);
		nexpath.Visibility = ((NexEntity == null) ? Visibility.Hidden : Visibility.Visible);
		mulpath.Visibility = ((Selections == null || Selections.Count <= 0) ? Visibility.Hidden : Visibility.Visible);
		if (NewEntity != null)
		{
			DrawingPath(NewEntity, newpath, newgeo);
		}
		if (NexEntity != null)
		{
			DrawingPath(NexEntity, nexpath, nexgeo);
		}
		if (Selections != null)
		{
			DrawingPaths(Selections, mulpath, mulgeo);
		}
	}

	public void PathHide()
	{
		newpath.Visibility = Visibility.Hidden;
		nexpath.Visibility = Visibility.Hidden;
		mulpath.Visibility = Visibility.Hidden;
		for (int i = 0; i < auxpaths.Count(); i++)
		{
			auxpaths[i].Visibility = Visibility.Hidden;
		}
	}

	public void PathUpdate()
	{
		int num = 0;
		if (NewEntity != null)
		{
			DrawingPath(NewEntity, newpath, newgeo);
			if (status != MouseStatus.None && status != MouseStatus.Selected)
			{
				num += DrawingAux(NewEntity, num);
			}
		}
		if (NexEntity != null)
		{
			DrawingPath(NexEntity, nexpath, nexgeo);
			if (status != MouseStatus.None && status != MouseStatus.Selected)
			{
				num += DrawingAux(NexEntity, num);
			}
		}
		if (Selections != null)
		{
			DrawingPaths(Selections, mulpath, mulgeo);
		}
		for (int i = 0; i < num; i++)
		{
			auxpaths[i].Visibility = Visibility.Visible;
		}
		for (int j = num; j < auxpaths.Count(); j++)
		{
			auxpaths[j].Visibility = Visibility.Hidden;
		}
	}

	public void CtrlShow()
	{
		for (int i = 0; i < pvusedcount; i++)
		{
			pviews[i].UpdatePosition();
			pviews[i].Visibility = Visibility.Visible;
		}
	}

	public void CtrlHide()
	{
		for (int i = 0; i < pvusedcount; i++)
		{
			pviews[i].Visibility = Visibility.Hidden;
		}
	}

	public void CtrlReset()
	{
		for (int i = 0; i < pvusedcount; i++)
		{
			pviews[i].Core = null;
		}
		pvitems.Clear();
		pvusedcount = 0;
	}

	public void CtrlUpdate()
	{
		CtrlReset();
		if (SelectedImage != null && SelectedImage.SelectedCount == 1)
		{
			int selectedStart = SelectedImage.SelectedStart;
			IPolylineEntity item = SelectedImage.Items[selectedStart];
			pvitems.Add(item);
		}
		if (MouEntity != null)
		{
			pvitems.Add(MouEntity);
		}
		foreach (IPolylineEntity pvitem in pvitems)
		{
			foreach (IPolylineControlPoint controlPoint in pvitem.ControlPoints)
			{
				if (pvusedcount >= pviews.Count())
				{
					PolylineEditorPointView polylineEditorPointView = new PolylineEditorPointView(this);
					canvas.Children.Add(polylineEditorPointView);
					pviews.Add(polylineEditorPointView);
				}
				PolylineEditorPointView polylineEditorPointView2 = pviews[pvusedcount++];
				polylineEditorPointView2.Core = controlPoint;
			}
		}
	}

	public void DrawingZoneStart()
	{
		if (SelectedImage == null || SelectedImage.SelectedCount < 2)
		{
			zonestart.Visibility = Visibility.Hidden;
			return;
		}
		IPolylineEntity polylineEntity = SelectedImage.Items[SelectedImage.SelectedStart];
		Point visualPosition = GetVisualPosition(polylineEntity.From);
		Vector vector = new Vector(1.0, 0.0);
		Vector vector2 = default(Vector);
		if (polylineEntity is IPolylineLine)
		{
			IPolylineLine polylineLine = (IPolylineLine)polylineEntity;
			vector2 = polylineLine.To - polylineLine.From;
		}
		else if (polylineEntity is IPolylineCircle)
		{
			IPolylineCircle polylineCircle = (IPolylineCircle)polylineEntity;
			vector2 = polylineCircle.Center - polylineCircle.From;
			vector2 = ((!polylineCircle.IsClockwise) ? new Vector(vector2.Y, 0.0 - vector2.X) : new Vector(0.0 - vector2.Y, vector2.X));
		}
		zonestart.Visibility = Visibility.Visible;
		Canvas.SetTop(zonestart, visualPosition.Y - zonestart.Height / 2.0);
		Canvas.SetLeft(zonestart, visualPosition.X - zonestart.Width / 2.0);
		double num = Vector.AngleBetween(vector, vector2);
		RotateTransform layoutTransform = new RotateTransform(0.0 - num, zonestart.ActualWidth / 2.0, zonestart.ActualHeight / 2.0);
		zonestart.LayoutTransform = layoutTransform;
	}

	public void DrawingZoneEnd()
	{
		if (SelectedImage == null || SelectedImage.SelectedCount < 2)
		{
			zoneend.Visibility = Visibility.Hidden;
			return;
		}
		IPolylineEntity polylineEntity = SelectedImage.Items[SelectedImage.SelectedStart + SelectedImage.SelectedCount - 1];
		Point visualPosition = GetVisualPosition(polylineEntity.To);
		zoneend.Visibility = Visibility.Visible;
		Canvas.SetTop(zoneend, visualPosition.Y - zoneend.ActualHeight / 2.0);
		Canvas.SetLeft(zoneend, visualPosition.X - zoneend.ActualWidth / 2.0);
	}

	public void DrawingZoneBounding()
	{
	}

	public void ZoneUpdate()
	{
		DrawingZoneStart();
		DrawingZoneEnd();
		DrawingZoneBounding();
	}

	public void ZoneHide()
	{
		zonestart.Visibility = Visibility.Hidden;
		zoneend.Visibility = Visibility.Hidden;
		zonebound.Visibility = Visibility.Hidden;
	}

	public void ZoneShow()
	{
		ZoneUpdate();
	}

	public void Select(IPolylineEntity _selentity)
	{
		if (_selentity != null)
		{
			NewEntity = _selentity;
			NexEntity = null;
			status = MouseStatus.Selected;
			selentities.Clear();
			selentities.Add(_selentity);
			PathShow();
		}
		else
		{
			NewEntity = null;
			NexEntity = null;
			status = MouseStatus.None;
			selentities.Clear();
			PathShow();
		}
	}

	public void Select(IEnumerable<IPolylineEntity> _selentities)
	{
		selentities.Clear();
		selentities.AddRange(_selentities);
		if (status == MouseStatus.Selected)
		{
			if (selentities.Count() == 1)
			{
				NewEntity = selentities.FirstOrDefault();
				PathShow();
			}
			else
			{
				NewEntity = null;
				PathHide();
			}
		}
		else
		{
			NewEntity = selentities.FirstOrDefault();
			NexEntity = null;
			status = MouseStatus.Selected;
			PathShow();
		}
	}

	public void StartMirror()
	{
		Escape();
		NewEntity = null;
		NexEntity = null;
		oldstatus = status;
		status = MouseStatus.SetMirrorFrom;
		uil_point.Begin("Starting point of the mirror axis");
		this.MouseActionStart?.Invoke(this, new MouseActionEventArgs(status));
	}

	public void StartBreak()
	{
		Escape();
		NewEntity = null;
		NexEntity = null;
		oldstatus = status;
		status = MouseStatus.SetBreakPoint;
		uil_point.Begin("Graphic element breaking point");
		this.MouseActionStart?.Invoke(this, new MouseActionEventArgs(status));
	}

	public void StartRound()
	{
		Escape();
		NewEntity = null;
		NexEntity = null;
		oldstatus = status;
		status = MouseStatus.SetRoundPoint;
		uil_radius.Begin("圆角Radius");
		this.MouseActionStart?.Invoke(this, new MouseActionEventArgs(status));
	}

	public void StartSharp()
	{
		Escape();
		NewEntity = null;
		NexEntity = null;
		oldstatus = status;
		status = MouseStatus.SetSharpPoint;
		this.MouseActionStart?.Invoke(this, new MouseActionEventArgs(status));
	}

	public void StartBevel()
	{
		Escape();
		NewEntity = null;
		NexEntity = null;
		oldstatus = status;
		status = MouseStatus.SetBevelPoint;
		uil_radius.Begin("斜角Radius");
		this.MouseActionStart?.Invoke(this, new MouseActionEventArgs(status));
	}

	public void Start(IPolylineEntity _newentity)
	{
		Escape();
		NewEntity = _newentity;
		NexEntity = NewEntity.Next;
		oldstatus = status;
		if (_newentity is IPolylineLine)
		{
			status = MouseStatus.SetTarget;
			uil_point.Begin("Draw a straight line");
		}
		else if (_newentity is IPolylineArch)
		{
			status = MouseStatus.SetTarget;
			uil_point.Begin("The end point of the arc");
		}
		else if (_newentity is IPolylineCircle)
		{
			status = MouseStatus.SetCenter;
			uil_radius.Begin("Set the center of the circle");
		}
		else if (_newentity is IPolylineRect)
		{
			status = MouseStatus.SetTarget;
			uil_point.Begin("Diagonals of a rectangle");
		}
		else if (_newentity is IPolylineFreeRect)
		{
			status = MouseStatus.SetFreeRectStart;
			uil_point.Begin("Free rectangle starting point");
		}
		else if (_newentity is IPolylineEllipseArch)
		{
			status = MouseStatus.SetTarget;
			uil_point.Begin("椭The end point of the arc");
		}
		else if (_newentity is IPolylineEllipse)
		{
			status = MouseStatus.SetEllipseCenter;
			uil_point.Begin("Set the center of the circle");
		}
		else if (_newentity is IPolylineB2Spline)
		{
			status = MouseStatus.SetB2SplinePoint;
			uil_point.Begin("Set passing points");
		}
		SelectedImage?.ActionBegin(NewEntity, NexEntity);
		PathShow();
		this.MouseActionStart?.Invoke(this, new MouseActionEventArgs(MouseStatus.SetTarget));
	}

	internal void Start(IPolylineEntity _newentity, MouseStatus _status)
	{
		Escape();
		NewEntity = _newentity;
		NexEntity = NewEntity.Next;
		oldstatus = status;
		status = _status;
		PathShow();
		if (_status == MouseStatus.MovePoint)
		{
			bool flag = false;
			flag |= NewEntity is IPolylineCircle && MovePoint.Core.ID == 2;
			SelectedImage?.ActionBegin(NewEntity, NexEntity);
			if (flag)
			{
				uil_radius.Begin("调Complete circle心");
			}
			else
			{
				uil_point.Begin("Control point");
			}
		}
		this.MouseActionStart?.Invoke(this, new MouseActionEventArgs(_status));
	}

	internal void Next(MouseStatus _status)
	{
		status = _status;
		switch (status)
		{
		case MouseStatus.SetMirrorTo:
			uil_mirror.Begin();
			uil_point.Begin("End point of the mirror axis");
			break;
		case MouseStatus.SetFreeRectHeight:
			uil_point.End();
			uil_radius.Begin("The main edge of a free rectangle");
			break;
		case MouseStatus.SetFreeRectWidth:
			uil_radius.Begin("Free rectangle side");
			break;
		case MouseStatus.SetEllipseLong:
			uil_point.End();
			uil_radius.Begin("椭圆主Radius");
			break;
		case MouseStatus.SetEllipseShort:
			uil_radius.Begin("椭圆副Radius");
			break;
		case MouseStatus.SetB2SplineControl:
			uil_point.Begin("设置Control point");
			break;
		case MouseStatus.SetB2SplinePoint:
			uil_point.Begin("Set passing points");
			break;
		}
		this.MouseActionStart?.Invoke(this, new MouseActionEventArgs(_status));
	}

	internal void Next(IPolylineEntity _newentity, MouseStatus _status)
	{
		if (uil_point.IsVisible)
		{
			Point p = uil_point.Point;
			ApplyPoint(ref p);
		}
		NewEntity = _newentity;
		NexEntity = NewEntity.Next;
		status = _status;
		PathShow();
		switch (status)
		{
		case MouseStatus.SetCenter:
			if (_newentity is IPolylineEllipseArch)
			{
				uil_point.Begin("Set the center of the circle");
				break;
			}
			uil_point.End();
			uil_radius.Begin("Set the center of the circle");
			break;
		case MouseStatus.SetEllipseAngle:
			uil_point.End();
			uil_radius.Begin("Spindle direction");
			break;
		}
		this.MouseActionStart?.Invoke(this, new MouseActionEventArgs(_status));
	}

	public void End()
	{
		if (uil_point.IsVisible)
		{
			Point p = uil_point.Point;
			ApplyPoint(ref p);
		}
		MouseStatus mouseStatus = status;
		MouseStatus mouseStatus2 = status;
		MouseStatus mouseStatus3 = mouseStatus2;
		if ((uint)(mouseStatus3 - 2) <= 1u || mouseStatus3 == MouseStatus.MovePoint)
		{
			SelectedImage?.ActionEnd(NewEntity, NexEntity);
		}
		IPolylineEntity newEntity = NewEntity;
		if (newEntity != null && newEntity.IsSpecial)
		{
			editor?.InvokeEntitySetting(NewEntity);
		}
		status = oldstatus;
		NewEntity = ((status == MouseStatus.Selected && selentities.Count() == 1) ? selentities[0] : null);
		NexEntity = null;
		uil_point.End();
		uil_radius.End();
		uil_mirror.End();
		PathShow();
		this.MouseActionDone?.Invoke(this, new MouseActionEventArgs(mouseStatus));
	}

	public void Escape()
	{
		if (status == MouseStatus.None || status == MouseStatus.Selected)
		{
			return;
		}
		MouseStatus mouseStatus = status;
		SelectedImage?.ActionEscape(NewEntity, NexEntity);
		status = oldstatus;
		NewEntity = ((status == MouseStatus.Selected && selentities.Count() == 1) ? selentities[0] : null);
		NexEntity = null;
		uil_point.End();
		uil_radius.End();
		uil_mirror.End();
		PathShow();
		this.MouseActionEscape?.Invoke(this, new MouseActionEventArgs(mouseStatus));
		MouseStatus mouseStatus2 = mouseStatus;
		MouseStatus mouseStatus3 = mouseStatus2;
		if ((uint)(mouseStatus3 - 2) <= 8u)
		{
			IPolylineAction polylineAction = SelectedImage?.Undo();
			if (polylineAction != null)
			{
				editor?.InvokeEntityUndo(polylineAction);
			}
		}
	}

	protected void ApplyPoint(ref Point p)
	{
		switch (status)
		{
		case MouseStatus.SetTarget:
			if (NewEntity is IPolylineRect)
			{
				IPolylineRect polylineRect = (IPolylineRect)NewEntity;
				polylineRect.Rect = new Rect(polylineRect.From, p);
			}
			NewEntity.To = p;
			break;
		case MouseStatus.SetCenter:
			if (NewEntity is IPolylineCircle)
			{
				IPolylineCircle polylineCircle = (IPolylineCircle)NewEntity;
				polylineCircle.Center = p;
			}
			else if (NewEntity is IPolylineEllipse)
			{
				IPolylineEllipse polylineEllipse3 = (IPolylineEllipse)NewEntity;
				polylineEllipse3.Center = p;
			}
			break;
		case MouseStatus.SetFreeRectStart:
			if (NewEntity is IPolylineFreeRect)
			{
				IPolylineFreeRect polylineFreeRect3 = (IPolylineFreeRect)NewEntity;
				polylineFreeRect3.From = p;
			}
			break;
		case MouseStatus.SetFreeRectHeight:
			if (NewEntity is IPolylineFreeRect)
			{
				IPolylineFreeRect polylineFreeRect2 = (IPolylineFreeRect)NewEntity;
				polylineFreeRect2.P1 = p;
			}
			break;
		case MouseStatus.SetFreeRectWidth:
			if (NewEntity is IPolylineFreeRect)
			{
				IPolylineFreeRect polylineFreeRect = (IPolylineFreeRect)NewEntity;
				polylineFreeRect.P2 = p;
			}
			break;
		case MouseStatus.SetEllipseCenter:
			if (NewEntity is IPolylineEllipse)
			{
				IPolylineEllipse polylineEllipse4 = (IPolylineEllipse)NewEntity;
				polylineEllipse4.Center = p;
			}
			break;
		case MouseStatus.SetEllipseLong:
			if (NewEntity is IPolylineEllipse)
			{
				IPolylineEllipse polylineEllipse2 = (IPolylineEllipse)NewEntity;
				polylineEllipse2.Direction = p - polylineEllipse2.Center;
				polylineEllipse2.LongRadius = uil_radius.Radius;
				polylineEllipse2.ShortRadius = polylineEllipse2.LongRadius;
			}
			break;
		case MouseStatus.SetEllipseShort:
			if (NewEntity is IPolylineEllipse)
			{
				IPolylineEllipse polylineEllipse = (IPolylineEllipse)NewEntity;
				polylineEllipse.ShortRadius = uil_radius.Radius;
			}
			break;
		case MouseStatus.SetEllipseAngle:
			if (NewEntity is IPolylineEllipseArch)
			{
				IPolylineEllipseArch polylineEllipseArch = (IPolylineEllipseArch)NewEntity;
				if (!polylineEllipseArch.RadiusAngle(uil_radius.Angle) && !polylineEllipseArch.RadiusFrom() && !polylineEllipseArch.RadiusTo())
				{
					polylineEllipseArch.CenterFrom();
				}
			}
			break;
		case MouseStatus.SetB2SplinePoint:
			if (NewEntity is IPolylineB2Spline)
			{
				IPolylineB2Spline polylineB2Spline2 = (IPolylineB2Spline)NewEntity;
				polylineB2Spline2.To = p;
				p = polylineB2Spline2.To;
			}
			break;
		case MouseStatus.SetB2SplineControl:
			if (NewEntity is IPolylineB2Spline)
			{
				IPolylineB2Spline polylineB2Spline = (IPolylineB2Spline)NewEntity;
				polylineB2Spline.TryMove(polylineB2Spline.Points.Count - 2, ref p);
			}
			break;
		case MouseStatus.MovePoint:
			if (MovePoint != null)
			{
				IPolylineControlPoint core = MovePoint.Core;
				IPolylineEntity polylineEntity = core?.Parent;
				core.Point = p;
			}
			break;
		}
		AdjustCenter(ref p);
	}

	protected void AdjustCenter(ref Point p)
	{
		MouseStatus mouseStatus = status;
		MouseStatus mouseStatus2 = mouseStatus;
		if ((uint)(mouseStatus2 - 2) <= 1u || mouseStatus2 == MouseStatus.MovePoint)
		{
			if (NewEntity is IPolylineCircle)
			{
				IPolylineCircle polylineCircle = (IPolylineCircle)NewEntity;
				if (uil_radius.RLock && uil_radius.ALock)
				{
					polylineCircle.CenterRadiusAngle(uil_radius.Radius, uil_radius.Angle);
				}
				else if (uil_radius.RLock)
				{
					polylineCircle.CenterRadius(uil_radius.Radius);
				}
				else if (uil_radius.ALock)
				{
					polylineCircle.CenterAngle(uil_radius.Angle);
				}
				else if (polylineCircle is IPolylineArch)
				{
					((IPolylineArch)polylineCircle).CenterFrom();
				}
				if (MovePoint != null && MovePoint.Core.Parent == polylineCircle && MovePoint.Core.ID == 2)
				{
					p = polylineCircle.Center;
				}
			}
			else if (NewEntity is IPolylineEllipseArch)
			{
				IPolylineEllipseArch polylineEllipseArch = (IPolylineEllipseArch)NewEntity;
				if (status == MouseStatus.SetTarget)
				{
					polylineEllipseArch.CenterFrom();
				}
				if (status == MouseStatus.SetCenter && !polylineEllipseArch.RadiusFrom())
				{
					polylineEllipseArch.RadiusTo();
				}
				if (MovePoint != null && MovePoint.Core.Parent == polylineEllipseArch && MovePoint.Core.ID == 2)
				{
					p = polylineEllipseArch.Center;
				}
			}
			if (NexEntity is IPolylineArch)
			{
				IPolylineArch polylineArch = (IPolylineArch)NexEntity;
				polylineArch.CenterTo();
				if (MovePoint != null && MovePoint.Core.Parent == polylineArch && MovePoint.Core.ID == 2)
				{
					p = polylineArch.Center;
				}
			}
			else if (NexEntity is IPolylineCircle)
			{
				IPolylineCircle polylineCircle2 = (IPolylineCircle)NexEntity;
				polylineCircle2.Center += polylineCircle2.From - polylineCircle2.To;
				polylineCircle2.To = polylineCircle2.From;
			}
			else if (NexEntity is IPolylineEllipse)
			{
				IPolylineEllipse polylineEllipse = (IPolylineEllipse)NexEntity;
				polylineEllipse.Center += polylineEllipse.From - polylineEllipse.To;
				polylineEllipse.To = polylineEllipse.From;
			}
		}
		if (NewEntity is IPolylineCircle && uil_radius.IsVisible)
		{
			IPolylineCircle polylineCircle3 = (IPolylineCircle)NewEntity;
			if (!uil_radius.RLock)
			{
				uil_radius.Radius = polylineCircle3.Radius;
			}
			if (!uil_radius.ALock)
			{
				uil_radius.Angle = Vector.AngleBetween(new Vector(1.0, 0.0), polylineCircle3.Center - polylineCircle3.From);
			}
		}
	}

	protected void AdjustPreview(ref Point p, MouseEventArgs e)
	{
		if (Keyboard.PrimaryDevice.Modifiers == ModifierKeys.Shift)
		{
			bool flag = false;
			Point point = default(Point);
			switch (status)
			{
			case MouseStatus.SetTarget:
				if (NewEntity is IPolylineLine)
				{
					flag = true;
					point = NewEntity.From;
				}
				break;
			case MouseStatus.MovePoint:
				if (NewEntity is IPolylineLine)
				{
					PolylineEditorPointView movePoint = MovePoint;
					if (movePoint != null && movePoint.Core?.ID == 1)
					{
						flag = true;
						point = NewEntity.From;
					}
				}
				break;
			case MouseStatus.SetMirrorTo:
				flag = true;
				point = uil_mirror.From;
				break;
			case MouseStatus.SetFreeRectHeight:
				if (NewEntity is IPolylineFreeRect)
				{
					IPolylineFreeRect polylineFreeRect = (IPolylineFreeRect)NewEntity;
					flag = true;
					point = polylineFreeRect.From;
				}
				break;
			}
			if (flag)
			{
				Vector vector = p - point;
				double num = Vector.AngleBetween(new Vector(1.0, 0.0), vector);
				double num2 = double.NaN;
				double num3 = double.MaxValue;
				double[] shiftAngles = ShiftAngles;
				foreach (double num4 in shiftAngles)
				{
					double num5 = Math.Abs(num4 - num);
					if (num5 < num3)
					{
						num3 = num5;
						num2 = num4;
					}
				}
				double num6 = Math.Sin(num2 * Math.PI / 180.0);
				double num7 = Math.Cos(num2 * Math.PI / 180.0);
				double length = vector.Length;
				p.X = point.X + length * num7;
				p.Y = point.Y + length * num6;
			}
		}
		if (uil_point.IsVisible)
		{
			p.X = (uil_point.XLock ? uil_point.Point.X : p.X);
			p.Y = (uil_point.YLock ? uil_point.Point.Y : p.Y);
			if (status == MouseStatus.SetBreakPoint && MouEntity != null)
			{
				p = MouEntity.ReflectInline(p, uil_point.XLock, uil_point.YLock);
			}
			uil_point.Point = p;
			uil_point.Follow(e, new Size(base.ActualWidth, base.ActualHeight));
			if (status == MouseStatus.SetBreakPoint && MouEntity != null)
			{
				DrawingPath(MouEntity, moupath, mougeo);
			}
		}
		if (!uil_radius.IsVisible)
		{
			return;
		}
		if (status == MouseStatus.SetRoundPoint && MouEntity != null)
		{
			double num8 = (MouEntity.To - p).Length;
			if (uil_radius.RLock)
			{
				num8 = uil_radius.Radius;
			}
			IPolylineArch polylineArch = SelectedImage.EntityRoundDemo(MouEntity, num8);
			if (polylineArch == null)
			{
				num8 = double.NaN;
			}
			if (!uil_radius.RLock)
			{
				uil_radius.Radius = num8;
			}
			DrawingPath(MouEntity, moupath, mougeo);
		}
		if (status == MouseStatus.SetBevelPoint && MouEntity != null)
		{
			double num9 = (MouEntity.To - p).Length;
			if (uil_radius.RLock)
			{
				num9 = uil_radius.Radius;
			}
			IPolylineLine polylineLine = SelectedImage.EntityBevelDemo(MouEntity, num9);
			if (polylineLine == null)
			{
				num9 = double.NaN;
			}
			if (!uil_radius.RLock)
			{
				uil_radius.Radius = num9;
			}
			DrawingPath(MouEntity, moupath, mougeo);
		}
		if (status == MouseStatus.SetFreeRectHeight && NewEntity is IPolylineFreeRect)
		{
			IPolylineFreeRect polylineFreeRect2 = (IPolylineFreeRect)NewEntity;
			Vector vector2 = p - polylineFreeRect2.From;
			double num10 = vector2.Length;
			double num11 = Vector.AngleBetween(new Vector(1.0, 0.0), vector2);
			if (uil_radius.RLock)
			{
				num10 = uil_radius.Radius;
			}
			else
			{
				uil_radius.Radius = num10;
			}
			if (uil_radius.ALock)
			{
				num11 = uil_radius.Angle;
			}
			else
			{
				uil_radius.Angle = num11;
			}
			if (uil_radius.RLock || uil_radius.ALock)
			{
				num11 *= Math.PI / 180.0;
				double num12 = Math.Sin(num11);
				double num13 = Math.Cos(num11);
				p = new Point(polylineFreeRect2.From.X + num13 * num10, polylineFreeRect2.From.Y + num12 * num10);
			}
		}
		if (status == MouseStatus.SetFreeRectWidth && NewEntity is IPolylineFreeRect)
		{
			IPolylineFreeRect polylineFreeRect3 = (IPolylineFreeRect)NewEntity;
			Vector vector3 = polylineFreeRect3.P1 - polylineFreeRect3.From;
			Vector vector4 = p - polylineFreeRect3.From;
			double num14 = Vector.CrossProduct(vector3, vector4);
			double num15 = Math.Abs(num14) / vector3.Length;
			if (uil_radius.RLock)
			{
				num15 = uil_radius.Radius;
			}
			else
			{
				uil_radius.Radius = num15;
			}
			vector4 = new Vector(0.0 - vector3.Y, vector3.X);
			vector4 *= (double)((num14 >= 0.0) ? 1 : (-1));
			vector4 *= num15 / vector4.Length;
			double angle = Vector.AngleBetween(new Vector(1.0, 0.0), vector4);
			uil_radius.Angle = angle;
			p = polylineFreeRect3.From + vector4;
		}
		if (status == MouseStatus.SetEllipseLong && NewEntity is IPolylineEllipse)
		{
			IPolylineEllipse polylineEllipse = (IPolylineEllipse)NewEntity;
			Vector vector5 = p - polylineEllipse.Center;
			double angle2 = Vector.AngleBetween(new Vector(1.0, 0.0), vector5);
			if (uil_radius.RLock && uil_radius.ALock)
			{
				angle2 = uil_radius.Angle * Math.PI / 180.0;
				vector5 = new Vector(Math.Cos(angle2), Math.Sin(angle2)) * uil_radius.Radius;
				p = polylineEllipse.Center + vector5;
			}
			else if (uil_radius.RLock)
			{
				vector5 *= uil_radius.Radius / vector5.Length;
				p = polylineEllipse.Center + vector5;
				uil_radius.Angle = angle2;
			}
			else if (uil_radius.ALock)
			{
				angle2 = uil_radius.Angle * Math.PI / 180.0;
				vector5 = new Vector(Math.Cos(angle2), Math.Sin(angle2)) * vector5.Length;
				p = polylineEllipse.Center + vector5;
				uil_radius.Radius = vector5.Length;
			}
			else
			{
				uil_radius.Radius = vector5.Length;
				uil_radius.Angle = angle2;
			}
		}
		if (status == MouseStatus.SetEllipseShort && NewEntity is IPolylineEllipse)
		{
			IPolylineEllipse polylineEllipse2 = (IPolylineEllipse)NewEntity;
			Vector vector6 = p - polylineEllipse2.Center;
			if (uil_radius.RLock)
			{
				vector6 *= uil_radius.Radius / vector6.Length;
				p = polylineEllipse2.Center + vector6;
			}
			else
			{
				uil_radius.Radius = vector6.Length;
			}
		}
		if (status == MouseStatus.SetEllipseAngle && NewEntity is IPolylineEllipseArch)
		{
			IPolylineEllipseArch polylineEllipseArch = (IPolylineEllipseArch)NewEntity;
			Vector vector7 = p - polylineEllipseArch.Center;
			if (!uil_radius.RLock)
			{
				uil_radius.Radius = vector7.Length;
			}
			if (!uil_radius.ALock)
			{
				uil_radius.Angle = Vector.AngleBetween(new Vector(1.0, 0.0), vector7);
			}
		}
		uil_radius.Follow(e, new Size(base.ActualWidth, base.ActualHeight));
	}

	protected void AdjustSubmit(ref Point p)
	{
		if (uil_point.IsVisible)
		{
			p = uil_point.Point;
			if (status == MouseStatus.SetBreakPoint && MouEntity != null)
			{
				p = MouEntity.ReflectInline(p, uil_point.XLock, uil_point.YLock);
			}
		}
		if (uil_radius.IsVisible)
		{
			double radius = uil_radius.Radius;
			double angle = uil_radius.Angle;
			angle *= Math.PI / 180.0;
			double num = Math.Sin(angle);
			double num2 = Math.Cos(angle);
			Point point = default(Point);
			if (NewEntity != null)
			{
				point = NewEntity.From;
			}
			p = new Point(point.X + radius * num, point.Y + radius * num2);
		}
	}

	protected void TrySubmit(Point p)
	{
		switch (status)
		{
		case MouseStatus.SetTarget:
			if (NewEntity is IPolylineArch)
			{
				Next(NewEntity, MouseStatus.SetCenter);
			}
			else if (NewEntity is IPolylineEllipseArch)
			{
				Next(NewEntity, MouseStatus.SetCenter);
			}
			else
			{
				End();
			}
			break;
		case MouseStatus.SetCenter:
			if (NewEntity is IPolylineEllipseArch)
			{
				Next(NewEntity, MouseStatus.SetEllipseAngle);
			}
			else
			{
				End();
			}
			break;
		case MouseStatus.SetFreeRectStart:
			Next(MouseStatus.SetFreeRectHeight);
			break;
		case MouseStatus.SetFreeRectHeight:
			Next(MouseStatus.SetFreeRectWidth);
			break;
		case MouseStatus.SetFreeRectWidth:
			End();
			break;
		case MouseStatus.SetEllipseCenter:
			Next(MouseStatus.SetEllipseLong);
			break;
		case MouseStatus.SetEllipseLong:
			Next(MouseStatus.SetEllipseShort);
			break;
		case MouseStatus.SetEllipseShort:
			End();
			break;
		case MouseStatus.SetB2SplinePoint:
			Next(MouseStatus.SetB2SplineControl);
			break;
		case MouseStatus.SetB2SplineControl:
			((IPolylineB2Spline)NewEntity).Add();
			Next(MouseStatus.SetB2SplinePoint);
			break;
		case MouseStatus.SetMirrorFrom:
			uil_mirror.From = p;
			uil_mirror.To = p;
			Next(MouseStatus.SetMirrorTo);
			break;
		case MouseStatus.SetMirrorTo:
		{
			Point s = uil_mirror.From;
			Vector v = uil_mirror.To - uil_mirror.From;
			IPolylineAction action = SelectedImage.GroupMirror(SelectedImage.SelectedGroups.ToList(), s, v);
			editor?.InvokeEntityAction(action);
			End();
			break;
		}
		case MouseStatus.SetBreakPoint:
			if (MouEntity != null)
			{
				IPolylineAction polylineAction2 = SelectedImage.EntityBreak(MouEntity, uil_point.Point);
				if (polylineAction2 == null)
				{
					Escape();
					break;
				}
				editor?.InvokeEntityAction(polylineAction2);
				End();
			}
			break;
		case MouseStatus.SetRoundPoint:
			if (MouEntity != null && !double.IsNaN(uil_radius.Radius))
			{
				IPolylineAction polylineAction4 = SelectedImage.EntityRound(MouEntity, uil_radius.Radius);
				if (polylineAction4 == null)
				{
					Escape();
					break;
				}
				editor?.InvokeEntityAction(polylineAction4);
				End();
			}
			break;
		case MouseStatus.SetBevelPoint:
			if (MouEntity != null && !double.IsNaN(uil_radius.Radius))
			{
				IPolylineAction polylineAction3 = SelectedImage.EntityBevel(MouEntity, uil_radius.Radius);
				if (polylineAction3 == null)
				{
					Escape();
					break;
				}
				editor?.InvokeEntityAction(polylineAction3);
				End();
			}
			break;
		case MouseStatus.SetSharpPoint:
			if (MouEntity != null)
			{
				IPolylineAction polylineAction = SelectedImage.EntitySharp(MouEntity);
				if (polylineAction == null)
				{
					Escape();
					break;
				}
				editor?.InvokeEntityAction(polylineAction);
				End();
			}
			break;
		case MouseStatus.SetEllipseAngle:
		case MouseStatus.MovePoint:
			break;
		}
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);
		if (SelectedImage == null)
		{
			return;
		}
		Point p = GetLogicalPosition(e);
		AdjustPreview(ref p, e);
		switch (status)
		{
		case MouseStatus.None:
		case MouseStatus.Selected:
		case MouseStatus.SetBreakPoint:
		case MouseStatus.SetRoundPoint:
		case MouseStatus.SetSharpPoint:
		case MouseStatus.SetBevelPoint:
		{
			IPolylineEntity mouEntity = null;
			double num = 12.0 / base.ActualWidth * Penning.ViewportWidth;
			foreach (IPolylineEntity item in SelectedImage.Items)
			{
				double dist = item.GetDist(p);
				if (dist < num)
				{
					num = dist;
					mouEntity = item;
				}
			}
			MouEntity = mouEntity;
			PolylineEditorPointView polylineEditorPointView = null;
			double num2 = double.MaxValue;
			for (int i = 0; i < pvusedcount; i++)
			{
				PolylineEditorPointView polylineEditorPointView2 = pviews[i];
				Point position = e.GetPosition(polylineEditorPointView2);
				position.X -= polylineEditorPointView2.ActualWidth / 2.0;
				position.Y -= polylineEditorPointView2.ActualHeight / 2.0;
				double num3 = position.X * position.X + position.Y * position.Y;
				if (polylineEditorPointView == null || num3 < num2)
				{
					polylineEditorPointView = polylineEditorPointView2;
					num2 = num3;
				}
			}
			if (polylineEditorPointView != null && num2 < 36.0)
			{
				MovePoint = polylineEditorPointView;
			}
			else
			{
				MovePoint = null;
			}
			break;
		}
		case MouseStatus.SetMirrorTo:
			uil_mirror.To = p;
			break;
		default:
			ApplyPoint(ref p);
			break;
		}
		MovePoint?.UpdatePosition();
		PathUpdate();
		MousePositionEventArgs e2 = new MousePositionEventArgs(p, e.GetPosition(this));
		this.MousePositionMove?.Invoke(this, e2);
	}

	protected override void OnMouseEnter(MouseEventArgs e)
	{
		base.OnMouseEnter(e);
	}

	protected override void OnMouseLeave(MouseEventArgs e)
	{
		base.OnMouseLeave(e);
		MouEntity = null;
	}

	protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
	{
		base.OnMouseLeftButtonDown(e);
		if (SelectedImage == null)
		{
			return;
		}
		Point p = GetLogicalPosition(e);
		AdjustSubmit(ref p);
		MouseStatus mouseStatus = status;
		MouseStatus mouseStatus2 = mouseStatus;
		if ((uint)mouseStatus2 <= 1u)
		{
			if (MovePoint != null)
			{
				Start(MovePoint.Core?.Parent, MouseStatus.MovePoint);
				CaptureMouse();
			}
			else if (MouEntity != null)
			{
				Focus();
				Keyboard.Focus(this);
				Editor?.InvokeEntityClick(MouEntity);
			}
		}
		else
		{
			TrySubmit(p);
		}
	}

	protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
	{
		base.OnMouseLeftButtonUp(e);
		if (SelectedImage != null)
		{
			Point logicalPosition = GetLogicalPosition(e);
			if (base.IsMouseCaptured)
			{
				ReleaseMouseCapture();
			}
			MouseStatus mouseStatus = status;
			MouseStatus mouseStatus2 = mouseStatus;
			if (mouseStatus2 == MouseStatus.MovePoint)
			{
				End();
				MovePoint = null;
			}
		}
	}

	protected override void OnRender(DrawingContext ctx)
	{
		base.OnRender(ctx);
	}

	protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
	{
		base.OnRenderSizeChanged(sizeInfo);
		base.Clip = new RectangleGeometry(new Rect(sizeInfo.NewSize));
		PathUpdate();
	}

	private void OnNewEntityPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		PathUpdate();
	}

	private void OnNexEntityPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		PathUpdate();
	}

	private void OnMouEntityPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		PathUpdate();
	}

	private void UIL_Point_Escape(object sender, RoutedEventArgs e)
	{
		Escape();
	}

	private void UIL_Point_Submit(object sender, RoutedEventArgs e)
	{
		Point p = uil_point.Point;
		AdjustSubmit(ref p);
		TrySubmit(p);
	}

	private void UIL_Radius_Escape(object sender, RoutedEventArgs e)
	{
		Escape();
	}

	private void UIL_Radius_Submit(object sender, RoutedEventArgs e)
	{
		Point p = default(Point);
		AdjustSubmit(ref p);
		TrySubmit(p);
	}

	private void OnSelectionsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		if (!SelectionFreezed)
		{
			PathUpdate();
		}
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using SamSoarII.Polyline.Entity;

namespace SamSoarII.Polyline.Control.TreeView;

public class PolylineSelectTreeView_Entity : UserControl, IComponentConnector
{
	protected static readonly Brush Foreground_Normal;

	protected static readonly Brush Background_Normal;

	protected static readonly Brush Foreground_Selected;

	protected static readonly Brush Background_Selected;

	protected static readonly ImageSource Image_Virt;

	protected static readonly ImageSource Image_Line;

	protected static readonly ImageSource Image_Arch;

	protected static readonly ImageSource Image_Circle;

	protected static readonly ImageSource Image_BSpline;

	protected static readonly Geometry Geometry_TreeStart;

	protected static readonly Geometry Geometry_TreeLine;

	protected static readonly Geometry Geometry_TreeEnd;

	protected static readonly DependencyProperty IsSelectedProperty;

	private PolylineSelectTreeBox parent;

	internal PolylineSelectTreeView_Entity This;

	internal Path PH_Tree;

	internal Image IM_Icon;

	internal TextBlock TL_Name;

	private bool _contentLoaded;

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

	public IPolylineEntity Entity => (base.DataContext is IPolylineEntity) ? ((IPolylineEntity)base.DataContext) : null;

	static PolylineSelectTreeView_Entity()
	{
		Foreground_Normal = new SolidColorBrush(new Color
		{
			A = byte.MaxValue,
			R = 0,
			G = 0,
			B = 0
		});
		Background_Normal = new SolidColorBrush(new Color
		{
			A = 0,
			R = byte.MaxValue,
			G = byte.MaxValue,
			B = byte.MaxValue
		});
		Foreground_Selected = new SolidColorBrush(new Color
		{
			A = byte.MaxValue,
			R = 240,
			G = 240,
			B = 240
		});
		Background_Selected = new SolidColorBrush(new Color
		{
			A = byte.MaxValue,
			R = 96,
			G = 96,
			B = 160
		});
		Image_Virt = new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Polyline;component/Resources/Icon/Virt.png"));
		Image_Line = new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Polyline;component/Resources/Icon/Line.png"));
		Image_Arch = new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Polyline;component/Resources/Icon/Arch.png"));
		Image_Circle = new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Polyline;component/Resources/Icon/Circle.png"));
		Image_BSpline = new BitmapImage(new Uri("pack://application:,,,/SamSoarII.Polyline;component/Resources/Icon/BSpline.png"));
		IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(PolylineSelectTreeView_Entity), new PropertyMetadata(false, OnPropertyChanged_IsSelected));
		StreamGeometry streamGeometry = null;
		streamGeometry = new StreamGeometry();
		using (StreamGeometryContext streamGeometryContext = streamGeometry.Open())
		{
			streamGeometryContext.BeginFigure(new Point(15.0, 10.0), isFilled: false, isClosed: false);
			streamGeometryContext.LineTo(new Point(10.0, 10.0), isStroked: true, isSmoothJoin: false);
			streamGeometryContext.LineTo(new Point(10.0, 20.0), isStroked: true, isSmoothJoin: false);
		}
		Geometry_TreeStart = streamGeometry;
		streamGeometry = new StreamGeometry();
		using (StreamGeometryContext streamGeometryContext2 = streamGeometry.Open())
		{
			streamGeometryContext2.BeginFigure(new Point(10.0, 0.0), isFilled: false, isClosed: false);
			streamGeometryContext2.LineTo(new Point(10.0, 20.0), isStroked: true, isSmoothJoin: false);
		}
		Geometry_TreeLine = streamGeometry;
		streamGeometry = new StreamGeometry();
		using (StreamGeometryContext streamGeometryContext3 = streamGeometry.Open())
		{
			streamGeometryContext3.BeginFigure(new Point(10.0, 0.0), isFilled: false, isClosed: false);
			streamGeometryContext3.LineTo(new Point(10.0, 10.0), isStroked: true, isSmoothJoin: false);
			streamGeometryContext3.LineTo(new Point(15.0, 10.0), isStroked: true, isSmoothJoin: false);
		}
		Geometry_TreeEnd = streamGeometry;
	}

	public PolylineSelectTreeView_Entity()
	{
		InitializeComponent();
		base.Foreground = Foreground_Normal;
		base.Background = Background_Normal;
		base.Loaded += OnLoaded;
		base.Unloaded += OnUnloaded;
	}

	private static void OnPropertyChanged_IsSelected(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
	}

	private void OnLoaded(object sender, RoutedEventArgs e)
	{
		for (FrameworkElement frameworkElement = this; frameworkElement != null; frameworkElement = ((!(frameworkElement.Parent is FrameworkElement)) ? ((!(frameworkElement.TemplatedParent is FrameworkElement)) ? null : ((FrameworkElement)frameworkElement.TemplatedParent)) : ((FrameworkElement)frameworkElement.Parent)))
		{
			if (frameworkElement is PolylineSelectTreeBox)
			{
				parent = (PolylineSelectTreeBox)frameworkElement;
			}
		}
	}

	private void OnUnloaded(object sender, RoutedEventArgs e)
	{
		parent = null;
	}

	protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		base.OnPropertyChanged(e);
		if (e.Property == FrameworkElement.DataContextProperty)
		{
			if (e.OldValue is INotifyPropertyChanged)
			{
				INotifyPropertyChanged notifyPropertyChanged = (INotifyPropertyChanged)e.OldValue;
				notifyPropertyChanged.PropertyChanged -= OnDataContextPropertyChanged;
			}
			if (e.NewValue is INotifyPropertyChanged)
			{
				INotifyPropertyChanged notifyPropertyChanged2 = (INotifyPropertyChanged)e.NewValue;
				notifyPropertyChanged2.PropertyChanged += OnDataContextPropertyChanged;
			}
			if (Entity != null)
			{
				IsSelected = Entity.IsSelected;
				if (Entity is IPolylineLine)
				{
					IPolylineLine polylineLine = (IPolylineLine)Entity;
					if (polylineLine.IsReal)
					{
						IM_Icon.Source = Image_Line;
					}
					else
					{
						IM_Icon.Source = Image_Virt;
					}
				}
				else if (Entity is IPolylineArch)
				{
					IM_Icon.Source = Image_Arch;
				}
				else if (Entity is IPolylineCircle)
				{
					IM_Icon.Source = Image_Circle;
				}
				if (Entity.Group == null)
				{
					PH_Tree.Visibility = Visibility.Collapsed;
				}
				else
				{
					PH_Tree.Visibility = Visibility.Visible;
					if (Entity.ID == Entity.Group.Start)
					{
						PH_Tree.Data = Geometry_TreeStart;
					}
					else if (Entity.ID == Entity.Group.Start + Entity.Group.Count - 1)
					{
						PH_Tree.Data = Geometry_TreeEnd;
					}
					else
					{
						PH_Tree.Data = Geometry_TreeLine;
					}
				}
			}
		}
		if (e.Property == FrameworkElement.DataContextProperty || e.Property == IsSelectedProperty)
		{
			base.Background = (IsSelected ? Background_Selected : Background_Normal);
			base.Foreground = (IsSelected ? Foreground_Selected : Foreground_Normal);
		}
	}

	private void OnDataContextPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		string propertyName = e.PropertyName;
		string text = propertyName;
		if (text == "IsSelected" && Entity != null)
		{
			IsSelected = Entity.IsSelected;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamSoarII.Polyline;component/control/treeview/polylineselecttreeview_entity.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		switch (connectionId)
		{
		case 1:
			This = (PolylineSelectTreeView_Entity)target;
			break;
		case 2:
			PH_Tree = (Path)target;
			break;
		case 3:
			IM_Icon = (Image)target;
			break;
		case 4:
			TL_Name = (TextBlock)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}

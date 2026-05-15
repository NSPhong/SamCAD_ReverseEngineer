using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using SamSoarII.Polyline.Entity;

namespace SamSoarII.Polyline.Control.TreeView;

public class PolylineSelectTreeView_Group : UserControl, IComponentConnector
{
	protected static readonly Brush Foreground_Normal = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 0,
		G = 0,
		B = 0
	});

	protected static readonly Brush Background_Normal = new SolidColorBrush(new Color
	{
		A = 0,
		R = byte.MaxValue,
		G = byte.MaxValue,
		B = byte.MaxValue
	});

	protected static readonly Brush Foreground_Selected = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 240,
		G = 240,
		B = 240
	});

	protected static readonly Brush Background_Selected = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 96,
		G = 96,
		B = 160
	});

	protected static readonly Brush BorderBrush_Reorder = new LinearGradientBrush(new GradientStopCollection
	{
		new GradientStop(new Color
		{
			A = byte.MaxValue,
			R = 55,
			G = 122,
			B = 162
		}, 0.0),
		new GradientStop(new Color
		{
			A = byte.MaxValue,
			R = 114,
			G = 138,
			B = 199
		}, 0.0)
	});

	protected static readonly DependencyProperty IsExpandProperty = DependencyProperty.Register("IsExpand", typeof(bool), typeof(PolylineSelectTreeView_Group), new PropertyMetadata(false, OnPropertyChanged_IsExpand));

	protected static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register("IsSelected", typeof(bool), typeof(PolylineSelectTreeView_Group), new PropertyMetadata(false, OnPropertyChanged_IsSelected));

	private DispatcherTimer timer;

	private PolylineSelectTreeBox parent;

	internal PolylineSelectTreeView_Group This;

	internal PolylineSelectTreeArrow UI_Arrow;

	internal PolylineSelectTreeGroupImage UI_Image;

	internal TextBlock TL_Name;

	internal Rectangle RN_Reorder;

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

	public IPolylineGroup Group => (base.DataContext is IPolylineGroup) ? ((IPolylineGroup)base.DataContext) : null;

	public event RoutedEventHandler ArrowClick;

	public event DependencyPropertyChangedEventHandler ReorderVisibleChanged;

	public PolylineSelectTreeView_Group()
	{
		InitializeComponent();
		base.Foreground = Foreground_Normal;
		base.Background = Background_Normal;
		base.Loaded += OnLoaded;
		base.Unloaded += OnUnloaded;
	}

	private static void OnPropertyChanged_IsExpand(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_IsSelected(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
	}

	public void UpdateReorder()
	{
		double num = RN_Reorder.StrokeDashOffset + 1.0;
		if (num >= 8.0)
		{
			num -= 8.0;
		}
		RN_Reorder.StrokeDashOffset = num;
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
			if (e.OldValue is IPolylineReorderingGroup)
			{
				IPolylineReorderingGroup polylineReorderingGroup = (IPolylineReorderingGroup)e.OldValue;
				polylineReorderingGroup.View = null;
			}
			if (e.NewValue is IPolylineReorderingGroup)
			{
				IPolylineReorderingGroup polylineReorderingGroup2 = (IPolylineReorderingGroup)e.NewValue;
				polylineReorderingGroup2.View = this;
			}
			if (Group != null)
			{
				IsExpand = Group.IsExpand;
				IsSelected = Group.IsSelected;
			}
		}
		if (e.Property == FrameworkElement.DataContextProperty || e.Property == IsSelectedProperty)
		{
			UI_Arrow.IsSelected = IsSelected;
			base.Background = (IsSelected ? Background_Selected : Background_Normal);
			base.Foreground = (IsSelected ? Foreground_Selected : Foreground_Normal);
		}
		if (e.Property == FrameworkElement.DataContextProperty || e.Property == IsExpandProperty)
		{
			UI_Arrow.IsExpand = IsExpand;
		}
		if (e.Property == FrameworkElement.DataContextProperty)
		{
			if (Group is IPolylineReorderingGroup)
			{
				IPolylineReorderingGroup polylineReorderingGroup3 = (IPolylineReorderingGroup)Group;
				RN_Reorder.Visibility = ((!polylineReorderingGroup3.IsMoved) ? Visibility.Hidden : Visibility.Visible);
			}
			else
			{
				RN_Reorder.Visibility = Visibility.Hidden;
			}
		}
	}

	private void OnDataContextPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		switch (e.PropertyName)
		{
		case "IsSelected":
			if (Group != null)
			{
				IsSelected = Group.IsSelected;
			}
			break;
		case "IsExpand":
			if (Group != null)
			{
				IsExpand = Group.IsExpand;
			}
			break;
		case "IsMoved":
			if (Group is IPolylineReorderingGroup)
			{
				IPolylineReorderingGroup polylineReorderingGroup = (IPolylineReorderingGroup)Group;
				RN_Reorder.Visibility = ((!polylineReorderingGroup.IsMoved) ? Visibility.Hidden : Visibility.Visible);
			}
			break;
		}
	}

	private void UI_Arrow_MouseDown(object sender, MouseButtonEventArgs e)
	{
		e.Handled = true;
		this.ArrowClick?.Invoke(this, e);
	}

	private void RN_Reorder_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		this.ReorderVisibleChanged?.Invoke(this, e);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamSoarII.Polyline;component/control/treeview/polylineselecttreeview_group.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	internal Delegate _CreateDelegate(Type delegateType, string handler)
	{
		return Delegate.CreateDelegate(delegateType, this, handler);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		switch (connectionId)
		{
		case 1:
			This = (PolylineSelectTreeView_Group)target;
			break;
		case 2:
			UI_Arrow = (PolylineSelectTreeArrow)target;
			break;
		case 3:
			UI_Image = (PolylineSelectTreeGroupImage)target;
			break;
		case 4:
			TL_Name = (TextBlock)target;
			break;
		case 5:
			RN_Reorder = (Rectangle)target;
			RN_Reorder.IsVisibleChanged += RN_Reorder_IsVisibleChanged;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}

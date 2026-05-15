using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SamSoarII.Shell;

public class CustomComboButton : Control
{
	public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(object), typeof(CustomComboButton), new PropertyMetadata(null, OnPropertyChanged_Icon));

	public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(object), typeof(CustomComboButton), new PropertyMetadata(null, OnPropertyChanged_Header));

	public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable<object>), typeof(CustomComboButton), new PropertyMetadata(new object[0], OnPropertyChanged_ItemsSource));

	public static readonly DependencyProperty IsPressedProperty = DependencyProperty.Register("IsPressed", typeof(bool), typeof(CustomComboButton), new PropertyMetadata(false, OnPropertyChanged_IsPressed));

	private DependencyObject cn_icon;

	private DependencyObject cn_header;

	private DependencyObject cn_downarrow;

	private DependencyObject downarrow;

	private ContextMenu ctxmenu;

	public object Icon
	{
		get
		{
			return GetValue(IconProperty);
		}
		set
		{
			SetValue(IconProperty, value);
		}
	}

	public object Header
	{
		get
		{
			return GetValue(HeaderProperty);
		}
		set
		{
			SetValue(HeaderProperty, value);
		}
	}

	public IEnumerable<object> ItemsSource
	{
		get
		{
			return (IEnumerable<object>)GetValue(ItemsSourceProperty);
		}
		set
		{
			SetValue(ItemsSourceProperty, value);
		}
	}

	public bool IsPressed
	{
		get
		{
			return (bool)GetValue(IsPressedProperty);
		}
		set
		{
			SetValue(IsPressedProperty, value);
		}
	}

	public event RoutedEventHandler Click;

	public CustomComboButton()
	{
		base.Template = (ControlTemplate)FindResource("CustomComboButton_DefaultTemplate");
	}

	private static void OnPropertyChanged_Icon(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is CustomComboButton)
		{
			((CustomComboButton)d).OnIconChanged(e);
		}
	}

	protected virtual void OnIconChanged(DependencyPropertyChangedEventArgs e)
	{
		CnSetup(cn_icon, Icon);
	}

	private static void OnPropertyChanged_Header(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is CustomComboButton)
		{
			((CustomComboButton)d).OnHeaderChanged(e);
		}
	}

	protected virtual void OnHeaderChanged(DependencyPropertyChangedEventArgs e)
	{
		CnSetup(cn_header, Header);
	}

	private static void OnPropertyChanged_ItemsSource(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is CustomComboButton)
		{
			((CustomComboButton)d).OnItemsSourceChanged(e);
		}
	}

	protected virtual void OnItemsSourceChanged(DependencyPropertyChangedEventArgs e)
	{
		if (ctxmenu == null)
		{
			ctxmenu = new ContextMenu();
			ctxmenu.Opened += delegate
			{
				CommandManager.InvalidateRequerySuggested();
			};
			ctxmenu.Closed += delegate
			{
				IsPressed = false;
			};
		}
		ctxmenu.Items.Clear();
		if (ItemsSource == null)
		{
			return;
		}
		foreach (object item in ItemsSource)
		{
			ctxmenu.Items.Add(item);
		}
		base.ContextMenu = ctxmenu;
	}

	private static void OnPropertyChanged_IsPressed(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is CustomComboButton)
		{
			((CustomComboButton)d).OnIsPressedChanged(e);
		}
	}

	protected virtual void OnIsPressedChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	public override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		cn_icon = GetTemplateChild("PART_Container_Icon");
		cn_header = GetTemplateChild("PART_Container_Header");
		cn_downarrow = GetTemplateChild("PART_Container_DownArrow");
		downarrow = GetTemplateChild("PART_DownArrow");
		CnSetup(cn_icon, Icon);
		CnSetup(cn_header, Header);
		if (downarrow == null)
		{
			CreateDownArrow();
			CnSetup(cn_downarrow, downarrow);
		}
		CnAdd_MouseDown(cn_icon, OnContentMouseDown);
		CnAdd_MouseDown(cn_header, OnContentMouseDown);
		CnAdd_MouseDown(cn_downarrow, OnDownArrowMouseDown);
		CnAdd_MouseUp(cn_icon, OnContentMouseUp);
		CnAdd_MouseUp(cn_header, OnContentMouseUp);
		CnAdd_MouseUp(cn_downarrow, OnDownArrowMouseUp);
	}

	protected UIElement CnConvUI(object obj)
	{
		UIElement uIElement = ((obj is UIElement) ? ((UIElement)obj) : ((obj == null) ? null : new TextBlock
		{
			Text = obj.ToString()
		}));
		if (uIElement is FrameworkElement)
		{
			FrameworkElement frameworkElement = (FrameworkElement)uIElement;
			frameworkElement.VerticalAlignment = VerticalAlignment.Center;
			frameworkElement.HorizontalAlignment = HorizontalAlignment.Center;
		}
		return uIElement;
	}

	protected void CnSetup(DependencyObject cn, object obj)
	{
		if (cn is Decorator)
		{
			((Decorator)cn).Child = CnConvUI(obj);
		}
		else if (cn is ContentControl)
		{
			((ContentControl)cn).Content = obj;
		}
		else if (cn is ItemsControl)
		{
			((ItemsControl)cn).Items.Add(obj);
		}
		else if (cn is Panel)
		{
			((Panel)cn).Children.Add(CnConvUI(obj));
		}
	}

	protected void CnAdd_MouseDown(DependencyObject cn, MouseButtonEventHandler md)
	{
		if (cn is UIElement)
		{
			((UIElement)cn).MouseDown += md;
		}
	}

	protected void CnAdd_MouseUp(DependencyObject cn, MouseButtonEventHandler mu)
	{
		if (cn is UIElement)
		{
			((UIElement)cn).MouseUp += mu;
		}
	}

	protected void CreateDownArrow()
	{
		StreamGeometry streamGeometry = new StreamGeometry();
		using (StreamGeometryContext streamGeometryContext = streamGeometry.Open())
		{
			streamGeometryContext.BeginFigure(new Point(0.0, 0.0), isFilled: true, isClosed: true);
			streamGeometryContext.LineTo(new Point(8.0, 0.0), isStroked: true, isSmoothJoin: true);
			streamGeometryContext.LineTo(new Point(4.0, 4.0), isStroked: true, isSmoothJoin: true);
		}
		Path path = new Path
		{
			Width = 8.0,
			Height = 4.0,
			Data = streamGeometry,
			Fill = new SolidColorBrush(Color.FromArgb(byte.MaxValue, 128, 128, 128)),
			HorizontalAlignment = HorizontalAlignment.Center,
			VerticalAlignment = VerticalAlignment.Center,
			SnapsToDevicePixels = true
		};
		downarrow = path;
	}

	private void OnContentMouseDown(object sender, MouseButtonEventArgs e)
	{
		IsPressed = true;
	}

	private void OnContentMouseUp(object sender, MouseButtonEventArgs e)
	{
		IsPressed = false;
		this.Click?.Invoke(this, e);
	}

	private void OnDownArrowMouseDown(object sender, MouseButtonEventArgs e)
	{
		if (ctxmenu != null && ctxmenu.Items.Count != 0)
		{
			ctxmenu.Placement = PlacementMode.Bottom;
			ctxmenu.PlacementTarget = this;
			ctxmenu.IsOpen = true;
			IsPressed = true;
		}
	}

	private void OnDownArrowMouseUp(object sender, MouseButtonEventArgs e)
	{
		IsPressed = false;
	}

	protected override void OnMouseLeave(MouseEventArgs e)
	{
		base.OnMouseLeave(e);
		IsPressed = false;
	}
}

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SamSoarII.Dock.Interface;
using SamSoarII.Dock.View;

namespace SamSoarII.Dock.AutoHide;

internal class AutoHidePopup : StackPanel, IDockView
{
	private DockManager parent;

	private IDockBaseView viewcontent;

	private AutoHideCommon.Sides side;

	private AutoHidePopupResizer resizer;

	private UserControl opposite;

	public DockManager ViewParent => parent;

	IDockView IDockView.ViewParent => ViewParent;

	public DockPanel DockPanel => parent.DP_AutoHide;

	public IDockBaseView ViewContent
	{
		get
		{
			return viewcontent;
		}
		protected set
		{
			if (viewcontent == value)
			{
				return;
			}
			FrameworkElement frameworkElement = null;
			if (viewcontent != null)
			{
				frameworkElement = ViewCommon.GetFrameworkElement(viewcontent);
				if (frameworkElement != null)
				{
				}
				if (viewcontent is FrameworkElement)
				{
					frameworkElement = (FrameworkElement)viewcontent;
					frameworkElement.Loaded -= OnViewContentLoaded;
					frameworkElement.LostFocus -= OnViewContentLostFocus;
				}
				base.Children.Clear();
			}
			viewcontent = value;
			if (viewcontent == null)
			{
				return;
			}
			frameworkElement = ViewCommon.GetFrameworkElement(viewcontent);
			if (frameworkElement != null)
			{
			}
			if (viewcontent is FrameworkElement)
			{
				frameworkElement = (FrameworkElement)viewcontent;
				frameworkElement.Loaded += OnViewContentLoaded;
				frameworkElement.LostFocus += OnViewContentLostFocus;
				switch (side)
				{
				case AutoHideCommon.Sides.Left:
					base.Orientation = Orientation.Horizontal;
					DockPanel.SetDock(this, System.Windows.Controls.Dock.Left);
					DockPanel.SetDock(opposite, System.Windows.Controls.Dock.Right);
					base.Children.Add(frameworkElement);
					base.Children.Add(resizer);
					resizer.Cursor = Cursors.SizeWE;
					base.Width = frameworkElement.Width + 8.0;
					base.MinWidth = frameworkElement.MinWidth + 8.0;
					frameworkElement.Height = parent.ActualHeight;
					break;
				case AutoHideCommon.Sides.Right:
					base.Orientation = Orientation.Horizontal;
					DockPanel.SetDock(this, System.Windows.Controls.Dock.Right);
					DockPanel.SetDock(opposite, System.Windows.Controls.Dock.Left);
					base.Children.Add(resizer);
					base.Children.Add(frameworkElement);
					resizer.Cursor = Cursors.SizeWE;
					base.Width = frameworkElement.Width + 8.0;
					base.MinWidth = frameworkElement.MinWidth + 8.0;
					frameworkElement.Height = parent.ActualHeight;
					break;
				case AutoHideCommon.Sides.Top:
					base.Orientation = Orientation.Vertical;
					DockPanel.SetDock(this, System.Windows.Controls.Dock.Top);
					DockPanel.SetDock(opposite, System.Windows.Controls.Dock.Bottom);
					base.Children.Add(frameworkElement);
					base.Children.Add(resizer);
					resizer.Cursor = Cursors.SizeNS;
					base.Height = frameworkElement.Height + 8.0;
					base.MinHeight = frameworkElement.MinHeight + 8.0;
					frameworkElement.Width = parent.ActualWidth;
					break;
				case AutoHideCommon.Sides.Bottom:
					base.Orientation = Orientation.Vertical;
					DockPanel.SetDock(this, System.Windows.Controls.Dock.Bottom);
					DockPanel.SetDock(opposite, System.Windows.Controls.Dock.Top);
					base.Children.Add(resizer);
					base.Children.Add(frameworkElement);
					resizer.Cursor = Cursors.SizeNS;
					base.Height = frameworkElement.Height + 8.0;
					base.MinHeight = frameworkElement.MinHeight + 8.0;
					frameworkElement.Width = parent.ActualWidth;
					break;
				}
			}
		}
	}

	public AutoHideCommon.Sides Side => side;

	public AutoHidePopup(DockManager _parent)
	{
		parent = _parent;
		resizer = new AutoHidePopupResizer(this);
		opposite = new UserControl();
		DockPanel.Children.Add(this);
		DockPanel.Children.Add(opposite);
	}

	public void Show(AutoHideSideBar sidebar, IDockBaseView baseview)
	{
		side = sidebar.Side;
		ViewContent = baseview;
		base.Visibility = Visibility.Visible;
	}

	public void Hide()
	{
		ViewContent = null;
		base.Visibility = Visibility.Hidden;
		base.Width = double.NaN;
		base.Height = double.NaN;
		base.MinWidth = 0.0;
		base.MinHeight = 0.0;
	}

	private void OnViewContentLoaded(object sender, RoutedEventArgs e)
	{
		FrameworkElement frameworkElement = null;
		frameworkElement = ((!(viewcontent is DockBaseView)) ? ViewCommon.GetFrameworkElement(viewcontent) : ((DockBaseView)viewcontent).MainGrid);
		if (frameworkElement != null)
		{
			frameworkElement.Focus();
			Keyboard.Focus(frameworkElement);
		}
	}

	private void OnViewContentLostFocus(object sender, RoutedEventArgs e)
	{
		if (!(viewcontent is FrameworkElement))
		{
			return;
		}
		FrameworkElement frameworkElement = (FrameworkElement)viewcontent;
		Window window = Window.GetWindow(this);
		if (window != null)
		{
			IInputElement focusedElement = FocusManager.GetFocusedElement(window);
			if ((!(focusedElement is DependencyObject) || !frameworkElement.IsAncestorOf((DependencyObject)focusedElement)) && (!(focusedElement is ComboBoxItem) || ((ComboBoxItem)focusedElement).Parent != null || ((ComboBoxItem)focusedElement).TemplatedParent != null))
			{
				parent.HideAutoHide();
			}
		}
	}

	protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		base.OnPropertyChanged(e);
		if (!(viewcontent is FrameworkElement))
		{
			return;
		}
		FrameworkElement frameworkElement = (FrameworkElement)viewcontent;
		switch (side)
		{
		case AutoHideCommon.Sides.Left:
		case AutoHideCommon.Sides.Right:
			if (e.Property == FrameworkElement.WidthProperty)
			{
				frameworkElement.Width = (double)e.NewValue - 8.0;
			}
			break;
		case AutoHideCommon.Sides.Top:
		case AutoHideCommon.Sides.Bottom:
			if (e.Property == FrameworkElement.HeightProperty)
			{
				frameworkElement.Height = (double)e.NewValue - 8.0;
			}
			break;
		}
	}

	protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
	{
		base.OnRenderSizeChanged(sizeInfo);
		if (viewcontent is FrameworkElement)
		{
			FrameworkElement frameworkElement = (FrameworkElement)viewcontent;
			switch (side)
			{
			case AutoHideCommon.Sides.Left:
			case AutoHideCommon.Sides.Right:
				frameworkElement.Height = base.ActualHeight;
				break;
			case AutoHideCommon.Sides.Top:
			case AutoHideCommon.Sides.Bottom:
				frameworkElement.Width = base.ActualWidth;
				break;
			}
		}
	}
}

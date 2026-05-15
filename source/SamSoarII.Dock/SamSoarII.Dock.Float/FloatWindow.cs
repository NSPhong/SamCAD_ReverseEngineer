using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using SamSoarII.Dock.Global;
using SamSoarII.Dock.Interface;
using SamSoarII.Dock.View;

namespace SamSoarII.Dock.Float;

public class FloatWindow : Window, IDisposable, IDockContainer, IDockView, IDockFloat, INotifyPropertyChanged, IUserFocus, IComponentConnector
{
	public enum ResizeModes
	{
		Null,
		None,
		Top,
		Bottom,
		Left,
		Right,
		TopLeft,
		TopRight,
		BottomLeft,
		BottomRight
	}

	public static readonly double BorderWidth = 20.0;

	public static readonly double BorderHeight = 20.0;

	public static readonly Color SurroundingColor_Unfocused_Fadein = new Color
	{
		A = 128,
		R = 172,
		G = 174,
		B = 187
	};

	public static readonly Color SurroundingColor_Unfocused_Fadeout = new Color
	{
		A = 0,
		R = 248,
		G = 248,
		B = 248
	};

	public static readonly Color SurroundingColor_Unfocused_Invisible = new Color
	{
		A = 0,
		R = 248,
		G = 248,
		B = 248
	};

	public static readonly Color SurroundingColor_Focused_Fadein = new Color
	{
		A = 128,
		R = 0,
		G = 122,
		B = 204
	};

	public static readonly Color SurroundingColor_Focused_Fadeout = new Color
	{
		A = 32,
		R = 128,
		G = 170,
		B = 220
	};

	public static readonly Color SurroundingColor_Focused_Invisible = new Color
	{
		A = 0,
		R = 160,
		G = 176,
		B = 248
	};

	private bool isdisposed = false;

	private DockManager parent;

	private Border resizer;

	private Point resizestart;

	private bool isresizing = false;

	private ResizeModes resizemode = ResizeModes.None;

	private IDockBaseView baseview;

	private bool toclose = false;

	internal Grid GD_Main;

	internal UserControl UC_TopLeft;

	internal UserControl UC_Top;

	internal UserControl UC_TopRight;

	internal UserControl UC_Right;

	internal UserControl UC_BottomRight;

	internal UserControl UC_Bottom;

	internal UserControl UC_BottomLeft;

	internal UserControl UC_Left;

	private bool _contentLoaded;

	public bool IsDisposed => isdisposed;

	public DockManager ViewParent => parent;

	internal MouseHelper MouseHelper => parent?.MouseHelper;

	IDockView IDockView.ViewParent => ViewParent;

	public bool IsResizing => isresizing;

	public ResizeModes ViewResizeMode => resizemode;

	public bool IsUserFocused
	{
		get
		{
			return ViewContent?.IsUserFocused ?? false;
		}
		set
		{
		}
	}

	public Color SurroundingColor_Fadein => IsUserFocused ? SurroundingColor_Focused_Fadein : SurroundingColor_Unfocused_Fadein;

	public Color SurroundingColor_Fadeout => IsUserFocused ? SurroundingColor_Focused_Fadeout : SurroundingColor_Unfocused_Fadeout;

	public Color SurroundingColor_Invisible => IsUserFocused ? SurroundingColor_Focused_Invisible : SurroundingColor_Unfocused_Invisible;

	internal IDockBaseView ViewContent
	{
		get
		{
			return baseview;
		}
		set
		{
			if (baseview == value)
			{
				return;
			}
			IDockBaseView dockBaseView = baseview;
			baseview = null;
			if (dockBaseView != null)
			{
				resizer.Child = null;
				if (dockBaseView.DockContainer != null)
				{
					dockBaseView.DockContainer = null;
				}
			}
			baseview = value;
			if (baseview != null)
			{
				if (baseview.DockContainer != this)
				{
					baseview.DockContainer = this;
				}
				if (baseview is FrameworkElement)
				{
					FrameworkElement frameworkElement = (FrameworkElement)baseview;
					resizer.Child = frameworkElement;
					base.Width = frameworkElement.Width + BorderWidth;
					base.Height = frameworkElement.Height + BorderHeight;
				}
			}
		}
	}

	IDockBaseView IDockContainer.ViewContent
	{
		get
		{
			return ViewContent;
		}
		set
		{
			ViewContent = value;
		}
	}

	public bool ToClose => toclose;

	public event PropertyChangedEventHandler PropertyChanged = delegate
	{
	};

	public FloatWindow(DockManager _parent)
	{
		InitializeComponent();
		parent = _parent;
		resizer = new Border();
		resizer.BorderThickness = new Thickness(10.0);
		resizer.BorderBrush = new SolidColorBrush(new Color
		{
			A = 1,
			R = byte.MaxValue,
			G = byte.MaxValue,
			B = byte.MaxValue
		});
		resizer.PreviewMouseMove += OnResizerPreviewMouseMove;
		resizer.MouseMove += OnResizerMouseMove;
		resizer.MouseLeftButtonDown += OnResizerLeftButtonDown;
		resizer.MouseLeftButtonUp += OnResizerLeftButtonUp;
		resizer.MouseLeave += OnResizerMouseLeave;
		Grid.SetRow(resizer, 0);
		Grid.SetColumn(resizer, 0);
		Grid.SetRowSpan(resizer, 3);
		Grid.SetColumnSpan(resizer, 3);
		GD_Main.Children.Add(resizer);
		base.DataContext = this;
	}

	public void Dispose()
	{
		if (!isdisposed)
		{
			isdisposed = true;
			ViewContent = null;
			resizer.PreviewMouseMove -= OnResizerPreviewMouseMove;
			resizer.MouseMove -= OnResizerMouseMove;
			resizer.MouseLeftButtonDown -= OnResizerLeftButtonDown;
			resizer.MouseLeftButtonUp -= OnResizerLeftButtonUp;
			resizer.MouseLeave -= OnResizerMouseLeave;
			parent = null;
			resizer = null;
		}
	}

	public void InvokeIsUserFocusedChanged()
	{
		this.PropertyChanged(this, new PropertyChangedEventArgs("SurroundingColor_Fadein"));
		this.PropertyChanged(this, new PropertyChangedEventArgs("SurroundingColor_Fadeout"));
		this.PropertyChanged(this, new PropertyChangedEventArgs("SurroundingColor_Invisible"));
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		base.OnClosing(e);
		if (!toclose)
		{
			toclose = true;
			parent?.HideFloatWindow(this);
		}
	}

	private void OnResizerPreviewMouseMove(object sender, MouseEventArgs e)
	{
		if (baseview is FrameworkElement && !isresizing)
		{
			FrameworkElement frameworkElement = (FrameworkElement)baseview;
			Point position = e.GetPosition(frameworkElement);
			if (position.X <= 0.0)
			{
				if (position.Y <= 0.0)
				{
					resizemode = ResizeModes.TopLeft;
				}
				else if (position.Y >= frameworkElement.ActualHeight)
				{
					resizemode = ResizeModes.BottomLeft;
				}
				else
				{
					resizemode = ResizeModes.Left;
				}
			}
			else if (position.X >= frameworkElement.ActualWidth)
			{
				if (position.Y <= 0.0)
				{
					resizemode = ResizeModes.TopRight;
				}
				else if (position.Y >= frameworkElement.ActualHeight)
				{
					resizemode = ResizeModes.BottomRight;
				}
				else
				{
					resizemode = ResizeModes.Right;
				}
			}
			else if (position.Y <= 0.0)
			{
				resizemode = ResizeModes.Top;
			}
			else if (position.Y >= frameworkElement.ActualHeight)
			{
				resizemode = ResizeModes.Bottom;
			}
			else
			{
				resizemode = ResizeModes.None;
			}
		}
		switch (resizemode)
		{
		case ResizeModes.Top:
		case ResizeModes.Bottom:
			resizer.Cursor = Cursors.SizeNS;
			break;
		case ResizeModes.Left:
		case ResizeModes.Right:
			resizer.Cursor = Cursors.SizeWE;
			break;
		case ResizeModes.TopLeft:
		case ResizeModes.BottomRight:
			resizer.Cursor = Cursors.SizeNWSE;
			break;
		case ResizeModes.TopRight:
		case ResizeModes.BottomLeft:
			resizer.Cursor = Cursors.SizeNESW;
			break;
		default:
			resizer.Cursor = null;
			break;
		}
	}

	private void OnResizerMouseMove(object sender, MouseEventArgs e)
	{
		if (baseview is FrameworkElement && isresizing)
		{
			Thickness resizeOffset = MouseHelper.ResizeOffset;
			FrameworkElement frameworkElement = (FrameworkElement)baseview;
			Point position = e.GetPosition(this);
			Point point = TranslatePoint(new Point(0.0, 0.0), parent.MouseHelper);
			position.Y = Math.Min(position.Y, parent.MouseHelper.ActualHeight - point.Y - 24.0);
			double num = position.X - resizestart.X;
			double num2 = position.Y - resizestart.Y;
			double val = 0.0 - num;
			double val2 = num;
			double val3 = 0.0 - num2;
			double val4 = num2;
			switch (resizemode)
			{
			case ResizeModes.Top:
				val3 = Math.Max(val3, frameworkElement.MinHeight - (frameworkElement.ActualHeight - resizeOffset.Bottom));
				MouseHelper.ResizeOffset = new Thickness(resizeOffset.Left, val3, resizeOffset.Right, resizeOffset.Bottom);
				break;
			case ResizeModes.Bottom:
				val4 = Math.Max(val4, frameworkElement.MinHeight - (frameworkElement.ActualHeight - resizeOffset.Top));
				MouseHelper.ResizeOffset = new Thickness(resizeOffset.Left, resizeOffset.Top, resizeOffset.Right, val4);
				break;
			case ResizeModes.Left:
				val = Math.Max(val, frameworkElement.MinWidth - (frameworkElement.ActualWidth - resizeOffset.Right));
				MouseHelper.ResizeOffset = new Thickness(val, resizeOffset.Top, resizeOffset.Right, resizeOffset.Bottom);
				break;
			case ResizeModes.Right:
				val2 = Math.Max(val2, frameworkElement.MinWidth - (frameworkElement.ActualWidth - resizeOffset.Left));
				MouseHelper.ResizeOffset = new Thickness(resizeOffset.Left, resizeOffset.Top, val2, resizeOffset.Bottom);
				break;
			case ResizeModes.TopLeft:
				val3 = Math.Max(val3, frameworkElement.MinHeight - (frameworkElement.ActualHeight - resizeOffset.Bottom));
				val = Math.Max(val, frameworkElement.MinWidth - (frameworkElement.ActualWidth - resizeOffset.Right));
				MouseHelper.ResizeOffset = new Thickness(val, val3, resizeOffset.Right, resizeOffset.Bottom);
				break;
			case ResizeModes.TopRight:
				val3 = Math.Max(val3, frameworkElement.MinHeight - (frameworkElement.ActualHeight - resizeOffset.Bottom));
				val2 = Math.Max(val2, frameworkElement.MinWidth - (frameworkElement.ActualWidth - resizeOffset.Left));
				MouseHelper.ResizeOffset = new Thickness(resizeOffset.Left, val3, val2, resizeOffset.Bottom);
				break;
			case ResizeModes.BottomLeft:
				val4 = Math.Max(val4, frameworkElement.MinHeight - (frameworkElement.ActualHeight - resizeOffset.Top));
				val = Math.Max(val, frameworkElement.MinWidth - (frameworkElement.ActualWidth - resizeOffset.Right));
				MouseHelper.ResizeOffset = new Thickness(val, resizeOffset.Top, resizeOffset.Right, val4);
				break;
			case ResizeModes.BottomRight:
				val4 = Math.Max(val4, frameworkElement.MinHeight - (frameworkElement.ActualHeight - resizeOffset.Top));
				val2 = Math.Max(val2, frameworkElement.MinWidth - (frameworkElement.ActualWidth - resizeOffset.Left));
				MouseHelper.ResizeOffset = new Thickness(resizeOffset.Left, resizeOffset.Top, val2, val4);
				break;
			}
		}
	}

	private void OnResizerMouseLeave(object sender, MouseEventArgs e)
	{
		if (!isresizing)
		{
			resizemode = ResizeModes.None;
			resizer.Cursor = null;
		}
	}

	private void OnResizerLeftButtonDown(object sender, MouseButtonEventArgs e)
	{
		if (!isresizing && resizemode != ResizeModes.None)
		{
			isresizing = true;
			resizestart = e.GetPosition(this);
			MouseHelper.StartResize(this);
			resizer.CaptureMouse();
		}
	}

	private void OnResizerLeftButtonUp(object sender, MouseButtonEventArgs e)
	{
		if (isresizing)
		{
			isresizing = false;
			MouseHelper.EndResize();
			resizer.ReleaseMouseCapture();
		}
	}

	protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
	{
		base.OnRenderSizeChanged(sizeInfo);
		if (ViewContent is FrameworkElement)
		{
			FrameworkElement frameworkElement = (FrameworkElement)ViewContent;
			if (base.WindowState == WindowState.Maximized)
			{
				frameworkElement.Width = base.ActualWidth;
				frameworkElement.Height = base.ActualHeight;
				resizer.BorderThickness = new Thickness(0.0);
			}
			else
			{
				frameworkElement.Width = base.ActualWidth - BorderWidth;
				frameworkElement.Height = base.ActualHeight - BorderHeight;
				resizer.BorderThickness = new Thickness(10.0);
			}
		}
	}

	protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		base.OnPropertyChanged(e);
		if ((e.Property == Window.TopProperty || e.Property == Window.LeftProperty) && ViewContent is DockBaseView)
		{
			parent.WriteSize(ViewContent);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamSoarII.Dock;component/float/floatwindow.xaml", UriKind.Relative);
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
			GD_Main = (Grid)target;
			break;
		case 2:
			UC_TopLeft = (UserControl)target;
			break;
		case 3:
			UC_Top = (UserControl)target;
			break;
		case 4:
			UC_TopRight = (UserControl)target;
			break;
		case 5:
			UC_Right = (UserControl)target;
			break;
		case 6:
			UC_BottomRight = (UserControl)target;
			break;
		case 7:
			UC_Bottom = (UserControl)target;
			break;
		case 8:
			UC_BottomLeft = (UserControl)target;
			break;
		case 9:
			UC_Left = (UserControl)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}

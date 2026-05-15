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

namespace SamSoarII.Shell;

public class GridPenningVerticalScroll : UserControl, IDisposable, IComponentConnector
{
	public static readonly double OffsetMaxinum = 1.0;

	public static readonly double OffsetMininum = 0.0;

	public static readonly double LengthMaxinum = 1.0;

	public static readonly double LengthMininum = 0.01;

	private GridPenning parent;

	private TextBlock[] tb_vrulers;

	public static readonly DependencyProperty OffsetProperty = DependencyProperty.Register("Offset", typeof(double), typeof(GridPenningVerticalScroll), new FrameworkPropertyMetadata(0.0, OnOffsetChanged));

	public static readonly DependencyProperty LengthProperty = DependencyProperty.Register("Length", typeof(double), typeof(GridPenningVerticalScroll), new FrameworkPropertyMetadata(1.0, OnLengthChanged));

	private bool isdraging = false;

	private bool isresizing = false;

	private Point startpoint;

	private Point endpoint;

	internal Canvas CV_VRuler;

	internal Canvas CV_Scroll;

	internal Rectangle RN_VScroll;

	internal Line LN_VTop;

	internal Line LN_VBottom;

	private bool _contentLoaded;

	public GridPenning ViewParent => parent;

	public IGridPenningSource DrawingSource => parent?.DrawingSource;

	public double Offset
	{
		get
		{
			return (double)GetValue(OffsetProperty);
		}
		set
		{
			SetValue(OffsetProperty, Math.Min(OffsetMaxinum, Math.Max(OffsetMininum, value)));
		}
	}

	public double Length
	{
		get
		{
			return (double)GetValue(LengthProperty);
		}
		set
		{
			SetValue(LengthProperty, Math.Min(LengthMaxinum, Math.Max(LengthMininum, value)));
		}
	}

	public event DependencyPropertyChangedEventHandler OffsetChanged = delegate
	{
	};

	public event DependencyPropertyChangedEventHandler LengthChanged = delegate
	{
	};

	public GridPenningVerticalScroll(GridPenning _parent)
	{
		InitializeComponent();
		parent = _parent;
		tb_vrulers = new TextBlock[GridPenning.YBlockCapacity];
		for (int num = 0; num < tb_vrulers.Length; num++)
		{
			tb_vrulers[num] = new TextBlock();
			tb_vrulers[num].Foreground = Brushes.White;
			tb_vrulers[num].FontSize = 10.0;
			double num2 = base.ActualHeight - (double)num * GridPenning.BlockDefaultHeight - 16.0;
			tb_vrulers[num].Visibility = ((!(num2 >= 0.0)) ? Visibility.Hidden : Visibility.Visible);
			Canvas.SetTop(tb_vrulers[num], num2);
			Canvas.SetLeft(tb_vrulers[num], 4.0);
			CV_VRuler.Children.Add(tb_vrulers[num]);
		}
		parent.PropertyChanged += Parent_OnPropertyChanged;
		base.Loaded += OnLoaded;
	}

	public void Dispose()
	{
		base.Loaded -= OnLoaded;
		parent.PropertyChanged -= Parent_OnPropertyChanged;
		CV_VRuler.Children.Clear();
		parent = null;
		tb_vrulers = null;
	}

	private static void OnOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GridPenningVerticalScroll)
		{
			GridPenningVerticalScroll gridPenningVerticalScroll = (GridPenningVerticalScroll)d;
			gridPenningVerticalScroll.OnOffsetChanged(e);
		}
	}

	protected virtual void OnOffsetChanged(DependencyPropertyChangedEventArgs e)
	{
		Update();
		this.OffsetChanged(this, e);
	}

	private static void OnLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GridPenningVerticalScroll)
		{
			GridPenningVerticalScroll gridPenningVerticalScroll = (GridPenningVerticalScroll)d;
			gridPenningVerticalScroll.OnLengthChanged(e);
		}
	}

	protected virtual void OnLengthChanged(DependencyPropertyChangedEventArgs e)
	{
		Update();
		this.LengthChanged(this, e);
		Offset = Math.Min(Offset, 1.0 - Length);
	}

	public void Update()
	{
		UpdateVerticalRuler();
		UpdateVerticalScroll();
	}

	public void UpdateVerticalRuler()
	{
		double num = parent.VerticalOffset;
		for (int i = 0; i < tb_vrulers.Length; i++)
		{
			tb_vrulers[i].Text = ToVerticalRulerString(DrawingSource.YStart + num);
			double num2 = base.ActualHeight - (double)i * GridPenning.BlockDefaultHeight - 16.0;
			tb_vrulers[i].Visibility = ((!(num2 >= 0.0)) ? Visibility.Hidden : Visibility.Visible);
			Canvas.SetTop(tb_vrulers[i], num2);
			num += parent.BlockHeight;
		}
	}

	public void UpdateVerticalScroll()
	{
		Canvas.SetTop(LN_VBottom, (1.0 - Offset) * base.ActualHeight);
		Canvas.SetTop(RN_VScroll, (1.0 - Offset - Length) * base.ActualHeight);
		Canvas.SetTop(LN_VTop, (1.0 - Offset - Length) * base.ActualHeight);
		RN_VScroll.Height = Length * base.ActualHeight;
	}

	protected string ToVerticalRulerString(double value)
	{
		if (DrawingSource is IGridPenningSourceEX)
		{
			IGridPenningSourceEX gridPenningSourceEX = (IGridPenningSourceEX)DrawingSource;
			return gridPenningSourceEX.GetYUnit(value);
		}
		int num = 0;
		string arg = DrawingSource.XUnit;
		while (DrawingSource.XValueBase != null && num < DrawingSource.XValueBase.Count)
		{
			double num2 = DrawingSource.XValueBase[num];
			if (value < num2)
			{
				break;
			}
			value /= num2;
			arg = DrawingSource.XUnitEx[num];
			num++;
		}
		return $"{value:f2}{arg:s}";
	}

	private void OnLoaded(object sender, RoutedEventArgs e)
	{
		Update();
	}

	private void Parent_OnPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
	}

	protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
	{
		base.OnRenderSizeChanged(sizeInfo);
		UpdateVerticalRuler();
		UpdateVerticalScroll();
	}

	private void RN_VScroll_MouseDown(object sender, MouseButtonEventArgs e)
	{
		RN_VScroll.CaptureMouse();
		parent.EnterControling();
		isdraging = true;
		startpoint = e.GetPosition(this);
	}

	private void RN_VScroll_MouseUp(object sender, MouseButtonEventArgs e)
	{
		RN_VScroll.ReleaseMouseCapture();
		parent.LeaveControling();
		isdraging = false;
	}

	private void RN_VScroll_MouseMove(object sender, MouseEventArgs e)
	{
		if (isdraging && e.LeftButton == MouseButtonState.Pressed)
		{
			endpoint = e.GetPosition(this);
			if (endpoint.Y < 0.0)
			{
				endpoint.Y = 0.0;
			}
			if (endpoint.Y > base.ActualHeight)
			{
				endpoint.Y = base.ActualHeight;
			}
			double num = endpoint.Y - startpoint.Y;
			if (Math.Abs(num) >= 1.0)
			{
				num /= base.ActualHeight;
				num = Math.Max(num, Offset + Length - 1.0);
				num = Math.Min(num, Offset);
				Offset -= num;
				startpoint = endpoint;
			}
		}
	}

	private void LN_VTop_MouseDown(object sender, MouseButtonEventArgs e)
	{
		LN_VTop.CaptureMouse();
		parent.EnterControling();
		isresizing = true;
		startpoint = e.GetPosition(this);
	}

	private void LN_VTop_MouseUp(object sender, MouseButtonEventArgs e)
	{
		LN_VTop.ReleaseMouseCapture();
		parent.LeaveControling();
		isresizing = false;
	}

	private void LN_VTop_MouseMove(object sender, MouseEventArgs e)
	{
		if (isresizing && e.LeftButton == MouseButtonState.Pressed)
		{
			endpoint = e.GetPosition(this);
			if (endpoint.Y < 0.0)
			{
				endpoint.Y = 0.0;
			}
			if (endpoint.Y > base.ActualHeight)
			{
				endpoint.Y = base.ActualHeight;
			}
			double num = endpoint.Y - startpoint.Y;
			if (Math.Abs(num) >= 1.0)
			{
				num /= base.ActualHeight;
				num = Math.Max(num, Offset + Length - 1.0);
				num = Math.Min(num, Length - LengthMininum);
				Length -= num;
				startpoint = endpoint;
			}
		}
	}

	private void LN_VBottom_MouseDown(object sender, MouseButtonEventArgs e)
	{
		LN_VBottom.CaptureMouse();
		parent.EnterControling();
		isresizing = true;
		startpoint = e.GetPosition(this);
	}

	private void LN_VBottom_MouseUp(object sender, MouseButtonEventArgs e)
	{
		LN_VBottom.ReleaseMouseCapture();
		parent.LeaveControling();
		isresizing = false;
	}

	private void LN_VBottom_MouseMove(object sender, MouseEventArgs e)
	{
		if (isresizing && e.LeftButton == MouseButtonState.Pressed)
		{
			endpoint = e.GetPosition(this);
			if (endpoint.Y < 0.0)
			{
				endpoint.Y = 0.0;
			}
			if (endpoint.Y > base.ActualHeight)
			{
				endpoint.Y = base.ActualHeight;
			}
			double num = endpoint.Y - startpoint.Y;
			if (Math.Abs(num) >= 1.0)
			{
				num /= base.ActualHeight;
				num = Math.Max(num, LengthMininum - Length);
				num = Math.Min(num, LengthMaxinum - Length);
				Offset -= num;
				Length += num;
				startpoint = endpoint;
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
			Uri resourceLocator = new Uri("/SamSoarII.Utility;component/controls/gridpenning/gridpenningverticalscroll.xaml", UriKind.Relative);
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
			CV_VRuler = (Canvas)target;
			break;
		case 2:
			CV_Scroll = (Canvas)target;
			break;
		case 3:
			RN_VScroll = (Rectangle)target;
			RN_VScroll.MouseDown += RN_VScroll_MouseDown;
			RN_VScroll.MouseUp += RN_VScroll_MouseUp;
			RN_VScroll.MouseMove += RN_VScroll_MouseMove;
			break;
		case 4:
			LN_VTop = (Line)target;
			LN_VTop.MouseDown += LN_VTop_MouseDown;
			LN_VTop.MouseUp += LN_VTop_MouseUp;
			LN_VTop.MouseMove += LN_VTop_MouseMove;
			break;
		case 5:
			LN_VBottom = (Line)target;
			LN_VBottom.MouseDown += LN_VBottom_MouseDown;
			LN_VBottom.MouseUp += LN_VBottom_MouseUp;
			LN_VBottom.MouseMove += LN_VBottom_MouseMove;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}

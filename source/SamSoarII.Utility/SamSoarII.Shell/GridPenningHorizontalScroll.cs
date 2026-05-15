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

public class GridPenningHorizontalScroll : UserControl, IComponentConnector
{
	public static readonly double OffsetMaxinum = 1.0;

	public static readonly double OffsetMininum = 0.0;

	public static readonly double LengthMaxinum = 1.0;

	public static readonly double LengthMininum = 0.01;

	private GridPenning parent;

	private TextBlock[] tb_hrulers;

	public static readonly DependencyProperty OffsetProperty = DependencyProperty.Register("Offset", typeof(double), typeof(GridPenningHorizontalScroll), new FrameworkPropertyMetadata(0.0, OnOffsetChanged));

	public static readonly DependencyProperty LengthProperty = DependencyProperty.Register("Length", typeof(double), typeof(GridPenningHorizontalScroll), new FrameworkPropertyMetadata(1.0, OnLengthChanged));

	private bool isdraging = false;

	private bool isresizing = false;

	private Point startpoint;

	private Point endpoint;

	internal Canvas CV_HRuler;

	internal Canvas CV_Scroll;

	internal Rectangle RN_HScroll;

	internal Line LN_HLeft;

	internal Line LN_HRight;

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

	public GridPenningHorizontalScroll(GridPenning _parent)
	{
		InitializeComponent();
		parent = _parent;
		tb_hrulers = new TextBlock[GridPenning.XBlockCapacity];
		for (int num = 0; num < tb_hrulers.Length; num++)
		{
			tb_hrulers[num] = new TextBlock();
			tb_hrulers[num].FontSize = 10.0;
			tb_hrulers[num].Foreground = Brushes.White;
			Canvas.SetTop(tb_hrulers[num], 16.0);
			Canvas.SetLeft(tb_hrulers[num], GridPenning.BlockDefaultWidth * (double)num);
			CV_HRuler.Children.Add(tb_hrulers[num]);
		}
		parent.PropertyChanged += Parent_OnPropertyChanged;
		base.Loaded += OnLoaded;
	}

	public void Dispose()
	{
		base.Loaded -= OnLoaded;
		parent.PropertyChanged -= Parent_OnPropertyChanged;
		CV_HRuler.Children.Clear();
		parent = null;
		tb_hrulers = null;
	}

	private static void OnOffsetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GridPenningHorizontalScroll)
		{
			GridPenningHorizontalScroll gridPenningHorizontalScroll = (GridPenningHorizontalScroll)d;
			gridPenningHorizontalScroll.OnOffsetChanged(e);
		}
	}

	protected virtual void OnOffsetChanged(DependencyPropertyChangedEventArgs e)
	{
		Update();
		this.OffsetChanged(this, e);
	}

	private static void OnLengthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GridPenningHorizontalScroll)
		{
			GridPenningHorizontalScroll gridPenningHorizontalScroll = (GridPenningHorizontalScroll)d;
			gridPenningHorizontalScroll.OnLengthChanged(e);
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
		UpdateHorizontalRuler();
		UpdateHorizontalScroll();
	}

	public void UpdateHorizontalRuler()
	{
		double num = parent.HorizontalOffset;
		for (int i = 0; i < tb_hrulers.Length; i++)
		{
			tb_hrulers[i].Text = ToHorizontalRulerString(DrawingSource.XStart + num);
			num += parent.BlockWidth;
		}
	}

	public void UpdateHorizontalScroll()
	{
		Canvas.SetLeft(LN_HLeft, Offset * base.ActualWidth);
		Canvas.SetLeft(RN_HScroll, Offset * base.ActualWidth);
		Canvas.SetLeft(LN_HRight, (Offset + Length) * base.ActualWidth);
		RN_HScroll.Width = Length * base.ActualWidth;
	}

	protected string ToHorizontalRulerString(double value)
	{
		if (DrawingSource is IGridPenningSourceEX)
		{
			IGridPenningSourceEX gridPenningSourceEX = (IGridPenningSourceEX)DrawingSource;
			return gridPenningSourceEX.GetXUnit(value);
		}
		int num = 0;
		string arg = DrawingSource.XUnit;
		while (DrawingSource.YValueBase != null && num < DrawingSource.YValueBase.Count)
		{
			double num2 = DrawingSource.YValueBase[num];
			if (value < num2)
			{
				break;
			}
			value /= num2;
			arg = DrawingSource.YUnitEx[num];
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
		UpdateHorizontalScroll();
	}

	private void RN_HScroll_MouseDown(object sender, MouseButtonEventArgs e)
	{
		RN_HScroll.CaptureMouse();
		parent.EnterControling();
		isdraging = true;
		startpoint = e.GetPosition(this);
	}

	private void RN_HScroll_MouseUp(object sender, MouseButtonEventArgs e)
	{
		RN_HScroll.ReleaseMouseCapture();
		parent.LeaveControling();
		isdraging = false;
	}

	private void RN_HScroll_MouseMove(object sender, MouseEventArgs e)
	{
		if (isdraging && e.LeftButton == MouseButtonState.Pressed)
		{
			endpoint = e.GetPosition(this);
			if (endpoint.X < 0.0)
			{
				endpoint.X = 0.0;
			}
			if (endpoint.X > base.ActualWidth)
			{
				endpoint.X = base.ActualWidth;
			}
			double num = endpoint.X - startpoint.X;
			if (Math.Abs(num) >= 1.0)
			{
				num /= base.ActualWidth;
				num = Math.Max(num, 0.0 - Offset);
				num = Math.Min(num, 1.0 - Offset - Length);
				Offset += num;
				startpoint = endpoint;
			}
		}
	}

	private void LN_HLeft_MouseDown(object sender, MouseButtonEventArgs e)
	{
		LN_HLeft.CaptureMouse();
		parent.EnterControling();
		isresizing = true;
		startpoint = e.GetPosition(this);
	}

	private void LN_HLeft_MouseUp(object sender, MouseButtonEventArgs e)
	{
		LN_HLeft.ReleaseMouseCapture();
		parent.LeaveControling();
		isresizing = false;
	}

	private void LN_HLeft_MouseMove(object sender, MouseEventArgs e)
	{
		if (isresizing && e.LeftButton == MouseButtonState.Pressed)
		{
			endpoint = e.GetPosition(this);
			if (endpoint.X < 0.0)
			{
				endpoint.X = 0.0;
			}
			if (endpoint.X > base.ActualWidth)
			{
				endpoint.X = base.ActualWidth;
			}
			double num = endpoint.X - startpoint.X;
			if (Math.Abs(num) >= 1.0)
			{
				num /= base.ActualWidth;
				num = Math.Max(num, Length - LengthMaxinum);
				num = Math.Min(num, Length - LengthMininum);
				Offset += num;
				Length -= num;
				startpoint = endpoint;
			}
		}
	}

	private void LN_HRight_MouseDown(object sender, MouseButtonEventArgs e)
	{
		LN_HRight.CaptureMouse();
		parent.EnterControling();
		isresizing = true;
		startpoint = e.GetPosition(this);
	}

	private void LN_HRight_MouseUp(object sender, MouseButtonEventArgs e)
	{
		LN_HRight.ReleaseMouseCapture();
		parent.LeaveControling();
		isresizing = false;
	}

	private void LN_HRight_MouseMove(object sender, MouseEventArgs e)
	{
		if (isresizing && e.LeftButton == MouseButtonState.Pressed)
		{
			endpoint = e.GetPosition(this);
			if (endpoint.X < 0.0)
			{
				endpoint.X = 0.0;
			}
			if (endpoint.X > base.ActualWidth)
			{
				endpoint.X = base.ActualWidth;
			}
			double num = endpoint.X - startpoint.X;
			if (Math.Abs(num) >= 1.0)
			{
				num /= base.ActualWidth;
				num = Math.Max(num, 0.0 - Length);
				num = Math.Min(num, 1.0 - Offset - Length);
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
			Uri resourceLocator = new Uri("/SamSoarII.Utility;component/controls/gridpenning/gridpenninghorizontalscroll.xaml", UriKind.Relative);
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
			CV_HRuler = (Canvas)target;
			break;
		case 2:
			CV_Scroll = (Canvas)target;
			break;
		case 3:
			RN_HScroll = (Rectangle)target;
			RN_HScroll.MouseDown += RN_HScroll_MouseDown;
			RN_HScroll.MouseUp += RN_HScroll_MouseUp;
			RN_HScroll.MouseMove += RN_HScroll_MouseMove;
			break;
		case 4:
			LN_HLeft = (Line)target;
			LN_HLeft.MouseDown += LN_HLeft_MouseDown;
			LN_HLeft.MouseUp += LN_HLeft_MouseUp;
			LN_HLeft.MouseMove += LN_HLeft_MouseMove;
			break;
		case 5:
			LN_HRight = (Line)target;
			LN_HRight.MouseDown += LN_HRight_MouseDown;
			LN_HRight.MouseUp += LN_HRight_MouseUp;
			LN_HRight.MouseMove += LN_HRight_MouseMove;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SamSoarII.Shell;

public class GridPenning : UserControl, INotifyPropertyChanged, IComponentConnector
{
	public enum AnimStatus
	{
		None,
		ZoomIn,
		ZoomOut
	}

	public static readonly int XBlockCapacity = 20;

	public static readonly int YBlockCapacity = 12;

	public static readonly double BlockDefaultWidth = 64.0;

	public static readonly double BlockDefaultHeight = 64.0;

	public static readonly double XScaleMaxinum = 1E+100;

	public static readonly double YScaleMaxinum = 1E+100;

	public static readonly double XScaleMininum = 1E-100;

	public static readonly double YScaleMininum = 1E-100;

	private bool isdisposed = false;

	public static readonly DependencyProperty DrawingSourceProperty = DependencyProperty.Register("DrawingSource", typeof(IGridPenningSource), typeof(GridPenning), new FrameworkPropertyMetadata(DefaultGridPenningSource.Default, OnDrawingSourceChanged));

	public static readonly DependencyProperty IsRenderAfterControlingProperty = DependencyProperty.Register("IsRenderAfterControling", typeof(bool), typeof(GridPenning), new FrameworkPropertyMetadata(true));

	public static readonly DependencyProperty IsLockingProperty = DependencyProperty.Register("IsLocking", typeof(bool), typeof(GridPenning), new FrameworkPropertyMetadata(false, OnIsLockingPropertyChanged));

	public static readonly DependencyProperty CanHintXProperty = DependencyProperty.Register("CanHintX", typeof(bool), typeof(GridPenning), new FrameworkPropertyMetadata(true, CanHintXPropertyChanged));

	public static readonly DependencyProperty CanHintYProperty = DependencyProperty.Register("CanHintY", typeof(bool), typeof(GridPenning), new FrameworkPropertyMetadata(false, CanHintYPropertyChanged));

	public static readonly DependencyProperty DrawingModeProperty = DependencyProperty.Register("DrawingMode", typeof(GridPenningDrawingMode), typeof(GridPenning), new FrameworkPropertyMetadata(GridPenningDrawingMode.WPFDevice, DrawingModePropertyChanged));

	public static readonly DependencyProperty IsArrowVisibleProperty = DependencyProperty.Register("IsArrowVisible", typeof(bool), typeof(GridPenning), new FrameworkPropertyMetadata(false, OnPropertyChange_IsArrowVisible));

	public static readonly DependencyProperty IsActualScaleProperty = DependencyProperty.Register("IsActualScale", typeof(bool), typeof(GridPenning), new FrameworkPropertyMetadata(false, OnPropertyChange_IsActualScale));

	private GridPenningPanel panel;

	private GridPenningHorizontalScroll hscroll;

	private GridPenningVerticalScroll vscroll;

	private GridPenningOutlinePanel outline;

	private GridPenningHint[] hints;

	private bool iscontroling;

	private bool ishintx;

	private bool ishinty;

	private double hintx;

	private double hinty;

	private Timer rendertimer;

	private bool rendernotified = false;

	private DispatcherTimer animtimer;

	private AnimStatus animstate;

	private int animtick;

	private double zoomincx;

	private double zoomincy;

	private SortedList<double, GridPenningHint> yhints = new SortedList<double, GridPenningHint>();

	internal Border BD_Panel;

	internal Border BD_Outline;

	internal Border BD_VRuler;

	internal Border BD_HRuler;

	internal Canvas CV_Panel;

	internal Line LN_ScanX;

	internal Line LN_ScanY;

	internal GridPenningZoomInAnimControl UI_Anim_ZoomIn;

	internal GridPenningZoomOutAnimControl UI_Anim_ZoomOut;

	private bool _contentLoaded;

	public bool IsDisposed => isdisposed;

	public IGridPenningSource DrawingSource
	{
		get
		{
			return (IGridPenningSource)GetValue(DrawingSourceProperty);
		}
		set
		{
			DependencyProperty drawingSourceProperty = DrawingSourceProperty;
			IGridPenningSource value2;
			if (value != null)
			{
				value2 = value;
			}
			else
			{
				IGridPenningSource gridPenningSource = DefaultGridPenningSource.Default;
				value2 = gridPenningSource;
			}
			SetValue(drawingSourceProperty, value2);
		}
	}

	public bool IsRenderAfterControling
	{
		get
		{
			return (bool)GetValue(IsRenderAfterControlingProperty);
		}
		set
		{
			SetValue(IsRenderAfterControlingProperty, value);
		}
	}

	public bool IsLocking
	{
		get
		{
			return (bool)GetValue(IsLockingProperty);
		}
		set
		{
			SetValue(IsLockingProperty, value);
		}
	}

	public bool CanHintX
	{
		get
		{
			return (bool)GetValue(CanHintXProperty);
		}
		set
		{
			SetValue(CanHintXProperty, value);
		}
	}

	public bool CanHintY
	{
		get
		{
			return (bool)GetValue(CanHintYProperty);
		}
		set
		{
			SetValue(CanHintYProperty, value);
		}
	}

	public GridPenningDrawingMode DrawingMode
	{
		get
		{
			return (GridPenningDrawingMode)GetValue(DrawingModeProperty);
		}
		set
		{
			SetValue(DrawingModeProperty, value);
		}
	}

	public bool IsArrowVisible
	{
		get
		{
			return (bool)GetValue(IsArrowVisibleProperty);
		}
		set
		{
			SetValue(IsArrowVisibleProperty, value);
		}
	}

	public bool IsActualScale
	{
		get
		{
			return (bool)GetValue(IsActualScaleProperty);
		}
		set
		{
			SetValue(IsActualScaleProperty, value);
		}
	}

	public double XScale => hscroll.Length;

	public double YScale => vscroll.Length;

	public double BlockWidth => BlockDefaultWidth * ExtendWidth / ExtendActualWidth;

	public double BlockHeight => BlockDefaultHeight * ExtendHeight / ExtendActualHeight;

	public double BlockActualWidth => BlockDefaultWidth;

	public double BlockActualHeight => BlockDefaultHeight;

	public double ExtendWidth => DrawingSource.XLength;

	public double ExtendHeight => DrawingSource.YLength;

	public double ExtendActualWidth => panel.ActualWidth / hscroll.Length;

	public double ExtendActualHeight => panel.ActualHeight / vscroll.Length;

	public double ViewportWidth => ExtendWidth * hscroll.Length;

	public double ViewportHeight => ExtendHeight * vscroll.Length;

	public double ViewportActualWidth => panel.ActualWidth;

	public double ViewportActualHeight => panel.ActualHeight;

	public double HorizontalOffset => ExtendWidth * hscroll.Offset;

	public double VerticalOffset => ExtendHeight * vscroll.Offset;

	public double HorizontalActualOffset => ExtendActualWidth * hscroll.Offset;

	public double VerticalActualOffset => ExtendActualWidth * hscroll.Offset;

	public int BlockXCount => (int)(BlockWidth / ExtendWidth) + 1;

	public int BlockYCount => (int)(BlockHeight / ExtendHeight) + 1;

	public int BlockXIndex => (int)(hscroll.Offset / BlockWidth);

	public int BlockYIndex => (int)(vscroll.Offset / BlockHeight);

	public double BlockXOffset => hscroll.Offset - (double)BlockXIndex * BlockWidth;

	public double BlockYOffset => vscroll.Offset - (double)BlockYIndex * BlockHeight;

	public bool IsControling => iscontroling;

	public bool IsHintX
	{
		get
		{
			return ishintx;
		}
		protected set
		{
			ishintx = value;
			InvokePropertyChanged("IsHintX");
		}
	}

	public bool IsHintY
	{
		get
		{
			return ishinty;
		}
		protected set
		{
			ishinty = value;
			InvokePropertyChanged("IsHintX");
		}
	}

	public double HintX
	{
		get
		{
			return hintx;
		}
		protected set
		{
			hintx = value;
			InvokePropertyChanged("HintX");
		}
	}

	public double HintY
	{
		get
		{
			return hinty;
		}
		protected set
		{
			hinty = value;
			InvokePropertyChanged("HintY");
		}
	}

	public event PropertyChangedEventHandler PropertyChanged = delegate
	{
	};

	public event DependencyPropertyChangedEventHandler DrawingSourceChanged;

	public event DependencyPropertyChangedEventHandler IsRenderAfterControlingChanged;

	public event DependencyPropertyChangedEventHandler IsLockingPropertyChanged;

	public event DependencyPropertyChangedEventHandler IsArrowVisibleChanged;

	public event DependencyPropertyChangedEventHandler IsActualScaleChanged;

	public event RoutedEventHandler EnterOutline;

	public event RoutedEventHandler LeaveOutline;

	public event GridPenningAnimEventHandler AnimStart;

	public event GridPenningAnimEventHandler AnimEnd;

	public GridPenning()
	{
		InitializeComponent();
		panel = new GridPenningPanel(this);
		hscroll = new GridPenningHorizontalScroll(this);
		vscroll = new GridPenningVerticalScroll(this);
		outline = new GridPenningOutlinePanel(this);
		hints = new GridPenningHint[8];
		animstate = AnimStatus.None;
		animtimer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 10), DispatcherPriority.Normal, OnAnimTimer, base.Dispatcher);
		for (int num = 0; num < hints.Length; num++)
		{
			hints[num] = new GridPenningHint(this);
			CV_Panel.Children.Add(hints[num]);
		}
		BD_Panel.Child = panel;
		BD_HRuler.Child = hscroll;
		BD_VRuler.Child = vscroll;
		BD_Outline.Child = outline;
		hscroll.OffsetChanged += HorizontalScroll_OnOffsetChanged;
		hscroll.LengthChanged += HorizontalScroll_OnLengthChanged;
		vscroll.OffsetChanged += VerticalScroll_OnOffsetChanged;
		vscroll.LengthChanged += VerticalScroll_OnLengthChanged;
		rendertimer = new Timer(_TimerRender, null, 0, 1000);
	}

	public void Dispose()
	{
		if (!isdisposed)
		{
			isdisposed = true;
			hscroll.OffsetChanged -= HorizontalScroll_OnOffsetChanged;
			hscroll.LengthChanged -= HorizontalScroll_OnLengthChanged;
			vscroll.OffsetChanged -= VerticalScroll_OnOffsetChanged;
			vscroll.LengthChanged -= VerticalScroll_OnLengthChanged;
			BD_Panel.Child = null;
			BD_HRuler.Child = null;
			BD_VRuler.Child = null;
			hscroll?.Dispose();
			vscroll?.Dispose();
			panel = null;
			hscroll = null;
			vscroll = null;
		}
	}

	protected virtual void InvokePropertyChanged(string propertyname)
	{
		this.PropertyChanged(this, new PropertyChangedEventArgs(propertyname));
	}

	private static void OnDrawingSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GridPenning)
		{
			GridPenning gridPenning = (GridPenning)d;
			gridPenning.OnDrawingSourceChanged(e);
		}
	}

	protected virtual void OnDrawingSourceChanged(DependencyPropertyChangedEventArgs e)
	{
		if (e.OldValue is IGridPenningSource)
		{
			IGridPenningSource gridPenningSource = (IGridPenningSource)e.OldValue;
			gridPenningSource.PropertyChanged -= OnDrawingSourcePropertyChanged;
			if (e.OldValue is IGridPenningSourceEX)
			{
				IGridPenningSourceEX gridPenningSourceEX = (IGridPenningSourceEX)e.OldValue;
				gridPenningSourceEX.EntityChanged -= OnDrawingSourceEntityChanged;
				gridPenningSourceEX.SizeChanged -= OnDrawingSourceSizeChanged;
			}
		}
		if (e.NewValue is IGridPenningSource)
		{
			IGridPenningSource gridPenningSource2 = (IGridPenningSource)e.NewValue;
			gridPenningSource2.PropertyChanged += OnDrawingSourcePropertyChanged;
			if (e.NewValue is IGridPenningSourceEX)
			{
				IGridPenningSourceEX gridPenningSourceEX2 = (IGridPenningSourceEX)e.NewValue;
				gridPenningSourceEX2.EntityChanged += OnDrawingSourceEntityChanged;
				gridPenningSourceEX2.SizeChanged += OnDrawingSourceSizeChanged;
			}
		}
		UpdateScan();
		this.DrawingSourceChanged?.Invoke(this, e);
		outline?.DrawingAll();
		InvokePropertyChanged("DrawingSource");
		InvokePropertyChanged("ExtendWidth");
		InvokePropertyChanged("ExtendHeight");
		InvokePropertyChanged("HorizontalOffset");
		InvokePropertyChanged("VerticalOffset");
	}

	private static void OnIsRenderAfterControlingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GridPenning)
		{
			GridPenning gridPenning = (GridPenning)d;
			gridPenning.OnIsRenderAfterControlingChanged(e);
		}
	}

	protected virtual void OnIsRenderAfterControlingChanged(DependencyPropertyChangedEventArgs e)
	{
		this.IsRenderAfterControlingChanged?.Invoke(this, e);
		InvokePropertyChanged("IsRenderAfterControling");
	}

	private static void OnIsLockingPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GridPenning)
		{
			GridPenning gridPenning = (GridPenning)d;
			gridPenning.OnIsLockingPropertyChanged(e);
		}
	}

	protected virtual void OnIsLockingPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		this.IsLockingPropertyChanged?.Invoke(this, e);
		InvokePropertyChanged("IsLocking");
	}

	private static void CanHintXPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GridPenning)
		{
			GridPenning gridPenning = (GridPenning)d;
			gridPenning.OnCanHintXPropertyChanged(e);
		}
	}

	protected virtual void OnCanHintXPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void CanHintYPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GridPenning)
		{
			GridPenning gridPenning = (GridPenning)d;
			gridPenning.OnCanHintYPropertyChanged(e);
		}
	}

	protected virtual void OnCanHintYPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void DrawingModePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GridPenning)
		{
			GridPenning gridPenning = (GridPenning)d;
			gridPenning.OnDrawingModePropertyChanged(e);
		}
	}

	protected virtual void OnDrawingModePropertyChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChange_IsArrowVisible(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GridPenning)
		{
			GridPenning gridPenning = (GridPenning)d;
			gridPenning.OnIsArrowVisibleChanged(e);
		}
	}

	protected virtual void OnIsArrowVisibleChanged(DependencyPropertyChangedEventArgs e)
	{
		this.IsArrowVisibleChanged?.Invoke(this, e);
		InvokePropertyChanged("IsArrowVisible");
	}

	private static void OnPropertyChange_IsActualScale(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GridPenning)
		{
			GridPenning gridPenning = (GridPenning)d;
			gridPenning.OnIsActualScaleChanged(e);
		}
	}

	protected virtual void OnIsActualScaleChanged(DependencyPropertyChangedEventArgs e)
	{
		this.IsActualScaleChanged?.Invoke(this, e);
		InvokePropertyChanged("IsActualScale");
	}

	public void ZoomIn(double cx = 0.5, double cy = 0.5)
	{
		double num = 0.0;
		double num2 = 0.0;
		num = hscroll.Length;
		num2 = hscroll.Offset;
		cx = num2 + num * cx;
		num2 += num / 2.0;
		num *= 0.9;
		num = Math.Max(num, 0.001);
		num2 = ((cx > 0.0) ? cx : num2) - num / 2.0;
		num2 = Math.Min(Math.Max(num2, 0.0), 1.0 - num);
		hscroll.Length = num;
		hscroll.Offset = num2;
		num = vscroll.Length;
		num2 = vscroll.Offset;
		cy = num2 + num * cy;
		num2 += num / 2.0;
		num *= 0.9;
		num = Math.Max(num, 0.001);
		num2 = ((cy > 0.0) ? cy : num2) - num / 2.0;
		num2 = Math.Min(Math.Max(num2, 0.0), 1.0 - num);
		vscroll.Length = num;
		vscroll.Offset = num2;
		panel.DrawingAll();
		outline.DrawingAll();
	}

	public void ZoomOut()
	{
		double num = 0.0;
		double num2 = 0.0;
		num = hscroll.Length;
		num2 = hscroll.Offset;
		num2 += num / 2.0;
		num /= 0.9;
		num = Math.Min(num, 1.0);
		num2 -= num / 2.0;
		num2 = Math.Min(Math.Max(num2, 0.0), 1.0 - num);
		hscroll.Length = num;
		hscroll.Offset = num2;
		num = vscroll.Length;
		num2 = vscroll.Offset;
		num2 += num / 2.0;
		num /= 0.9;
		num = Math.Min(num, 1.0);
		num2 -= num / 2.0;
		num2 = Math.Min(Math.Max(num2, 0.0), 1.0 - num);
		vscroll.Length = num;
		vscroll.Offset = num2;
		panel.DrawingAll();
		outline.DrawingAll();
	}

	public void MoveLeft()
	{
		double num = 0.0;
		double num2 = 0.0;
		num = hscroll.Length;
		num2 = hscroll.Offset;
		num2 -= num * 0.25;
		num2 = Math.Min(Math.Max(num2, 0.0), 1.0 - num);
		hscroll.Offset = num2;
		panel.DrawingAll();
		outline.DrawingAll();
	}

	public void MoveRight()
	{
		double num = 0.0;
		double num2 = 0.0;
		num = hscroll.Length;
		num2 = hscroll.Offset;
		num2 += num * 0.25;
		num2 = Math.Min(Math.Max(num2, 0.0), 1.0 - num);
		hscroll.Offset = num2;
		panel.DrawingAll();
		outline.DrawingAll();
	}

	public void MoveUp()
	{
		double num = 0.0;
		double num2 = 0.0;
		num = vscroll.Length;
		num2 = vscroll.Offset;
		num2 += num * 0.25;
		num2 = Math.Min(Math.Max(num2, 0.0), 1.0 - num);
		vscroll.Offset = num2;
		panel.DrawingAll();
		outline.DrawingAll();
	}

	public void MoveDown()
	{
		double num = 0.0;
		double num2 = 0.0;
		num = vscroll.Length;
		num2 = vscroll.Offset;
		num2 -= num * 0.25;
		num2 = Math.Min(Math.Max(num2, 0.0), 1.0 - num);
		vscroll.Offset = num2;
		panel.DrawingAll();
		outline.DrawingAll();
	}

	public void KeepActualScale(bool xstay = false, bool ystay = false)
	{
		if (ViewportActualWidth < 1.0 || ViewportActualHeight < 1.0)
		{
			return;
		}
		double num = ViewportWidth / ViewportActualWidth;
		double num2 = ViewportHeight / ViewportActualHeight;
		if (Math.Abs(num / num2 - 1.0) < 0.0001)
		{
			return;
		}
		double num3 = num2 * ViewportActualWidth / ExtendWidth;
		double num4 = num * ViewportActualHeight / ExtendHeight;
		if (!double.IsNaN(num3) && !double.IsNaN(num4))
		{
			if (xstay && num4 <= 1.0)
			{
				vscroll.Length = num4;
			}
			else if (ystay && num3 <= 1.0)
			{
				hscroll.Length = num3;
			}
			else if (num3 <= 1.0 && num3 > hscroll.Length)
			{
				hscroll.Length = num3;
			}
			else if (num4 <= 1.0 && num4 > vscroll.Length)
			{
				vscroll.Length = num4;
			}
			else if (num3 <= 1.0)
			{
				hscroll.Length = num3;
			}
			else if (num4 <= 1.0)
			{
				vscroll.Length = num4;
			}
		}
	}

	public void ZoomInWithAnim(double cx = 0.5, double cy = 0.5)
	{
		if (animstate == AnimStatus.None)
		{
			UI_Anim_ZoomIn.Rect = new Rect(cx - 0.45, 1.0 - cy - 0.45, 0.9, 0.9);
			UI_Anim_ZoomIn.Tick = 0;
			UI_Anim_ZoomIn.Visibility = Visibility.Visible;
			animtick = 0;
			zoomincx = cx;
			zoomincy = cy;
			animstate = AnimStatus.ZoomIn;
			this.AnimStart?.Invoke(this, new GridPenningAnimEventArgs(animstate));
		}
	}

	public void ZoomOutWithAnim()
	{
		if (animstate == AnimStatus.None)
		{
			double num = 0.0;
			double num2 = 0.0;
			num = hscroll.Length;
			num2 = hscroll.Offset;
			num2 += num / 2.0;
			num /= 0.9;
			num = Math.Min(num, 1.0);
			num2 -= num / 2.0;
			num2 = Math.Min(Math.Max(num2, 0.0), 1.0 - num);
			double x = (num2 - hscroll.Offset) / num;
			double width = hscroll.Length / num;
			num = vscroll.Length;
			num2 = vscroll.Offset;
			num2 += num / 2.0;
			num /= 0.9;
			num = Math.Min(num, 1.0);
			num2 -= num / 2.0;
			num2 = Math.Min(Math.Max(num2, 0.0), 1.0 - num);
			double y = (num2 - vscroll.Offset) / num;
			double height = vscroll.Length / num;
			UI_Anim_ZoomOut.Rect = new Rect(x, y, width, height);
			UI_Anim_ZoomOut.Tick = 0;
			UI_Anim_ZoomOut.Visibility = Visibility.Visible;
			animtick = 0;
			animstate = AnimStatus.ZoomOut;
			this.AnimStart?.Invoke(this, new GridPenningAnimEventArgs(animstate));
		}
	}

	public void EnterControling()
	{
		if (!iscontroling && IsRenderAfterControling)
		{
			iscontroling = true;
			this.EnterOutline?.Invoke(this, new RoutedEventArgs());
			outline.Show();
		}
	}

	public void LeaveControling()
	{
		if (iscontroling)
		{
			iscontroling = false;
			this.LeaveOutline?.Invoke(this, new RoutedEventArgs());
			outline.Hide();
			panel.DrawingAll();
		}
	}

	public void UpdateScan()
	{
		UpdateScanX();
		UpdateScanY();
	}

	public void UpdateScanX()
	{
		if (!(DrawingSource is IGridPenningSourceEX))
		{
			LN_ScanX.Visibility = Visibility.Hidden;
			return;
		}
		IGridPenningSourceEX gridPenningSourceEX = (IGridPenningSourceEX)DrawingSource;
		if (!gridPenningSourceEX.IsScanXEnabled || gridPenningSourceEX.ScanX < HorizontalOffset || gridPenningSourceEX.ScanX > HorizontalOffset + ViewportWidth)
		{
			LN_ScanX.Visibility = Visibility.Hidden;
			return;
		}
		LN_ScanX.Visibility = Visibility.Visible;
		Canvas.SetLeft(LN_ScanX, CV_Panel.ActualWidth * (gridPenningSourceEX.ScanX - HorizontalOffset) / ViewportWidth);
	}

	public void UpdateScanY()
	{
		if (!(DrawingSource is IGridPenningSourceEX))
		{
			LN_ScanY.Visibility = Visibility.Hidden;
			return;
		}
		IGridPenningSourceEX gridPenningSourceEX = (IGridPenningSourceEX)DrawingSource;
		if (!gridPenningSourceEX.IsScanYEnabled || gridPenningSourceEX.ScanY < VerticalOffset || gridPenningSourceEX.ScanY > VerticalOffset + ViewportHeight)
		{
			LN_ScanY.Visibility = Visibility.Hidden;
			return;
		}
		LN_ScanY.Visibility = Visibility.Visible;
		Canvas.SetLeft(LN_ScanY, CV_Panel.ActualHeight * (gridPenningSourceEX.ScanY - VerticalOffset) / ViewportHeight);
	}

	private void _TimerRender(object arg)
	{
		if (rendernotified)
		{
			base.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate
			{
				panel.DrawingAll();
				outline.DrawingAll();
				hscroll.Update();
				vscroll.Update();
			});
		}
		rendernotified = false;
	}

	private void OnAnimTimer(object sender, EventArgs e)
	{
		switch (animstate)
		{
		case AnimStatus.ZoomIn:
			UI_Anim_ZoomIn.Tick = animtick++;
			if (animtick >= GridPenningZoomInAnimControl.TickMax)
			{
				animstate = AnimStatus.None;
				animtick = 0;
				UI_Anim_ZoomIn.Visibility = Visibility.Hidden;
				ZoomIn(zoomincx, zoomincy);
				this.AnimEnd?.Invoke(this, new GridPenningAnimEventArgs(animstate));
			}
			break;
		case AnimStatus.ZoomOut:
			UI_Anim_ZoomOut.Tick = animtick++;
			if (animtick >= GridPenningZoomInAnimControl.TickMax)
			{
				animstate = AnimStatus.None;
				animtick = 0;
				UI_Anim_ZoomOut.Visibility = Visibility.Hidden;
				ZoomOut();
				this.AnimEnd?.Invoke(this, new GridPenningAnimEventArgs(animstate));
			}
			break;
		}
	}

	protected void UpdateHintLayout(Point mouse)
	{
		Rect drawingRectAll = panel.GetDrawingRectAll();
		Rect rect = default(Rect);
		GridPenningHint gridPenningHint = null;
		yhints.Clear();
		GridPenningHint[] array = hints;
		foreach (GridPenningHint gridPenningHint2 in array)
		{
			if (gridPenningHint2.Core == null)
			{
				break;
			}
			double num = 0.0;
			if (gridPenningHint2.CoreXY != null)
			{
				double y = 0.0;
				if (!gridPenningHint2.CoreXY.GetY(hintx, ref y, 0))
				{
					continue;
				}
				num = (y - drawingRectAll.Y) * panel.ActualHeight / ViewportHeight;
			}
			if (gridPenningHint2.CoreBL != null)
			{
				bool y2 = false;
				if (!gridPenningHint2.CoreBL.GetY(hintx, ref y2, 0))
				{
					continue;
				}
				int num2 = gridPenningHint2.CoreBL.RulerID * (GridPenningPanel.OneBoolHeight + GridPenningPanel.OneBoolMargin);
				num = (y2 ? (num2 + GridPenningPanel.OneBoolHeight) : num2);
			}
			for (num = panel.ActualHeight - num; yhints.ContainsKey(num); num += 1.0 / 64.0)
			{
			}
			yhints.Add(num, gridPenningHint2);
		}
		foreach (KeyValuePair<double, GridPenningHint> yhint in yhints)
		{
			GridPenningHint value = yhint.Value;
			rect = value.ActualRect;
			rect.X = ((mouse.X < value.ActualWidth) ? mouse.X : (mouse.X - value.ActualWidth));
			rect.Y = Math.Max(yhint.Key, gridPenningHint?.ActualRect.Bottom ?? 0.0);
			value.SetPosition(rect.X, rect.Y);
			gridPenningHint = value;
		}
		gridPenningHint = null;
		foreach (KeyValuePair<double, GridPenningHint> item in yhints.Reverse())
		{
			GridPenningHint value2 = item.Value;
			rect = value2.ActualRect;
			rect.Y = Math.Min(rect.Y, (gridPenningHint != null) ? (gridPenningHint.ActualRect.Top - rect.Height) : (panel.ActualHeight - rect.Height));
			value2.SetPosition(rect.X, rect.Y);
			gridPenningHint = value2;
		}
	}

	public void DrawingAll()
	{
		hscroll.Update();
		vscroll.Update();
		panel.DrawingAll();
		outline.DrawingAll();
	}

	private void OnDrawingSourceSizeChanged(IGridPenningSource sender, IGridPenningSizeChangedEventArgs e)
	{
		if (iscontroling)
		{
			return;
		}
		bool flag = false;
		if (!IsLocking)
		{
			hscroll.Length = 1.0 - hscroll.Offset;
			vscroll.Offset = 0.0;
			vscroll.Length = 1.0;
			flag = true;
		}
		else
		{
			Rect rect = new Rect(e.OldSize.Left + e.OldSize.Width * hscroll.Offset, e.OldSize.Top + e.OldSize.Height * vscroll.Offset, e.OldSize.Width * hscroll.Length, e.OldSize.Height * vscroll.Length);
			if (rect.Left < e.NewSize.Left)
			{
				rect.X += e.NewSize.Left - rect.Left;
				flag = true;
			}
			if (rect.Top < e.NewSize.Top)
			{
				rect.Y += e.NewSize.Top - rect.Top;
				flag = true;
			}
			if (rect.Right > e.NewSize.Right)
			{
				rect.Width += e.NewSize.Right - rect.Right;
				flag = true;
			}
			if (rect.Bottom > e.NewSize.Bottom)
			{
				rect.Height += e.NewSize.Bottom - rect.Bottom;
				flag = true;
			}
			hscroll.Offset = (rect.Left - e.NewSize.Left) / e.NewSize.Width;
			hscroll.Length = rect.Width / e.NewSize.Width;
			vscroll.Offset = (rect.Top - e.NewSize.Top) / e.NewSize.Height;
			vscroll.Length = rect.Height / e.NewSize.Height;
		}
		if (flag)
		{
			hscroll.Update();
			vscroll.Update();
			panel.DrawingAll();
		}
		if (IsActualScale)
		{
			KeepActualScale();
		}
		outline.DrawingAll();
	}

	private void OnDrawingSourceEntityChanged(IGridPenningSource sender, IGridPenningEntityChangedEventArgs e)
	{
		if (!iscontroling)
		{
			switch (e.Action)
			{
			case GridPenningEntityChangedAction.Reset:
				panel.DrawingAll();
				outline.DrawingAll();
				hscroll.Update();
				vscroll.Update();
				break;
			case GridPenningEntityChangedAction.Add:
				panel.DrawingCover(e.ChangedZone);
				outline.DrawingCover(e.ChangedZone);
				break;
			case GridPenningEntityChangedAction.Remove:
				panel.Drawing(e.ChangedZone);
				break;
			}
		}
	}

	private void OnDrawingSourcePropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		switch (e.PropertyName)
		{
		case "IsScanXEnabled":
		case "ScanX":
			UpdateScanX();
			break;
		case "IsScanYEnabled":
		case "ScanY":
			UpdateScanY();
			break;
		}
	}

	protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
	{
		base.OnRenderSizeChanged(sizeInfo);
		rendernotified = true;
		if (IsActualScale)
		{
			KeepActualScale();
		}
	}

	private void HorizontalScroll_OnOffsetChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		if (iscontroling)
		{
			outline.Update();
		}
	}

	private void HorizontalScroll_OnLengthChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		if (IsActualScale)
		{
			KeepActualScale(xstay: true);
		}
		if (iscontroling)
		{
			outline.Update();
		}
	}

	private void VerticalScroll_OnOffsetChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		if (iscontroling)
		{
			outline.Update();
		}
	}

	private void VerticalScroll_OnLengthChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		if (IsActualScale)
		{
			KeepActualScale(xstay: false, ystay: true);
		}
		if (iscontroling)
		{
			outline.Update();
		}
	}

	private void CV_Panel_MouseEnter(object sender, MouseEventArgs e)
	{
		if (iscontroling)
		{
			return;
		}
		if (CanHintX)
		{
			IsHintX = true;
		}
		if (CanHintY)
		{
			IsHintY = true;
		}
		if (IsHintX || IsHintY)
		{
			Rect drawingRectAll = panel.GetDrawingRectAll();
			IEnumerable<IGridPenningEntity> entities = DrawingSource.GetEntities(drawingRectAll);
			IGridPenningEntity[] array = entities.Where((IGridPenningEntity et) => et is IGridPenningFunctionXY || et is IGridPenningBoolRuler).ToArray();
			for (int num = 0; num < Math.Min(array.Length, hints.Length); num++)
			{
				hints[num].Core = array[num];
			}
			Point position = e.GetPosition(panel);
			if (IsHintX)
			{
				HintX = DrawingSource.XStart + HorizontalOffset + position.X * ViewportWidth / panel.ActualWidth;
			}
			if (IsHintY)
			{
				HintY = DrawingSource.YStart + VerticalOffset + position.Y * ViewportHeight / panel.ActualHeight;
			}
			UpdateHintLayout(position);
		}
	}

	private void CV_Panel_MouseLeave(object sender, MouseEventArgs e)
	{
		if (IsHintX)
		{
			IsHintX = false;
		}
		if (IsHintY)
		{
			IsHintY = false;
		}
		GridPenningHint[] array = hints;
		foreach (GridPenningHint gridPenningHint in array)
		{
			gridPenningHint.Core = null;
		}
	}

	private void CV_Panel_MouseMove(object sender, MouseEventArgs e)
	{
		if (IsHintX || IsHintY)
		{
			Point position = e.GetPosition(panel);
			if (IsHintX)
			{
				HintX = DrawingSource.XStart + HorizontalOffset + position.X * ViewportWidth / panel.ActualWidth;
			}
			if (IsHintY)
			{
				HintY = DrawingSource.YStart + VerticalOffset + position.Y * ViewportHeight / panel.ActualHeight;
			}
			UpdateHintLayout(position);
		}
	}

	private void CV_Panel_MouseWheel(object sender, MouseWheelEventArgs e)
	{
		if (!iscontroling)
		{
			Point position = e.GetPosition(panel);
			double cx = position.X / panel.ActualWidth;
			double cy = position.Y / panel.ActualHeight;
			if ((double)e.Delta > 0.0)
			{
				ZoomIn(cx, cy);
			}
			else
			{
				ZoomOut();
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
			Uri resourceLocator = new Uri("/SamSoarII.Utility;component/controls/gridpenning/gridpenning.xaml", UriKind.Relative);
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
			BD_Panel = (Border)target;
			break;
		case 2:
			BD_Outline = (Border)target;
			break;
		case 3:
			BD_VRuler = (Border)target;
			break;
		case 4:
			BD_HRuler = (Border)target;
			break;
		case 5:
			CV_Panel = (Canvas)target;
			CV_Panel.MouseEnter += CV_Panel_MouseEnter;
			CV_Panel.MouseLeave += CV_Panel_MouseLeave;
			CV_Panel.MouseMove += CV_Panel_MouseMove;
			CV_Panel.MouseWheel += CV_Panel_MouseWheel;
			break;
		case 6:
			LN_ScanX = (Line)target;
			break;
		case 7:
			LN_ScanY = (Line)target;
			break;
		case 8:
			UI_Anim_ZoomIn = (GridPenningZoomInAnimControl)target;
			break;
		case 9:
			UI_Anim_ZoomOut = (GridPenningZoomOutAnimControl)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}

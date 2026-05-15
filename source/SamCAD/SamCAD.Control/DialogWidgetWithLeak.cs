using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SamCAD.Control;

public class DialogWidgetWithLeak : UserControl, IComponentConnector
{
	protected static readonly DependencyProperty LeakContentProperty = DependencyProperty.Register("LeakContent", typeof(FrameworkElement), typeof(DialogWidgetWithLeak), new PropertyMetadata(null, OnPropertyChanged_LeakContent));

	protected static readonly DependencyProperty LeakTopProperty = DependencyProperty.Register("LeakTop", typeof(double), typeof(DialogWidgetWithLeak), new PropertyMetadata(0.0, OnPropertyChanged_LeakTop));

	protected static readonly DependencyProperty LeakLeftProperty = DependencyProperty.Register("LeakLeft", typeof(double), typeof(DialogWidgetWithLeak), new PropertyMetadata(0.0, OnPropertyChanged_LeakLeft));

	protected static readonly DependencyProperty LeakWidthProperty = DependencyProperty.Register("LeakWidth", typeof(double), typeof(DialogWidgetWithLeak), new PropertyMetadata(0.0, OnPropertyChanged_LeakWidth));

	protected static readonly DependencyProperty LeakHeightProperty = DependencyProperty.Register("LeakHeight", typeof(double), typeof(DialogWidgetWithLeak), new PropertyMetadata(0.0, OnPropertyChanged_LeakHeight));

	private Border ui_border;

	private Grid ui_gridmain;

	private bool _contentLoaded;

	public FrameworkElement LeakContent
	{
		get
		{
			return (FrameworkElement)GetValue(LeakContentProperty);
		}
		set
		{
			SetValue(LeakContentProperty, value);
		}
	}

	public double LeakTop
	{
		get
		{
			return (double)GetValue(LeakTopProperty);
		}
		set
		{
			SetValue(LeakTopProperty, value);
		}
	}

	public double LeakLeft
	{
		get
		{
			return (double)GetValue(LeakLeftProperty);
		}
		set
		{
			SetValue(LeakLeftProperty, value);
		}
	}

	public double LeakWidth
	{
		get
		{
			return (double)GetValue(LeakWidthProperty);
		}
		set
		{
			SetValue(LeakWidthProperty, value);
		}
	}

	public double LeakHeight
	{
		get
		{
			return (double)GetValue(LeakHeightProperty);
		}
		set
		{
			SetValue(LeakHeightProperty, value);
		}
	}

	public DialogWidgetWithLeak()
	{
		InitializeComponent();
	}

	private static void OnPropertyChanged_LeakContent(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is DialogWidgetWithLeak)
		{
			((DialogWidgetWithLeak)d).OnLeakContentChanged(e);
		}
	}

	protected virtual void OnLeakContentChanged(DependencyPropertyChangedEventArgs e)
	{
		if (e.OldValue is FrameworkElement)
		{
			FrameworkElement frameworkElement = (FrameworkElement)e.OldValue;
			frameworkElement.SizeChanged -= OnLeakContentSizeChanged;
		}
		if (e.NewValue is FrameworkElement)
		{
			FrameworkElement frameworkElement2 = (FrameworkElement)e.NewValue;
			frameworkElement2.SizeChanged += OnLeakContentSizeChanged;
		}
		UpdateSize_Leak();
		UpdateSize_Border();
	}

	private static void OnPropertyChanged_LeakTop(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is DialogWidgetWithLeak)
		{
			((DialogWidgetWithLeak)d).OnLeakTopChanged(e);
		}
	}

	protected virtual void OnLeakTopChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_LeakLeft(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is DialogWidgetWithLeak)
		{
			((DialogWidgetWithLeak)d).OnLeakLeftChanged(e);
		}
	}

	protected virtual void OnLeakLeftChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_LeakWidth(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is DialogWidgetWithLeak)
		{
			((DialogWidgetWithLeak)d).OnLeakWidthChanged(e);
		}
	}

	protected virtual void OnLeakWidthChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_LeakHeight(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is DialogWidgetWithLeak)
		{
			((DialogWidgetWithLeak)d).OnLeakHeightChanged(e);
		}
	}

	protected virtual void OnLeakHeightChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	protected void UpdateSize_Border()
	{
		double leakLeft = LeakLeft;
		double num = base.ActualWidth - LeakLeft - LeakWidth;
		double num2 = base.ActualHeight - LeakTop;
		if (ui_border.ActualWidth <= num)
		{
			Grid.SetColumn(ui_border, 2);
			ui_border.HorizontalAlignment = HorizontalAlignment.Left;
		}
		else
		{
			Grid.SetColumn(ui_border, 0);
			ui_border.HorizontalAlignment = HorizontalAlignment.Right;
		}
		if (ui_border.ActualHeight <= num2)
		{
			Grid.SetRow(ui_border, 1);
			Grid.SetRowSpan(ui_border, 2);
			ui_border.VerticalAlignment = VerticalAlignment.Top;
		}
		else
		{
			Grid.SetRow(ui_border, 0);
			Grid.SetRowSpan(ui_border, 3);
			ui_border.VerticalAlignment = VerticalAlignment.Bottom;
		}
	}

	protected void UpdateSize_Leak()
	{
		if (LeakContent != null)
		{
			Point point = LeakContent.TranslatePoint(new Point(0.0, 0.0), this);
			LeakLeft = point.X;
			LeakTop = point.Y;
			LeakWidth = LeakContent.ActualWidth;
			LeakHeight = LeakContent.ActualHeight;
			RowDefinition[] array = ui_gridmain.RowDefinitions.ToArray();
			ColumnDefinition[] array2 = ui_gridmain.ColumnDefinitions.ToArray();
			array[0].Height = new GridLength(point.Y, GridUnitType.Pixel);
			array[1].Height = new GridLength(LeakContent.ActualHeight, GridUnitType.Pixel);
			array2[0].Width = new GridLength(point.X, GridUnitType.Pixel);
			array2[1].Width = new GridLength(LeakContent.ActualWidth, GridUnitType.Pixel);
		}
	}

	public override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		DependencyObject templateChild = GetTemplateChild("PART_Border");
		if (templateChild is Border)
		{
			ui_border = (Border)templateChild;
			ui_border.SizeChanged += OnBorderSizeChanged;
		}
		templateChild = GetTemplateChild("PART_GridMain");
		if (templateChild is Grid)
		{
			ui_gridmain = (Grid)templateChild;
		}
	}

	private void OnBorderSizeChanged(object sender, SizeChangedEventArgs e)
	{
		UpdateSize_Border();
	}

	private void OnLeakContentSizeChanged(object sender, SizeChangedEventArgs e)
	{
		UpdateSize_Leak();
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamCAD;component/control/dialogwidgetwithleak.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		_contentLoaded = true;
	}
}

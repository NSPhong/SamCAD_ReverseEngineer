using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using SamSoarII.Polyline;

namespace SamCAD.Control;

public class GroupMatrixWindow : UserControl, IComponentConnector
{
	protected static readonly DependencyProperty RowProperty = DependencyProperty.Register("Row", typeof(int), typeof(GroupMatrixWindow), new PropertyMetadata(2, OnPropertyChanged_Row));

	protected static readonly DependencyProperty ColumnProperty = DependencyProperty.Register("Column", typeof(int), typeof(GroupMatrixWindow), new PropertyMetadata(2, OnPropertyChanged_Column));

	protected static readonly DependencyProperty OffsetXProperty = DependencyProperty.Register("OffsetX", typeof(double), typeof(GroupMatrixWindow), new PropertyMetadata(0.0, OnPropertyChanged_OffsetX));

	protected static readonly DependencyProperty OffsetYProperty = DependencyProperty.Register("OffsetY", typeof(double), typeof(GroupMatrixWindow), new PropertyMetadata(0.0, OnPropertyChanged_OffsetY));

	protected static readonly DependencyProperty OffsetProperty = DependencyProperty.Register("Offset", typeof(Point), typeof(GroupMatrixWindow), new PropertyMetadata(default(Point), OnPropertyChanged_Offset));

	protected static readonly DependencyProperty StrategyProperty = DependencyProperty.Register("Strategy", typeof(MatrixStrategy), typeof(GroupMatrixWindow), new PropertyMetadata(MatrixStrategy.Sprial, OnPropertyChanged_Strategy));

	protected static readonly DependencyProperty PriorityProperty = DependencyProperty.Register("Priority", typeof(MatrixPriority), typeof(GroupMatrixWindow), new PropertyMetadata(MatrixPriority.Horizontal, OnPropertyChanged_Strategy));

	internal GroupMatrixWindow This;

	internal TextBox TX_Row;

	internal TextBox TX_Col;

	internal TextBox TX_XOffset;

	internal TextBox TX_YOffset;

	internal RadioButton RB_Spiral;

	internal RadioButton RB_Fold;

	internal RadioButton RB_ZipZap;

	internal RadioButton RB_HFirst;

	internal RadioButton RB_VFirst;

	internal GroupMatrixImage UI_Image;

	internal Button BN_Yes;

	internal Button BN_No;

	private bool _contentLoaded;

	public int Row
	{
		get
		{
			return (int)GetValue(RowProperty);
		}
		set
		{
			SetValue(RowProperty, value);
		}
	}

	public int Column
	{
		get
		{
			return (int)GetValue(ColumnProperty);
		}
		set
		{
			SetValue(ColumnProperty, value);
		}
	}

	public double OffsetX
	{
		get
		{
			return (double)GetValue(OffsetXProperty);
		}
		set
		{
			SetValue(OffsetXProperty, value);
		}
	}

	public double OffsetY
	{
		get
		{
			return (double)GetValue(OffsetYProperty);
		}
		set
		{
			SetValue(OffsetYProperty, value);
		}
	}

	public Point Offset
	{
		get
		{
			return (Point)GetValue(OffsetProperty);
		}
		set
		{
			SetValue(OffsetProperty, value);
		}
	}

	public MatrixStrategy Strategy
	{
		get
		{
			return (MatrixStrategy)GetValue(StrategyProperty);
		}
		set
		{
			SetValue(StrategyProperty, value);
		}
	}

	public MatrixPriority Priority
	{
		get
		{
			return (MatrixPriority)GetValue(PriorityProperty);
		}
		set
		{
			SetValue(PriorityProperty, value);
		}
	}

	public event RoutedEventHandler Yes;

	public event RoutedEventHandler No;

	public GroupMatrixWindow()
	{
		InitializeComponent();
		UI_Image.ViewParent = this;
	}

	private static void OnPropertyChanged_Row(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GroupMatrixWindow)
		{
			((GroupMatrixWindow)d).OnRowChanged(e);
		}
	}

	protected virtual void OnRowChanged(DependencyPropertyChangedEventArgs e)
	{
		UI_Image?.InvalidateVisual();
	}

	private static void OnPropertyChanged_Column(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GroupMatrixWindow)
		{
			((GroupMatrixWindow)d).OnColumnChanged(e);
		}
	}

	protected virtual void OnColumnChanged(DependencyPropertyChangedEventArgs e)
	{
		UI_Image?.InvalidateVisual();
	}

	private static void OnPropertyChanged_OffsetX(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GroupMatrixWindow)
		{
			((GroupMatrixWindow)d).OnOffsetXChanged(e);
		}
	}

	protected virtual void OnOffsetXChanged(DependencyPropertyChangedEventArgs e)
	{
		Offset = new Point(OffsetX, Offset.Y);
	}

	private static void OnPropertyChanged_OffsetY(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GroupMatrixWindow)
		{
			((GroupMatrixWindow)d).OnOffsetYChanged(e);
		}
	}

	protected virtual void OnOffsetYChanged(DependencyPropertyChangedEventArgs e)
	{
		Offset = new Point(Offset.X, OffsetY);
	}

	private static void OnPropertyChanged_Offset(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GroupMatrixWindow)
		{
			((GroupMatrixWindow)d).OnOffsetChanged(e);
		}
	}

	protected virtual void OnOffsetChanged(DependencyPropertyChangedEventArgs e)
	{
		UI_Image?.InvalidateVisual();
	}

	private static void OnPropertyChanged_Strategy(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GroupMatrixWindow)
		{
			((GroupMatrixWindow)d).OnStrategyChanged(e);
		}
	}

	protected virtual void OnStrategyChanged(DependencyPropertyChangedEventArgs e)
	{
		UI_Image?.InvalidateVisual();
	}

	private static void OnPropertyChanged_Priority(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GroupMatrixWindow)
		{
			((GroupMatrixWindow)d).OnPriorityChanged(e);
		}
	}

	protected virtual void OnPriorityChanged(DependencyPropertyChangedEventArgs e)
	{
		UI_Image?.InvalidateVisual();
	}

	private void RB_Spiral_Checked(object sender, RoutedEventArgs e)
	{
		Strategy = MatrixStrategy.Sprial;
	}

	private void RB_Fold_Checked(object sender, RoutedEventArgs e)
	{
		Strategy = MatrixStrategy.Fold;
	}

	private void RB_ZipZap_Checked(object sender, RoutedEventArgs e)
	{
		Strategy = MatrixStrategy.ZipZap;
	}

	private void RB_HFirst_Checked(object sender, RoutedEventArgs e)
	{
		Priority = MatrixPriority.Horizontal;
	}

	private void RB_VFirst_Checked(object sender, RoutedEventArgs e)
	{
		Priority = MatrixPriority.Vertical;
	}

	private void BN_Yes_Click(object sender, RoutedEventArgs e)
	{
		this.Yes?.Invoke(this, e);
	}

	private void BN_No_Click(object sender, RoutedEventArgs e)
	{
		this.No?.Invoke(this, e);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamCAD;component/control/groupmatrixwindow.xaml", UriKind.Relative);
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
			This = (GroupMatrixWindow)target;
			break;
		case 2:
			TX_Row = (TextBox)target;
			break;
		case 3:
			TX_Col = (TextBox)target;
			break;
		case 4:
			TX_XOffset = (TextBox)target;
			break;
		case 5:
			TX_YOffset = (TextBox)target;
			break;
		case 6:
			RB_Spiral = (RadioButton)target;
			RB_Spiral.Checked += RB_Spiral_Checked;
			break;
		case 7:
			RB_Fold = (RadioButton)target;
			RB_Fold.Checked += RB_Fold_Checked;
			break;
		case 8:
			RB_ZipZap = (RadioButton)target;
			RB_ZipZap.Checked += RB_ZipZap_Checked;
			break;
		case 9:
			RB_HFirst = (RadioButton)target;
			RB_HFirst.Checked += RB_HFirst_Checked;
			break;
		case 10:
			RB_VFirst = (RadioButton)target;
			RB_VFirst.Checked += RB_VFirst_Checked;
			break;
		case 11:
			UI_Image = (GroupMatrixImage)target;
			break;
		case 12:
			BN_Yes = (Button)target;
			BN_Yes.Click += BN_Yes_Click;
			break;
		case 13:
			BN_No = (Button)target;
			BN_No.Click += BN_No_Click;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}

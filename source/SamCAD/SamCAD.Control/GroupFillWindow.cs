using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using SamSoarII.Polyline;

namespace SamCAD.Control;

public class GroupFillWindow : UserControl, IComponentConnector
{
	protected static readonly DependencyProperty SizeProperty = DependencyProperty.Register("Size", typeof(double), typeof(GroupFillWindow), new PropertyMetadata(4.0, OnPropertyChanged_Size));

	protected static readonly DependencyProperty StrategyProperty = DependencyProperty.Register("Strategy", typeof(FillStrategy), typeof(GroupFillWindow), new PropertyMetadata(FillStrategy.Horizontal, OnPropertyChanged_Strategy));

	protected static readonly DependencyProperty IsRemoveOldProperty = DependencyProperty.Register("IsRemoveOld", typeof(bool), typeof(GroupFillWindow), new PropertyMetadata(true, OnPropertyChanged_IsRemoveOld));

	internal GroupFillWindow This;

	internal RadioButton RB_0;

	internal RadioButton RB_1;

	internal RadioButton RB_2;

	internal GroupFillImage UI_Image;

	internal Button BN_Yes;

	internal Button BN_No;

	private bool _contentLoaded;

	public double Size
	{
		get
		{
			return (double)GetValue(SizeProperty);
		}
		set
		{
			SetValue(SizeProperty, value);
		}
	}

	public FillStrategy Strategy
	{
		get
		{
			return (FillStrategy)GetValue(StrategyProperty);
		}
		set
		{
			SetValue(StrategyProperty, value);
		}
	}

	public bool IsRemoveOld
	{
		get
		{
			return (bool)GetValue(IsRemoveOldProperty);
		}
		set
		{
			SetValue(IsRemoveOldProperty, value);
		}
	}

	public event RoutedEventHandler Yes;

	public event RoutedEventHandler No;

	public GroupFillWindow()
	{
		InitializeComponent();
	}

	private static void OnPropertyChanged_Size(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GroupFillWindow)
		{
			((GroupFillWindow)d).OnSizeChanged(e);
		}
	}

	protected virtual void OnSizeChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_Strategy(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GroupFillWindow)
		{
			((GroupFillWindow)d).OnStrategyChanged(e);
		}
	}

	protected virtual void OnStrategyChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_IsRemoveOld(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GroupFillWindow)
		{
			((GroupFillWindow)d).OnIsRemoveOldChanged(e);
		}
	}

	protected virtual void OnIsRemoveOldChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private void RB_0_Checked(object sender, RoutedEventArgs e)
	{
		FillStrategy strategy = Strategy;
		strategy &= (FillStrategy)(-19);
		strategy |= FillStrategy.Horizontal;
		Strategy = strategy;
	}

	private void RB_1_Checked(object sender, RoutedEventArgs e)
	{
		FillStrategy strategy = Strategy;
		strategy &= (FillStrategy)(-18);
		strategy |= FillStrategy.Vertical;
		Strategy = strategy;
	}

	private void RB_2_Checked(object sender, RoutedEventArgs e)
	{
		FillStrategy strategy = Strategy;
		strategy &= (FillStrategy)(-4);
		strategy |= FillStrategy.Rounding;
		Strategy = strategy;
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
			Uri resourceLocator = new Uri("/SamCAD;component/control/groupfillwindow.xaml", UriKind.Relative);
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
			This = (GroupFillWindow)target;
			break;
		case 2:
			RB_0 = (RadioButton)target;
			RB_0.Checked += RB_0_Checked;
			break;
		case 3:
			RB_1 = (RadioButton)target;
			RB_1.Checked += RB_1_Checked;
			break;
		case 4:
			RB_2 = (RadioButton)target;
			RB_2.Checked += RB_2_Checked;
			break;
		case 5:
			UI_Image = (GroupFillImage)target;
			break;
		case 6:
			BN_Yes = (Button)target;
			BN_Yes.Click += BN_Yes_Click;
			break;
		case 7:
			BN_No = (Button)target;
			BN_No.Click += BN_No_Click;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}

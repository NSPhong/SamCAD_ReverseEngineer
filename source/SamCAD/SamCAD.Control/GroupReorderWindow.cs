using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using SamSoarII.Polyline;

namespace SamCAD.Control;

public class GroupReorderWindow : UserControl, IComponentConnector
{
	protected static readonly DependencyProperty ReorderingStrategyProperty = DependencyProperty.Register("ReorderingStrategy", typeof(ReorderingStrategy), typeof(GroupReorderWindow), new PropertyMetadata(ReorderingStrategy.None, OnPropertyChanged_ReorderingStrategy));

	internal ListBox LB_Action;

	internal RadioButton RB_MinLength;

	internal RadioButton RB_FlatCorner;

	internal Button BN_Optimize_Decide;

	internal Button BN_Optimize_Cancel;

	internal Button BN_Yes;

	internal Button BN_No;

	private bool _contentLoaded;

	public ReorderingStrategy ReorderingStrategy
	{
		get
		{
			return (ReorderingStrategy)GetValue(ReorderingStrategyProperty);
		}
		set
		{
			SetValue(ReorderingStrategyProperty, value);
		}
	}

	public event RoutedEventHandler Yes;

	public event RoutedEventHandler No;

	public GroupReorderWindow()
	{
		InitializeComponent();
		base.Loaded += OnLoaded;
	}

	private static void OnPropertyChanged_ReorderingStrategy(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is GroupReorderWindow)
		{
			((GroupReorderWindow)d).OnReorderingStrategyChanged(e);
		}
	}

	protected virtual void OnReorderingStrategyChanged(DependencyPropertyChangedEventArgs e)
	{
		UpdateActionSource();
	}

	public void Begin()
	{
		ReorderingStrategy = ReorderingStrategy.None;
	}

	public void End()
	{
	}

	protected void UpdateActionSource()
	{
		if (ReorderingStrategy == ReorderingStrategy.None)
		{
			LB_Action.ItemsSource = App.Client?.UI_Select?.Moveds;
			BN_Optimize_Decide.IsEnabled = true;
			BN_Optimize_Cancel.IsEnabled = false;
		}
		else
		{
			LB_Action.ItemsSource = new object[1] { ReorderingStrategy };
			BN_Optimize_Decide.IsEnabled = false;
			BN_Optimize_Cancel.IsEnabled = true;
		}
	}

	private void OnLoaded(object sender, RoutedEventArgs e)
	{
		UpdateActionSource();
	}

	private void BN_Optimize_Decide_Click(object sender, RoutedEventArgs e)
	{
		if (RB_MinLength.IsChecked == true)
		{
			ReorderingStrategy = ReorderingStrategy.MinimizeLength;
		}
		else if (RB_FlatCorner.IsChecked == true)
		{
			ReorderingStrategy = ReorderingStrategy.FlatCorners;
		}
	}

	private void BN_Optimize_Cancel_Click(object sender, RoutedEventArgs e)
	{
		ReorderingStrategy = ReorderingStrategy.None;
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
			Uri resourceLocator = new Uri("/SamCAD;component/control/groupreorderwindow.xaml", UriKind.Relative);
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
			LB_Action = (ListBox)target;
			break;
		case 2:
			RB_MinLength = (RadioButton)target;
			break;
		case 3:
			RB_FlatCorner = (RadioButton)target;
			break;
		case 4:
			BN_Optimize_Decide = (Button)target;
			BN_Optimize_Decide.Click += BN_Optimize_Decide_Click;
			break;
		case 5:
			BN_Optimize_Cancel = (Button)target;
			BN_Optimize_Cancel.Click += BN_Optimize_Cancel_Click;
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

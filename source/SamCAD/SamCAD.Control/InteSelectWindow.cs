using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SamCAD.Control;

public class InteSelectWindow : UserControl, IComponentConnector
{
	internal InteSelectWindow This;

	internal Button BN_SelectAllDash;

	internal Button BN_SelectAllReal;

	internal Button BN_SelectAllLine;

	internal Button BN_SelectAllCirc;

	internal Button BN_SelectAllArch;

	private bool _contentLoaded;

	public event InteSelectEventHandler Select;

	public InteSelectWindow()
	{
		InitializeComponent();
	}

	private void BN_SelectAllDash_Click(object sender, RoutedEventArgs e)
	{
		this.Select?.Invoke(this, new InteSelectEventArgs(Enum_InteSelectEvent.SelectAllDash));
	}

	private void BN_SelectAllReal_Click(object sender, RoutedEventArgs e)
	{
		this.Select?.Invoke(this, new InteSelectEventArgs(Enum_InteSelectEvent.SelectAllReal));
	}

	private void BN_SelectAllLine_Click(object sender, RoutedEventArgs e)
	{
		this.Select?.Invoke(this, new InteSelectEventArgs(Enum_InteSelectEvent.SelectAllLine));
	}

	private void BN_SelectAllCirc_Click(object sender, RoutedEventArgs e)
	{
		this.Select?.Invoke(this, new InteSelectEventArgs(Enum_InteSelectEvent.SelectAllCirc));
	}

	private void BN_SelectAllArch_Click(object sender, RoutedEventArgs e)
	{
		this.Select?.Invoke(this, new InteSelectEventArgs(Enum_InteSelectEvent.SelectAllArch));
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamCAD;component/control/inteselectwindow.xaml", UriKind.Relative);
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
			This = (InteSelectWindow)target;
			break;
		case 2:
			BN_SelectAllDash = (Button)target;
			BN_SelectAllDash.Click += BN_SelectAllDash_Click;
			break;
		case 3:
			BN_SelectAllReal = (Button)target;
			BN_SelectAllReal.Click += BN_SelectAllReal_Click;
			break;
		case 4:
			BN_SelectAllLine = (Button)target;
			BN_SelectAllLine.Click += BN_SelectAllLine_Click;
			break;
		case 5:
			BN_SelectAllCirc = (Button)target;
			BN_SelectAllCirc.Click += BN_SelectAllCirc_Click;
			break;
		case 6:
			BN_SelectAllArch = (Button)target;
			BN_SelectAllArch.Click += BN_SelectAllArch_Click;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}

using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SamSoarII.Shell.Dialogs;

public class ReplaceReportWindow : Window, IComponentConnector
{
	internal TextBlock TB_Subtitle;

	internal CustomTextBox TB_Message;

	internal CustomButton B_Continue;

	private bool _contentLoaded;

	public ReplaceReportWindow(string brief, string detail)
	{
		InitializeComponent();
		TB_Subtitle.Text = brief;
		TB_Message.Text = detail;
	}

	private void B_Continue_Click(object sender, RoutedEventArgs e)
	{
		Close();
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamSoarII.Utility;component/controls/dialogs/replacereportwindow.xaml", UriKind.Relative);
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
			TB_Subtitle = (TextBlock)target;
			break;
		case 2:
			TB_Message = (CustomTextBox)target;
			break;
		case 3:
			B_Continue = (CustomButton)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}

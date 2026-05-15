using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Markup;

namespace SamSoarII.Shell.Dialogs;

public class CSVExportDialog : Window, IDisposable, IComponentConnector
{
	internal CustomTextBox NameTextBox;

	internal CustomTextBox PathTextBox;

	internal CustomButton ExportButton;

	internal CustomButton CancelButton;

	internal CustomButton BrowseButton;

	private bool _contentLoaded;

	public string FileName => NameTextBox.Text;

	public string Path => PathTextBox.Text;

	public event RoutedEventHandler ExportButtonClick;

	public CSVExportDialog()
	{
		InitializeComponent();
		base.WindowStartupLocation = WindowStartupLocation.CenterScreen;
		ExportButton.Click += ExportButton_Click;
		CancelButton.Click += CancelButton_Click;
		BrowseButton.Click += BrowseButton_Click;
	}

	private void BrowseButton_Click(object sender, RoutedEventArgs e)
	{
		FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
		if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
		{
			PathTextBox.Text = folderBrowserDialog.SelectedPath;
		}
	}

	private void CancelButton_Click(object sender, RoutedEventArgs e)
	{
		Close();
	}

	private void ExportButton_Click(object sender, RoutedEventArgs e)
	{
		if (this.ExportButtonClick != null)
		{
			this.ExportButtonClick(this, new RoutedEventArgs());
		}
	}

	public void Dispose()
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
			Uri resourceLocator = new Uri("/SamSoarII.Utility;component/controls/dialogs/csvexportdialog.xaml", UriKind.Relative);
			System.Windows.Application.LoadComponent(this, resourceLocator);
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
			NameTextBox = (CustomTextBox)target;
			break;
		case 2:
			PathTextBox = (CustomTextBox)target;
			break;
		case 3:
			ExportButton = (CustomButton)target;
			break;
		case 4:
			CancelButton = (CustomButton)target;
			break;
		case 5:
			BrowseButton = (CustomButton)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}

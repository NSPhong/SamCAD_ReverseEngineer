using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Markup;
using Microsoft.Win32;

namespace SamSoarII.Shell.Dialogs;

public class CSVImportDialog : Window, IDisposable, IComponentConnector
{
	internal CustomTextBox FileTextBox;

	internal CustomButton ImportButton;

	internal CustomButton CancelButton;

	internal CustomButton BrowseButton;

	private bool _contentLoaded;

	public string FileName => FileTextBox.Text;

	public event RoutedEventHandler ImportButtonClick;

	public CSVImportDialog()
	{
		InitializeComponent();
		base.WindowStartupLocation = WindowStartupLocation.CenterScreen;
		ImportButton.Click += ImportButton_Click;
		CancelButton.Click += CancelButton_Click;
		BrowseButton.Click += BrowseButton_Click;
	}

	private void BrowseButton_Click(object sender, RoutedEventArgs e)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Filter = "csv file: *.csv";
		openFileDialog.Multiselect = false;
		if (openFileDialog.ShowDialog() == true)
		{
			FileTextBox.Text = openFileDialog.FileName;
		}
	}

	private void CancelButton_Click(object sender, RoutedEventArgs e)
	{
		Close();
	}

	private void ImportButton_Click(object sender, RoutedEventArgs e)
	{
		if (this.ImportButtonClick != null)
		{
			this.ImportButtonClick(this, new RoutedEventArgs());
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
			Uri resourceLocator = new Uri("/SamSoarII.Utility;component/controls/dialogs/csvimportdialog.xaml", UriKind.Relative);
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
			FileTextBox = (CustomTextBox)target;
			break;
		case 2:
			ImportButton = (CustomButton)target;
			break;
		case 3:
			CancelButton = (CustomButton)target;
			break;
		case 4:
			BrowseButton = (CustomButton)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}

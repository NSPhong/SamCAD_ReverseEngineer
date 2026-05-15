using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.Win32;
using SamSoarII.Polyline.Arguments;

namespace SamCAD.Control;

public class CreateProjectWindow : UserControl, IComponentConnector
{
	protected static readonly DependencyProperty ProjectNameProperty = DependencyProperty.Register("ProjectName", typeof(string), typeof(CreateProjectWindow), new PropertyMetadata("New image", OnPropertyChanged_ProjectName));

	protected static readonly DependencyProperty FileNameProperty = DependencyProperty.Register("FileName", typeof(string), typeof(CreateProjectWindow), new PropertyMetadata(string.Empty, OnPropertyChanged_FileName));

	protected static readonly DependencyProperty ArgumentTypesProperty = DependencyProperty.Register("ArgumentTypes", typeof(IEnumerable<ImageArgumentTypes>), typeof(CreateProjectWindow), new PropertyMetadata(Enum.GetValues(typeof(ImageArgumentTypes)), OnPropertyChanged_ArgumentTypes));

	protected static readonly DependencyProperty ArgumentTypeProperty = DependencyProperty.Register("ArgumentType", typeof(ImageArgumentTypes), typeof(CreateProjectWindow), new PropertyMetadata(ImageArgumentTypes.HMIPLINE, OnPropertyChanged_ArgumentType));

	internal CreateProjectWindow This;

	internal TextBox TX_Name;

	internal TextBox TX_File;

	internal Button BN_File;

	internal ComboBox CB_Args;

	internal Button BN_Yes;

	internal Button BN_No;

	private bool _contentLoaded;

	public string ProjectName
	{
		get
		{
			return (string)GetValue(ProjectNameProperty);
		}
		set
		{
			SetValue(ProjectNameProperty, value);
		}
	}

	public string FileName
	{
		get
		{
			return (string)GetValue(FileNameProperty);
		}
		set
		{
			SetValue(FileNameProperty, value);
		}
	}

	public IEnumerable<ImageArgumentTypes> ArgumentTypes
	{
		get
		{
			return (IEnumerable<ImageArgumentTypes>)GetValue(ArgumentTypesProperty);
		}
		set
		{
			SetValue(ArgumentTypesProperty, value);
		}
	}

	public ImageArgumentTypes ArgumentType
	{
		get
		{
			return (ImageArgumentTypes)GetValue(ArgumentTypeProperty);
		}
		set
		{
			SetValue(ArgumentTypeProperty, value);
		}
	}

	public event RoutedEventHandler Yes;

	public event RoutedEventHandler No;

	public CreateProjectWindow()
	{
		InitializeComponent();
	}

	private static void OnPropertyChanged_ProjectName(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_FileName(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_ArgumentTypes(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_ArgumentType(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
	}

	private void BN_File_Click(object sender, RoutedEventArgs e)
	{
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.RestoreDirectory = true;
		saveFileDialog.Filter = "SamCAD file: *.sca file: *.ssd";
		if (saveFileDialog.ShowDialog() == true)
		{
			FileName = saveFileDialog.FileName;
		}
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
			Uri resourceLocator = new Uri("/SamCAD;component/control/createprojectwindow.xaml", UriKind.Relative);
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
			This = (CreateProjectWindow)target;
			break;
		case 2:
			TX_Name = (TextBox)target;
			break;
		case 3:
			TX_File = (TextBox)target;
			break;
		case 4:
			BN_File = (Button)target;
			BN_File.Click += BN_File_Click;
			break;
		case 5:
			CB_Args = (ComboBox)target;
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

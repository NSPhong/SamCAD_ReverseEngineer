using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using SamSoarII.Polyline.Arguments;

namespace SamCAD.Control;

public class LoadModeWindow : UserControl, IComponentConnector
{
	public static readonly DependencyProperty FileNameProperty = DependencyProperty.Register("FileName", typeof(string), typeof(LoadModeWindow), new PropertyMetadata(string.Empty, OnPropertyChanged_FileName));

	public static readonly DependencyProperty ModeListProperty = DependencyProperty.Register("ModeList", typeof(IList<LoadMode>), typeof(LoadModeWindow), new PropertyMetadata(new List<LoadMode>
	{
		new LoadMode(Enum_LoadMode.None),
		new LoadMode(Enum_LoadMode.Drill)
	}, OnPropertyChanged_ModeList));

	public static readonly DependencyProperty ModeProperty = DependencyProperty.Register("Mode", typeof(LoadMode), typeof(LoadModeWindow), new PropertyMetadata(new LoadMode(Enum_LoadMode.None), OnPropertyChanged_Mode));

	public static readonly DependencyProperty StartAtFirstEntityProperty = DependencyProperty.Register("StartAtFirstEntity", typeof(bool), typeof(LoadModeWindow), new PropertyMetadata(false, OnPropertyChanged_StartAtFirstEntity));

	protected static readonly DependencyProperty ArgumentTypesProperty = DependencyProperty.Register("ArgumentTypes", typeof(IEnumerable<ImageArgumentTypes>), typeof(LoadModeWindow), new PropertyMetadata(Enum.GetValues(typeof(ImageArgumentTypes)), OnPropertyChanged_ArgumentTypes));

	protected static readonly DependencyProperty ArgumentTypeProperty = DependencyProperty.Register("ArgumentType", typeof(ImageArgumentTypes), typeof(LoadModeWindow), new PropertyMetadata(ImageArgumentTypes.HMIPLINE, OnPropertyChanged_ArgumentType));

	public RoutedEventHandler Yes;

	public RoutedEventHandler No;

	internal LoadModeWindow This;

	internal ComboBox CB_Args;

	internal Button BN_Yes;

	internal Button BN_No;

	private bool _contentLoaded;

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

	public IList<LoadMode> ModeList
	{
		get
		{
			return (IList<LoadMode>)GetValue(ModeListProperty);
		}
		set
		{
			SetValue(ModeListProperty, value);
		}
	}

	public LoadMode Mode
	{
		get
		{
			return (LoadMode)GetValue(ModeProperty);
		}
		set
		{
			SetValue(ModeProperty, value);
		}
	}

	public bool StartAtFirstEntity
	{
		get
		{
			return (bool)GetValue(StartAtFirstEntityProperty);
		}
		set
		{
			SetValue(StartAtFirstEntityProperty, value);
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

	public LoadModeWindow()
	{
		InitializeComponent();
	}

	private static void OnPropertyChanged_FileName(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is LoadModeWindow)
		{
			((LoadModeWindow)d).OnFileNameChanged(e);
		}
	}

	protected void OnFileNameChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_ModeList(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is LoadModeWindow)
		{
			((LoadModeWindow)d).OnModeListChanged(e);
		}
	}

	protected void OnModeListChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_Mode(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is LoadModeWindow)
		{
			((LoadModeWindow)d).OnModeChanged(e);
		}
	}

	protected void OnModeChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_StartAtFirstEntity(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is LoadModeWindow)
		{
			((LoadModeWindow)d).OnStartAtFirstEntityChanged(e);
		}
	}

	protected void OnStartAtFirstEntityChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_ArgumentTypes(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_ArgumentType(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
	}

	private void BN_Yes_Click(object sender, RoutedEventArgs e)
	{
		Yes?.Invoke(this, e);
	}

	private void BN_No_Click(object sender, RoutedEventArgs e)
	{
		No?.Invoke(this, e);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamCAD;component/control/loadmodewindow.xaml", UriKind.Relative);
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
			This = (LoadModeWindow)target;
			break;
		case 2:
			CB_Args = (ComboBox)target;
			break;
		case 3:
			BN_Yes = (Button)target;
			BN_Yes.Click += BN_Yes_Click;
			break;
		case 4:
			BN_No = (Button)target;
			BN_No.Click += BN_No_Click;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}

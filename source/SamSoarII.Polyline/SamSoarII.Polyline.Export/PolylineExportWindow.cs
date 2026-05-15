using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using Microsoft.Win32;

namespace SamSoarII.Polyline.Export;

public class PolylineExportWindow : UserControl, IComponentConnector
{
	protected static readonly DependencyProperty CoreProperty = DependencyProperty.Register("Core", typeof(PolylineExportCore), typeof(PolylineExportWindow), new PropertyMetadata(null, OnPropertyChanged_Core));

	protected static readonly DependencyProperty SelectedColumnProperty = DependencyProperty.Register("SelectedColumn", typeof(PolylineExportColumn), typeof(PolylineExportWindow), new PropertyMetadata(null, OnPropertyChanged_SelectedColumn));

	internal PolylineExportWindow This;

	internal TextBox TX_File;

	internal Button BN_File;

	internal Button BN_All;

	internal ListBox LX_Columns;

	internal Button BN_CAdd;

	internal Button BN_CDel;

	internal Button BN_CUp;

	internal Button BN_CDown;

	internal DataGrid DG_Map;

	internal DataGridTextColumn DGC_From;

	internal DataGridTextColumn DGC_To;

	internal Button BN_Export;

	internal Button BN_Cancel;

	private bool _contentLoaded;

	public PolylineExportCore Core
	{
		get
		{
			return (PolylineExportCore)GetValue(CoreProperty);
		}
		set
		{
			SetValue(CoreProperty, value);
		}
	}

	public PolylineExportColumn SelectedColumn
	{
		get
		{
			return (PolylineExportColumn)GetValue(SelectedColumnProperty);
		}
		set
		{
			SetValue(SelectedColumnProperty, value);
		}
	}

	public string FileName
	{
		get
		{
			return TX_File.Text;
		}
		set
		{
			TX_File.Text = value;
		}
	}

	public event RoutedEventHandler Export;

	public event RoutedEventHandler Cancel;

	public event RoutedEventHandler ColAdd;

	public event RoutedEventHandler All;

	public PolylineExportWindow()
	{
		InitializeComponent();
	}

	private static void OnPropertyChanged_Core(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineExportWindow)
		{
			((PolylineExportWindow)d).OnCoreChanged(e);
		}
	}

	protected virtual void OnCoreChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_SelectedColumn(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineExportWindow)
		{
			((PolylineExportWindow)d).OnSelectedColumnChanged(e);
		}
	}

	protected virtual void OnSelectedColumnChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private void BN_Export_Click(object sender, RoutedEventArgs e)
	{
		this.Export?.Invoke(this, e);
	}

	private void BN_Cancel_Click(object sender, RoutedEventArgs e)
	{
		this.Cancel?.Invoke(this, e);
	}

	private void BN_File_Click(object sender, RoutedEventArgs e)
	{
		SaveFileDialog saveFileDialog = new SaveFileDialog();
		saveFileDialog.Filter = "csv file: *.csv";
		if (saveFileDialog.ShowDialog() == true)
		{
			TX_File.Text = saveFileDialog.FileName;
		}
	}

	private void BN_CAdd_Click(object sender, RoutedEventArgs e)
	{
		this.ColAdd?.Invoke(this, e);
	}

	private void BN_CDel_Click(object sender, RoutedEventArgs e)
	{
		if (LX_Columns.SelectedItem != null)
		{
			PolylineExportColumn[] array = LX_Columns.SelectedItems.Cast<PolylineExportColumn>().ToArray();
			foreach (PolylineExportColumn item in array)
			{
				Core.Columns.Remove(item);
			}
		}
	}

	private void BN_CUp_Click(object sender, RoutedEventArgs e)
	{
		if (LX_Columns.SelectedItem is PolylineExportColumn)
		{
			PolylineExportColumn item = (PolylineExportColumn)LX_Columns.SelectedItem;
			int selectedIndex = LX_Columns.SelectedIndex;
			if (selectedIndex >= 0)
			{
				Core.Columns.RemoveAt(selectedIndex);
				Core.Columns.Insert(selectedIndex - 1, item);
				LX_Columns.SelectedIndex = selectedIndex - 1;
			}
		}
	}

	private void BN_CDown_Click(object sender, RoutedEventArgs e)
	{
		if (LX_Columns.SelectedItem is PolylineExportColumn)
		{
			PolylineExportColumn item = (PolylineExportColumn)LX_Columns.SelectedItem;
			int selectedIndex = LX_Columns.SelectedIndex;
			if (selectedIndex >= 0 && selectedIndex + 1 < Core.Columns.Count())
			{
				Core.Columns.RemoveAt(selectedIndex);
				Core.Columns.Insert(selectedIndex + 1, item);
				LX_Columns.SelectedIndex = selectedIndex + 1;
			}
		}
	}

	private void BN_All_Click(object sender, RoutedEventArgs e)
	{
		this.All?.Invoke(this, e);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamSoarII.Polyline;component/export/polylineexportwindow.xaml", UriKind.Relative);
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
			This = (PolylineExportWindow)target;
			break;
		case 2:
			TX_File = (TextBox)target;
			break;
		case 3:
			BN_File = (Button)target;
			BN_File.Click += BN_File_Click;
			break;
		case 4:
			BN_All = (Button)target;
			BN_All.Click += BN_All_Click;
			break;
		case 5:
			LX_Columns = (ListBox)target;
			break;
		case 6:
			BN_CAdd = (Button)target;
			BN_CAdd.Click += BN_CAdd_Click;
			break;
		case 7:
			BN_CDel = (Button)target;
			BN_CDel.Click += BN_CDel_Click;
			break;
		case 8:
			BN_CUp = (Button)target;
			BN_CUp.Click += BN_CUp_Click;
			break;
		case 9:
			BN_CDown = (Button)target;
			BN_CDown.Click += BN_CDown_Click;
			break;
		case 10:
			DG_Map = (DataGrid)target;
			break;
		case 11:
			DGC_From = (DataGridTextColumn)target;
			break;
		case 12:
			DGC_To = (DataGridTextColumn)target;
			break;
		case 13:
			BN_Export = (Button)target;
			BN_Export.Click += BN_Export_Click;
			break;
		case 14:
			BN_Cancel = (Button)target;
			BN_Cancel.Click += BN_Cancel_Click;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}

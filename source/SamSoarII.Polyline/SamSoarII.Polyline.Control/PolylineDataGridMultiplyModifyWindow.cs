using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using SamSoarII.Polyline.Entity.User;
using SamSoarII.Polyline.Export;

namespace SamSoarII.Polyline.Control;

public class PolylineDataGridMultiplyModifyWindow : UserControl, IComponentConnector
{
	public static readonly DependencyProperty CoreProperty = DependencyProperty.Register("Core", typeof(PolylineDataGridMultiplyModifyCore), typeof(PolylineDataGridMultiplyModifyWindow), new PropertyMetadata(null, OnPropertyChanged_Core));

	public static readonly DependencyProperty ColumnProperty = DependencyProperty.Register("Column", typeof(PolylineExportColumn), typeof(PolylineDataGridMultiplyModifyWindow), new PropertyMetadata(null, OnPropertyChanged_Column));

	internal PolylineDataGridMultiplyModifyWindow This;

	internal ComboBox CB_Column;

	internal TextBox TX_Start;

	internal TextBox TX_End;

	internal TextBox TX_Value;

	internal CheckBox CK_Value;

	internal Button BN_All;

	internal Button BN_Ensure;

	internal Button BN_Cancel;

	private bool _contentLoaded;

	public PolylineDataGridMultiplyModifyCore Core
	{
		get
		{
			return (PolylineDataGridMultiplyModifyCore)GetValue(CoreProperty);
		}
		set
		{
			SetValue(CoreProperty, value);
		}
	}

	public PolylineExportColumn Column
	{
		get
		{
			return (PolylineExportColumn)GetValue(ColumnProperty);
		}
		set
		{
			SetValue(ColumnProperty, value);
		}
	}

	public event RoutedEventHandler Ensure;

	public event RoutedEventHandler Cancel;

	public PolylineDataGridMultiplyModifyWindow()
	{
		InitializeComponent();
	}

	private static void OnPropertyChanged_Core(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineDataGridMultiplyModifyWindow)
		{
			((PolylineDataGridMultiplyModifyWindow)d).OnCoreChanged(e);
		}
	}

	protected virtual void OnCoreChanged(DependencyPropertyChangedEventArgs e)
	{
		if (Core != null)
		{
			TX_Value.Text = Core.ModifyValue?.ToString() ?? string.Empty;
			CK_Value.IsChecked = Core.ModifyValue is bool && (bool)Core.ModifyValue;
			Column = Core.Columns.FirstOrDefault();
		}
	}

	private static void OnPropertyChanged_Column(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineDataGridMultiplyModifyWindow)
		{
			((PolylineDataGridMultiplyModifyWindow)d).OnColumnChanged(e);
		}
	}

	protected virtual void OnColumnChanged(DependencyPropertyChangedEventArgs e)
	{
		Core.SelectedColumn = Column;
		if (Column == null)
		{
			TX_Value.Visibility = Visibility.Hidden;
			CK_Value.Visibility = Visibility.Hidden;
		}
		else if (Column.UserFormat.DataType == PolylineUserDataType.Bool)
		{
			TX_Value.Visibility = Visibility.Hidden;
			CK_Value.Visibility = Visibility.Visible;
		}
		else
		{
			TX_Value.Visibility = Visibility.Visible;
			CK_Value.Visibility = Visibility.Hidden;
		}
	}

	private void BN_All_Click(object sender, RoutedEventArgs e)
	{
		if (Core != null)
		{
			Core.IsModifySelected = false;
			Core.StartLine = 0;
			Core.EndLine = Core.Core.Items.Count() - 1;
		}
	}

	private void BN_Ensure_Click(object sender, RoutedEventArgs e)
	{
		if (Column == null)
		{
			MessageBox.Show("The selected ones are listed as blank!");
			return;
		}
		switch (Column.UserFormat.DataType)
		{
		case PolylineUserDataType.Bool:
			Core.ModifyValue = CK_Value.IsChecked == true;
			break;
		case PolylineUserDataType.String:
			Core.ModifyValue = TX_Value.Text;
			break;
		case PolylineUserDataType.Int:
		{
			int result2 = 0;
			if (!int.TryParse(TX_Value.Text, out result2))
			{
				MessageBox.Show("输入的Integer非法！");
				return;
			}
			Core.ModifyValue = result2;
			break;
		}
		case PolylineUserDataType.Double:
		{
			double result = 0.0;
			if (!double.TryParse(TX_Value.Text, out result))
			{
				MessageBox.Show("输入的Real number非法！");
				return;
			}
			Core.ModifyValue = result;
			break;
		}
		}
		this.Ensure?.Invoke(this, e);
	}

	private void BN_Cancel_Click(object sender, RoutedEventArgs e)
	{
		this.Cancel?.Invoke(this, e);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamSoarII.Polyline;component/control/polylinedatagridmultiplymodifywindow.xaml", UriKind.Relative);
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
			This = (PolylineDataGridMultiplyModifyWindow)target;
			break;
		case 2:
			CB_Column = (ComboBox)target;
			break;
		case 3:
			TX_Start = (TextBox)target;
			break;
		case 4:
			TX_End = (TextBox)target;
			break;
		case 5:
			TX_Value = (TextBox)target;
			break;
		case 6:
			CK_Value = (CheckBox)target;
			break;
		case 7:
			BN_All = (Button)target;
			BN_All.Click += BN_All_Click;
			break;
		case 8:
			BN_Ensure = (Button)target;
			BN_Ensure.Click += BN_Ensure_Click;
			break;
		case 9:
			BN_Cancel = (Button)target;
			BN_Cancel.Click += BN_Cancel_Click;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using SamSoarII.Polyline.Entity.User;

namespace SamSoarII.Polyline.Export;

public class PolylineExportColumnAddWindow : UserControl, IComponentConnector
{
	protected static readonly DependencyProperty CoreProperty = DependencyProperty.Register("Core", typeof(PolylineExportCore), typeof(PolylineExportColumnAddWindow), new PropertyMetadata(null));

	protected static readonly DependencyProperty SystemColumnsProperty = DependencyProperty.Register("SystemColumns", typeof(IList<PolylineExportColumn>), typeof(PolylineExportColumnAddWindow), new PropertyMetadata(null));

	protected static readonly DependencyProperty UserColumnsProperty = DependencyProperty.Register("UserColumns", typeof(IList<PolylineExportColumn>), typeof(PolylineExportColumnAddWindow), new PropertyMetadata(null));

	internal PolylineExportColumnAddWindow This;

	internal TextBox TX_C;

	internal ListBox LX_S;

	internal ListBox LX_U;

	internal Button BN_Yes;

	internal Button BN_No;

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

	public IList<PolylineExportColumn> SystemColumns
	{
		get
		{
			return (IList<PolylineExportColumn>)GetValue(SystemColumnsProperty);
		}
		private set
		{
			SetValue(SystemColumnsProperty, value);
		}
	}

	public IList<PolylineExportColumn> UserColumns
	{
		get
		{
			return (IList<PolylineExportColumn>)GetValue(UserColumnsProperty);
		}
		private set
		{
			SetValue(UserColumnsProperty, value);
		}
	}

	public event RoutedEventHandler Yes;

	public event RoutedEventHandler No;

	public PolylineExportColumnAddWindow()
	{
		InitializeComponent();
	}

	public PolylineExportColumn GetSelectedColumn()
	{
		if (LX_S.SelectedItem is PolylineExportColumn)
		{
			return (PolylineExportColumn)LX_S.SelectedItem;
		}
		if (LX_U.SelectedItem is PolylineExportColumn)
		{
			return (PolylineExportColumn)LX_U.SelectedItem;
		}
		return null;
	}

	private void BN_Yes_Click(object sender, RoutedEventArgs e)
	{
		this.Yes?.Invoke(this, e);
	}

	private void BN_No_Click(object sender, RoutedEventArgs e)
	{
		this.No?.Invoke(this, e);
	}

	private void LX_S_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (LX_S.SelectedItem != null)
		{
			LX_U.SelectedItem = null;
			TX_C.Text = LX_S.SelectedItem.ToString();
			BN_Yes.IsEnabled = true;
		}
	}

	private void LX_U_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (LX_U.SelectedItem != null)
		{
			LX_S.SelectedItem = null;
			TX_C.Text = LX_U.SelectedItem.ToString();
			BN_Yes.IsEnabled = true;
		}
	}

	protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		base.OnPropertyChanged(e);
		if (e.Property != UIElement.IsVisibleProperty || !(e.OldValue is bool) || !(e.NewValue is bool) || (bool)e.OldValue || !(bool)e.NewValue)
		{
			return;
		}
		IPolylineImage image = Core.Image;
		List<PolylineExportColumn> list = new List<PolylineExportColumn>();
		List<PolylineExportColumn> list2 = new List<PolylineExportColumn>();
		if (Core.Columns.FirstOrDefault((PolylineExportColumn c) => c.Type == PolylineExportColumnType.X) == null)
		{
			list.Add(new PolylineExportColumn(PolylineExportColumnType.X));
		}
		if (Core.Columns.FirstOrDefault((PolylineExportColumn c) => c.Type == PolylineExportColumnType.Y) == null)
		{
			list.Add(new PolylineExportColumn(PolylineExportColumnType.Y));
		}
		if (Core.Columns.FirstOrDefault((PolylineExportColumn c) => c.Type == PolylineExportColumnType.Radius) == null)
		{
			list.Add(new PolylineExportColumn(PolylineExportColumnType.Radius));
		}
		if (Core.Columns.FirstOrDefault((PolylineExportColumn c) => c.Type == PolylineExportColumnType.Type) == null)
		{
			list.Add(new PolylineExportColumn(PolylineExportColumnType.Type));
		}
		if (Core.Columns.FirstOrDefault((PolylineExportColumn c) => c.Type == PolylineExportColumnType.CenterX) == null)
		{
			list.Add(new PolylineExportColumn(PolylineExportColumnType.CenterX));
		}
		if (Core.Columns.FirstOrDefault((PolylineExportColumn c) => c.Type == PolylineExportColumnType.CenterY) == null)
		{
			list.Add(new PolylineExportColumn(PolylineExportColumnType.CenterY));
		}
		foreach (IPolylineUserFormat uf in image.UserFmts)
		{
			if (Core.Columns.FirstOrDefault((PolylineExportColumn c) => c.Type == PolylineExportColumnType.User && c.UserFormat == uf) == null)
			{
				list2.Add(new PolylineExportColumn(PolylineExportColumnType.User)
				{
					UserFormat = uf
				});
			}
		}
		SystemColumns = list;
		UserColumns = list2;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamSoarII.Polyline;component/export/polylineexportcolumnaddwindow.xaml", UriKind.Relative);
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
			This = (PolylineExportColumnAddWindow)target;
			break;
		case 2:
			TX_C = (TextBox)target;
			break;
		case 3:
			LX_S = (ListBox)target;
			LX_S.SelectionChanged += LX_S_SelectionChanged;
			break;
		case 4:
			LX_U = (ListBox)target;
			LX_U.SelectionChanged += LX_U_SelectionChanged;
			break;
		case 5:
			BN_Yes = (Button)target;
			BN_Yes.Click += BN_Yes_Click;
			break;
		case 6:
			BN_No = (Button)target;
			BN_No.Click += BN_No_Click;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using SamSoarII.Polyline.Entity.User;

namespace SamSoarII.Polyline.Control;

public class PolylineUserDataSetting : UserControl, IComponentConnector
{
	protected static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(IList<IPolylineUserFormat>), typeof(PolylineUserDataSetting), new PropertyMetadata(new IPolylineUserFormat[0], OnPropertyChanged_Items));

	internal PolylineUserDataSetting This;

	internal Button BN_Yes;

	internal Button BN_No;

	internal Button BN_Add;

	internal Button BN_Insert;

	internal Button BN_Remove;

	internal Button BN_Up;

	internal Button BN_Down;

	internal DataGrid DG_Main;

	private bool _contentLoaded;

	public IList<IPolylineUserFormat> Items
	{
		get
		{
			return (IList<IPolylineUserFormat>)GetValue(ItemsProperty);
		}
		set
		{
			SetValue(ItemsProperty, value);
		}
	}

	public event RoutedEventHandler Yes;

	public event RoutedEventHandler No;

	public PolylineUserDataSetting()
	{
		InitializeComponent();
	}

	private static void OnPropertyChanged_Items(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineUserDataSetting)
		{
			((PolylineUserDataSetting)d).OnItemsChanged(e);
		}
	}

	protected virtual void OnItemsChanged(DependencyPropertyChangedEventArgs e)
	{
		if (e.OldValue is ObservableCollection<IPolylineUserFormat>)
		{
			ObservableCollection<IPolylineUserFormat> observableCollection = (ObservableCollection<IPolylineUserFormat>)e.OldValue;
			observableCollection.CollectionChanged -= OnItemsCollectionChanged;
		}
		if (e.NewValue is ObservableCollection<IPolylineUserFormat>)
		{
			ObservableCollection<IPolylineUserFormat> observableCollection2 = (ObservableCollection<IPolylineUserFormat>)e.NewValue;
			observableCollection2.CollectionChanged += OnItemsCollectionChanged;
		}
	}

	private void OnItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		for (int i = 0; i < Items.Count(); i++)
		{
			Items[i].ID = i;
		}
	}

	private void Invoke(PolylineUserDataEventHandler handler)
	{
		PolylineUserDataEventArgs e = new PolylineUserDataEventArgs(DG_Main.SelectedItems.Cast<IPolylineUserFormat>());
		handler?.Invoke(this, e);
	}

	private void BN_Add_Click(object sender, RoutedEventArgs e)
	{
		Items.Add(new PolylineUserFormat("New parameter", PolylineUserDataType.Int));
	}

	private void BN_Insert_Click(object sender, RoutedEventArgs e)
	{
		int selectedIndex = DG_Main.SelectedIndex;
		PolylineUserFormat item = new PolylineUserFormat("New parameter", PolylineUserDataType.Int);
		if (selectedIndex >= 0)
		{
			Items.Insert(selectedIndex, item);
		}
		else
		{
			Items.Add(item);
		}
	}

	private void BN_Remove_Click(object sender, RoutedEventArgs e)
	{
		IPolylineUserFormat[] array = DG_Main.SelectedItems.Cast<IPolylineUserFormat>().ToArray();
		foreach (IPolylineUserFormat item in array)
		{
			Items.Remove(item);
		}
	}

	private void BN_Up_Click(object sender, RoutedEventArgs e)
	{
		int selectedIndex = DG_Main.SelectedIndex;
		if (selectedIndex > 0)
		{
			IPolylineUserFormat item = Items[selectedIndex];
			Items.Remove(item);
			Items.Insert(selectedIndex - 1, item);
		}
	}

	private void BN_Down_Click(object sender, RoutedEventArgs e)
	{
		int selectedIndex = DG_Main.SelectedIndex;
		if (selectedIndex >= 0 && selectedIndex + 1 < Items.Count())
		{
			IPolylineUserFormat item = Items[selectedIndex];
			Items.Remove(item);
			Items.Insert(selectedIndex + 1, item);
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
			Uri resourceLocator = new Uri("/SamSoarII.Polyline;component/control/polylineuserdatasetting.xaml", UriKind.Relative);
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
			This = (PolylineUserDataSetting)target;
			break;
		case 2:
			BN_Yes = (Button)target;
			BN_Yes.Click += BN_Yes_Click;
			break;
		case 3:
			BN_No = (Button)target;
			BN_No.Click += BN_No_Click;
			break;
		case 4:
			BN_Add = (Button)target;
			BN_Add.Click += BN_Add_Click;
			break;
		case 5:
			BN_Insert = (Button)target;
			BN_Insert.Click += BN_Insert_Click;
			break;
		case 6:
			BN_Remove = (Button)target;
			BN_Remove.Click += BN_Remove_Click;
			break;
		case 7:
			BN_Up = (Button)target;
			BN_Up.Click += BN_Up_Click;
			break;
		case 8:
			BN_Down = (Button)target;
			BN_Down.Click += BN_Down_Click;
			break;
		case 9:
			DG_Main = (DataGrid)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}

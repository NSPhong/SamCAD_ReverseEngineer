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
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using SamSoarII.Dock.Interface;
using SamSoarII.Polyline.Entity;
using SamSoarII.Polyline.Entity.User;

namespace SamSoarII.Polyline.Control;

public class PolylineDataGridWindow : UserControl, IDockContent, INotifyPropertyChanged, IComponentConnector, IStyleConnector
{
	protected static readonly DependencyProperty ProjectProperty = DependencyProperty.Register("Project", typeof(PolylineProject), typeof(PolylineDataGridWindow), new PropertyMetadata(null, OnPropertyChanged_Project));

	public static readonly DependencyProperty SelectedImageProperty = DependencyProperty.Register("SelectedImage", typeof(IPolylineImage), typeof(PolylineDataGridWindow), new PropertyMetadata(null, OnPropertyChanged_SelectedImage));

	public static readonly DependencyProperty SelectedEntityProperty = DependencyProperty.Register("SelectedEntity", typeof(IPolylineEntity), typeof(PolylineDataGridWindow), new PropertyMetadata(null, OnPropertyChanged_SelectedEntity));

	protected static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(PolylineDataGridItem), typeof(PolylineDataGridWindow), new PropertyMetadata(null, OnPropertyChanged_SelectedItem));

	protected static readonly DependencyProperty ItemsProperty = DependencyProperty.Register("Items", typeof(IList<PolylineDataGridItem>), typeof(PolylineDataGridWindow), new PropertyMetadata(null, OnPropertyChanged_Items));

	internal PolylineDataGridWindow This;

	internal Button BN_Add;

	internal Button BN_Remove;

	internal Button BN_USet;

	internal Button BN_UMod;

	internal Button BN_Import;

	internal Button BN_Export;

	internal DataGrid DG_Main;

	internal DataGridTextColumn DGC_ID;

	internal DataGridTextColumn DGC_X;

	internal DataGridTextColumn DGC_Y;

	internal DataGridComboBoxColumn DGC_Type;

	internal DataGridTextColumn DGC_Radius;

	internal DataGridTextColumn DGC_CenterX;

	internal DataGridTextColumn DGC_CenterY;

	private bool _contentLoaded;

	public ushort DockID => 10001;

	public ImageSource Icon => null;

	public string Header => "Tabular data";

	public PolylineProject Project
	{
		get
		{
			return (PolylineProject)GetValue(ProjectProperty);
		}
		set
		{
			SetValue(ProjectProperty, value);
		}
	}

	public IPolylineImage SelectedImage
	{
		get
		{
			return (IPolylineImage)GetValue(SelectedImageProperty);
		}
		set
		{
			SetValue(SelectedImageProperty, value);
		}
	}

	public IPolylineEntity SelectedEntity
	{
		get
		{
			return (IPolylineEntity)GetValue(SelectedEntityProperty);
		}
		set
		{
			SetValue(SelectedEntityProperty, value);
		}
	}

	public PolylineDataGridItem SelectedItem
	{
		get
		{
			return (PolylineDataGridItem)GetValue(SelectedItemProperty);
		}
		set
		{
			SetValue(SelectedItemProperty, value);
		}
	}

	public IList<PolylineDataGridItem> Items
	{
		get
		{
			return (IList<PolylineDataGridItem>)GetValue(ItemsProperty);
		}
		set
		{
			SetValue(ItemsProperty, value);
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public event PolylineActionEventHandler EntityInsert;

	public event PolylineActionEventHandler EntityRemove;

	public event PolylineActionEventHandler EntityReplace;

	public event PolylineActionEventHandler EntityChanged;

	public PolylineDataGridWindow()
	{
		InitializeComponent();
		PolylineDataGridUserColumn.ShowTemplateT = (DataTemplate)base.Resources["DT_ShowTemplateT"];
		PolylineDataGridUserColumn.EditTemplateT = (DataTemplate)base.Resources["DT_EditTemplateT"];
		PolylineDataGridUserColumn.ShowTemplateB = (DataTemplate)base.Resources["DT_ShowTemplateB"];
		PolylineDataGridUserColumn.EditTemplateB = (DataTemplate)base.Resources["DT_EditTemplateB"];
	}

	private static void OnPropertyChanged_Project(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineDataGridWindow)
		{
			((PolylineDataGridWindow)d).OnProjectChanged(e);
		}
	}

	protected virtual void OnProjectChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_SelectedImage(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineDataGridWindow)
		{
			((PolylineDataGridWindow)d).OnSelectedImageChanged(e);
		}
	}

	protected virtual void OnSelectedImageChanged(DependencyPropertyChangedEventArgs e)
	{
		if (e.OldValue is PolylineImage)
		{
			PolylineImage polylineImage = (PolylineImage)e.OldValue;
			polylineImage.ItemsChanged -= OnSelectedImageItemsChanged;
			polylineImage.UserFmtsChanged -= OnSelectedImageUserFmtsChanged;
			polylineImage.Undoed -= OnSelectedImageUndoed;
			polylineImage.Redoed -= OnSelectedImageRedoed;
		}
		if (e.NewValue is PolylineImage)
		{
			PolylineImage polylineImage2 = (PolylineImage)e.NewValue;
			polylineImage2.ItemsChanged += OnSelectedImageItemsChanged;
			polylineImage2.UserFmtsChanged += OnSelectedImageUserFmtsChanged;
			polylineImage2.Undoed += OnSelectedImageUndoed;
			polylineImage2.Redoed += OnSelectedImageRedoed;
		}
		ItemsRefresh();
	}

	private void OnSelectedImageUserFmtsChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		ColumnsRefresh();
	}

	private void OnSelectedImageRedoed(object sender, PolylineActionEventArgs e)
	{
		if (base.IsVisible)
		{
			IPolylineAction action = e.Action;
			if (action is PolylineActionCollection || action.Target == ChangedTarget.None)
			{
				ItemsRefresh();
			}
		}
	}

	private void OnSelectedImageUndoed(object sender, PolylineActionEventArgs e)
	{
		if (base.IsVisible)
		{
			IPolylineAction action = e.Action;
			if (action is PolylineActionCollection || action.Target == ChangedTarget.None)
			{
				ItemsRefresh();
			}
		}
	}

	private void OnSelectedImageItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		if (!base.IsVisible)
		{
			return;
		}
		if (e.Action == NotifyCollectionChangedAction.Reset)
		{
			ItemsRefresh();
			return;
		}
		if (e.OldItems != null && e.NewItems != null && e.OldItems.Count > 0 && e.NewItems.Count > 0)
		{
			ItemsRefresh();
			return;
		}
		if (e.OldItems != null && e.OldItems.Count > 5)
		{
			ItemsRefresh();
			return;
		}
		if (e.NewItems != null && e.NewItems.Count > 5)
		{
			ItemsRefresh();
			return;
		}
		if (e.OldItems != null && e.OldItems.Count > 0)
		{
			for (int i = 0; i < e.OldItems.Count; i++)
			{
				Items[e.OldStartingIndex].Dispose();
				Items.RemoveAt(e.OldStartingIndex);
			}
		}
		if (e.NewItems != null && e.NewItems.Count > 0)
		{
			for (int num = e.NewItems.Count - 1; num >= 0; num--)
			{
				Items.Insert(e.NewStartingIndex, new PolylineDataGridItem(this)
				{
					Core = (IPolylineEntity)e.NewItems[num]
				});
			}
		}
	}

	private static void OnPropertyChanged_SelectedEntity(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineDataGridWindow)
		{
			((PolylineDataGridWindow)d).OnSelectedEntityChanged(e);
		}
	}

	protected virtual void OnSelectedEntityChanged(DependencyPropertyChangedEventArgs e)
	{
		SelectedItem = ((Items != null && SelectedEntity != null && Items.Count > SelectedEntity.ID) ? Items[SelectedEntity.ID] : null);
	}

	private static void OnPropertyChanged_SelectedItem(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineDataGridWindow)
		{
			((PolylineDataGridWindow)d).OnSelectedItemChanged(e);
		}
	}

	protected virtual void OnSelectedItemChanged(DependencyPropertyChangedEventArgs e)
	{
		SelectedEntity = SelectedItem?.Core;
	}

	private static void OnPropertyChanged_Items(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineDataGridWindow)
		{
			((PolylineDataGridWindow)d).OnItemsChanged(e);
		}
	}

	protected virtual void OnItemsChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	public void ItemsRefresh()
	{
		if (Items != null)
		{
			foreach (PolylineDataGridItem item in Items)
			{
				item.Dispose();
			}
		}
		ObservableCollection<PolylineDataGridItem> observableCollection = new ObservableCollection<PolylineDataGridItem>();
		if (SelectedImage != null)
		{
			foreach (IPolylineEntity item2 in SelectedImage.Items)
			{
				observableCollection.Add(new PolylineDataGridItem(this)
				{
					Core = item2
				});
			}
		}
		Items = observableCollection;
	}

	public void ColumnsRefresh()
	{
		DG_Main.Columns.Clear();
		DG_Main.Columns.Add(DGC_ID);
		DG_Main.Columns.Add(DGC_X);
		DG_Main.Columns.Add(DGC_Y);
		DG_Main.Columns.Add(DGC_Type);
		DG_Main.Columns.Add(DGC_Radius);
		DG_Main.Columns.Add(DGC_CenterX);
		DG_Main.Columns.Add(DGC_CenterY);
		foreach (IPolylineUserFormat userFmt in SelectedImage.UserFmts)
		{
			DG_Main.Columns.Add(ColumnCreate(userFmt));
		}
	}

	private DataGridColumn ColumnCreate(IPolylineUserFormat uf)
	{
		return new PolylineDataGridUserColumn
		{
			Core = uf
		};
	}

	public void SetupCommands(IList<RoutedCommand> cmds)
	{
		BN_Add.Command = cmds[0];
		BN_Remove.Command = cmds[1];
		BN_Import.Command = cmds[2];
		BN_Export.Command = cmds[3];
		BN_USet.Command = cmds[4];
		BN_UMod.Command = cmds[5];
	}

	public void MultiplyModify(PolylineDataGridMultiplyModifyCore core)
	{
		if (core.IsModifySelected)
		{
			if (DG_Main.SelectedItems == null)
			{
				return;
			}
			PolylineDataGridItem[] array = DG_Main.SelectedItems.Cast<PolylineDataGridItem>().ToArray();
			foreach (PolylineDataGridItem polylineDataGridItem in array)
			{
				polylineDataGridItem.Core.UserObjs[core.SelectedColumn.UserFormat.ID].Value = core.ModifyValue;
				foreach (IPolylineDataGridControl view in polylineDataGridItem.Views)
				{
					if (view.Core == core.SelectedColumn.UserFormat)
					{
						PolylineDataGridHelper.Read(view);
						break;
					}
				}
			}
			return;
		}
		int num = Math.Max(0, Math.Min(Items.Count() - 1, core.StartLine));
		int num2 = Math.Max(0, Math.Min(Items.Count() - 1, core.EndLine));
		for (int j = num; j <= num2; j++)
		{
			Items[j].Core.UserObjs[core.SelectedColumn.UserFormat.ID].Value = core.ModifyValue;
			foreach (IPolylineDataGridControl view2 in Items[j].Views)
			{
				if (view2.Core == core.SelectedColumn.UserFormat)
				{
					PolylineDataGridHelper.Read(view2);
					break;
				}
			}
		}
	}

	protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		base.OnPropertyChanged(e);
		if (e.Property == UIElement.IsVisibleProperty && e.OldValue is bool && e.NewValue is bool && !(bool)e.OldValue && (bool)e.NewValue)
		{
			ItemsRefresh();
		}
	}

	private void BN_Add_Click(object sender, RoutedEventArgs e)
	{
		if (SelectedImage != null)
		{
			IPolylineEntity polylineEntity = null;
			IPolylineEntity polylineEntity2 = SelectedEntity?.Next;
			IPolylineCircle polylineCircle = ((SelectedEntity is IPolylineCircle) ? ((IPolylineCircle)SelectedEntity) : null);
			IPolylineArch polylineArch = ((SelectedEntity is IPolylineArch) ? ((IPolylineArch)SelectedEntity) : null);
			IPolylineCircle polylineCircle2 = ((polylineEntity2 is IPolylineCircle) ? ((IPolylineCircle)polylineEntity2) : null);
			IPolylineArch polylineArch2 = ((polylineEntity2 is IPolylineArch) ? ((IPolylineArch)polylineEntity2) : null);
			Point center = polylineArch?.Center ?? polylineArch2?.Center ?? default(Point);
			bool isclockwise = polylineArch?.IsClockwise ?? polylineArch2?.IsClockwise ?? false;
			bool islarge = polylineArch?.IsLarge ?? polylineArch2?.IsLarge ?? false;
			polylineEntity = ((polylineEntity2 == null) ? new PolylineLine(SelectedImage, SelectedImage.Items.Count(), new Point(0.0, 0.0), _isreal: false) : ((polylineEntity2.Type == PolylineType.Line) ? new PolylineLine(SelectedImage, polylineEntity2.ID, polylineEntity2.From + (polylineEntity2.To - polylineEntity2.From) / 2.0, polylineEntity2.IsReal) : ((polylineEntity2.Type != PolylineType.Arch) ? ((PolylineEntity)new PolylineLine(SelectedImage, polylineEntity2.ID, polylineEntity2.From)) : ((PolylineEntity)new PolylineArch(SelectedImage, polylineEntity2.ID, polylineEntity2.From, center, isclockwise, islarge)))));
			IPolylineAction action = SelectedImage.Insert(polylineEntity.ID, polylineEntity);
			this.EntityInsert?.Invoke(this, new PolylineActionEventArgs(SelectedImage, action));
		}
	}

	private void BN_Remove_Click(object sender, RoutedEventArgs e)
	{
		if (SelectedImage != null && SelectedEntity != null)
		{
			IEnumerable<IPolylineEntity> source = from PolylineDataGridItem i in DG_Main.SelectedItems
				select i.Core;
			IPolylineAction action = SelectedImage.Remove(source.ToArray());
			this.EntityRemove?.Invoke(this, new PolylineActionEventArgs(SelectedImage, action));
		}
	}

	public void InvokeEntityReplace(IPolylineAction action)
	{
		PolylineActionEventArgs e = new PolylineActionEventArgs(SelectedImage, action);
		this.EntityReplace?.Invoke(this, e);
	}

	public void InvokeEntityChange(IPolylineAction action)
	{
		PolylineActionEventArgs e = new PolylineActionEventArgs(SelectedImage, action);
		this.EntityChanged?.Invoke(this, e);
	}

	private void DataGridRow_Loaded(object sender, RoutedEventArgs e)
	{
		if (sender is DataGridRow)
		{
			DataGridRow dataGridRow = (DataGridRow)sender;
			dataGridRow.DataContextChanged += DataGirdRow_DataContextChanged;
			if (dataGridRow.DataContext is PolylineDataGridItem)
			{
				PolylineDataGridItem polylineDataGridItem = (PolylineDataGridItem)dataGridRow.DataContext;
				polylineDataGridItem.Row = dataGridRow;
			}
		}
	}

	private void DataGridRow_Unloaded(object sender, RoutedEventArgs e)
	{
		if (sender is DataGridRow)
		{
			DataGridRow dataGridRow = (DataGridRow)sender;
			dataGridRow.DataContextChanged -= DataGirdRow_DataContextChanged;
			if (dataGridRow.DataContext is PolylineDataGridItem)
			{
				PolylineDataGridItem polylineDataGridItem = (PolylineDataGridItem)dataGridRow.DataContext;
				polylineDataGridItem.Row = null;
			}
		}
	}

	private void DataGirdRow_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		if (sender is DataGridRow)
		{
			DataGridRow row = (DataGridRow)sender;
			if (e.OldValue is PolylineDataGridItem)
			{
				PolylineDataGridItem polylineDataGridItem = (PolylineDataGridItem)e.OldValue;
				polylineDataGridItem.Row = null;
			}
			if (e.NewValue is PolylineDataGridItem)
			{
				PolylineDataGridItem polylineDataGridItem2 = (PolylineDataGridItem)e.NewValue;
				polylineDataGridItem2.Row = row;
			}
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamSoarII.Polyline;component/control/polylinedatagridwindow.xaml", UriKind.Relative);
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
			This = (PolylineDataGridWindow)target;
			break;
		case 2:
			BN_Add = (Button)target;
			BN_Add.Click += BN_Add_Click;
			break;
		case 3:
			BN_Remove = (Button)target;
			BN_Remove.Click += BN_Remove_Click;
			break;
		case 4:
			BN_USet = (Button)target;
			break;
		case 5:
			BN_UMod = (Button)target;
			break;
		case 6:
			BN_Import = (Button)target;
			break;
		case 7:
			BN_Export = (Button)target;
			break;
		case 8:
			DG_Main = (DataGrid)target;
			break;
		case 10:
			DGC_ID = (DataGridTextColumn)target;
			break;
		case 11:
			DGC_X = (DataGridTextColumn)target;
			break;
		case 12:
			DGC_Y = (DataGridTextColumn)target;
			break;
		case 13:
			DGC_Type = (DataGridComboBoxColumn)target;
			break;
		case 14:
			DGC_Radius = (DataGridTextColumn)target;
			break;
		case 15:
			DGC_CenterX = (DataGridTextColumn)target;
			break;
		case 16:
			DGC_CenterY = (DataGridTextColumn)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IStyleConnector.Connect(int connectionId, object target)
	{
		if (connectionId == 9)
		{
			EventSetter eventSetter = new EventSetter();
			eventSetter.Event = FrameworkElement.LoadedEvent;
			eventSetter.Handler = new RoutedEventHandler(DataGridRow_Loaded);
			((Style)target).Setters.Add(eventSetter);
			eventSetter = new EventSetter();
			eventSetter.Event = FrameworkElement.UnloadedEvent;
			eventSetter.Handler = new RoutedEventHandler(DataGridRow_Unloaded);
			((Style)target).Setters.Add(eventSetter);
		}
	}
}

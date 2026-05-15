using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
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

namespace SamSoarII.Polyline.Control;

public class PolylineSelectWindow : UserControl, IDockContent, INotifyPropertyChanged, IComponentConnector
{
	protected static readonly DependencyProperty ProjectProperty = DependencyProperty.Register("Project", typeof(IPolylineProject), typeof(PolylineSelectWindow), new PropertyMetadata(null, OnPropertyChanged_Project));

	public static readonly DependencyProperty SelectedImageProperty = DependencyProperty.Register("SelectedImage", typeof(IPolylineImage), typeof(PolylineSelectWindow), new PropertyMetadata(null, OnPropertyChanged_SelectedImage));

	public static readonly DependencyProperty SelectedEntityProperty = DependencyProperty.Register("SelectedEntity", typeof(IPolylineEntity), typeof(PolylineSelectWindow), new PropertyMetadata(null, OnPropertyChanged_SelectedEntity));

	protected static readonly DependencyProperty IsReorderModeProperty = DependencyProperty.Register("IsReorderMode", typeof(bool), typeof(PolylineSelectWindow), new PropertyMetadata(false, OnPropertyChanged_IsReorderMode));

	private bool _invoke_selectionchanged = true;

	internal PolylineSelectWindow This;

	internal ComboBox CB_Image;

	internal Button BN_InteSelect;

	internal PolylineSelectTreeBox UI_Main;

	private bool _contentLoaded;

	public IPolylineProject Project
	{
		get
		{
			return (IPolylineProject)GetValue(ProjectProperty);
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

	public bool IsReorderMode
	{
		get
		{
			return (bool)GetValue(IsReorderModeProperty);
		}
		protected set
		{
			SetValue(IsReorderModeProperty, value);
		}
	}

	public IEnumerable<IPolylineEntity> SelectedEntities => SelectedImage?.SelectedEntities ?? new IPolylineEntity[0];

	public int SelectedStart
	{
		get
		{
			return SelectedImage?.SelectedStart ?? 0;
		}
		set
		{
		}
	}

	public int SelectedCount
	{
		get
		{
			return SelectedImage?.SelectedCount ?? 0;
		}
		set
		{
		}
	}

	public IList<IPolylineEntity> Reorders => UI_Main?.Reorders;

	public IList<IPolylineReorderingGroup> Moveds => UI_Main?.Moveds;

	ushort IDockContent.DockID => 13;

	string IDockContent.Header => "List of graphic elements";

	ImageSource IDockContent.Icon => null;

	public event DependencyPropertyChangedEventHandler SelectedImageChanged;

	public event DependencyPropertyChangedEventHandler SelectedEntityChanged;

	public event PropertyChangedEventHandler PropertyChanged;

	public event SelectionChangedEventHandler SelectionChanged;

	public event RoutedEventHandler InteSelect;

	public PolylineSelectWindow()
	{
		InitializeComponent();
	}

	private static void OnPropertyChanged_Project(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineSelectWindow)
		{
			((PolylineSelectWindow)d).OnProjectChanged(e);
		}
	}

	protected virtual void OnProjectChanged(DependencyPropertyChangedEventArgs e)
	{
		if (Project != null)
		{
			CB_Image.SelectedItem = Project.Items.FirstOrDefault();
		}
	}

	private static void OnPropertyChanged_SelectedImage(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineSelectWindow)
		{
			((PolylineSelectWindow)d).OnSelctedImageChanged(e);
		}
	}

	protected virtual void OnSelctedImageChanged(DependencyPropertyChangedEventArgs e)
	{
		this.SelectedImageChanged?.Invoke(this, e);
	}

	private static void OnPropertyChanged_SelectedEntity(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineSelectWindow)
		{
			((PolylineSelectWindow)d).OnSelctedEntityChanged(e);
		}
	}

	protected virtual void OnSelctedEntityChanged(DependencyPropertyChangedEventArgs e)
	{
		this.SelectedEntityChanged?.Invoke(this, e);
	}

	private static void OnPropertyChanged_IsReorderMode(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineSelectWindow)
		{
			((PolylineSelectWindow)d).OnIsReorderModeChanged(e);
		}
	}

	protected virtual void OnIsReorderModeChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	public void InvalidateItems()
	{
		UI_Main.InvalidateItems();
	}

	public void UpdateRedo(IPolylineAction action)
	{
		if (action == null)
		{
			return;
		}
		int num = 0;
		int num2 = 0;
		bool flag = _NeedInvalidateItems(action);
		while (action is IPolylineActionCollection)
		{
			IPolylineActionCollection polylineActionCollection = (IPolylineActionCollection)action;
			action = polylineActionCollection.Items.LastOrDefault();
		}
		if (action.AddedItems != null && action.AddedItems.Count > 0)
		{
			num = action.Index;
			num2 = action.AddedItems.Count;
			string message = action.Message;
			string text = message;
			if (text == "EntityRound" || text == "EntityBevel")
			{
				num++;
				num2 = 1;
			}
		}
		else if (action.Target != ChangedTarget.None)
		{
			num = action.Index;
			num2 = 1;
		}
		else
		{
			num = 0;
			num2 = 0;
		}
		if (!flag)
		{
			Select(num, num2);
			return;
		}
		SelectedImage.Select(num, num2);
		InvalidateItems();
	}

	public void UpdateUndo(IPolylineAction action)
	{
		if (action != null)
		{
			int num = 0;
			int num2 = 0;
			bool flag = _NeedInvalidateItems(action);
			while (action is IPolylineActionCollection)
			{
				IPolylineActionCollection polylineActionCollection = (IPolylineActionCollection)action;
				action = polylineActionCollection.Items.FirstOrDefault();
			}
			if (action.RemovedItems != null && action.RemovedItems.Count > 0)
			{
				num = action.Index;
				num2 = action.RemovedItems.Count;
			}
			else if (action.Target != ChangedTarget.None)
			{
				num = action.Index;
				num2 = 1;
			}
			else
			{
				num = 0;
				num2 = 0;
			}
			if (!flag)
			{
				Select(num, num2);
				return;
			}
			SelectedImage.Select(num, num2);
			InvalidateItems();
		}
	}

	protected bool _NeedInvalidateItems(IPolylineAction action)
	{
		if (action is IPolylineActionCollection)
		{
			IPolylineActionCollection polylineActionCollection = (IPolylineActionCollection)action;
			foreach (IPolylineAction item in polylineActionCollection.Items)
			{
				if (_NeedInvalidateItems(item))
				{
					return true;
				}
			}
			return false;
		}
		if (action.AddedItems != null && action.AddedItems.Count > 0)
		{
			return true;
		}
		if (action.RemovedItems != null && action.RemovedItems.Count > 0)
		{
			return true;
		}
		if (action.Target == ChangedTarget.IsReal)
		{
			return true;
		}
		return false;
	}

	public void Select(int _start, int _count)
	{
		UI_Main.Select(_start, _count);
	}

	public void SelectSingle(int _index)
	{
		if (SelectedImage != null)
		{
			IPolylineEntity polylineEntity = SelectedImage.Items[_index];
			IPolylineGroup polylineGroup = polylineEntity?.Group;
			if (polylineGroup != null && !polylineGroup.IsExpand)
			{
				polylineGroup.IsExpand = true;
				UI_Main.InvalidateItems();
			}
			UI_Main.SelectSingle(polylineEntity);
		}
	}

	public void SelectShift(int _index)
	{
		if (SelectedEntity == null)
		{
			SelectSingle(_index);
		}
		else
		{
			UI_Main.SelectShift(_index);
		}
	}

	public void SelectCtrl(int _index)
	{
		if (SelectedEntity == null)
		{
			SelectSingle(_index);
		}
		else
		{
			UI_Main.SelectCtrl(_index);
		}
	}

	public void SelectKeyboard(int _index)
	{
		switch (Keyboard.PrimaryDevice.Modifiers)
		{
		case ModifierKeys.None:
			SelectSingle(_index);
			break;
		case ModifierKeys.Shift:
			SelectShift(_index);
			break;
		case ModifierKeys.Control:
			SelectCtrl(_index);
			break;
		case ModifierKeys.Alt:
		case ModifierKeys.Alt | ModifierKeys.Control:
			break;
		}
	}

	public void ReorderBegin()
	{
		UI_Main.ReorderBegin();
		IsReorderMode = true;
	}

	public void ReorderEnd()
	{
		UI_Main.ReorderEnd();
		IsReorderMode = false;
	}

	public void ReorderEscape()
	{
		UI_Main.ReorderEscape();
		IsReorderMode = false;
	}

	protected void InvokePropertyChanged(string propname)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
	}

	private void UI_Main_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		if (_invoke_selectionchanged)
		{
			InvokePropertyChanged("SelectedEntities");
			InvokePropertyChanged("SelectedStart");
			InvokePropertyChanged("SelectedCount");
			this.SelectionChanged?.Invoke(this, e);
		}
	}

	private void BN_InteSelect_Click(object sender, RoutedEventArgs e)
	{
		this.InteSelect?.Invoke(this, e);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamSoarII.Polyline;component/control/polylineselectwindow.xaml", UriKind.Relative);
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
			This = (PolylineSelectWindow)target;
			break;
		case 2:
			CB_Image = (ComboBox)target;
			break;
		case 3:
			BN_InteSelect = (Button)target;
			BN_InteSelect.Click += BN_InteSelect_Click;
			break;
		case 4:
			UI_Main = (PolylineSelectTreeBox)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}

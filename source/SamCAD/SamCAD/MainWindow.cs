using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using Microsoft.Win32;
using SamCAD.Control;
using SamSoarII.Dock;
using SamSoarII.Dock.View;
using SamSoarII.Polyline;
using SamSoarII.Polyline.Arguments;
using SamSoarII.Polyline.Control;
using SamSoarII.Polyline.Entity;
using SamSoarII.Polyline.Entity.User;
using SamSoarII.Polyline.Export;
using SamSoarII.Utility;

namespace SamCAD;

public class MainWindow : Window, IComponentConnector
{
	protected static readonly DependencyProperty ProjectProperty = DependencyProperty.Register("Project", typeof(IPolylineProject), typeof(MainWindow), new PropertyMetadata(null, OnPropertyChanged_Project));

	protected static readonly DependencyProperty SelectedImageProperty = DependencyProperty.Register("SelectdImage", typeof(IPolylineImage), typeof(MainWindow), new PropertyMetadata(null, OnPropertyChanged_SelectedImage));

	private List<DialogWidget> dialogs;

	private int dialogs_usedcount;

	private PolylineEditor ui_editor;

	private PolylineSelectWindow ui_select;

	private PolylineSelectContextMenu cm_select;

	private PolylineArgumentWindow ui_argument;

	private PolylineDataGridWindow ui_datagrid;

	private WaitingWindow ui_waiting;

	private CreateProjectWindow ui_createproject;

	private CreateRectWindow ui_createrect;

	private CreateFreeRectWindow ui_createfreerect;

	private CreateEllipseWindow ui_createellipse;

	private GroupMoveWindow ui_groupmove;

	private GroupScaleWindow ui_groupscale;

	private GroupRotateWindow ui_grouprotate;

	private GroupReorderWindow ui_groupreorder;

	private GroupMatrixWindow ui_groupmatrix;

	private GroupExpandWindow ui_groupexpand;

	private GroupFillWindow ui_groupfill;

	private ImageResizeWindow ui_imageresize;

	private AboutWindow ui_about;

	private PolylineExportWindow ui_export;

	private PolylineExportColumnAddWindow ui_ec_add;

	private PolylineUserDataSetting ui_uset;

	private PolylineDataGridMultiplyModifyWindow ui_mulmod;

	private LoadModeWindow ui_loadmode;

	private InteSelectWindow ui_inteselect;

	private IPolylineRect creatingrect;

	private IPolylineFreeRect creatingfreerect;

	private IPolylineEllipse creatingellipse;

	private bool isclosing = false;

	private bool forceclose = false;

	internal Grid GD_Main;

	internal Menu UI_Menu;

	internal MenuItem MI_File;

	internal DockManager UI_Dock;

	internal UnderBar UI_Under;

	internal DialogWidgetWithLeak UI_Leak;

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

	public PolylineEditor UI_Editor => ui_editor;

	public PolylineSelectWindow UI_Select => ui_select;

	public PolylineSelectContextMenu CM_Select => cm_select;

	public PolylineArgumentWindow UI_Argument => ui_argument;

	public PolylineDataGridWindow UI_DataGrid => ui_datagrid;

	public WaitingWindow UI_Waiting => ui_waiting;

	public CreateProjectWindow UI_CreateProject => ui_createproject;

	public CreateRectWindow UI_CreateRect => ui_createrect;

	public CreateFreeRectWindow UI_CreateFreeRect => ui_createfreerect;

	public CreateEllipseWindow UI_CreateEllipse => ui_createellipse;

	public GroupMoveWindow UI_GroupMove => ui_groupmove;

	public GroupScaleWindow UI_GroupScale => ui_groupscale;

	public GroupRotateWindow UI_GroupRotate => ui_grouprotate;

	public GroupReorderWindow UI_GroupReorder => ui_groupreorder;

	public GroupMatrixWindow UI_GroupMatrix => ui_groupmatrix;

	public GroupExpandWindow UI_GroupExpand => ui_groupexpand;

	public GroupFillWindow UI_GroupFill => ui_groupfill;

	public ImageResizeWindow UI_ImageResize => ui_imageresize;

	public AboutWindow UI_About => ui_about;

	public PolylineExportWindow UI_Export => ui_export;

	public PolylineExportColumnAddWindow UI_EC_Add => ui_ec_add;

	public PolylineUserDataSetting UI_USet => ui_uset;

	public PolylineDataGridMultiplyModifyWindow UI_MulMod => ui_mulmod;

	public LoadModeWindow UI_LoadMode => ui_loadmode;

	public InteSelectWindow UI_InteSelect => ui_inteselect;

	public IPolylineRect CreatingRect => creatingrect;

	public IPolylineFreeRect CreatingFreeRect => creatingfreerect;

	public IPolylineEllipse CreatingEllipse => creatingellipse;

	public MainWindow()
	{
		InitializeComponent();
		ui_editor = new PolylineEditor();
		ui_select = new PolylineSelectWindow();
		ui_datagrid = new PolylineDataGridWindow();
		cm_select = new PolylineSelectContextMenu();
		ui_argument = new PolylineArgumentWindow();
		ui_waiting = new WaitingWindow();
		ui_createproject = new CreateProjectWindow();
		ui_createrect = new CreateRectWindow();
		ui_createfreerect = new CreateFreeRectWindow();
		ui_createellipse = new CreateEllipseWindow();
		ui_groupmove = new GroupMoveWindow();
		ui_groupscale = new GroupScaleWindow();
		ui_grouprotate = new GroupRotateWindow();
		ui_groupreorder = new GroupReorderWindow();
		ui_groupmatrix = new GroupMatrixWindow();
		ui_groupexpand = new GroupExpandWindow();
		ui_groupfill = new GroupFillWindow();
		ui_imageresize = new ImageResizeWindow();
		ui_about = new AboutWindow();
		ui_export = new PolylineExportWindow();
		ui_uset = new PolylineUserDataSetting();
		ui_ec_add = new PolylineExportColumnAddWindow();
		ui_mulmod = new PolylineDataGridMultiplyModifyWindow();
		ui_loadmode = new LoadModeWindow();
		ui_inteselect = new InteSelectWindow();
		dialogs = new List<DialogWidget>();
		dialogs_usedcount = 0;
		creatingrect = null;
		ui_select.ContextMenu = cm_select;
		ui_select.SelectedImageChanged += UI_Select_SelectedImageChanged;
		ui_select.SelectionChanged += UI_Select_SelectionChanged;
		ui_editor.EntityClick += UI_Editor_EntityClick;
		ui_editor.EntityInsert += UI_Editor_EntityInsert;
		ui_editor.EntityAction += UI_Editor_EntityAction;
		ui_editor.EntityUndo += UI_Editor_EntityUndo;
		ui_editor.EntitySetting += UI_Editor_EntitySetting;
		ui_editor.MouseActionStart += UI_Editor_MouseActionStart;
		ui_editor.MouseActionDone += UI_Editor_MouseActionDone;
		ui_editor.MouseActionEscape += UI_Editor_MouseActionEscape;
		ui_editor.MousePositionMove += UI_Editor_MousePositionMove;
		ui_datagrid.EntityInsert += UI_DataGrid_EntityInsert;
		ui_datagrid.EntityRemove += UI_DataGrid_EntityRemove;
		ui_datagrid.EntityReplace += UI_DataGrid_EntityReplace;
		ui_datagrid.EntityChanged += UI_DataGrid_EntityChanged;
		ui_argument.MultiModify += UI_Argument_MultiModify;
		ui_argument.EntityChanged += UI_Argument_EntityChanged;
		ui_createproject.Yes += UI_CreateProject_Yes;
		ui_createproject.No += UI_CreateProject_No;
		ui_createrect.Yes += UI_CreateRect_Yes;
		ui_createrect.No += UI_CreateRect_No;
		ui_createfreerect.Yes += UI_CreateFreeRect_Yes;
		ui_createfreerect.No += UI_CreateFreeRect_No;
		ui_createellipse.Yes += UI_CreateEllipse_Yes;
		ui_createellipse.No += UI_CreateEllipse_No;
		GroupMoveWindow groupMoveWindow = ui_groupmove;
		groupMoveWindow.Yes = (RoutedEventHandler)Delegate.Combine(groupMoveWindow.Yes, new RoutedEventHandler(UI_GroupMove_Yes));
		GroupMoveWindow groupMoveWindow2 = ui_groupmove;
		groupMoveWindow2.No = (RoutedEventHandler)Delegate.Combine(groupMoveWindow2.No, new RoutedEventHandler(UI_GroupMove_No));
		ui_groupscale.Yes += UI_GroupScale_Yes;
		ui_groupscale.No += UI_GroupScale_No;
		GroupRotateWindow groupRotateWindow = ui_grouprotate;
		groupRotateWindow.Yes = (RoutedEventHandler)Delegate.Combine(groupRotateWindow.Yes, new RoutedEventHandler(UI_GroupRotate_Yes));
		GroupRotateWindow groupRotateWindow2 = ui_grouprotate;
		groupRotateWindow2.No = (RoutedEventHandler)Delegate.Combine(groupRotateWindow2.No, new RoutedEventHandler(UI_GroupRotate_No));
		ui_groupreorder.Yes += UI_GroupReorder_Yes;
		ui_groupreorder.No += UI_GroupReorder_No;
		ui_groupmatrix.Yes += UI_GroupMatrix_Yes;
		ui_groupmatrix.No += UI_GroupMatrix_No;
		ui_groupexpand.Yes += UI_GroupExpand_Yes;
		ui_groupexpand.No += UI_GroupExpand_No;
		ui_groupfill.Yes += UI_GroupFill_Yes;
		ui_groupfill.No += UI_GroupFill_No;
		ImageResizeWindow imageResizeWindow = ui_imageresize;
		imageResizeWindow.Yes = (RoutedEventHandler)Delegate.Combine(imageResizeWindow.Yes, new RoutedEventHandler(UI_ImageResize_Yes));
		ImageResizeWindow imageResizeWindow2 = ui_imageresize;
		imageResizeWindow2.No = (RoutedEventHandler)Delegate.Combine(imageResizeWindow2.No, new RoutedEventHandler(UI_ImageResize_No));
		ui_about.Yes += UI_About_Yes;
		ui_export.ColAdd += UI_Export_ColAdd;
		ui_export.All += UI_Export_All;
		ui_export.Export += UI_Export_Export;
		ui_export.Cancel += UI_Export_Cancel;
		ui_uset.Yes += UI_USet_Yes;
		ui_uset.No += UI_USet_No;
		ui_ec_add.Yes += UI_EC_Add_Yes;
		ui_ec_add.No += UI_EC_Add_No;
		ui_mulmod.Ensure += UI_MulMod_Yes;
		ui_mulmod.Cancel += UI_MulMod_No;
		LoadModeWindow loadModeWindow = ui_loadmode;
		loadModeWindow.Yes = (RoutedEventHandler)Delegate.Combine(loadModeWindow.Yes, new RoutedEventHandler(UI_LoadMode_Yes));
		LoadModeWindow loadModeWindow2 = ui_loadmode;
		loadModeWindow2.No = (RoutedEventHandler)Delegate.Combine(loadModeWindow2.No, new RoutedEventHandler(UI_LoadMode_No));
		ui_select.InteSelect += UI_Select_InteSelect;
		ui_inteselect.Select += UI_InteSelect_Select;
		base.Loaded += OnWindowLoaded;
		base.Unloaded += OnWindowOnloaded;
		SetupCommands();
		SetupBindings();
	}

	private static void OnPropertyChanged_Project(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is MainWindow)
		{
			((MainWindow)d).OnProjectChanged(e);
		}
	}

	protected virtual void OnProjectChanged(DependencyPropertyChangedEventArgs e)
	{
		ui_editor.Project = Project;
		ui_select.Project = Project;
		ui_argument.Project = Project;
		if (Project != null)
		{
			UI_Dock.Show(ui_editor);
			UI_Dock.Show(ui_select);
			UI_Dock.Show(ui_argument);
		}
		else
		{
			UI_Dock.Hide(ui_editor);
			UI_Dock.Hide(ui_select);
			UI_Dock.Hide(ui_argument);
		}
	}

	private static void OnPropertyChanged_SelectedImage(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is MainWindow)
		{
			((MainWindow)d).OnSelectedImageChanged(e);
		}
	}

	protected virtual void OnSelectedImageChanged(DependencyPropertyChangedEventArgs e)
	{
		if (e.OldValue is IPolylineImage)
		{
			IPolylineImage polylineImage = (IPolylineImage)e.OldValue;
			if (polylineImage.Argument != null)
			{
				polylineImage.Argument.PropertyChanged -= OnImageArgumentPropertyChanged;
			}
		}
		if (e.NewValue is IPolylineImage)
		{
			IPolylineImage polylineImage2 = (IPolylineImage)e.NewValue;
			if (polylineImage2.Argument != null)
			{
				polylineImage2.Argument.PropertyChanged += OnImageArgumentPropertyChanged;
			}
		}
		ui_editor.SelectedImage = SelectedImage;
		ui_argument.SelectedImage = SelectedImage;
	}

	public bool DialogIsFree()
	{
		return dialogs_usedcount == 0 && !UI_Leak.IsVisible;
	}

	public void WaitBegin()
	{
		DialogBegin(ui_waiting);
	}

	public void WaitEnd()
	{
		DialogEnd();
	}

	public void DialogBegin(UIElement content)
	{
		DialogWidget dialogWidget = null;
		if (dialogs.Count() <= dialogs_usedcount)
		{
			dialogWidget = new DialogWidget();
			GD_Main.Children.Add(dialogWidget);
			dialogs.Add(dialogWidget);
			dialogs_usedcount++;
		}
		else
		{
			dialogWidget = dialogs[dialogs_usedcount];
			dialogs_usedcount++;
		}
		dialogWidget.Content = content;
		dialogWidget.Visibility = Visibility.Visible;
	}

	public void DialogEnd()
	{
		DialogWidget dialogWidget = dialogs[dialogs_usedcount - 1];
		dialogWidget.Content = null;
		dialogWidget.Visibility = Visibility.Hidden;
		dialogs_usedcount--;
	}

	public void LeakDialogBegin(UIElement content, FrameworkElement leakcontent)
	{
		UI_Leak.LeakContent = leakcontent;
		UI_Leak.Content = content;
		UI_Leak.Visibility = Visibility.Visible;
	}

	public void LeakDialogEnd()
	{
		UI_Leak.LeakContent = null;
		UI_Leak.Content = null;
		UI_Leak.Visibility = Visibility.Hidden;
	}

	public void ShowDialog_CreateProject()
	{
		DialogBegin(ui_createproject);
	}

	public void ShowDialog_GroupMove()
	{
		DialogBegin(ui_groupmove);
	}

	public void ShowDialog_GroupScale()
	{
		DialogBegin(ui_groupscale);
	}

	public void ShowDialog_GroupRotate()
	{
		DialogBegin(ui_grouprotate);
	}

	public void ShowDialog_GroupMatrix()
	{
		DialogBegin(ui_groupmatrix);
	}

	public void ShowDialog_GroupExpand()
	{
		DialogBegin(ui_groupexpand);
	}

	public void ShowDialog_GroupFill()
	{
		DialogBegin(ui_groupfill);
	}

	public void ShowDialog_GroupReorder()
	{
		if (!UI_Dock.IsDocked(ui_select))
		{
			UI_Dock.CommandDock(ui_select);
		}
		LeakDialogBegin(ui_groupreorder, ui_select);
		ui_select.ReorderBegin();
	}

	public void ShowDialog_ImageResize()
	{
		ui_imageresize.Begin(SelectedImage);
		DialogBegin(ui_imageresize);
	}

	public void ShowDialog_About()
	{
		DialogBegin(ui_about);
	}

	public void ShowDialog_Import()
	{
	}

	public void ShowDialog_Export()
	{
		ui_export.Core = ((PolylineImage)SelectedImage).EPCore;
		ui_export.Core.RowStart = 0;
		ui_export.Core.RowEnd = ui_datagrid.Items.Count - 1;
		DialogBegin(ui_export);
	}

	public void ShowDialog_EC_Add()
	{
		ui_ec_add.Core = ((PolylineImage)SelectedImage).EPCore;
		DialogBegin(ui_ec_add);
	}

	public void ShowDialog_UserSet()
	{
		SelectedImage.UserStart();
		ObservableCollection<IPolylineUserFormat> observableCollection = new ObservableCollection<IPolylineUserFormat>();
		foreach (IPolylineUserFormat userFmt in SelectedImage.UserFmts)
		{
			observableCollection.Add(userFmt);
		}
		ui_uset.Items = observableCollection;
		DialogBegin(ui_uset);
	}

	public void ShowDialog_MulMod()
	{
		PolylineDataGridMultiplyModifyCore polylineDataGridMultiplyModifyCore = new PolylineDataGridMultiplyModifyCore(SelectedImage);
		polylineDataGridMultiplyModifyCore.StartLine = SelectedImage.SelectedStart;
		polylineDataGridMultiplyModifyCore.EndLine = SelectedImage.SelectedStart + SelectedImage.SelectedCount - 1;
		polylineDataGridMultiplyModifyCore.IsModifySelected = true;
		ui_mulmod.Core = polylineDataGridMultiplyModifyCore;
		DialogBegin(ui_mulmod);
	}

	protected void SetupCommands()
	{
		List<RoutedCommand> list = new List<RoutedCommand>();
		list.Add(UserCommands.GroupMerge);
		list.Add(UserCommands.GroupSplit);
		list.Add(UserCommands.GroupMove);
		list.Add(UserCommands.GroupRotate);
		list.Add(UserCommands.GroupScale);
		list.Add(UserCommands.GroupMirror);
		list.Add(UserCommands.EntityBreak);
		list.Add(UserCommands.EntityRound);
		list.Add(UserCommands.EntitySharp);
		list.Add(UserCommands.EntityBevel);
		list.Add(UserCommands.GroupMatrix);
		list.Add(UserCommands.GroupReverse);
		list.Add(UserCommands.GroupExpand);
		list.Add(UserCommands.GroupFill);
		ui_editor.CommandRightTool(list);
		list = new List<RoutedCommand>();
		list.Add(null);
		list.Add(null);
		list.Add(UserCommands.Import);
		list.Add(UserCommands.Export);
		list.Add(UserCommands.TableUser);
		list.Add(UserCommands.TableModify);
		ui_datagrid.SetupCommands(list);
	}

	protected void SetupBindings()
	{
		Binding binding = new Binding("SelectedImage")
		{
			Source = ui_select,
			Mode = BindingMode.OneWay
		};
		BindingOperations.SetBinding(ui_datagrid, PolylineDataGridWindow.SelectedImageProperty, binding);
		binding = new Binding("SelectedEntity")
		{
			Source = ui_select,
			Mode = BindingMode.TwoWay
		};
		BindingOperations.SetBinding(ui_datagrid, PolylineDataGridWindow.SelectedEntityProperty, binding);
	}

	protected void SelectFileAndOpen()
	{
		App.Root.SaveAsk(delegate
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "dxf file: *.dxf|SamCAD file: *.sca file: *.ssd",
				RestoreDirectory = true
			};
			if (openFileDialog.ShowDialog() == true)
			{
				ui_loadmode.FileName = openFileDialog.FileName;
				DialogBegin(ui_loadmode);
			}
		});
	}

	private void OnWindowLoaded(object sender, RoutedEventArgs e)
	{
		UI_Dock.AddView(ui_select, ViewCommon.BaseViewTypes.Anchor);
		UI_Dock.AddView(ui_argument, ViewCommon.BaseViewTypes.Anchor);
		UI_Dock.AddView(ui_editor, ViewCommon.BaseViewTypes.Document);
		UI_Dock.AddView(ui_datagrid, ViewCommon.BaseViewTypes.Document);
	}

	private void OnWindowOnloaded(object sender, RoutedEventArgs e)
	{
	}

	protected override void OnClosing(CancelEventArgs e)
	{
		isclosing = true;
		base.OnClosing(e);
		if (forceclose)
		{
			isclosing = false;
			return;
		}
		e.Cancel = true;
		if (!DialogIsFree())
		{
			isclosing = false;
			return;
		}
		App.Root.SaveAsk(delegate
		{
			e.Cancel = false;
			if (!isclosing)
			{
				forceclose = true;
				Close();
			}
		});
		isclosing = false;
	}

	protected override void OnClosed(EventArgs e)
	{
		base.OnClosed(e);
		UI_Dock?.Dispose();
	}

	private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
	{
		if (e.Command == ApplicationCommands.New)
		{
			e.CanExecute = App.Root.CanNew();
		}
		if (e.Command == ApplicationCommands.Open)
		{
			e.CanExecute = App.Root.CanOpen();
		}
		if (e.Command == ApplicationCommands.Save)
		{
			e.CanExecute = App.Root.CanSave();
		}
		if (e.Command == ApplicationCommands.SaveAs)
		{
			e.CanExecute = App.Root.CanSaveAs();
		}
		if (e.Command == ApplicationCommands.Close)
		{
			e.CanExecute = App.Root.CanClose();
		}
		if (e.Command == ApplicationCommands.Undo)
		{
			e.CanExecute = App.Root.CanUndo();
		}
		if (e.Command == ApplicationCommands.Redo)
		{
			e.CanExecute = App.Root.CanRedo();
		}
		if (e.Command == ApplicationCommands.Copy)
		{
			e.CanExecute = App.Root.CanCopy();
		}
		if (e.Command == ApplicationCommands.Cut)
		{
			e.CanExecute = App.Root.CanCut();
		}
		if (e.Command == ApplicationCommands.Paste)
		{
			e.CanExecute = App.Root.CanPaste();
		}
		if (e.Command == ApplicationCommands.Delete)
		{
			e.CanExecute = App.Root.CanDelete();
		}
		if (e.Command == ApplicationCommands.Print)
		{
			e.CanExecute = App.Root.CanPrint();
		}
		if (e.Command == ApplicationCommands.PrintPreview)
		{
			e.CanExecute = App.Root.CanPrintPreview();
		}
		if (e.Command == UserCommands.CreateImage)
		{
			e.CanExecute = App.Root.CanCreateImage();
		}
		if (e.Command == UserCommands.ImportImage)
		{
			e.CanExecute = App.Root.CanImportImage();
		}
		if (e.Command == UserCommands.RemoveImage)
		{
			e.CanExecute = App.Root.CanRemoveImage();
		}
		if (e.Command == UserCommands.ResizeImage)
		{
			e.CanExecute = App.Root.CanResizeImage();
		}
		if (e.Command == UserCommands.ShowEditor)
		{
			e.CanExecute = App.Root.CanShowEditor();
		}
		if (e.Command == UserCommands.ShowSelect)
		{
			e.CanExecute = App.Root.CanShowSelect();
		}
		if (e.Command == UserCommands.ShowArgument)
		{
			e.CanExecute = App.Root.CanShowArgument();
		}
		if (e.Command == UserCommands.ShowDataGrid)
		{
			e.CanExecute = App.Root.CanShowDataGrid();
		}
		if (e.Command == UserCommands.GroupMerge)
		{
			e.CanExecute = App.Root.CanGroupMerge();
		}
		if (e.Command == UserCommands.GroupSplit)
		{
			e.CanExecute = App.Root.CanGroupSplit();
		}
		if (e.Command == UserCommands.GroupMove)
		{
			e.CanExecute = App.Root.CanGroupMove();
		}
		if (e.Command == UserCommands.GroupReverse)
		{
			e.CanExecute = App.Root.CanGroupReverse();
		}
		if (e.Command == UserCommands.GroupMirror)
		{
			e.CanExecute = App.Root.CanGroupMirror();
		}
		if (e.Command == UserCommands.GroupRotate)
		{
			e.CanExecute = App.Root.CanGroupRotate();
		}
		if (e.Command == UserCommands.GroupReorder)
		{
			e.CanExecute = App.Root.CanGroupReorder();
		}
		if (e.Command == UserCommands.GroupScale)
		{
			e.CanExecute = App.Root.CanGroupScale();
		}
		if (e.Command == UserCommands.GroupMatrix)
		{
			e.CanExecute = App.Root.CanGroupMatrix();
		}
		if (e.Command == UserCommands.GroupExpand)
		{
			e.CanExecute = App.Root.CanGroupExpand();
		}
		if (e.Command == UserCommands.GroupFill)
		{
			e.CanExecute = App.Root.CanGroupFill();
		}
		if (e.Command == UserCommands.EntityBreak)
		{
			e.CanExecute = App.Root.CanEntityBreak();
		}
		if (e.Command == UserCommands.EntityRound)
		{
			e.CanExecute = App.Root.CanEntityRound();
		}
		if (e.Command == UserCommands.EntityBevel)
		{
			e.CanExecute = App.Root.CanEntityBevel();
		}
		if (e.Command == UserCommands.EntitySharp)
		{
			e.CanExecute = App.Root.CanEntitySharp();
		}
		if (e.Command == UserCommands.DrawVirt)
		{
			e.CanExecute = App.Root.CanDrawVirt();
		}
		if (e.Command == UserCommands.DrawLine)
		{
			e.CanExecute = App.Root.CanDrawLine();
		}
		if (e.Command == UserCommands.DrawArch)
		{
			e.CanExecute = App.Root.CanDrawArch();
		}
		if (e.Command == UserCommands.DrawCircle)
		{
			e.CanExecute = App.Root.CanDrawCircle();
		}
		if (e.Command == UserCommands.DrawRect)
		{
			e.CanExecute = App.Root.CanDrawRect();
		}
		if (e.Command == UserCommands.DrawFreeRect)
		{
			e.CanExecute = App.Root.CanDrawFreeRect();
		}
		if (e.Command == UserCommands.UserBook)
		{
			e.CanExecute = App.Root.CanUserBook();
		}
		if (e.Command == UserCommands.About)
		{
			e.CanExecute = App.Root.CanAbout();
		}
		if (e.Command == UserCommands.PageSetup)
		{
			e.CanExecute = App.Root.CanPageSetup();
		}
		if (e.Command == UserCommands.Import)
		{
			e.CanExecute = App.Root.CanImport();
		}
		if (e.Command == UserCommands.Export)
		{
			e.CanExecute = App.Root.CanExport();
		}
		if (e.Command == UserCommands.TableUser)
		{
			e.CanExecute = App.Root.CanTableUser();
		}
		if (e.Command == UserCommands.TableModify)
		{
			e.CanExecute = App.Root.CanTableModify();
		}
	}

	private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
	{
		if (e.Command == ApplicationCommands.New)
		{
			ShowDialog_CreateProject();
		}
		if (e.Command == ApplicationCommands.Open)
		{
			SelectFileAndOpen();
		}
		if (e.Command == ApplicationCommands.Save)
		{
			App.Root.Save();
		}
		if (e.Command == ApplicationCommands.SaveAs)
		{
			App.Root.SaveAs();
		}
		if (e.Command == ApplicationCommands.Close)
		{
			App.Root.Close();
		}
		if (e.Command == ApplicationCommands.Undo)
		{
			App.Root.Undo();
		}
		if (e.Command == ApplicationCommands.Redo)
		{
			App.Root.Redo();
		}
		if (e.Command == ApplicationCommands.Copy)
		{
			App.Root.Copy();
		}
		if (e.Command == ApplicationCommands.Cut)
		{
			App.Root.Cut();
		}
		if (e.Command == ApplicationCommands.Paste)
		{
			App.Root.Paste();
		}
		if (e.Command == ApplicationCommands.Delete)
		{
			App.Root.Delete();
		}
		if (e.Command == ApplicationCommands.Print)
		{
			App.Root.Print();
		}
		if (e.Command == ApplicationCommands.PrintPreview)
		{
			App.Root.PrintPreview();
		}
		if (e.Command == UserCommands.CreateImage)
		{
			App.Root.CreateImage();
		}
		if (e.Command == UserCommands.ImportImage)
		{
			App.Root.ImportImage();
		}
		if (e.Command == UserCommands.RemoveImage)
		{
			App.Root.RemoveImage();
		}
		if (e.Command == UserCommands.ResizeImage)
		{
			App.Root.ResizeImage();
		}
		if (e.Command == UserCommands.ShowEditor)
		{
			UI_Dock.Show(ui_editor);
		}
		if (e.Command == UserCommands.ShowSelect)
		{
			UI_Dock.Show(ui_select);
		}
		if (e.Command == UserCommands.ShowArgument)
		{
			UI_Dock.Show(ui_argument);
		}
		if (e.Command == UserCommands.ShowDataGrid)
		{
			UI_Dock.Show(ui_datagrid);
		}
		if (e.Command == UserCommands.GroupMerge)
		{
			App.Root.GroupMerge();
		}
		if (e.Command == UserCommands.GroupSplit)
		{
			App.Root.GroupSplit();
		}
		if (e.Command == UserCommands.GroupMove)
		{
			App.Root.GroupMove();
		}
		if (e.Command == UserCommands.GroupReverse)
		{
			App.Root.GroupReverse();
		}
		if (e.Command == UserCommands.GroupMirror)
		{
			App.Root.GroupMirror();
		}
		if (e.Command == UserCommands.GroupRotate)
		{
			App.Root.GroupRotate();
		}
		if (e.Command == UserCommands.GroupReorder)
		{
			App.Root.GroupReorder();
		}
		if (e.Command == UserCommands.GroupScale)
		{
			App.Root.GroupScale();
		}
		if (e.Command == UserCommands.GroupMatrix)
		{
			App.Root.GroupMatrix();
		}
		if (e.Command == UserCommands.GroupExpand)
		{
			App.Root.GroupExpand();
		}
		if (e.Command == UserCommands.GroupFill)
		{
			App.Root.GroupFill();
		}
		if (e.Command == UserCommands.EntityBreak)
		{
			App.Root.EntityBreak();
		}
		if (e.Command == UserCommands.EntityRound)
		{
			App.Root.EntityRound();
		}
		if (e.Command == UserCommands.EntityBevel)
		{
			App.Root.EntityBevel();
		}
		if (e.Command == UserCommands.EntitySharp)
		{
			App.Root.EntitySharp();
		}
		if (e.Command == UserCommands.DrawVirt)
		{
			App.Root.DrawVirt();
		}
		if (e.Command == UserCommands.DrawLine)
		{
			App.Root.DrawLine();
		}
		if (e.Command == UserCommands.DrawArch)
		{
			App.Root.DrawArch();
		}
		if (e.Command == UserCommands.DrawCircle)
		{
			App.Root.DrawCircle();
		}
		if (e.Command == UserCommands.DrawRect)
		{
			App.Root.DrawRect();
		}
		if (e.Command == UserCommands.DrawFreeRect)
		{
			App.Root.DrawFreeRect();
		}
		if (e.Command == UserCommands.UserBook)
		{
			App.Root.UserBook();
		}
		if (e.Command == UserCommands.About)
		{
			App.Root.About();
		}
		if (e.Command == UserCommands.PageSetup)
		{
			App.Root.PageSetup();
		}
		if (e.Command == UserCommands.Import)
		{
			App.Root.Import();
		}
		if (e.Command == UserCommands.Export)
		{
			App.Root.Export();
		}
		if (e.Command == UserCommands.TableUser)
		{
			App.Root.TableUser();
		}
		if (e.Command == UserCommands.TableModify)
		{
			App.Root.TableModify();
		}
	}

	private void UI_Select_SelectedImageChanged(object sender, DependencyPropertyChangedEventArgs e)
	{
		IPolylineImage selectedImage = ((e.NewValue is IPolylineImage) ? ((IPolylineImage)e.NewValue) : null);
		SelectedImage = selectedImage;
	}

	private void UI_Select_SelectionChanged(object sender, SelectionChangedEventArgs e)
	{
		ui_editor.Select(ui_select.SelectedStart, ui_select.SelectedCount);
		ui_editor.IvSelect(e);
		if (ui_select.SelectedEntity != null && ui_select.SelectedEntity.Argument == null)
		{
			ui_select.SelectedEntity.Argument = new HMIPLINEArgument();
		}
		ui_argument.SelectedEntity = ui_select.SelectedEntity;
	}

	private void UI_Argument_MultiModify(object sender, RoutedEventArgs e)
	{
		if (ui_select.SelectedEntities == null || ui_argument.SelectedEntity?.Argument == null)
		{
			return;
		}
		foreach (IPolylineEntity selectedEntity in ui_select.SelectedEntities)
		{
			if (selectedEntity.Argument == null)
			{
				selectedEntity.Argument = ui_argument.SelectedEntity.Argument.Clone();
			}
			else
			{
				selectedEntity.Argument.Load(ui_argument.SelectedEntity.Argument);
			}
		}
	}

	private void UI_Argument_EntityChanged(object sender, PolylineActionEventArgs e)
	{
		e.Image.Redos.Clear();
		e.Image.Redos.Add(e.Action);
		e.Image.Redo();
		ui_select.UpdateRedo(e.Action);
		ui_editor.DrawingAll();
	}

	private void UI_Editor_EntityClick(object sender, PolylineEntityEventArgs e)
	{
		ui_argument.Submit();
		ui_select.SelectKeyboard(e.Entity.ID);
	}

	private void UI_Editor_EntityInsert(object sender, PolylineActionEventArgs e)
	{
		ui_select.UpdateRedo(e.Action);
	}

	private void UI_Editor_EntityAction(object sender, PolylineActionEventArgs e)
	{
		ui_select.UpdateRedo(e.Action);
		ui_editor.DrawingAll();
	}

	private void UI_Editor_EntityUndo(object sender, PolylineActionEventArgs e)
	{
		ui_select.UpdateUndo(e.Action);
		ui_editor.DrawingAll();
	}

	private void UI_Editor_EntitySetting(object sender, PolylineEntityEventArgs e)
	{
		if (e.Entity is IPolylineRect)
		{
			creatingrect = (IPolylineRect)e.Entity;
			ui_createrect.OldRect = creatingrect.Rect;
			DialogBegin(ui_createrect);
		}
		else if (e.Entity is IPolylineFreeRect)
		{
			creatingfreerect = (IPolylineFreeRect)e.Entity;
			ui_createfreerect.Rect = creatingfreerect;
			DialogBegin(ui_createfreerect);
		}
		else if (e.Entity is IPolylineEllipse)
		{
			creatingellipse = (IPolylineEllipse)e.Entity;
			ui_createellipse.Ellipse = creatingellipse;
			DialogBegin(ui_createellipse);
		}
	}

	private void UI_DataGrid_EntityInsert(object sender, PolylineActionEventArgs e)
	{
		ui_select.UpdateRedo(e.Action);
		ui_editor.DrawingAll();
	}

	private void UI_DataGrid_EntityRemove(object sender, PolylineActionEventArgs e)
	{
		ui_select.UpdateRedo(e.Action);
		ui_editor.DrawingAll();
	}

	private void UI_DataGrid_EntityReplace(object sender, PolylineActionEventArgs e)
	{
		ui_select.UpdateRedo(e.Action);
		ui_editor.DrawingAll();
	}

	private void UI_DataGrid_EntityChanged(object sender, PolylineActionEventArgs e)
	{
		e.Image.Redos.Clear();
		e.Image.Redos.Add(e.Action);
		e.Image.Redo();
		ui_select.UpdateRedo(e.Action);
		ui_editor.DrawingAll();
	}

	private void UI_GroupMove_Yes(object sender, RoutedEventArgs e)
	{
		Vector move = ui_groupmove.Move;
		DialogEnd();
		App.Root.GroupMove(move);
	}

	private void UI_GroupMove_No(object sender, RoutedEventArgs e)
	{
		DialogEnd();
	}

	private void UI_GroupScale_Yes(object sender, RoutedEventArgs e)
	{
		Point start = ui_groupscale.Start;
		double scaleX = ui_groupscale.ScaleX;
		double scaleY = ui_groupscale.ScaleY;
		DialogEnd();
		App.Root.GroupScale(start, scaleX, scaleY);
	}

	private void UI_GroupScale_No(object sender, RoutedEventArgs e)
	{
		DialogEnd();
	}

	private void UI_GroupRotate_Yes(object sender, RoutedEventArgs e)
	{
		Point start = ui_grouprotate.Start;
		double angle = ui_grouprotate.Angle;
		DialogEnd();
		App.Root.GroupRotate(start, angle);
	}

	private void UI_GroupRotate_No(object sender, RoutedEventArgs e)
	{
		DialogEnd();
	}

	private void UI_ImageResize_Yes(object sender, RoutedEventArgs e)
	{
		DialogEnd();
		Rect newSize = ui_imageresize.NewSize;
		if (double.IsNaN(newSize.X) || double.IsNaN(newSize.Y) || double.IsNaN(newSize.Width) || double.IsNaN(newSize.Height))
		{
			MessageBox.Show("Input illegal!");
			return;
		}
		SelectedImage.Left = newSize.Left;
		SelectedImage.Top = newSize.Top;
		SelectedImage.Width = newSize.Width;
		SelectedImage.Height = newSize.Height;
	}

	private void UI_ImageResize_No(object sender, RoutedEventArgs e)
	{
		DialogEnd();
	}

	private void UI_GroupReorder_No(object sender, RoutedEventArgs e)
	{
		ui_select.ReorderEscape();
		LeakDialogEnd();
	}

	private void UI_GroupReorder_Yes(object sender, RoutedEventArgs e)
	{
		if (ui_groupreorder.ReorderingStrategy == ReorderingStrategy.None)
		{
			App.Root.GroupReorder(ui_select.Reorders.Cast<IPolylineReorderingGroup>().ToList());
		}
		else
		{
			App.Root.GroupReorder(ui_groupreorder.ReorderingStrategy);
		}
		ui_select.ReorderEnd();
		LeakDialogEnd();
	}

	private void UI_GroupMatrix_Yes(object sender, RoutedEventArgs e)
	{
		DialogEnd();
		int row = ui_groupmatrix.Row;
		int column = ui_groupmatrix.Column;
		Point offset = ui_groupmatrix.Offset;
		MatrixStrategy strategy = ui_groupmatrix.Strategy;
		MatrixPriority priority = ui_groupmatrix.Priority;
		App.Root.GroupMatrix(row, column, offset, strategy, priority);
	}

	private void UI_GroupMatrix_No(object sender, RoutedEventArgs e)
	{
		DialogEnd();
	}

	private void UI_GroupExpand_Yes(object sender, RoutedEventArgs e)
	{
		DialogEnd();
		double r = ui_groupexpand.R;
		App.Root.GroupExpand(r);
	}

	private void UI_GroupExpand_No(object sender, RoutedEventArgs e)
	{
		DialogEnd();
	}

	private void UI_GroupFill_Yes(object sender, RoutedEventArgs e)
	{
		DialogEnd();
		double size = ui_groupfill.Size;
		FillStrategy strategy = ui_groupfill.Strategy;
		bool isRemoveOld = ui_groupfill.IsRemoveOld;
		App.Root.GroupFill(size, strategy, isRemoveOld);
	}

	private void UI_GroupFill_No(object sender, RoutedEventArgs e)
	{
		DialogEnd();
	}

	private void OnImageArgumentPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		if (!(sender is IHMIPLINEImageArgument))
		{
			return;
		}
		IHMIPLINEImageArgument iHMIPLINEImageArgument = (IHMIPLINEImageArgument)sender;
		string propertyName = e.PropertyName;
		string text = propertyName;
		if (text == "StartupMode" && iHMIPLINEImageArgument.StartupMode == StartupMode.SpotOnly)
		{
			if (MessageBox.Show("点位控制不支持圆弧图元，isno转换为Straight line？", "Attention", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
			{
				SelectedImage.ArchToLine();
				ui_editor.DrawingAll();
				ui_select.InvalidateItems();
			}
			else
			{
				iHMIPLINEImageArgument.StartupMode = StartupMode.Trajectory;
			}
		}
	}

	private void UI_CreateProject_Yes(object sender, RoutedEventArgs e)
	{
		if (!(sender is CreateProjectWindow))
		{
			return;
		}
		CreateProjectWindow createProjectWindow = (CreateProjectWindow)sender;
		string fileName = ui_createproject.FileName;
		if (fileName.Length > 0)
		{
			try
			{
				string directoryName = Path.GetDirectoryName(fileName);
				if (!Directory.Exists(directoryName))
				{
					MessageBox.Show("The specified path does not exist.");
					return;
				}
				if (File.Exists(fileName) && MessageBox.Show("文件已存在，isno覆盖？", "Attention", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
				{
					return;
				}
			}
			catch (Exception)
			{
				MessageBox.Show("The input file path is illegal!");
				return;
			}
		}
		DialogEnd();
		App.Root.New(createProjectWindow.ArgumentType);
		if (Project != null)
		{
			IPolylineImage polylineImage = Project.Items.FirstOrDefault();
			polylineImage.Name = ui_createproject.ProjectName;
			if (fileName.Length > 0)
			{
				Project.Filename = fileName;
				App.Root.Save();
			}
		}
	}

	private void UI_CreateProject_No(object sender, RoutedEventArgs e)
	{
		DialogEnd();
	}

	private void UI_CreateRect_Yes(object sender, RoutedEventArgs e)
	{
		DialogEnd();
		Rect newRect = ui_createrect.NewRect;
		bool isClockwise = ui_createrect.IsClockwise;
		int iD = creatingrect.ID;
		IPolylineImage selectedImage = SelectedImage;
		IPolylineLine polylineLine = (IPolylineLine)selectedImage.Items[iD];
		IPolylineLine polylineLine2 = (IPolylineLine)selectedImage.Items[iD + 1];
		IPolylineLine polylineLine3 = (IPolylineLine)selectedImage.Items[iD + 2];
		IPolylineLine polylineLine4 = (IPolylineLine)selectedImage.Items[iD + 3];
		if (selectedImage.IsEqualP(polylineLine.From, newRect.TopLeft))
		{
			if (!isClockwise)
			{
				selectedImage.Rect4To(polylineLine4, polylineLine, polylineLine2, polylineLine3, newRect);
			}
			else
			{
				selectedImage.Rect4To(polylineLine4, polylineLine3, polylineLine2, polylineLine, newRect);
			}
		}
		else if (selectedImage.IsEqualP(polylineLine.From, newRect.TopRight))
		{
			if (!isClockwise)
			{
				selectedImage.Rect4To(polylineLine3, polylineLine4, polylineLine, polylineLine2, newRect);
			}
			else
			{
				selectedImage.Rect4To(polylineLine, polylineLine4, polylineLine3, polylineLine2, newRect);
			}
		}
		else if (selectedImage.IsEqualP(polylineLine.From, newRect.BottomRight))
		{
			if (!isClockwise)
			{
				selectedImage.Rect4To(polylineLine2, polylineLine3, polylineLine4, polylineLine, newRect);
			}
			else
			{
				selectedImage.Rect4To(polylineLine2, polylineLine, polylineLine4, polylineLine3, newRect);
			}
		}
		else if (selectedImage.IsEqualP(polylineLine.From, newRect.BottomLeft))
		{
			if (!isClockwise)
			{
				selectedImage.Rect4To(polylineLine, polylineLine2, polylineLine3, polylineLine4, newRect);
			}
			else
			{
				selectedImage.Rect4To(polylineLine3, polylineLine2, polylineLine, polylineLine4, newRect);
			}
		}
		else if (selectedImage.IsEqualX(polylineLine.From.X, newRect.Left))
		{
			IPolylineLine polylineLine5 = new PolylineLine(selectedImage, iD + 4, polylineLine4.To);
			selectedImage.Insert(polylineLine5.ID, polylineLine5);
			if (!isClockwise)
			{
				polylineLine.To = newRect.TopLeft;
				selectedImage.Rect4To(polylineLine5, polylineLine2, polylineLine3, polylineLine4, newRect);
			}
			else
			{
				polylineLine.To = newRect.BottomLeft;
				selectedImage.Rect4To(polylineLine4, polylineLine3, polylineLine2, polylineLine5, newRect);
			}
		}
		else if (selectedImage.IsEqualX(polylineLine.From.X, newRect.Right))
		{
			IPolylineLine polylineLine6 = new PolylineLine(selectedImage, iD + 4, polylineLine4.To);
			selectedImage.Insert(polylineLine6.ID, polylineLine6);
			if (!isClockwise)
			{
				polylineLine.To = newRect.BottomRight;
				selectedImage.Rect4To(polylineLine3, polylineLine4, polylineLine6, polylineLine2, newRect);
			}
			else
			{
				polylineLine.To = newRect.TopRight;
				selectedImage.Rect4To(polylineLine2, polylineLine6, polylineLine4, polylineLine3, newRect);
			}
		}
		else if (selectedImage.IsEqualY(polylineLine.From.Y, newRect.Top))
		{
			IPolylineLine polylineLine7 = new PolylineLine(selectedImage, iD + 4, polylineLine4.To);
			selectedImage.Insert(polylineLine7.ID, polylineLine7);
			if (!isClockwise)
			{
				polylineLine.To = newRect.TopRight;
				selectedImage.Rect4To(polylineLine4, polylineLine7, polylineLine2, polylineLine3, newRect);
			}
			else
			{
				polylineLine.To = newRect.TopLeft;
				selectedImage.Rect4To(polylineLine7, polylineLine4, polylineLine3, polylineLine2, newRect);
			}
		}
		else if (selectedImage.IsEqualY(polylineLine.From.Y, newRect.Bottom))
		{
			IPolylineLine polylineLine8 = new PolylineLine(selectedImage, iD + 4, polylineLine4.To);
			selectedImage.Insert(polylineLine8.ID, polylineLine8);
			if (!isClockwise)
			{
				polylineLine.To = newRect.BottomLeft;
				selectedImage.Rect4To(polylineLine2, polylineLine3, polylineLine4, polylineLine8, newRect);
			}
			else
			{
				polylineLine.To = newRect.BottomRight;
				selectedImage.Rect4To(polylineLine3, polylineLine2, polylineLine8, polylineLine4, newRect);
			}
		}
		else
		{
			IPolylineLine polylineLine9 = new PolylineLine(selectedImage, iD + 4, polylineLine4.To);
			selectedImage.Insert(polylineLine9.ID, polylineLine9);
			polylineLine.To = newRect.TopLeft;
			polylineLine.IsReal = false;
			if (!isClockwise)
			{
				selectedImage.Rect4To(polylineLine9, polylineLine2, polylineLine3, polylineLine4, newRect);
			}
			else
			{
				selectedImage.Rect4To(polylineLine9, polylineLine4, polylineLine3, polylineLine2, newRect);
			}
		}
		ui_select.InvalidateItems();
		ui_editor.DrawingAll();
	}

	private void UI_CreateRect_No(object sender, RoutedEventArgs e)
	{
		DialogEnd();
		App.Root.Undo();
	}

	private void UI_CreateFreeRect_Yes(object sender, RoutedEventArgs e)
	{
		DialogEnd();
		creatingfreerect = ui_createfreerect.Rect;
		IPolylineImage selectedImage = SelectedImage;
		Point point = creatingfreerect.From;
		Point p = creatingfreerect.P1;
		Point p2 = creatingfreerect.P2;
		Vector vector = p - point;
		Vector vector2 = p2 - point;
		double num = Vector.CrossProduct(vector, vector2);
		bool isClockwise = ui_createfreerect.IsClockwise;
		if (ui_createfreerect.IsRound)
		{
			double roundRadius = ui_createfreerect.RoundRadius;
			roundRadius = Math.Max(roundRadius, 0.0);
			roundRadius = Math.Min(roundRadius, vector.Length * 0.5);
			roundRadius = Math.Min(roundRadius, vector2.Length * 0.5);
			Vector vector3 = vector * roundRadius / vector.Length;
			Vector vector4 = vector2 * roundRadius / vector2.Length;
			IPolylineLine polylineLine = new PolylineLine(selectedImage, creatingfreerect.ID, point + vector3, _isreal: false);
			List<IPolylineEntity> list = new List<IPolylineEntity>();
			list.Add(polylineLine);
			if (isClockwise ^ (num >= 0.0))
			{
				Vector vector5 = vector;
				vector = vector2;
				vector2 = vector5;
				vector5 = vector3;
				vector3 = vector4;
				vector4 = vector5;
			}
			list.Add(new PolylineArch(selectedImage, polylineLine.ID, point + vector4, point + vector3 + vector4, isClockwise));
			point += vector2;
			list.Add(new PolylineLine(selectedImage, polylineLine.ID, point - vector4));
			list.Add(new PolylineArch(selectedImage, polylineLine.ID, point + vector3, point + vector3 - vector4, isClockwise));
			point += vector;
			list.Add(new PolylineLine(selectedImage, polylineLine.ID, point - vector3));
			list.Add(new PolylineArch(selectedImage, polylineLine.ID, point - vector4, point - vector3 - vector4, isClockwise));
			point -= vector2;
			list.Add(new PolylineLine(selectedImage, polylineLine.ID, point + vector4));
			list.Add(new PolylineArch(selectedImage, polylineLine.ID, point - vector3, point - vector3 + vector4, isClockwise));
			point -= vector;
			list.Add(new PolylineLine(selectedImage, polylineLine.ID, point + vector3));
			selectedImage.Replace(polylineLine.ID, 4, list);
		}
		else if (ui_createfreerect.IsBevel)
		{
			double bevelRadius = ui_createfreerect.BevelRadius;
			bevelRadius = Math.Max(bevelRadius, 0.0);
			bevelRadius = Math.Min(bevelRadius, vector.Length * 0.5);
			bevelRadius = Math.Min(bevelRadius, vector2.Length * 0.5);
			Vector vector6 = vector * bevelRadius / vector.Length;
			Vector vector7 = vector2 * bevelRadius / vector2.Length;
			IPolylineLine polylineLine2 = new PolylineLine(selectedImage, creatingfreerect.ID, point + vector6, _isreal: false);
			List<IPolylineEntity> list2 = new List<IPolylineEntity>();
			list2.Add(polylineLine2);
			if (isClockwise ^ (num >= 0.0))
			{
				Vector vector8 = vector;
				vector = vector2;
				vector2 = vector8;
				vector8 = vector6;
				vector6 = vector7;
				vector7 = vector8;
			}
			list2.Add(new PolylineLine(selectedImage, polylineLine2.ID, point + vector7));
			point += vector2;
			list2.Add(new PolylineLine(selectedImage, polylineLine2.ID, point - vector7));
			list2.Add(new PolylineLine(selectedImage, polylineLine2.ID, point + vector6));
			point += vector;
			list2.Add(new PolylineLine(selectedImage, polylineLine2.ID, point - vector6));
			list2.Add(new PolylineLine(selectedImage, polylineLine2.ID, point - vector7));
			point -= vector2;
			list2.Add(new PolylineLine(selectedImage, polylineLine2.ID, point + vector7));
			list2.Add(new PolylineLine(selectedImage, polylineLine2.ID, point - vector6));
			point -= vector;
			list2.Add(new PolylineLine(selectedImage, polylineLine2.ID, point + vector6));
			selectedImage.Replace(polylineLine2.ID, 4, list2);
		}
		else
		{
			IPolylineLine polylineLine3 = new PolylineLine(selectedImage, creatingfreerect.ID, point, _isreal: false);
			List<IPolylineEntity> list3 = new List<IPolylineEntity>();
			list3.Add(polylineLine3);
			if (isClockwise ^ (num >= 0.0))
			{
				Vector vector9 = vector;
				vector = vector2;
				vector2 = vector9;
			}
			list3.Add(new PolylineLine(selectedImage, polylineLine3.ID, point + vector2));
			list3.Add(new PolylineLine(selectedImage, polylineLine3.ID, point + vector2 + vector));
			list3.Add(new PolylineLine(selectedImage, polylineLine3.ID, point + vector));
			list3.Add(new PolylineLine(selectedImage, polylineLine3.ID, point));
			selectedImage.Replace(polylineLine3.ID, 4, list3);
		}
		ui_select.InvalidateItems();
		ui_editor.DrawingAll();
		creatingfreerect = null;
	}

	private void UI_CreateFreeRect_No(object sender, RoutedEventArgs e)
	{
		DialogEnd();
		App.Root.Undo();
	}

	private void UI_CreateEllipse_Yes(object sender, RoutedEventArgs e)
	{
		DialogEnd();
		SelectedImage?.RepairFor(creatingellipse);
		UI_Editor?.DrawingAll();
		creatingellipse = null;
	}

	private void UI_CreateEllipse_No(object sender, RoutedEventArgs e)
	{
		DialogEnd();
		App.Root.Undo();
	}

	private void UI_About_Yes(object sender, RoutedEventArgs e)
	{
		DialogEnd();
	}

	private void UI_Export_ColAdd(object sender, RoutedEventArgs e)
	{
		ShowDialog_EC_Add();
	}

	private void UI_Export_All(object sender, RoutedEventArgs e)
	{
		ui_export.Core.RowStart = 0;
		ui_export.Core.RowEnd = ui_datagrid.Items.Count - 1;
	}

	private void UI_Export_Export(object sender, RoutedEventArgs e)
	{
		ui_export.Core.RowStart = Math.Max(ui_export.Core.RowStart, 0);
		ui_export.Core.RowEnd = Math.Min(ui_export.Core.RowEnd, ui_datagrid.Items.Count - 1);
		string fileName = ui_export.FileName;
		FileStream fileStream = null;
		StreamWriter streamWriter = null;
		try
		{
			fileStream = File.Create(fileName);
		}
		catch (Exception ex)
		{
			MessageBox.Show(ex.Message);
			return;
		}
		try
		{
			streamWriter = new StreamWriter(fileStream, Encoding.Default);
		}
		catch (Exception ex2)
		{
			MessageBox.Show(ex2.Message);
			fileStream.Close();
			return;
		}
		try
		{
			PolylineExportCore core = ui_export.Core;
			int num = core.Columns.Count();
			if (core.KKSK && core.ToSKDevice)
			{
				for (int i = core.RowStart; i <= core.RowEnd; i++)
				{
					PolylineDataGridItem polylineDataGridItem = ui_datagrid.Items[i];
					for (int j = 0; j < num; j++)
					{
						PolylineExportColumn polylineExportColumn = core.Columns[j];
						switch (polylineExportColumn.Type)
						{
						case PolylineExportColumnType.X:
							streamWriter.Write(ValueConverter.FloatToUInt((float)polylineDataGridItem.X));
							break;
						case PolylineExportColumnType.Y:
							streamWriter.Write(ValueConverter.FloatToUInt((float)polylineDataGridItem.Y));
							break;
						case PolylineExportColumnType.Radius:
							streamWriter.Write(ValueConverter.FloatToUInt((float)polylineDataGridItem.R));
							break;
						case PolylineExportColumnType.CenterX:
							streamWriter.Write(ValueConverter.FloatToUInt((float)polylineDataGridItem.CenterX));
							break;
						case PolylineExportColumnType.CenterY:
							streamWriter.Write(ValueConverter.FloatToUInt((float)polylineDataGridItem.CenterY));
							break;
						case PolylineExportColumnType.Type:
						{
							int from = polylineDataGridItem.Type_I;
							int value = polylineExportColumn.Exchanges.FirstOrDefault((PolylineExportExchange polylineExportExchange) => polylineExportExchange.From == from)?.To ?? from;
							streamWriter.Write(value);
							break;
						}
						case PolylineExportColumnType.User:
						{
							IPolylineUserFormat userFormat = polylineExportColumn.UserFormat;
							IPolylineUserObject polylineUserObject = polylineDataGridItem.Core.UserObjs[userFormat.ID];
							switch (userFormat.DataType)
							{
							case PolylineUserDataType.Bool:
								streamWriter.Write(((bool)polylineUserObject.Value) ? 1 : 0);
								break;
							case PolylineUserDataType.Int:
								streamWriter.Write((int)polylineUserObject.Value);
								break;
							case PolylineUserDataType.Double:
								streamWriter.Write(ValueConverter.FloatToUInt((float)(double)polylineUserObject.Value));
								break;
							case PolylineUserDataType.String:
								streamWriter.Write(polylineUserObject.Value.ToString());
								break;
							}
							break;
						}
						}
						streamWriter.Write('\n');
					}
				}
			}
			else
			{
				if (core.KKSK)
				{
					streamWriter.WriteLine($"Name :{core.SK.DeciptName}");
					streamWriter.WriteLine($"Description :{core.SK.Description}");
					streamWriter.WriteLine($"Formula length :{core.RowEnd - core.RowStart + 1}");
					streamWriter.WriteLine($"Total number of recipes :{core.Columns.Count()}");
					streamWriter.WriteLine(string.Format("Data category :{0}", "Floating-point number"));
					streamWriter.WriteLine(string.Format("isno写配方到PLC:{0}", core.SK.SavePLC ? "is" : "no"));
					streamWriter.WriteLine(string.Format("isno从PLC读取地址:{0}", core.SK.LoadPLC ? "is" : "no"));
				}
				for (int num2 = 0; num2 < num; num2++)
				{
					streamWriter.Write(string.Format("{0},{1}", core.Columns[num2], (num2 >= num - 1) ? "\n" : ""));
				}
				for (int num3 = core.RowStart; num3 <= core.RowEnd; num3++)
				{
					PolylineDataGridItem polylineDataGridItem2 = ui_datagrid.Items[num3];
					for (int num4 = 0; num4 < num; num4++)
					{
						PolylineExportColumn polylineExportColumn2 = core.Columns[num4];
						switch (polylineExportColumn2.Type)
						{
						case PolylineExportColumnType.X:
							streamWriter.Write(polylineDataGridItem2.X);
							break;
						case PolylineExportColumnType.Y:
							streamWriter.Write(polylineDataGridItem2.Y);
							break;
						case PolylineExportColumnType.Radius:
							streamWriter.Write(polylineDataGridItem2.R);
							break;
						case PolylineExportColumnType.CenterX:
							streamWriter.Write(polylineDataGridItem2.CenterX);
							break;
						case PolylineExportColumnType.CenterY:
							streamWriter.Write(polylineDataGridItem2.CenterY);
							break;
						case PolylineExportColumnType.Type:
						{
							int from2 = polylineDataGridItem2.Type_I;
							int value2 = polylineExportColumn2.Exchanges.FirstOrDefault((PolylineExportExchange polylineExportExchange) => polylineExportExchange.From == from2)?.To ?? from2;
							streamWriter.Write(value2);
							break;
						}
						case PolylineExportColumnType.User:
						{
							IPolylineUserFormat userFormat2 = polylineExportColumn2.UserFormat;
							IPolylineUserObject polylineUserObject2 = polylineDataGridItem2.Core.UserObjs[userFormat2.ID];
							switch (userFormat2.DataType)
							{
							case PolylineUserDataType.Bool:
								streamWriter.Write((bool)polylineUserObject2.Value);
								break;
							case PolylineUserDataType.Int:
								streamWriter.Write((int)polylineUserObject2.Value);
								break;
							case PolylineUserDataType.Double:
								streamWriter.Write((float)(double)polylineUserObject2.Value);
								break;
							case PolylineUserDataType.String:
								streamWriter.Write(polylineUserObject2.Value.ToString());
								break;
							}
							break;
						}
						}
						streamWriter.Write(',');
						if (num4 >= num - 1)
						{
							streamWriter.Write('\n');
						}
					}
				}
			}
		}
		catch (Exception ex3)
		{
			MessageBox.Show(ex3.Message);
			return;
		}
		finally
		{
			streamWriter.Close();
			fileStream.Close();
		}
		MessageBox.Show("Export successful!");
		DialogEnd();
	}

	private void UI_Export_Cancel(object sender, RoutedEventArgs e)
	{
		DialogEnd();
	}

	private void UI_USet_Yes(object sender, RoutedEventArgs e)
	{
		SelectedImage.UserYes(ui_uset.Items);
		DialogEnd();
	}

	private void UI_USet_No(object sender, RoutedEventArgs e)
	{
		SelectedImage.UserNo();
		DialogEnd();
	}

	private void UI_EC_Add_Yes(object sender, RoutedEventArgs e)
	{
		PolylineExportColumn selectedColumn = ui_ec_add.GetSelectedColumn();
		if (selectedColumn != null)
		{
			ui_ec_add.Core.Columns.Add(selectedColumn);
		}
		DialogEnd();
	}

	private void UI_EC_Add_No(object sender, RoutedEventArgs e)
	{
		DialogEnd();
	}

	private void UI_MulMod_Yes(object sender, RoutedEventArgs e)
	{
		ui_datagrid.MultiplyModify(ui_mulmod.Core);
		DialogEnd();
	}

	private void UI_MulMod_No(object sender, RoutedEventArgs e)
	{
		DialogEnd();
	}

	private void UI_LoadMode_Yes(object sender, RoutedEventArgs e)
	{
		DialogEnd();
		App.Root.Open(ui_loadmode.FileName, ui_loadmode.Mode, ui_loadmode.StartAtFirstEntity, ui_loadmode.ArgumentType);
	}

	private void UI_LoadMode_No(object sender, RoutedEventArgs e)
	{
		DialogEnd();
	}

	private void UI_Select_InteSelect(object sender, RoutedEventArgs e)
	{
		DialogBegin(ui_inteselect);
	}

	private void UI_InteSelect_Select(object sender, InteSelectEventArgs e)
	{
		IPolylineImage selectedImage = ui_select.SelectedImage;
		bool flag = true;
		DialogEnd();
		if (selectedImage == null)
		{
			return;
		}
		for (int i = 0; i < selectedImage.Items.Count(); i++)
		{
			IPolylineEntity polylineEntity = selectedImage.Items[i];
			bool flag2 = false;
			switch (e.Event)
			{
			case Enum_InteSelectEvent.SelectAllDash:
				if (!polylineEntity.IsReal)
				{
					flag2 = true;
				}
				break;
			case Enum_InteSelectEvent.SelectAllReal:
				if (polylineEntity.IsReal)
				{
					flag2 = true;
				}
				break;
			case Enum_InteSelectEvent.SelectAllLine:
				if (polylineEntity.IsReal && polylineEntity is IPolylineLine)
				{
					flag2 = true;
				}
				break;
			case Enum_InteSelectEvent.SelectAllCirc:
				if (polylineEntity.IsReal && !(polylineEntity is IPolylineArch) && polylineEntity is IPolylineCircle)
				{
					flag2 = true;
				}
				break;
			case Enum_InteSelectEvent.SelectAllArch:
				if (polylineEntity.IsReal && polylineEntity is IPolylineArch)
				{
					flag2 = true;
				}
				break;
			}
			if (flag2)
			{
				if (flag)
				{
					ui_select.Select(i, 1);
				}
				else
				{
					ui_select.SelectCtrl(i);
				}
				flag = false;
			}
		}
		if (flag)
		{
			MessageBox.Show("No graphics that meet the conditions were found.");
		}
	}

	private void UI_Editor_MousePositionMove(object sender, MousePositionEventArgs e)
	{
		UI_Under.Point = e.LogicalPosition;
	}

	private void UI_Editor_MouseActionEscape(object sender, MouseActionEventArgs e)
	{
		UI_Under.Message = "Ready";
	}

	private void UI_Editor_MouseActionDone(object sender, MouseActionEventArgs e)
	{
		UI_Under.Message = "Ready";
	}

	private void UI_Editor_MouseActionStart(object sender, MouseActionEventArgs e)
	{
		switch (e.Status)
		{
		case MouseStatus.SetTarget:
			UI_Under.Message = "Set the target point";
			break;
		case MouseStatus.SetCenter:
			UI_Under.Message = "Set the center point";
			break;
		case MouseStatus.MovePoint:
			UI_Under.Message = "移动Control point";
			break;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamCAD;component/mainwindow.xaml", UriKind.Relative);
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
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 2:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 3:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 4:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 5:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 6:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 7:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 8:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 9:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 10:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 11:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 12:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 13:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 14:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 15:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 16:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 17:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 18:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 19:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 20:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 21:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 22:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 23:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 24:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 25:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 26:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 27:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 28:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 29:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 30:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 31:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 32:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 33:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 34:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 35:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 36:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 37:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 38:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 39:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 40:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 41:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 42:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 43:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 44:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 45:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 46:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 47:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 48:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 49:
			((CommandBinding)target).CanExecute += CommandBinding_CanExecute;
			((CommandBinding)target).Executed += CommandBinding_Executed;
			break;
		case 50:
			GD_Main = (Grid)target;
			break;
		case 51:
			UI_Menu = (Menu)target;
			break;
		case 52:
			MI_File = (MenuItem)target;
			break;
		case 53:
			UI_Dock = (DockManager)target;
			break;
		case 54:
			UI_Under = (UnderBar)target;
			break;
		case 55:
			UI_Leak = (DialogWidgetWithLeak)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}

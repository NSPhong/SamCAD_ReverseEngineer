using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Microsoft.Win32;
using SamCAD.Control;
using SamCAD.Import;
using SamSoarII.Polyline;
using SamSoarII.Polyline.Arguments;
using SamSoarII.Polyline.Entity;
using SamSoarII.Utility;
using SamSoarII.Utility.DXF;

namespace SamCAD;

public class RootMain
{
	private enum Status
	{
		None,
		ImageJpeg
	}

	private const int ImageJpeg_Span = 20;

	private PolylineProject project;

	private Printer printer = new Printer();

	private DispatcherTimer timer;

	private Status status = Status.None;

	private int tick;

	private int tickmax;

	private Stream stream;

	private Action<Stream> streamaction;

	private List<IPolylineGroup> groups = new List<IPolylineGroup>();

	private List<IPolylineEntity> virts = new List<IPolylineEntity>();

	private List<IPolylineEntity> reals = new List<IPolylineEntity>();

	private IPolylineArgument dxfdefaultargument = new HMIPLINEArgument();

	public PolylineProject Project => project;

	public Printer Printer => printer;

	public void GetImageJpeg(Action<Stream> _streamaction)
	{
		if (timer == null)
		{
			timer = new DispatcherTimer(new TimeSpan(0, 0, 0, 0, 10), DispatcherPriority.Normal, OnTimer, App.Client.Dispatcher);
		}
		if (stream != null)
		{
			stream.Close();
		}
		streamaction = _streamaction;
		stream = new MemoryStream();
		tickmax = project.Items.Count * 20;
		tick = 0;
		status = Status.ImageJpeg;
		App.Client.WaitBegin();
	}

	public void RefreshRedo(IPolylineImage _selectedimage, IPolylineAction _action)
	{
		App.Client.UI_Select.SelectedImage = _selectedimage;
		App.Client.UI_Select.UpdateRedo(_action);
		App.Client.UI_Editor.DrawingAll();
	}

	public void RefreshUndo(IPolylineImage _selectedimage, IPolylineAction _action)
	{
		App.Client.UI_Select.SelectedImage = _selectedimage;
		App.Client.UI_Select.UpdateUndo(_action);
		App.Client.UI_Editor.DrawingAll();
	}

	public void SetDxfDefaultArgument(IPolylineArgument arg)
	{
		if (dxfdefaultargument != arg)
		{
			dxfdefaultargument.Load(arg);
		}
	}

	public bool HasSelectedImage()
	{
		return project != null && App.Client?.UI_Select?.SelectedImage != null;
	}

	public bool HasSelectedGroup()
	{
		if (!HasSelectedImage())
		{
			return false;
		}
		IPolylineImage selectedImage = App.Client.UI_Select.SelectedImage;
		if (selectedImage.SelectedStart < 0 || selectedImage.SelectedCount <= 0)
		{
			return false;
		}
		groups.Clear();
		virts.Clear();
		reals.Clear();
		foreach (IPolylineEntity selectedEntity in selectedImage.SelectedEntities)
		{
			if (!selectedEntity.IsReal)
			{
				virts.Add(selectedEntity);
				continue;
			}
			if (groups.Count() == 0 || selectedEntity.Group != groups.Last())
			{
				groups.Add(selectedEntity.Group);
			}
			reals.Add(selectedEntity);
		}
		return groups.Count() > 0;
	}

	public bool CanNew()
	{
		return true;
	}

	public bool CanOpen()
	{
		return true;
	}

	public bool CanSave()
	{
		return project != null;
	}

	public bool CanSaveAs()
	{
		return project != null;
	}

	public bool CanClose()
	{
		return true;
	}

	public bool CanCreateImage()
	{
		return project != null;
	}

	public bool CanImportImage()
	{
		return project != null;
	}

	public bool CanRemoveImage()
	{
		return HasSelectedImage();
	}

	public bool CanResizeImage()
	{
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		return project != null && polylineImage != null;
	}

	public bool CanUndo()
	{
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		return project != null && polylineImage != null && polylineImage.CanUndo() && !App.Client.UI_Editor.IsEditing;
	}

	public bool CanRedo()
	{
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		return project != null && polylineImage != null && polylineImage.CanRedo() && !App.Client.UI_Editor.IsEditing;
	}

	public bool CanCopy()
	{
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		return project != null && polylineImage != null && !App.Client.UI_Editor.IsEditing && App.Client.UI_Select.SelectedEntity != null;
	}

	public bool CanCut()
	{
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		return project != null && polylineImage != null && !App.Client.UI_Editor.IsEditing && App.Client.UI_Select.SelectedEntity != null;
	}

	public bool CanPaste()
	{
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		return project != null && polylineImage != null && !App.Client.UI_Editor.IsEditing && App.Client.UI_Select.SelectedEntity != null;
	}

	public bool CanDelete()
	{
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		return project != null && polylineImage != null && !App.Client.UI_Editor.IsEditing && App.Client.UI_Select.SelectedEntity != null;
	}

	public bool CanShowEditor()
	{
		return project != null;
	}

	public bool CanShowSelect()
	{
		return project != null;
	}

	public bool CanShowArgument()
	{
		return project != null;
	}

	public bool CanShowDataGrid()
	{
		return project != null;
	}

	public bool CanDrawVirt()
	{
		return HasSelectedImage();
	}

	public bool CanDrawLine()
	{
		return HasSelectedImage();
	}

	public bool CanDrawArch()
	{
		return HasSelectedImage();
	}

	public bool CanDrawCircle()
	{
		return HasSelectedImage();
	}

	public bool CanDrawRect()
	{
		return HasSelectedImage();
	}

	public bool CanDrawFreeRect()
	{
		return HasSelectedImage();
	}

	public bool CanGroupMerge()
	{
		return HasSelectedGroup() && groups.Count() > 1;
	}

	public bool CanGroupSplit()
	{
		return HasSelectedGroup() && reals.Count() > 0 && virts.Count() == 0;
	}

	public bool CanGroupMove()
	{
		return HasSelectedGroup() && reals.Count() > 0;
	}

	public bool CanGroupReverse()
	{
		return HasSelectedGroup() && groups.Count() > 0;
	}

	public bool CanGroupMirror()
	{
		return HasSelectedGroup() && groups.Count() > 0;
	}

	public bool CanGroupRotate()
	{
		return HasSelectedGroup() && groups.Count() > 0;
	}

	public bool CanGroupScale()
	{
		return HasSelectedGroup() && groups.Count() > 0;
	}

	public bool CanGroupMatrix()
	{
		return HasSelectedGroup() && groups.Count() > 0;
	}

	public bool CanGroupExpand()
	{
		return HasSelectedGroup() && groups.Count() > 0;
	}

	public bool CanGroupFill()
	{
		return HasSelectedGroup() && groups.Count() > 0;
	}

	public bool CanGroupReorder()
	{
		return HasSelectedImage();
	}

	public bool CanEntityBreak()
	{
		return HasSelectedImage();
	}

	public bool CanEntityRound()
	{
		return HasSelectedImage();
	}

	public bool CanEntityBevel()
	{
		return HasSelectedImage();
	}

	public bool CanEntitySharp()
	{
		return HasSelectedImage();
	}

	public bool CanUserBook()
	{
		return true;
	}

	public bool CanAbout()
	{
		return true;
	}

	public bool CanPageSetup()
	{
		return project != null;
	}

	public bool CanPrint()
	{
		return project != null;
	}

	public bool CanPrintPreview()
	{
		return project != null;
	}

	public bool CanExport()
	{
		return project != null;
	}

	public bool CanImport()
	{
		return project != null;
	}

	public bool CanTableUser()
	{
		return project != null;
	}

	public bool CanTableModify()
	{
		return project != null;
	}

	public bool New(ImageArgumentTypes argtype)
	{
		return SaveAsk(delegate
		{
			project = new PolylineProject(argtype);
			printer.Project = project;
			App.Client.Project = project;
		}) != MessageBoxResult.Cancel;
	}

	public bool Open()
	{
		return SaveAsk(delegate
		{
			OpenFileDialog openFileDialog = new OpenFileDialog
			{
				Filter = "dxf file: *.dxf|SamCAD file: *.sca file: *.ssd",
				RestoreDirectory = true
			};
			if (openFileDialog.ShowDialog() == true)
			{
				project = new PolylineProject(openFileDialog.FileName, dxfdefaultargument);
				printer.Project = project;
				App.Client.Project = project;
				AdjustSizeAsync();
			}
		}) != MessageBoxResult.Cancel;
	}

	public void Open(string filename, LoadMode loadmode, bool startatfirstentity, ImageArgumentTypes argumenttype)
	{
		switch (argumenttype)
		{
		case ImageArgumentTypes.HMIBLOCK:
			if (!(dxfdefaultargument is IHMIBLOCKArgument))
			{
				dxfdefaultargument = new HMIBLOCKArgument();
			}
			break;
		case ImageArgumentTypes.HMIPLINE:
			if (!(dxfdefaultargument is IHMIPLINEArgument))
			{
				dxfdefaultargument = new HMIPLINEArgument();
			}
			break;
		}
		if (loadmode.E == Enum_LoadMode.Drill)
		{
			project = PolylineProject.CreateDrill(filename, dxfdefaultargument);
		}
		else
		{
			project = new PolylineProject(filename, dxfdefaultargument);
		}
		if (startatfirstentity)
		{
			foreach (IPolylineImage item in project.Items)
			{
				IPolylineEntity polylineEntity = item.Items.FirstOrDefault();
				if (!(polylineEntity is IPolylineLine))
				{
					continue;
				}
				IPolylineLine polylineLine = (IPolylineLine)polylineEntity;
				if (polylineLine.IsReal)
				{
					continue;
				}
				item.StartPoint = polylineLine.To;
				List<IPolylineEntity> list = new List<IPolylineEntity>();
				foreach (IPolylineEntity item2 in item.Items)
				{
					list.Add(item2.Move(-(Vector)item.StartPoint));
				}
				item.StartPoint = new Point(0.0, 0.0);
				item.Replace(0, item.Items.Count, list);
			}
		}
		printer.Project = project;
		App.Client.Project = project;
		AdjustSizeAsync();
	}

	public bool Save(Action next = null)
	{
		if (project == null)
		{
			return false;
		}
		if (project.Filename == null)
		{
			return SaveAs(next);
		}
		string extension = Path.GetExtension(project.Filename);
		if (extension.Equals(".ssd"))
		{
			GetImageJpeg(delegate(Stream stream)
			{
				project.SaveSSD(project.Filename, stream);
				next?.Invoke();
			});
		}
		else
		{
			project.Save(project.Filename);
			next?.Invoke();
		}
		return true;
	}

	public bool SaveAs(Action next = null)
	{
		SaveFileDialog dialog = new SaveFileDialog();
		dialog.RestoreDirectory = true;
		dialog.Filter = "SamCAD file: *.sca file: *.ssd";
		if (dialog.ShowDialog() != true)
		{
			next?.Invoke();
			return false;
		}
		string extension = Path.GetExtension(dialog.FileName);
		if (extension.Equals(".ssd"))
		{
			GetImageJpeg(delegate(Stream stream)
			{
				project.SaveSSD(dialog.FileName, stream);
				next?.Invoke();
			});
		}
		else
		{
			project.Save(dialog.FileName);
			next?.Invoke();
		}
		return true;
	}

	public MessageBoxResult SaveAsk(Action next = null)
	{
		if (project == null)
		{
			next?.Invoke();
			return MessageBoxResult.No;
		}
		string messageBoxText = $"isno保存 {project.Name}?";
		MessageBoxResult messageBoxResult = MessageBox.Show(messageBoxText, "Attention", MessageBoxButton.YesNoCancel);
		if (messageBoxResult == MessageBoxResult.Yes)
		{
			Save(next);
		}
		if (messageBoxResult == MessageBoxResult.No)
		{
			next?.Invoke();
		}
		return messageBoxResult;
	}

	public bool Close()
	{
		if (project == null)
		{
			return false;
		}
		return SaveAsk(delegate
		{
			App.Client.Project = null;
			printer.Project = null;
			project = null;
		}) != MessageBoxResult.Cancel;
	}

	public void AdjustSizeAsync()
	{
		Task.Factory.StartNew(delegate
		{
			Thread.Sleep(1000);
			Application.Current.Dispatcher.Invoke((ThreadStart)delegate
			{
				MainWindow mainWindow = (MainWindow)Application.Current.MainWindow;
				IPolylineImage selectedImage = mainWindow.SelectedImage;
				if (selectedImage != null)
				{
					Rect boundary = mainWindow.UI_Editor.GetBoundary(selectedImage, autoleft: true, autoright: true, autotop: true, autobottom: true, visibleall: true);
					selectedImage.Left = boundary.Left;
					selectedImage.Top = boundary.Top;
					selectedImage.Width = boundary.Width;
					selectedImage.Height = boundary.Height;
				}
			});
		});
	}

	public bool CreateImage()
	{
		if (project == null)
		{
			return false;
		}
		PolylineImage image = new PolylineImage();
		int num = 0;
		while (project.Items.FirstOrDefault((IPolylineImage i) => i.Name.Equals(image.Name)) != null)
		{
			image.Name = $"New graphic _{++num}";
		}
		image.ArgumentType = project.ArgumentType;
		project.Items.Add(image);
		App.Client.UI_Select.SelectedImage = image;
		return true;
	}

	public bool ImportImage()
	{
		if (project == null)
		{
			return false;
		}
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.RestoreDirectory = true;
		openFileDialog.Filter = "dxf file: *.dxf";
		if (openFileDialog.ShowDialog() != true)
		{
			return false;
		}
		string fileName = openFileDialog.FileName;
		string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);
		DXFModel dXFModel = new DXFModel();
		PolylineImage image = new PolylineImage();
		image.ArgumentType = project.ArgumentType;
		int num = 0;
		dXFModel.Convert(fileName);
		image.Load(dXFModel);
		image.Name = fileNameWithoutExtension;
		while (project.Items.FirstOrDefault((IPolylineImage i) => i.Name.Equals(image.Name)) != null)
		{
			image.Name = $"{fileNameWithoutExtension}_{++num}";
		}
		project.Items.Add(image);
		App.Client.UI_Select.SelectedImage = image;
		return true;
	}

	public bool RemoveImage()
	{
		if (!HasSelectedImage())
		{
			return false;
		}
		IPolylineImage selectedImage = App.Client.UI_Select.SelectedImage;
		if (MessageBox.Show($"isno删除图形 {selectedImage.Name}？", "Attention", MessageBoxButton.YesNo) != MessageBoxResult.Yes)
		{
			return false;
		}
		project.Items.Remove(selectedImage);
		if (project.Items.Count() == 0)
		{
			project.Items.Add(new PolylineImage
			{
				ArgumentType = project.ArgumentType
			});
		}
		App.Client.UI_Select.SelectedImage = project.Items.FirstOrDefault();
		return true;
	}

	public bool ResizeImage()
	{
		App.Client?.ShowDialog_ImageResize();
		return true;
	}

	public bool Undo()
	{
		App.Client.UI_Select.SelectedEntity = null;
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		IPolylineAction polylineAction = polylineImage?.Undo();
		if (polylineAction == null)
		{
			return false;
		}
		RefreshUndo(polylineImage, polylineAction);
		return true;
	}

	public bool Redo()
	{
		App.Client.UI_Select.SelectedEntity = null;
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		IPolylineAction polylineAction = polylineImage?.Redo();
		if (polylineAction == null)
		{
			return false;
		}
		RefreshRedo(polylineImage, polylineAction);
		return true;
	}

	public bool Copy()
	{
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		List<IPolylineEntity> list = App.Client?.UI_Select?.SelectedEntities?.ToList();
		list.Sort((IPolylineEntity e1, IPolylineEntity e2) => e1.ID.CompareTo(e2.ID));
		if (polylineImage == null || list == null)
		{
			return false;
		}
		IPolylineAction action = polylineImage.Copy(list);
		RefreshRedo(polylineImage, action);
		return true;
	}

	public bool Cut()
	{
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		List<IPolylineEntity> list = App.Client?.UI_Select?.SelectedEntities?.ToList();
		list.Sort((IPolylineEntity e1, IPolylineEntity e2) => e1.ID.CompareTo(e2.ID));
		if (polylineImage == null || list == null)
		{
			return false;
		}
		App.Client.UI_Select.SelectedEntity = null;
		IPolylineAction action = polylineImage.Cut(list);
		RefreshRedo(polylineImage, action);
		return true;
	}

	public bool Paste()
	{
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		if (polylineImage == null)
		{
			return false;
		}
		int index = polylineImage.SelectedStart + polylineImage.SelectedCount - 1;
		App.Client.UI_Select.SelectedEntity = null;
		IPolylineAction action = polylineImage.Paste(index);
		RefreshRedo(polylineImage, action);
		return true;
	}

	public bool Delete()
	{
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		IList<IPolylineEntity> list = App.Client?.UI_Select?.SelectedEntities?.ToList();
		if (polylineImage == null || list == null)
		{
			return false;
		}
		App.Client.UI_Select.SelectedEntity = null;
		IPolylineAction action = polylineImage.Remove(list);
		RefreshRedo(polylineImage, action);
		return true;
	}

	public bool GroupMerge()
	{
		if (!CanGroupMerge())
		{
			return false;
		}
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		IPolylineAction polylineAction = polylineImage?.GroupMerge(groups);
		if (polylineAction == null)
		{
			return false;
		}
		RefreshRedo(polylineImage, polylineAction);
		return true;
	}

	public bool GroupSplit()
	{
		if (!CanGroupSplit())
		{
			return false;
		}
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		IPolylineAction polylineAction = polylineImage?.GroupSplit(reals);
		if (polylineAction == null)
		{
			return false;
		}
		RefreshRedo(polylineImage, polylineAction);
		return false;
	}

	public bool GroupMove()
	{
		if (!CanGroupMove())
		{
			return false;
		}
		App.Client?.ShowDialog_GroupMove();
		return true;
	}

	public bool GroupMove(Vector move)
	{
		if (!CanGroupMove())
		{
			return false;
		}
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		IPolylineAction polylineAction = polylineImage?.GroupMove(polylineImage.SelectedEntities.ToList(), move);
		if (polylineAction == null)
		{
			return false;
		}
		RefreshRedo(polylineImage, polylineAction);
		return false;
	}

	public bool GroupReverse()
	{
		if (!CanGroupReverse())
		{
			return false;
		}
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		IPolylineAction polylineAction = polylineImage?.GroupReverse(groups);
		if (polylineAction == null)
		{
			return false;
		}
		RefreshRedo(polylineImage, polylineAction);
		return false;
	}

	public bool GroupScale()
	{
		if (!CanGroupScale())
		{
			return false;
		}
		App.Client?.ShowDialog_GroupScale();
		return true;
	}

	public bool GroupScale(Point p, double xs, double ys)
	{
		if (!CanGroupScale())
		{
			return false;
		}
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		IPolylineAction polylineAction = polylineImage?.GroupScale(groups, p, xs, ys);
		if (polylineAction == null)
		{
			return false;
		}
		RefreshRedo(polylineImage, polylineAction);
		return true;
	}

	public bool GroupMirror()
	{
		if (!CanGroupMirror())
		{
			return false;
		}
		App.Client?.UI_Editor?.GroupMirror();
		return false;
	}

	public bool GroupMirror(Point p, Vector v)
	{
		if (!CanGroupMirror())
		{
			return false;
		}
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		IPolylineAction polylineAction = polylineImage?.GroupMirror(groups, p, v);
		if (polylineAction == null)
		{
			return false;
		}
		RefreshRedo(polylineImage, polylineAction);
		return true;
	}

	public bool GroupRotate()
	{
		if (!CanGroupRotate())
		{
			return false;
		}
		App.Client?.ShowDialog_GroupRotate();
		return false;
	}

	public bool GroupRotate(Point s, double a)
	{
		if (!CanGroupRotate())
		{
			return false;
		}
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		IPolylineAction polylineAction = polylineImage?.GroupRotate(groups, s, a);
		if (polylineAction == null)
		{
			return false;
		}
		RefreshRedo(polylineImage, polylineAction);
		return true;
	}

	public bool GroupReorder()
	{
		if (!CanGroupReorder())
		{
			return false;
		}
		App.Client?.ShowDialog_GroupReorder();
		return false;
	}

	public bool GroupReorder(IList<IPolylineReorderingGroup> regroups)
	{
		if (!CanGroupReorder())
		{
			return false;
		}
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		IPolylineAction action = polylineImage.ReorderSpecific(regroups);
		App.Client?.UI_Select?.ReorderEnd();
		RefreshRedo(polylineImage, action);
		return true;
	}

	public bool GroupReorder(ReorderingStrategy strategy)
	{
		if (!CanGroupReorder())
		{
			return false;
		}
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		IPolylineAction action = polylineImage.ReorderIntelligence(strategy);
		App.Client?.UI_Select?.ReorderEnd();
		RefreshRedo(polylineImage, action);
		return true;
	}

	public bool GroupMatrix()
	{
		if (!CanGroupMatrix())
		{
			return false;
		}
		App.Client?.ShowDialog_GroupMatrix();
		return true;
	}

	public bool GroupMatrix(int row, int column, Point offset, MatrixStrategy strategy, MatrixPriority priority)
	{
		if (!CanGroupMatrix())
		{
			return false;
		}
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		IPolylineAction polylineAction = polylineImage?.GroupMatrix(groups, row, column, offset, strategy, priority);
		if (polylineAction == null)
		{
			return false;
		}
		RefreshRedo(polylineImage, polylineAction);
		return true;
	}

	public bool GroupExpand()
	{
		if (!CanGroupExpand())
		{
			return false;
		}
		App.Client?.ShowDialog_GroupExpand();
		return true;
	}

	public bool GroupExpand(double r)
	{
		if (!CanGroupExpand())
		{
			return false;
		}
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		IPolylineAction polylineAction = polylineImage?.GroupExpand(groups, r, removeold: true);
		if (polylineAction == null)
		{
			return false;
		}
		RefreshRedo(polylineImage, polylineAction);
		return true;
	}

	public bool GroupFill()
	{
		if (!CanGroupFill())
		{
			return false;
		}
		App.Client?.ShowDialog_GroupFill();
		return true;
	}

	public bool GroupFill(double r, FillStrategy strategy, bool removeold)
	{
		if (!CanGroupFill())
		{
			return false;
		}
		IPolylineImage polylineImage = App.Client?.UI_Select?.SelectedImage;
		IPolylineAction polylineAction = polylineImage?.GroupFill(groups, r, strategy, removeold);
		if (polylineAction == null)
		{
			return false;
		}
		RefreshRedo(polylineImage, polylineAction);
		return true;
	}

	public bool EntityBreak()
	{
		if (!CanEntityBreak())
		{
			return false;
		}
		App.Client?.UI_Editor?.EntityBreak();
		return true;
	}

	public bool EntityRound()
	{
		if (!CanEntityRound())
		{
			return false;
		}
		App.Client?.UI_Editor?.EntityRound();
		return true;
	}

	public bool EntityBevel()
	{
		if (!CanEntityBevel())
		{
			return false;
		}
		App.Client?.UI_Editor?.EntityBevel();
		return true;
	}

	public bool EntitySharp()
	{
		if (!CanEntitySharp())
		{
			return false;
		}
		App.Client?.UI_Editor?.EntitySharp();
		return true;
	}

	public bool DrawVirt()
	{
		if (!CanDrawVirt())
		{
			return false;
		}
		App.Client?.UI_Editor?.CommandLeftTool(PolylineType.Line, isreal: false);
		return true;
	}

	public bool DrawLine()
	{
		if (!CanDrawLine())
		{
			return false;
		}
		App.Client?.UI_Editor?.CommandLeftTool(PolylineType.Line);
		return true;
	}

	public bool DrawCircle()
	{
		if (!CanDrawCircle())
		{
			return false;
		}
		App.Client?.UI_Editor?.CommandLeftTool(PolylineType.Circle);
		return true;
	}

	public bool DrawArch()
	{
		if (!CanDrawArch())
		{
			return false;
		}
		App.Client?.UI_Editor?.CommandLeftTool(PolylineType.Arch);
		return true;
	}

	public bool DrawRect()
	{
		if (!CanDrawRect())
		{
			return false;
		}
		App.Client?.UI_Editor?.CommandLeftTool(PolylineType.Rect);
		return true;
	}

	public bool DrawFreeRect()
	{
		if (!CanDrawFreeRect())
		{
			return false;
		}
		App.Client?.UI_Editor?.CommandLeftTool(PolylineType.FreeRect);
		return true;
	}

	public bool UserBook()
	{
		Process process = new Process();
		process.StartInfo.FileName = $"{FileHelper.AppRootPath}\\SamCAD User Manual.pdf";
		process.StartInfo.CreateNoWindow = true;
		process.Start();
		return true;
	}

	public bool About()
	{
		App.Client?.ShowDialog_About();
		return true;
	}

	public bool PageSetup()
	{
		printer.ShowDialog_PageSetup();
		return true;
	}

	public bool Print()
	{
		printer.ShowDialog_Print();
		return true;
	}

	public bool PrintPreview()
	{
		printer.ShowDialog_PrintPreview();
		return true;
	}

	public bool Import()
	{
		App.Client?.ShowDialog_Import();
		return true;
	}

	public bool Export()
	{
		App.Client?.ShowDialog_Export();
		return true;
	}

	public bool TableUser()
	{
		App.Client?.ShowDialog_UserSet();
		return true;
	}

	public bool TableModify()
	{
		App.Client?.ShowDialog_MulMod();
		return true;
	}

	private void OnTimer(object sender, EventArgs e)
	{
		Status status = this.status;
		Status status2 = status;
		if (status2 != Status.ImageJpeg)
		{
			return;
		}
		int num = tick / 20;
		int num2 = tick % 20;
		App.Client.UI_Waiting.Value = (double)tick * 100.0 / (double)tickmax;
		tick++;
		switch (num2)
		{
		case 0:
		{
			PolylineEditor uI_Editor2 = App.Client.UI_Editor;
			IPolylineImage selectedImage = project.Items[num];
			App.Client.UI_Select.SelectedImage = selectedImage;
			App.Client.UI_Waiting.Message = $"The {num} th image is being saved...";
			ScaleTransform renderTransform = new ScaleTransform(0.25, 0.25);
			uI_Editor2.RenderTransform = renderTransform;
			break;
		}
		case 19:
		{
			PolylineEditor uI_Editor = App.Client.UI_Editor;
			RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap((int)(uI_Editor.ActualWidth * 0.25), (int)(uI_Editor.ActualHeight * 0.25), 96.0, 96.0, PixelFormats.Pbgra32);
			BmpBitmapEncoder bmpBitmapEncoder = new BmpBitmapEncoder();
			string text = Path.Combine(Path.GetTempPath(), "temppic.bmp");
			FileStream fileStream = File.Open(text, FileMode.Create);
			renderTargetBitmap.Render(uI_Editor);
			bmpBitmapEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));
			bmpBitmapEncoder.Save(fileStream);
			fileStream.Close();
			QImageEncoder qImageEncoder = new QImageEncoder();
			byte[] qImageData = qImageEncoder.GetQImageData(text);
			stream.Write(new byte[4] { 1, 0, 0, 0 }, 0, 4);
			stream.Write(qImageData, 0, qImageData.Length);
			if (num == project.Items.Count() - 1)
			{
				this.status = Status.None;
				App.Client?.WaitEnd();
				streamaction?.Invoke(stream);
				streamaction = null;
				uI_Editor.RenderTransform = null;
			}
			break;
		}
		}
	}
}

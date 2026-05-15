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
using SamSoarII.Polyline.Control;
using SamSoarII.Polyline.Entity;
using SamSoarII.Shell;

namespace SamSoarII.Polyline;

public class PolylineEditor : UserControl, IDockContent, INotifyPropertyChanged, IComponentConnector
{
	protected static readonly DependencyProperty ProjectProperty = DependencyProperty.Register("Project", typeof(IPolylineProject), typeof(PolylineEditor), new PropertyMetadata(null, OnPropertyChanged_Project));

	protected static readonly DependencyProperty SelectedImageProperty = DependencyProperty.Register("SelectedImage", typeof(IPolylineImage), typeof(PolylineEditor), new PropertyMetadata(null, OnPropertyChanged_SelectedImage));

	internal Button BN_AddVirt;

	internal Button BN_AddLine;

	internal Button BN_AddArch;

	internal Button BN_AddCircle;

	internal Button BN_AddRect;

	internal Button BN_AddFreeRect;

	internal Button BN_AddEllipse;

	internal Button BN_AddEllipseArch;

	internal Button BN_AddB2Spline;

	internal Button BN_EBreak;

	internal Button BN_ERound;

	internal Button BN_EBevel;

	internal Button BN_ESharp;

	internal Button BN_GMerge;

	internal Button BN_GSplit;

	internal Button BN_GMove;

	internal Button BN_GRotate;

	internal Button BN_GScale;

	internal Button BN_GMirror;

	internal Button BN_GReverse;

	internal Button BN_GMatrix;

	internal Button BN_Expand;

	internal Button BN_Fill;

	internal GridPenning UI_Penning;

	internal PolylineEditorPanel UI_Panel;

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

	public bool IsEditing => UI_Panel?.IsEditing ?? false;

	ushort IDockContent.DockID => 10000;

	ImageSource IDockContent.Icon => null;

	string IDockContent.Header => "Drawing board";

	public event PropertyChangedEventHandler PropertyChanged;

	public event PolylineEntityEventHandler EntityClick;

	public event PolylineEntityEventHandler EntitySetting;

	public event PolylineActionEventHandler EntityAction;

	public event PolylineActionEventHandler EntityUndo;

	public event PolylineActionEventHandler EntityInsert;

	public event MouseActionEventHandler MouseActionStart;

	public event MouseActionEventHandler MouseActionDone;

	public event MouseActionEventHandler MouseActionEscape;

	public event MousePositionEventHandler MousePositionMove;

	public PolylineEditor()
	{
		InitializeComponent();
	}

	private static void OnPropertyChanged_Project(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineEditor)
		{
			((PolylineEditor)d).OnProjectChanged(e);
		}
	}

	protected virtual void OnProjectChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_SelectedImage(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineEditor)
		{
			((PolylineEditor)d).OnSelectedImageChanged(e);
		}
	}

	protected virtual void OnSelectedImageChanged(DependencyPropertyChangedEventArgs e)
	{
		GridPenning uI_Penning = UI_Penning;
		IGridPenningSource drawingSource;
		if (SelectedImage == null)
		{
			IGridPenningSource gridPenningSource = new DefaultGridPenningSource();
			drawingSource = gridPenningSource;
		}
		else
		{
			IGridPenningSource gridPenningSource = SelectedImage;
			drawingSource = gridPenningSource;
		}
		uI_Penning.DrawingSource = drawingSource;
		UI_Panel.CtrlReset();
	}

	public void Select(int start, int count)
	{
		if (SelectedImage != null)
		{
			UI_Panel.Select(SelectedImage.SelectedEntity);
			UI_Panel.CtrlUpdate();
			UI_Panel.ZoneUpdate();
		}
	}

	public void IvSelect(SelectionChangedEventArgs e)
	{
		UI_Panel.SelectionFreezed = true;
		if (e.RemovedItems != null)
		{
			foreach (IPolylineEntity removedItem in e.RemovedItems)
			{
				if (removedItem is IPolylineGroup)
				{
					IPolylineGroup polylineGroup = (IPolylineGroup)removedItem;
					foreach (IPolylineEntity item in polylineGroup)
					{
						if (UI_Panel.Selections.Contains(item))
						{
							UI_Panel.Selections.Remove(item);
						}
					}
				}
				else if (UI_Panel.Selections.Contains(removedItem))
				{
					UI_Panel.Selections.Remove(removedItem);
				}
			}
		}
		if (e.AddedItems != null)
		{
			foreach (IPolylineEntity addedItem in e.AddedItems)
			{
				if (addedItem is IPolylineGroup)
				{
					IPolylineGroup polylineGroup2 = (IPolylineGroup)addedItem;
					foreach (IPolylineEntity item2 in polylineGroup2)
					{
						if (!UI_Panel.Selections.Contains(item2))
						{
							UI_Panel.Selections.Add(item2);
						}
					}
				}
				else if (!UI_Panel.Selections.Contains(addedItem))
				{
					UI_Panel.Selections.Add(addedItem);
				}
			}
		}
		UI_Panel.SelectionFreezed = false;
	}

	public void DrawingAll()
	{
		UI_Penning.DrawingAll();
	}

	public void GroupMirror()
	{
		UI_Panel.StartMirror();
	}

	public void EntityBreak()
	{
		UI_Panel.StartBreak();
	}

	public void EntityRound()
	{
		UI_Panel.StartRound();
	}

	public void EntityBevel()
	{
		UI_Panel.StartBevel();
	}

	public void EntitySharp()
	{
		UI_Panel.StartSharp();
	}

	public Rect GetBoundary(IPolylineImage image, bool autoleft, bool autoright, bool autotop, bool autobottom, bool visibleall)
	{
		double num = 0.0;
		double num2 = 100.0;
		double num3 = 0.0;
		double num4 = 100.0;
		if ((autoleft || autoright || autotop || autobottom) && image.Items.Count() == 0)
		{
			return new Rect(0.0, 0.0, 100.0, 100.0);
		}
		if (autoleft)
		{
			num = double.MaxValue;
			foreach (IPolylineEntity item in image.Items)
			{
				num = Math.Min(num, item.Bounding.Left);
			}
		}
		if (autoright)
		{
			num2 = double.MinValue;
			foreach (IPolylineEntity item2 in image.Items)
			{
				num2 = Math.Max(num2, item2.Bounding.Right);
			}
		}
		if (autotop)
		{
			num3 = double.MaxValue;
			foreach (IPolylineEntity item3 in image.Items)
			{
				num3 = Math.Min(num3, item3.Bounding.Top);
			}
		}
		if (autobottom)
		{
			num4 = double.MinValue;
			foreach (IPolylineEntity item4 in image.Items)
			{
				num4 = Math.Max(num4, item4.Bounding.Bottom);
			}
		}
		double num5 = num2 - num;
		double num6 = num4 - num3;
		if (autoleft)
		{
			num -= num5 * 0.05;
		}
		if (autoright)
		{
			num2 += num5 * 0.05;
		}
		if (autotop)
		{
			num3 -= num6 * 0.05;
		}
		if (autobottom)
		{
			num4 += num6 * 0.05;
		}
		if (visibleall)
		{
			double num7 = (num2 - num) / (base.ActualWidth - 64.0);
			double num8 = (num4 - num3) / (base.ActualHeight - 32.0);
			if (num8 < num7)
			{
				num6 = num4 - num3;
				num3 -= num6 * (num7 / num8 - 1.0) * 0.5;
				num4 += num6 * (num7 / num8 - 1.0) * 0.5;
			}
			else if (num8 > num7)
			{
				num5 = num2 - num;
				num -= num5 * (num8 / num7 - 1.0) * 0.5;
				num2 += num5 * (num8 / num7 - 1.0) * 0.5;
			}
		}
		return new Rect(num, num3, num2 - num, num4 - num3);
	}

	protected void InvokePropertyChanged(string propname)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
	}

	internal void InvokeEntityClick(IPolylineEntity entity)
	{
		this.EntityClick?.Invoke(this, new PolylineEntityEventArgs(entity));
	}

	internal void InvokeEntitySetting(IPolylineEntity entity)
	{
		this.EntitySetting?.Invoke(this, new PolylineEntityEventArgs(entity));
	}

	internal void InvokeEntityAction(IPolylineAction action)
	{
		this.EntityAction?.Invoke(this, new PolylineActionEventArgs(SelectedImage, action));
	}

	internal void InvokeEntityUndo(IPolylineAction action)
	{
		this.EntityUndo?.Invoke(this, new PolylineActionEventArgs(SelectedImage, action));
	}

	private void OnLeftToolClick(object sender, RoutedEventArgs e)
	{
		PolylineType type = PolylineType.Line;
		if (sender == BN_AddLine || sender == BN_AddVirt)
		{
			type = PolylineType.Line;
		}
		if (sender == BN_AddArch)
		{
			type = PolylineType.Arch;
		}
		if (sender == BN_AddCircle)
		{
			type = PolylineType.Circle;
		}
		if (sender == BN_AddRect)
		{
			type = PolylineType.Rect;
		}
		if (sender == BN_AddFreeRect)
		{
			type = PolylineType.FreeRect;
		}
		if (sender == BN_AddEllipse)
		{
			type = PolylineType.Ellipse;
		}
		if (sender == BN_AddEllipseArch)
		{
			type = PolylineType.EllipseArch;
		}
		if (sender == BN_AddB2Spline)
		{
			type = PolylineType.B2Spline;
		}
		CommandLeftTool(type, sender != BN_AddVirt);
	}

	public void CommandLeftTool(PolylineType type, bool isreal = true)
	{
		if (SelectedImage == null)
		{
			return;
		}
		IPolylineEntity polylineEntity = null;
		IPolylineEntity polylineEntity2 = null;
		int val = SelectedImage.Items.Count;
		if (SelectedImage.SelectedStart >= 0 && SelectedImage.SelectedCount > 0)
		{
			val = SelectedImage.SelectedStart + SelectedImage.SelectedCount;
		}
		val = Math.Min(val, SelectedImage.Items.Count);
		if (val < SelectedImage.Items.Count)
		{
			polylineEntity2 = SelectedImage.Items[val];
		}
		if (val >= 0)
		{
			Point point = ((val > 0 && val - 1 < SelectedImage.Items.Count) ? SelectedImage.Items[val - 1].To : default(Point));
			switch (type)
			{
			case PolylineType.Line:
				polylineEntity = new PolylineLine(SelectedImage, val, point);
				break;
			case PolylineType.Arch:
				polylineEntity = new PolylineArch(SelectedImage, val, point, point);
				break;
			case PolylineType.Circle:
				polylineEntity = new PolylineCircle(SelectedImage, val, point, point);
				break;
			case PolylineType.Ellipse:
				polylineEntity = new PolylineEllipse(SelectedImage, val, point, point, new Vector(1.0, 0.0), 1.0, 1.0, _isclockwise: true);
				break;
			case PolylineType.EllipseArch:
				polylineEntity = new PolylineEllipseArch(SelectedImage, val, point, point, new Vector(1.0, 0.0), 1.0, 1.0, _isclockwise: true);
				break;
			case PolylineType.Rect:
				polylineEntity = new PolylineRect(SelectedImage, val, new Rect(point, point));
				break;
			case PolylineType.FreeRect:
				polylineEntity = new PolylineFreeRect(SelectedImage, val);
				break;
			case PolylineType.B2Spline:
				polylineEntity = new PolylineB2Spline(SelectedImage, val, point);
				break;
			}
			polylineEntity.IsReal = isreal;
			IPolylineAction action = SelectedImage.Insert(val, polylineEntity);
			this.EntityInsert?.Invoke(this, new PolylineActionEventArgs(SelectedImage, action));
			UI_Panel.Start(polylineEntity);
		}
	}

	public void CommandRightTool(IList<RoutedCommand> cmds)
	{
		BN_GMerge.Command = cmds[0];
		BN_GSplit.Command = cmds[1];
		BN_GMove.Command = cmds[2];
		BN_GRotate.Command = cmds[3];
		BN_GScale.Command = cmds[4];
		BN_GMirror.Command = cmds[5];
		BN_EBreak.Command = cmds[6];
		BN_ERound.Command = cmds[7];
		BN_ESharp.Command = cmds[8];
		BN_EBevel.Command = cmds[9];
		BN_GMatrix.Command = cmds[10];
		BN_GReverse.Command = cmds[11];
		BN_Expand.Command = cmds[12];
		BN_Fill.Command = cmds[13];
	}

	private void UI_Panel_MouseActionStart(object sender, MouseActionEventArgs e)
	{
		this.MouseActionStart?.Invoke(this, e);
	}

	private void UI_Panel_MouseActionDone(object sender, MouseActionEventArgs e)
	{
		UI_Penning.DrawingAll();
		this.MouseActionDone?.Invoke(this, e);
	}

	private void UI_Panel_MouseActionEscape(object sender, MouseActionEventArgs e)
	{
		UI_Penning.DrawingAll();
		this.MouseActionEscape?.Invoke(this, e);
	}

	private void UI_Panel_MousePositionMove(object sender, MousePositionEventArgs e)
	{
		this.MousePositionMove?.Invoke(this, e);
	}

	private void UI_Panel_MouseWheel(object sender, MouseWheelEventArgs e)
	{
		Point position = e.GetPosition(UI_Panel);
		double cx = position.X / UI_Panel.ActualWidth;
		double cy = (UI_Panel.ActualHeight - position.Y) / UI_Panel.ActualHeight;
		if (e.Delta > 0)
		{
			UI_Penning.ZoomInWithAnim(cx, cy);
		}
		else
		{
			UI_Penning.ZoomOutWithAnim();
		}
	}

	private void UI_Penning_EnterOutline(object sender, RoutedEventArgs e)
	{
		UI_Panel.PathHide();
		UI_Panel.CtrlHide();
		UI_Panel.ZoneHide();
	}

	private void UI_Penning_LeaveOutline(object sender, RoutedEventArgs e)
	{
		UI_Panel.PathShow();
		UI_Panel.CtrlShow();
		UI_Panel.ZoneShow();
	}

	private void UI_Penning_AnimStart(object sender, GridPenningAnimEventArgs e)
	{
		UI_Panel.PathHide();
		UI_Panel.CtrlHide();
		UI_Panel.ZoneHide();
	}

	private void UI_Penning_AnimEnd(object sender, GridPenningAnimEventArgs e)
	{
		UI_Panel.PathShow();
		UI_Panel.CtrlShow();
		UI_Panel.ZoneShow();
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamSoarII.Polyline;component/polylineeditor.xaml", UriKind.Relative);
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
			BN_AddVirt = (Button)target;
			BN_AddVirt.Click += OnLeftToolClick;
			break;
		case 2:
			BN_AddLine = (Button)target;
			BN_AddLine.Click += OnLeftToolClick;
			break;
		case 3:
			BN_AddArch = (Button)target;
			BN_AddArch.Click += OnLeftToolClick;
			break;
		case 4:
			BN_AddCircle = (Button)target;
			BN_AddCircle.Click += OnLeftToolClick;
			break;
		case 5:
			BN_AddRect = (Button)target;
			BN_AddRect.Click += OnLeftToolClick;
			break;
		case 6:
			BN_AddFreeRect = (Button)target;
			BN_AddFreeRect.Click += OnLeftToolClick;
			break;
		case 7:
			BN_AddEllipse = (Button)target;
			BN_AddEllipse.Click += OnLeftToolClick;
			break;
		case 8:
			BN_AddEllipseArch = (Button)target;
			BN_AddEllipseArch.Click += OnLeftToolClick;
			break;
		case 9:
			BN_AddB2Spline = (Button)target;
			BN_AddB2Spline.Click += OnLeftToolClick;
			break;
		case 10:
			BN_EBreak = (Button)target;
			break;
		case 11:
			BN_ERound = (Button)target;
			break;
		case 12:
			BN_EBevel = (Button)target;
			break;
		case 13:
			BN_ESharp = (Button)target;
			break;
		case 14:
			BN_GMerge = (Button)target;
			break;
		case 15:
			BN_GSplit = (Button)target;
			break;
		case 16:
			BN_GMove = (Button)target;
			break;
		case 17:
			BN_GRotate = (Button)target;
			break;
		case 18:
			BN_GScale = (Button)target;
			break;
		case 19:
			BN_GMirror = (Button)target;
			break;
		case 20:
			BN_GReverse = (Button)target;
			break;
		case 21:
			BN_GMatrix = (Button)target;
			break;
		case 22:
			BN_Expand = (Button)target;
			break;
		case 23:
			BN_Fill = (Button)target;
			break;
		case 24:
			UI_Penning = (GridPenning)target;
			UI_Penning.EnterOutline += UI_Penning_EnterOutline;
			UI_Penning.LeaveOutline += UI_Penning_LeaveOutline;
			UI_Penning.AnimStart += UI_Penning_AnimStart;
			UI_Penning.AnimEnd += UI_Penning_AnimEnd;
			break;
		case 25:
			UI_Panel = (PolylineEditorPanel)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}

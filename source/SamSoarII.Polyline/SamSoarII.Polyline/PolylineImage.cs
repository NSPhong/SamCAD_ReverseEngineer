using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using SamSoarII.Core.Files;
using SamSoarII.Polyline.Arguments;
using SamSoarII.Polyline.Entity;
using SamSoarII.Polyline.Entity.Module;
using SamSoarII.Polyline.Entity.User;
using SamSoarII.Polyline.Expands;
using SamSoarII.Polyline.Export;
using SamSoarII.Shell;
using SamSoarII.Utility.DXF;

namespace SamSoarII.Polyline;

public class PolylineImage : IPolylineImage, INotifyPropertyChanged, IDisposable, IGridPenningSourceEX, IGridPenningSource
{
	private enum Directions
	{
		Up,
		Down,
		Left,
		Right
	}

	private bool isdisposed = false;

	private string name;

	private string filename;

	private ImageArgumentTypes argumenttype;

	private bool isdrill;

	private ObservableCollection<IPolylineEntity> items;

	private ObservableCollection<IPolylineGroup> groups;

	private ObservableCollection<IPolylineUserFormat> userfmts;

	private int selectedstart;

	private int selectedcount;

	private double top;

	private double left;

	private double width;

	private double height;

	private int skipindex;

	private IImageArgument argument;

	private List<IPolylineAction> undos;

	private List<IPolylineAction> redos;

	private PolylineExportCore epcore;

	private Point startpoint;

	private IPolylineEntity nowitem;

	private IPolylineEntity nexitem;

	private bool ispaste;

	private List<IPolylineEntity> pasteitems;

	private Rect oldsize;

	private Rect newsize;

	private bool _invoke_selectupdate = true;

	private bool _invoke_itemschanged = true;

	private bool _invoke_userfmtschanged = true;

	public bool IsDisposed => isdisposed;

	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
			InvokePropertyChanged("Name");
		}
	}

	public string Filename
	{
		get
		{
			return filename;
		}
		set
		{
			filename = value;
			InvokePropertyChanged("Filename");
		}
	}

	public ImageArgumentTypes ArgumentType
	{
		get
		{
			return argumenttype;
		}
		set
		{
			_setArgumentType(value);
			InvokePropertyChanged("ArgumentType");
		}
	}

	public bool IsDrill
	{
		get
		{
			return isdrill;
		}
		set
		{
			isdrill = value;
			InvokePropertyChanged("IsDrill");
		}
	}

	public IList<IPolylineEntity> Items
	{
		get
		{
			IList<IPolylineEntity> result;
			if (!ispaste)
			{
				IList<IPolylineEntity> list = items;
				result = list;
			}
			else
			{
				IList<IPolylineEntity> list = pasteitems;
				result = list;
			}
			return result;
		}
	}

	public IList<IPolylineGroup> Groups => groups;

	public IList<IPolylineUserFormat> UserFmts => userfmts;

	public IEnumerable<IPolylineEntity> SelectedEntities
	{
		get
		{
			foreach (IPolylineEntity item in items)
			{
				if (item.IsSelected || (item.Group?.IsSelected ?? false))
				{
					yield return item;
				}
			}
		}
	}

	public IEnumerable<IPolylineGroup> SelectedGroups
	{
		get
		{
			IPolylineGroup lastgroup = null;
			foreach (IPolylineEntity item in SelectedEntities)
			{
				if (item.Group != null && item.Group != lastgroup)
				{
					yield return item.Group;
					lastgroup = item.Group;
				}
			}
		}
	}

	public IPolylineEntity SelectedEntity => (selectedcount == 1) ? items[selectedstart] : null;

	public int SelectedStart => selectedstart;

	public int SelectedCount => selectedcount;

	public double Top
	{
		get
		{
			return top;
		}
		set
		{
			GetOldSize();
			top = value;
			InvokePropertyChanged("Top");
			GetNewSize();
		}
	}

	public double Left
	{
		get
		{
			return left;
		}
		set
		{
			GetOldSize();
			left = value;
			InvokePropertyChanged("Left");
			GetNewSize();
		}
	}

	public double Width
	{
		get
		{
			return width;
		}
		set
		{
			GetOldSize();
			width = value;
			InvokePropertyChanged("Width");
			GetNewSize();
		}
	}

	public double Height
	{
		get
		{
			return height;
		}
		set
		{
			GetOldSize();
			height = value;
			InvokePropertyChanged("Height");
			GetNewSize();
		}
	}

	public int SkipIndex
	{
		get
		{
			return skipindex;
		}
		set
		{
			skipindex = value;
		}
	}

	public IImageArgument Argument
	{
		get
		{
			return argument;
		}
		set
		{
			argument = value;
			InvokePropertyChanged("Argument");
		}
	}

	public IList<IPolylineAction> Undos => undos;

	public IList<IPolylineAction> Redos => redos;

	public PolylineExportCore EPCore => epcore;

	public Point StartPoint
	{
		get
		{
			return startpoint;
		}
		set
		{
			startpoint = value;
			InvokePropertyChanged("StartPoint");
		}
	}

	bool IGridPenningSourceEX.IsScanXEnabled => false;

	bool IGridPenningSourceEX.IsScanYEnabled => false;

	double IGridPenningSourceEX.ScanX => 0.0;

	double IGridPenningSourceEX.ScanY => 0.0;

	double IGridPenningSource.XStart => Left;

	double IGridPenningSource.YStart => Top;

	double IGridPenningSource.XLength => Width;

	double IGridPenningSource.YLength => Height;

	string IGridPenningSource.XUnit => string.Empty;

	string IGridPenningSource.YUnit => string.Empty;

	IList<string> IGridPenningSource.XUnitEx => new string[0];

	IList<string> IGridPenningSource.YUnitEx => new string[0];

	IList<int> IGridPenningSource.XValueBase => new int[0];

	IList<int> IGridPenningSource.YValueBase => new int[0];

	public event GridPenningEntityChangedEventHandler EntityChanged;

	public event GridPenningSizeChangedEventHandler SizeChanged;

	public event PolylineActionEventHandler Undoed;

	public event PolylineActionEventHandler Redoed;

	public event PropertyChangedEventHandler PropertyChanged;

	public event NotifyCollectionChangedEventHandler ItemsChanged;

	public event NotifyCollectionChangedEventHandler UserFmtsChanged;

	public PolylineImage()
	{
		name = "New image";
		filename = null;
		items = new ObservableCollection<IPolylineEntity>();
		groups = new ObservableCollection<IPolylineGroup>();
		userfmts = new ObservableCollection<IPolylineUserFormat>();
		top = 0.0;
		left = 0.0;
		width = 100.0;
		height = 100.0;
		skipindex = -2;
		argument = new HMIPLINEImageArgument();
		undos = new List<IPolylineAction>();
		redos = new List<IPolylineAction>();
		ispaste = false;
		pasteitems = new List<IPolylineEntity>();
		selectedstart = 0;
		selectedcount = 0;
		epcore = new PolylineExportCore(this);
		startpoint = new Point(0.0, 0.0);
		items.CollectionChanged += OnItemsChanged;
		userfmts.CollectionChanged += OnUserFmtsChanged;
	}

	public void Dispose()
	{
		if (isdisposed)
		{
			return;
		}
		isdisposed = true;
		foreach (IPolylineEntity item in items)
		{
			items.Remove(item);
			item.Dispose();
		}
		items.CollectionChanged -= OnItemsChanged;
	}

	public override string ToString()
	{
		return name ?? "<Untitled Image>";
	}

	protected void _setArgumentType(ImageArgumentTypes value)
	{
		argumenttype = value;
		foreach (IPolylineEntity item in items)
		{
			item.ArgumentType = value;
		}
		switch (argumenttype)
		{
		case ImageArgumentTypes.HMIPLINE:
			Argument = new HMIPLINEImageArgument();
			break;
		case ImageArgumentTypes.HMIBLOCK:
			Argument = new HMIBLOCKImageArgument();
			break;
		}
	}

	string IGridPenningSourceEX.GetXUnit(double xvalue)
	{
		return $"{xvalue:f1}";
	}

	string IGridPenningSourceEX.GetYUnit(double yvalue)
	{
		return $"{yvalue:f1}";
	}

	IEnumerable<IGridPenningEntity> IGridPenningSource.GetEntities(Rect rect)
	{
		for (int i = 0; i < items.Count(); i++)
		{
			IPolylineEntity item = items[i];
			if ((skipindex >= 0 && i == skipindex) || (skipindex >= 0 && i == skipindex + 1) || !item.Bounding.IntersectsWith(rect))
			{
				continue;
			}
			foreach (IGridPenningEntity penning in item.Pennings)
			{
				yield return penning;
			}
		}
	}

	protected void GetOldSize()
	{
		oldsize = new Rect(left, top, width, height);
	}

	protected void GetNewSize()
	{
		newsize = new Rect(left, top, width, height);
		GridPenningSizeChangedEventArgs e = new GridPenningSizeChangedEventArgs(oldsize, newsize);
		this.SizeChanged?.Invoke(this, e);
	}

	public void Load(PolylineImageHeader header)
	{
		name = FileFormat.GetString(header.spName);
		filename = FileFormat.GetString(header.spFilename);
		Point? point = null;
		int num = header.lpUserFmts;
		for (int i = 0; i < header.dwUserFmtsCount; i++)
		{
			PolylineUserFormatHeader polylineUserFormatHeader = (PolylineUserFormatHeader)FileFormat.GetHeader(num, FileHeaderTypes.PolylineUserFormat);
			PolylineUserFormat item = new PolylineUserFormat(polylineUserFormatHeader);
			userfmts.Add(item);
			num += polylineUserFormatHeader.HeaderSize;
		}
		int num2 = header.lpItems;
		for (int j = 0; j < header.dwItemsCount; j++)
		{
			PolylineEntityHeader polylineEntityHeader = (PolylineEntityHeader)FileFormat.GetHeader(num2, FileHeaderTypes.PolylineEntity);
			switch (polylineEntityHeader.bEntityType)
			{
			case 1:
			{
				PolylineLineHeader polylineLineHeader = (PolylineLineHeader)FileFormat.GetHeader(num2, FileHeaderTypes.PolylineLine);
				PolylineLine polylineLine5 = new PolylineLine(this, items.Count(), default(Point));
				polylineLine5.Load(polylineLineHeader);
				num2 += polylineLineHeader.HeaderSize;
				if (point.HasValue)
				{
					if (polylineLine5.IsReal)
					{
						IPolylineLine polylineLine6 = new PolylineLine(this, items.Count(), point.Value);
						polylineLine6.IsReal = false;
						items.Add(polylineLine6);
						polylineLine5.ID++;
					}
					point = null;
				}
				items.Add(polylineLine5);
				break;
			}
			case 2:
			{
				PolylineCircleHeader polylineCircleHeader = (PolylineCircleHeader)FileFormat.GetHeader(num2, FileHeaderTypes.PolylineCircle);
				PolylineCircle polylineCircle = new PolylineCircle(this, items.Count(), default(Point), default(Point));
				polylineCircle.Load(polylineCircleHeader);
				num2 += polylineCircleHeader.HeaderSize;
				polylineCircle.To = polylineCircle.From;
				if (point.HasValue)
				{
					IPolylineLine polylineLine3 = new PolylineLine(this, items.Count(), point.Value);
					polylineLine3.IsReal = false;
					items.Add(polylineLine3);
					polylineCircle.ID++;
					point = null;
				}
				if (IsDrill)
				{
					IPolylineEntity polylineEntity2 = items.LastOrDefault();
					point = polylineEntity2?.To ?? new Point(0.0, 0.0);
					if (polylineEntity2 == null || polylineEntity2.IsReal)
					{
						PolylineLine polylineLine4 = new PolylineLine();
						polylineLine4.IsReal = false;
						polylineLine4.From = new Point(0.0, 0.0);
						polylineLine4.To = polylineCircle.Center;
					}
					else
					{
						polylineEntity2.To = polylineCircle.Center;
					}
					polylineCircle.Dispose();
				}
				else
				{
					point = null;
					items.Add(polylineCircle);
				}
				break;
			}
			case 3:
			{
				PolylineArchHeader polylineArchHeader = (PolylineArchHeader)FileFormat.GetHeader(num2, FileHeaderTypes.PolylineArch);
				PolylineArch polylineArch = new PolylineArch(this, items.Count(), default(Point), default(Point));
				polylineArch.Load(polylineArchHeader);
				num2 += polylineArchHeader.HeaderSize;
				if (point.HasValue)
				{
					IPolylineLine polylineLine = new PolylineLine(this, items.Count(), point.Value);
					polylineLine.IsReal = false;
					items.Add(polylineLine);
					polylineArch.ID++;
					point = null;
				}
				if (IsDrill)
				{
					IPolylineEntity polylineEntity = items.LastOrDefault();
					point = polylineArch.To;
					if (polylineEntity == null || polylineEntity.IsReal)
					{
						PolylineLine polylineLine2 = new PolylineLine();
						polylineLine2.IsReal = false;
						polylineLine2.From = new Point(0.0, 0.0);
						polylineLine2.To = polylineArch.Center;
					}
					else
					{
						polylineEntity.To = polylineArch.Center;
					}
					polylineArch.Dispose();
				}
				else
				{
					point = null;
					items.Add(polylineArch);
				}
				break;
			}
			}
		}
		int dwArgumentType = header.dwArgumentType;
		int num3 = dwArgumentType;
		if (num3 == 1)
		{
			IFileHeader header2 = FileFormat.GetHeader(header.lpArgument, FileHeaderTypes.HMIPLINEImageArgument);
			HMIPLINEImageArgument hMIPLINEImageArgument = new HMIPLINEImageArgument();
			hMIPLINEImageArgument.Load(header2);
			argument = hMIPLINEImageArgument;
		}
		int lpGroup = header.lpGroup;
		for (int k = 0; k < header.dwGroupCount; k++)
		{
			PolylineGroupHeader header3 = (PolylineGroupHeader)FileFormat.GetHeader(lpGroup, FileHeaderTypes.PolylineGroup);
			PolylineGroup polylineGroup = new PolylineGroup(this, -1, groups.Count(), -1, -1);
			polylineGroup.Load(header3);
			polylineGroup.ID = polylineGroup.Start;
			groups.Add(polylineGroup);
			for (int l = polylineGroup.Start; l < polylineGroup.Start + polylineGroup.Count; l++)
			{
				items[l].Group = polylineGroup;
			}
		}
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		PolylineUserDataHeader header4 = (PolylineUserDataHeader)FileFormat.GetHeader(header.lpUserData, FileHeaderTypes.PolylineUserData);
		foreach (IPolylineUserFormat userfmt in userfmts)
		{
			num5 = num4;
			num6 = 0;
			foreach (IPolylineEntity item2 in items)
			{
				num5 = item2.UserObjs[userfmt.ID].Read(header4, num5, ref num6);
			}
			num4 += userfmt.GetSize(header.dwItemsCount);
		}
		GroupUpdate();
		left -= width * 0.02;
		top -= height * 0.02;
		width += width * 0.04;
		height += height * 0.04;
		width = Math.Max(width, height);
		height = width;
	}

	public void Save(PolylineImageHeader header)
	{
		FileFormat.AllocHeaderString(header, 0, name);
		FileFormat.AllocHeaderString(header, 1, filename);
		LocatedFileHeader[] array = items.Select((IPolylineEntity i) => i.AllocHeader()).ToArray();
		LocatedFileHeader[] array2 = groups.Select((IPolylineGroup g) => FileFormat.AllocHeader(FileHeaderTypes.PolylineGroup)).ToArray();
		LocatedFileHeader[] array3 = userfmts.Select((IPolylineUserFormat f) => FileFormat.AllocHeader(FileHeaderTypes.PolylineUserFormat)).ToArray();
		header.dwItemsCount = items.Count();
		header.lpItems = array.FirstOrDefault()?.LPHeader ?? (-1);
		header.dwGroupCount = groups.Count();
		header.lpGroup = array2.FirstOrDefault()?.LPHeader ?? (-1);
		header.dwUserFmtsCount = userfmts.Count();
		header.lpUserFmts = array3.FirstOrDefault()?.LPHeader ?? (-1);
		header.dwArgumentType = 0;
		header.lpArgument = -1;
		if (argument is IHMIPLINEImageArgument)
		{
			header.dwArgumentType = 1;
		}
		if (argument != null)
		{
			LocatedFileHeader locatedFileHeader = argument.AllocHeader();
			argument.Save(locatedFileHeader.Header);
			header.lpArgument = locatedFileHeader.LPHeader;
		}
		for (int num = 0; num < items.Count(); num++)
		{
			PolylineEntityHeader header2 = (PolylineEntityHeader)array[num].Header;
			items[num].Save(header2);
		}
		for (int num2 = 0; num2 < groups.Count(); num2++)
		{
			PolylineGroupHeader header3 = (PolylineGroupHeader)array2[num2].Header;
			groups[num2].Save(header3);
		}
		for (int num3 = 0; num3 < userfmts.Count(); num3++)
		{
			PolylineUserFormatHeader header4 = (PolylineUserFormatHeader)array3[num3].Header;
			userfmts[num3].Save(header4);
		}
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		foreach (IPolylineUserFormat userfmt in userfmts)
		{
			num4 += userfmt.GetSize(header.dwItemsCount);
		}
		LocatedFileHeader locatedFileHeader2 = FileFormat.AllocHeader(FileHeaderTypes.PolylineUserData, num4 + 4);
		PolylineUserDataHeader header5 = (PolylineUserDataHeader)locatedFileHeader2.Header;
		header.lpUserData = locatedFileHeader2.LPHeader;
		num4 = 0;
		foreach (IPolylineUserFormat userfmt2 in userfmts)
		{
			num6 = num4;
			num5 = 0;
			foreach (IPolylineEntity item in items)
			{
				num6 = item.UserObjs[userfmt2.ID].Write(header5, num6, ref num5);
			}
			num4 += userfmt2.GetSize(header.dwItemsCount);
		}
	}

	public void Load(UploadReader ur)
	{
		int num = ur.Read32();
		int num2 = ur.Read16();
		Point? point = null;
		switch (num2)
		{
		case 248:
		{
			name = ur.Read16S();
			IHMIPLINEImageArgument iHMIPLINEImageArgument = new HMIPLINEImageArgument();
			iHMIPLINEImageArgument.PlaneID = ur.Read16();
			iHMIPLINEImageArgument.StartupMode = (StartupMode)ur.Read8();
			argument = iHMIPLINEImageArgument;
			argumenttype = ImageArgumentTypes.HMIPLINE;
			ur.Skip(10);
			break;
		}
		case 233:
		{
			name = ur.Read16S();
			IHMIBLOCKImageArgument iHMIBLOCKImageArgument = new HMIBLOCKImageArgument();
			argument = iHMIBLOCKImageArgument;
			argumenttype = ImageArgumentTypes.HMIBLOCK;
			ur.Skip(20);
			break;
		}
		}
		int num3 = ur.Read32();
		for (int i = 0; i < num3; i++)
		{
			switch (ur.Read8())
			{
			case 0:
			{
				IPolylineLine polylineLine5 = new PolylineLine(this, items.Count(), default(Point));
				polylineLine5.Load(ur);
				if (point.HasValue)
				{
					if (polylineLine5.IsReal)
					{
						IPolylineLine polylineLine6 = new PolylineLine(this, items.Count(), point.Value);
						polylineLine6.IsReal = false;
						items.Add(polylineLine6);
						polylineLine5.ID++;
					}
					point = null;
				}
				items.Add(polylineLine5);
				break;
			}
			case 1:
			{
				IPolylineArch polylineArch = new PolylineArch(this, items.Count(), default(Point), default(Point));
				polylineArch.Load(ur);
				if (point.HasValue)
				{
					IPolylineLine polylineLine3 = new PolylineLine(this, items.Count(), point.Value);
					polylineLine3.IsReal = false;
					items.Add(polylineLine3);
					polylineArch.ID++;
					point = null;
				}
				if (IsDrill)
				{
					IPolylineEntity polylineEntity2 = items.LastOrDefault();
					point = polylineArch.To;
					if (polylineEntity2 == null || polylineEntity2.IsReal)
					{
						PolylineLine polylineLine4 = new PolylineLine();
						polylineLine4.IsReal = false;
						polylineLine4.From = polylineEntity2?.To ?? new Point(0.0, 0.0);
						polylineLine4.To = polylineArch.Center;
					}
					else
					{
						polylineEntity2.To = polylineArch.Center;
					}
					polylineArch.Dispose();
				}
				else
				{
					point = null;
					items.Add(polylineArch);
				}
				break;
			}
			case 2:
			{
				IPolylineCircle polylineCircle = new PolylineCircle(this, items.Count(), default(Point), default(Point));
				polylineCircle.Load(ur);
				if (point.HasValue)
				{
					IPolylineLine polylineLine = new PolylineLine(this, items.Count(), point.Value);
					polylineLine.IsReal = false;
					items.Add(polylineLine);
					polylineCircle.ID++;
					point = null;
				}
				if (IsDrill)
				{
					IPolylineEntity polylineEntity = items.LastOrDefault();
					point = polylineEntity?.To ?? new Point(0.0, 0.0);
					if (polylineEntity == null || polylineEntity.IsReal)
					{
						PolylineLine polylineLine2 = new PolylineLine();
						polylineLine2.IsReal = false;
						polylineLine2.From = polylineEntity?.To ?? new Point(0.0, 0.0);
						polylineLine2.To = polylineCircle.Center;
					}
					else
					{
						polylineEntity.To = polylineCircle.Center;
					}
					polylineCircle.Dispose();
				}
				else
				{
					items.Add(polylineCircle);
					point = null;
					polylineCircle.To = polylineCircle.From;
				}
				break;
			}
			}
		}
		GroupUpdate();
		left -= width * 0.02;
		top -= height * 0.02;
		width += width * 0.04;
		height += height * 0.04;
		width = Math.Max(width, height);
		height = width;
	}

	public void Save(DownloadWriter dw)
	{
		if (argument is IHMIPLINEImageArgument)
		{
			dw.PushL();
			dw.Write((ushort)248);
			dw.Write16(name);
			if (argument is IHMIPLINEImageArgument)
			{
				IHMIPLINEImageArgument iHMIPLINEImageArgument = (IHMIPLINEImageArgument)argument;
				dw.Write((ushort)iHMIPLINEImageArgument.PlaneID);
				dw.Write((byte)iHMIPLINEImageArgument.StartupMode);
			}
			else
			{
				dw.Write((ushort)0);
				dw.Write((byte)0);
			}
			dw.Skip(10);
			dw.Write(items.Count());
			foreach (IPolylineEntity item in items)
			{
				item.Save(dw);
			}
			dw.PopL();
		}
		else
		{
			if (!(argument is IHMIBLOCKImageArgument))
			{
				return;
			}
			dw.PushL();
			dw.Write((ushort)233);
			dw.Write16(name);
			dw.Skip(20);
			dw.Write(items.Count());
			foreach (IPolylineEntity item2 in items)
			{
				item2.Save(dw);
			}
			dw.PopL();
		}
	}

	public void Load(DXFModel dxf)
	{
		foreach (DXFEdge item in dxf.Graph.Path)
		{
			Load(item.Entity);
		}
		GroupUpdate();
		left -= width * 0.02;
		top -= height * 0.02;
		width += width * 0.04;
		height += height * 0.04;
		width = Math.Max(width, height);
		height = width;
	}

	public void Load(DXFEntity dxfen)
	{
		Point? point = null;
		switch (dxfen.Type)
		{
		case EntityType.Line:
		{
			if (!(dxfen is DXFLine))
			{
				break;
			}
			DXFLine dXFLine = (DXFLine)dxfen;
			PolylineLine polylineLine4 = new PolylineLine(this, items.Count(), dXFLine.IsReverse ? dXFLine.StartP : dXFLine.EndP, dXFLine.IsReal);
			if (point.HasValue)
			{
				if (polylineLine4.IsReal)
				{
					IPolylineLine polylineLine5 = new PolylineLine(this, items.Count(), point.Value);
					polylineLine5.IsReal = false;
					items.Add(polylineLine5);
					polylineLine4.ID++;
				}
				point = null;
			}
			items.Add(polylineLine4);
			break;
		}
		case EntityType.Arc:
			if (point.HasValue)
			{
				IPolylineLine polylineLine = new PolylineLine(this, items.Count(), point.Value);
				polylineLine.IsReal = false;
				items.Add(polylineLine);
				point = null;
			}
			if (IsDrill)
			{
				DXFArc dXFArc = (DXFArc)dxfen;
				IPolylineEntity polylineEntity = items.LastOrDefault();
				point = (dXFArc.IsReverse ? dXFArc.StartP : dXFArc.EndP);
				if (polylineEntity == null || polylineEntity.IsReal)
				{
					PolylineLine polylineLine2 = new PolylineLine();
					polylineLine2.IsReal = false;
					polylineLine2.From = new Point(0.0, 0.0);
					polylineLine2.To = dXFArc.CenterP;
				}
				else
				{
					polylineEntity.To = dXFArc.CenterP;
				}
			}
			else if (dxfen is DXFArc)
			{
				DXFArc dXFArc2 = (DXFArc)dxfen;
				PolylineArch item = new PolylineArch(this, items.Count(), dXFArc2.IsReverse ? dXFArc2.StartP : dXFArc2.EndP, dXFArc2.CenterP, dXFArc2.IsReverse, dXFArc2.EAngle - dXFArc2.SAngle > 180.0);
				items.Add(item);
			}
			break;
		case EntityType.Circle:
			if (point.HasValue)
			{
				IPolylineLine polylineLine6 = new PolylineLine(this, items.Count(), point.Value);
				polylineLine6.IsReal = false;
				items.Add(polylineLine6);
				point = null;
			}
			if (IsDrill)
			{
				DXFCircle dXFCircle = (DXFCircle)dxfen;
				IPolylineEntity polylineEntity2 = items.LastOrDefault();
				point = polylineEntity2?.To ?? new Point(0.0, 0.0);
				if (polylineEntity2 == null || polylineEntity2.IsReal)
				{
					PolylineLine polylineLine7 = new PolylineLine();
					polylineLine7.IsReal = false;
					polylineLine7.From = new Point(0.0, 0.0);
					polylineLine7.To = dXFCircle.CenterP;
				}
				else
				{
					polylineEntity2.To = dXFCircle.CenterP;
				}
			}
			else if (dxfen is DXFCircle)
			{
				DXFCircle dXFCircle2 = (DXFCircle)dxfen;
				Point point2 = items.LastOrDefault()?.To ?? default(Point);
				Point centerP = dXFCircle2.CenterP;
				Vector vector = centerP - point2;
				vector /= vector.Length;
				vector *= dXFCircle2.radius;
				PolylineCircle polylineCircle = new PolylineCircle(this, items.Count(), point2, point2 + vector);
				items.Add(polylineCircle);
				polylineCircle.To = polylineCircle.From;
			}
			break;
		case EntityType.Spline:
		{
			if (point.HasValue)
			{
				IPolylineLine polylineLine3 = new PolylineLine(this, items.Count(), point.Value);
				polylineLine3.IsReal = false;
				items.Add(polylineLine3);
				point = null;
			}
			if (!(dxfen is DXFSpline))
			{
				break;
			}
			DXFSpline dXFSpline = (DXFSpline)dxfen;
			Point[] samplePoints = dXFSpline.GetSamplePoints();
			PolylineBSpline polylineBSpline = new PolylineBSpline(this, items.Count(), dXFSpline.IsReverse ? dXFSpline.ControlP.First() : dXFSpline.ControlP.Last());
			if (dXFSpline.IsReverse)
			{
				Array.Reverse(samplePoints);
			}
			polylineBSpline.Nodes.Add(items.LastOrDefault()?.To ?? default(Point));
			foreach (Point item3 in dXFSpline.ControlP)
			{
				polylineBSpline.Points.Add(item3);
			}
			Point[] array = samplePoints;
			foreach (Point item2 in array)
			{
				polylineBSpline.Nodes.Add(item2);
			}
			foreach (double weight in dXFSpline.Weights)
			{
				polylineBSpline.Weights.Add(weight);
			}
			foreach (double node in dXFSpline.Nodes)
			{
				polylineBSpline.Offsets.Add(node);
			}
			polylineBSpline.Nodes.Add(polylineBSpline.To);
			items.Add(polylineBSpline);
			break;
		}
		case EntityType.Section:
		{
			if (!(dxfen is DXFSection))
			{
				break;
			}
			DXFSection dXFSection = (DXFSection)dxfen;
			{
				foreach (DXFEntity entity in dXFSection.Entities)
				{
					Load(entity);
				}
				break;
			}
		}
		case EntityType.Ellipse:
			break;
		}
	}

	public IPolylineAction Insert(int index, IPolylineEntity item)
	{
		if (item is IPolylineRect || item is IPolylineFreeRect)
		{
			IPolylineLine[] array = new IPolylineLine[4];
			Point to = ((index < items.Count()) ? items[index].From : ((index > 0) ? items[index - 1].To : default(Point)));
			for (int i = 0; i < 4; i++)
			{
				array[i] = new PolylineLine(this, index + i, to);
			}
			item.ID = index;
			return Insert(index, array);
		}
		if (item is IPolylineEllipse)
		{
			IPolylineEntity polylineEntity = ((index > 0) ? items[index - 1] : null);
			Point to2 = ((index < items.Count()) ? items[index].From : ((index > 0) ? items[index - 1].To : default(Point)));
			if (polylineEntity == null || polylineEntity.IsReal)
			{
				IPolylineEntity[] newitems = new IPolylineEntity[2]
				{
					new PolylineLine(this, index, to2, _isreal: false),
					item
				};
				return Insert(index, newitems);
			}
		}
		Items.Insert(index, item);
		IPolylineAction polylineAction = new PolylineAction(item.ID, new IPolylineEntity[0], new IPolylineEntity[1] { item });
		undos.Add(polylineAction);
		redos.Clear();
		GroupUpdate();
		RepairAll();
		return polylineAction;
	}

	public IPolylineAction Insert(int index, IEnumerable<IPolylineEntity> newitems)
	{
		items.CollectionChanged -= OnItemsChanged;
		ObservableCollection<IPolylineEntity> observableCollection = new ObservableCollection<IPolylineEntity>();
		int num = newitems.Count();
		for (int i = 0; i < index; i++)
		{
			observableCollection.Add(items[i]);
		}
		foreach (IPolylineEntity newitem in newitems)
		{
			observableCollection.Add(newitem);
		}
		for (int j = index; j < items.Count(); j++)
		{
			observableCollection.Add(items[j]);
		}
		items.CollectionChanged -= OnItemsChanged;
		items = observableCollection;
		Select(index, newitems.Count());
		items.CollectionChanged += OnItemsChanged;
		this.ItemsChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		IPolylineAction polylineAction = new PolylineAction(index, new IPolylineEntity[0], newitems.ToArray());
		undos.Add(polylineAction);
		redos.Clear();
		GroupUpdate();
		RepairAll();
		return polylineAction;
	}

	public IPolylineAction Remove(IPolylineEntity item)
	{
		Items.Remove(item);
		IPolylineAction polylineAction = new PolylineAction(item.ID, new IPolylineEntity[1] { item }, new IPolylineEntity[0]);
		undos.Add(polylineAction);
		redos.Clear();
		GroupUpdate();
		RepairAll();
		return polylineAction;
	}

	public IPolylineAction Remove(IEnumerable<IPolylineEntity> removes)
	{
		items.CollectionChanged -= OnItemsChanged;
		HashSet<IPolylineEntity> hashSet = new HashSet<IPolylineEntity>(removes);
		ObservableCollection<IPolylineEntity> observableCollection = new ObservableCollection<IPolylineEntity>();
		foreach (IPolylineEntity item in items)
		{
			if (!hashSet.Contains(item))
			{
				observableCollection.Add(item);
			}
		}
		items = observableCollection;
		Select(0, 0);
		items.CollectionChanged += OnItemsChanged;
		this.ItemsChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		IPolylineAction polylineAction = new PolylineAction(removes.FirstOrDefault().ID, removes.ToArray(), new IPolylineEntity[0]);
		undos.Add(polylineAction);
		redos.Clear();
		GroupUpdate();
		RepairAll();
		return polylineAction;
	}

	public IPolylineAction Remove(int index)
	{
		IPolylineEntity polylineEntity = items[index];
		Items.RemoveAt(index);
		IPolylineAction polylineAction = new PolylineAction(polylineEntity.ID, new IPolylineEntity[1] { polylineEntity }, new IPolylineEntity[0]);
		undos.Add(polylineAction);
		redos.Clear();
		GroupUpdate();
		RepairAll();
		return polylineAction;
	}

	public IPolylineAction Remove(int index, int count)
	{
		items.CollectionChanged -= OnItemsChanged;
		List<IPolylineEntity> list = new List<IPolylineEntity>();
		ObservableCollection<IPolylineEntity> observableCollection = new ObservableCollection<IPolylineEntity>();
		for (int i = 0; i < index; i++)
		{
			observableCollection.Add(items[i]);
		}
		for (int j = index; j < index + count; j++)
		{
			list.Add(items[j]);
		}
		for (int k = index + count; k < items.Count(); k++)
		{
			observableCollection.Add(items[k]);
		}
		items = observableCollection;
		items.CollectionChanged += OnItemsChanged;
		this.ItemsChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		IPolylineAction polylineAction = new PolylineAction(index, list, new IPolylineEntity[0]);
		undos.Add(polylineAction);
		redos.Clear();
		GroupUpdate();
		RepairAll();
		return polylineAction;
	}

	public IPolylineAction Replace(int index, int count, IEnumerable<IPolylineEntity> newitems)
	{
		items.CollectionChanged -= OnItemsChanged;
		List<IPolylineEntity> list = new List<IPolylineEntity>();
		ObservableCollection<IPolylineEntity> observableCollection = new ObservableCollection<IPolylineEntity>();
		for (int i = 0; i < index; i++)
		{
			observableCollection.Add(items[i]);
		}
		foreach (IPolylineEntity newitem in newitems)
		{
			observableCollection.Add(newitem);
		}
		for (int j = index; j < index + count; j++)
		{
			list.Add(items[j]);
		}
		for (int k = index + count; k < items.Count(); k++)
		{
			observableCollection.Add(items[k]);
		}
		items = observableCollection;
		items.CollectionChanged += OnItemsChanged;
		this.ItemsChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
		IPolylineAction polylineAction = new PolylineAction(index, list, newitems.ToArray());
		undos.Add(polylineAction);
		redos.Clear();
		GroupUpdate();
		RepairAll();
		return polylineAction;
	}

	public void ActionBegin(IPolylineEntity _nowitem, IPolylineEntity _nexitem)
	{
		nowitem = _nowitem?.Clone();
		nexitem = _nexitem?.Clone();
	}

	public IPolylineAction ActionEnd(IPolylineEntity _nowitem, IPolylineEntity _nexitem)
	{
		IPolylineActionCollection polylineActionCollection = new PolylineActionCollection();
		IPolylineAction polylineAction = null;
		_ActionGather(nowitem, _nowitem, polylineActionCollection);
		_ActionGather(nexitem, _nexitem, polylineActionCollection);
		if (polylineActionCollection.Items.Count == 1)
		{
			polylineAction = polylineActionCollection.Items.FirstOrDefault();
		}
		else if (polylineActionCollection.Items.Count > 1)
		{
			polylineAction = polylineActionCollection;
		}
		if (polylineAction != null)
		{
			undos.Add(polylineAction);
			redos.Clear();
		}
		nowitem = null;
		nexitem = null;
		return polylineAction;
	}

	public void ActionEscape(IPolylineEntity _nowitem, IPolylineEntity _nexitem)
	{
		_nowitem?.Load(nowitem);
		_nexitem?.Load(nexitem);
		nowitem = null;
		nexitem = null;
	}

	protected void _ActionGather(IPolylineEntity olditem, IPolylineEntity newitem, IPolylineActionCollection colle)
	{
		if (olditem == null || newitem == null)
		{
			return;
		}
		if (!olditem.To.Equals(newitem.To))
		{
			colle.Items.Add(new PolylineAction(olditem.ID, ChangedTarget.To, olditem.To, newitem.To));
		}
		if (olditem is IPolylineCircle && newitem is IPolylineCircle)
		{
			IPolylineCircle polylineCircle = (IPolylineCircle)olditem;
			IPolylineCircle polylineCircle2 = (IPolylineCircle)newitem;
			if (!polylineCircle.Center.Equals(polylineCircle2.Center))
			{
				colle.Items.Add(new PolylineAction(polylineCircle.ID, ChangedTarget.Center, polylineCircle.Center, polylineCircle2.Center));
			}
			if (polylineCircle.IsClockwise != polylineCircle2.IsClockwise)
			{
				colle.Items.Add(new PolylineAction(polylineCircle.ID, ChangedTarget.IsClockwise, polylineCircle.IsClockwise, polylineCircle2.IsClockwise));
			}
		}
		if (olditem is IPolylineEllipse && newitem is IPolylineEllipse)
		{
			IPolylineEllipse polylineEllipse = (IPolylineEllipse)olditem;
			IPolylineEllipse polylineEllipse2 = (IPolylineEllipse)newitem;
			if (!polylineEllipse.Center.Equals(polylineEllipse2.Center))
			{
				colle.Items.Add(new PolylineAction(polylineEllipse.ID, ChangedTarget.Center, polylineEllipse.Center, polylineEllipse2.Center));
			}
			if (polylineEllipse.IsClockwise != polylineEllipse2.IsClockwise)
			{
				colle.Items.Add(new PolylineAction(polylineEllipse.ID, ChangedTarget.IsClockwise, polylineEllipse.IsClockwise, polylineEllipse2.IsClockwise));
			}
			if (polylineEllipse.Direction != polylineEllipse2.Direction)
			{
				colle.Items.Add(new PolylineAction(polylineEllipse.ID, ChangedTarget.Direction, new Point(polylineEllipse.Direction.X, polylineEllipse.Direction.Y), new Point(polylineEllipse2.Direction.X, polylineEllipse2.Direction.Y)));
			}
			if (polylineEllipse.ShortRadius != polylineEllipse2.ShortRadius)
			{
				colle.Items.Add(new PolylineAction(polylineEllipse.ID, ChangedTarget.ShortRadius, polylineEllipse.ShortRadius, polylineEllipse2.ShortRadius));
			}
			if (polylineEllipse.LongRadius != polylineEllipse2.LongRadius)
			{
				colle.Items.Add(new PolylineAction(polylineEllipse.ID, ChangedTarget.LongRadius, polylineEllipse.LongRadius, polylineEllipse2.LongRadius));
			}
		}
	}

	public bool CanUndo()
	{
		return undos.Count() > 0;
	}

	public bool CanRedo()
	{
		return redos.Count() > 0;
	}

	public IPolylineAction Undo()
	{
		if (undos.Count() == 0)
		{
			return null;
		}
		IPolylineAction polylineAction = undos.LastOrDefault();
		undos.RemoveAt(undos.Count() - 1);
		redos.Add(polylineAction);
		_Undo(polylineAction);
		this.Undoed?.Invoke(this, new PolylineActionEventArgs(this, polylineAction));
		return polylineAction;
	}

	public IPolylineAction Redo()
	{
		if (redos.Count() == 0)
		{
			return null;
		}
		IPolylineAction polylineAction = redos.LastOrDefault();
		redos.RemoveAt(redos.Count() - 1);
		undos.Add(polylineAction);
		_Redo(polylineAction);
		this.Redoed?.Invoke(this, new PolylineActionEventArgs(this, polylineAction));
		return polylineAction;
	}

	protected void _Undo(IPolylineAction action, bool repair = true)
	{
		if (action is IPolylineActionCollection)
		{
			IPolylineActionCollection polylineActionCollection = (IPolylineActionCollection)action;
			for (int num = polylineActionCollection.Items.Count() - 1; num >= 0; num--)
			{
				_Undo(polylineActionCollection.Items[num], repair: false);
			}
			if (repair)
			{
				RepairAll();
			}
			return;
		}
		if (action.AddedItems != null && action.RemovedItems != null)
		{
			List<IPolylineEntity> list = new List<IPolylineEntity>();
			List<IPolylineEntity> list2 = new List<IPolylineEntity>();
			int i = 0;
			int num2 = 0;
			for (; i < items.Count(); i++)
			{
				if (num2 < action.AddedItems.Count && i == action.AddedItems[num2].ID)
				{
					num2++;
				}
				else
				{
					list.Add(items[i]);
				}
			}
			int num3 = 0;
			int num4 = 0;
			while (num3 < list.Count() || num4 < action.RemovedItems.Count())
			{
				if (num4 < action.RemovedItems.Count && list2.Count() == action.RemovedItems[num4].ID)
				{
					list2.Add(action.RemovedItems[num4++]);
					continue;
				}
				if (num3 < list.Count())
				{
					list2.Add(list[num3++]);
					continue;
				}
				break;
			}
			_invoke_itemschanged = false;
			items.Clear();
			foreach (IPolylineEntity item in list2)
			{
				items.Add(item);
			}
			_invoke_itemschanged = true;
			GroupUpdate();
			if (repair)
			{
				RepairAll();
			}
			return;
		}
		IPolylineEntity polylineEntity = items[action.Index];
		switch (action.Target)
		{
		case ChangedTarget.From:
			polylineEntity.From = action.OldPoint;
			break;
		case ChangedTarget.To:
			polylineEntity.To = action.OldPoint;
			break;
		case ChangedTarget.Center:
			if (polylineEntity is IPolylineCircle)
			{
				IPolylineCircle polylineCircle3 = (IPolylineCircle)polylineEntity;
				if (polylineCircle3 is IPolylineArch)
				{
					IPolylineArch polylineArch = (IPolylineArch)polylineCircle3;
					Point oldPoint = action.OldPoint;
					Vector vector = (polylineArch.To - polylineArch.From) / 2.0;
					Point point = polylineArch.From + vector;
					double y = vector.Y;
					double num5 = 0.0 - vector.X;
					if (action.Flag == ChangedFlags.OnlyX)
					{
						oldPoint.Y = point.Y + (oldPoint.X - point.X) / y * num5;
						if (double.IsNaN(oldPoint.Y) || double.IsInfinity(oldPoint.Y))
						{
							oldPoint = action.OldPoint;
						}
						else
						{
							repair = false;
						}
					}
					else if (action.Flag == ChangedFlags.OnlyY)
					{
						oldPoint.X = point.X + (oldPoint.Y - point.Y) / num5 * y;
						if (double.IsNaN(oldPoint.X) || double.IsInfinity(oldPoint.X))
						{
							oldPoint = action.OldPoint;
						}
						else
						{
							repair = false;
						}
					}
					polylineArch.Center = oldPoint;
				}
				else
				{
					polylineCircle3.Center = action.OldPoint;
				}
			}
			if (polylineEntity is IPolylineEllipse)
			{
				IPolylineEllipse polylineEllipse5 = (IPolylineEllipse)polylineEntity;
				polylineEllipse5.Center = action.OldPoint;
			}
			break;
		case ChangedTarget.Direction:
			if (polylineEntity is IPolylineEllipse)
			{
				IPolylineEllipse polylineEllipse2 = (IPolylineEllipse)polylineEntity;
				polylineEllipse2.Direction = new Vector(action.OldPoint.X, action.OldPoint.Y);
			}
			break;
		case ChangedTarget.Radius:
			if (polylineEntity is IPolylineCircle)
			{
				IPolylineCircle polylineCircle2 = (IPolylineCircle)polylineEntity;
				polylineCircle2.Radius = action.OldDouble;
			}
			break;
		case ChangedTarget.LongRadius:
			if (polylineEntity is IPolylineEllipse)
			{
				IPolylineEllipse polylineEllipse4 = (IPolylineEllipse)polylineEntity;
				polylineEllipse4.LongRadius = action.OldDouble;
			}
			break;
		case ChangedTarget.ShortRadius:
			if (polylineEntity is IPolylineEllipse)
			{
				IPolylineEllipse polylineEllipse3 = (IPolylineEllipse)polylineEntity;
				polylineEllipse3.ShortRadius = action.OldDouble;
			}
			break;
		case ChangedTarget.IsReal:
			polylineEntity.IsReal = action.OldBool;
			GroupUpdate();
			break;
		case ChangedTarget.IsClockwise:
			if (polylineEntity is IPolylineCircle)
			{
				IPolylineCircle polylineCircle = (IPolylineCircle)polylineEntity;
				polylineCircle.IsClockwise = action.OldBool;
			}
			if (polylineEntity is IPolylineEllipse)
			{
				IPolylineEllipse polylineEllipse = (IPolylineEllipse)polylineEntity;
				polylineEllipse.IsClockwise = action.OldBool;
			}
			break;
		}
		if (repair)
		{
			RepairFor(polylineEntity);
		}
		RepairAll();
	}

	protected void _Redo(IPolylineAction action, bool repair = true)
	{
		if (action is IPolylineActionCollection)
		{
			IPolylineActionCollection polylineActionCollection = (IPolylineActionCollection)action;
			for (int i = 0; i < polylineActionCollection.Items.Count; i++)
			{
				_Redo(polylineActionCollection.Items[i], repair: false);
			}
			if (repair)
			{
				RepairAll();
			}
			return;
		}
		if (action.AddedItems != null && action.RemovedItems != null)
		{
			List<IPolylineEntity> list = new List<IPolylineEntity>();
			List<IPolylineEntity> list2 = new List<IPolylineEntity>();
			int j = 0;
			int num = 0;
			for (; j < items.Count(); j++)
			{
				if (num < action.RemovedItems.Count && j == action.RemovedItems[num].ID)
				{
					num++;
				}
				else
				{
					list.Add(items[j]);
				}
			}
			int num2 = 0;
			int num3 = 0;
			while (num2 < list.Count() || num3 < action.AddedItems.Count())
			{
				if (num3 < action.AddedItems.Count && list2.Count() == action.AddedItems[num3].ID)
				{
					list2.Add(action.AddedItems[num3++]);
					continue;
				}
				if (num2 < list.Count())
				{
					list2.Add(list[num2++]);
					continue;
				}
				break;
			}
			_invoke_itemschanged = false;
			items.Clear();
			foreach (IPolylineEntity item in list2)
			{
				items.Add(item);
			}
			_invoke_itemschanged = true;
			GroupUpdate();
			if (repair)
			{
				RepairAll();
			}
			return;
		}
		IPolylineEntity polylineEntity = items[action.Index];
		switch (action.Target)
		{
		case ChangedTarget.From:
			polylineEntity.From = action.NewPoint;
			break;
		case ChangedTarget.To:
			polylineEntity.To = action.NewPoint;
			break;
		case ChangedTarget.Center:
			if (polylineEntity is IPolylineCircle)
			{
				IPolylineCircle polylineCircle3 = (IPolylineCircle)polylineEntity;
				if (polylineCircle3 is IPolylineArch)
				{
					IPolylineArch polylineArch = (IPolylineArch)polylineCircle3;
					Point newPoint = action.NewPoint;
					Vector vector = (polylineArch.To - polylineArch.From) / 2.0;
					Point point = polylineArch.From + vector;
					double y = vector.Y;
					double num4 = 0.0 - vector.X;
					if (action.Flag == ChangedFlags.OnlyX)
					{
						newPoint.Y = point.Y + (newPoint.X - point.X) / y * num4;
						if (double.IsNaN(newPoint.Y) || double.IsInfinity(newPoint.Y))
						{
							newPoint = action.NewPoint;
						}
						else
						{
							repair = false;
						}
					}
					else if (action.Flag == ChangedFlags.OnlyY)
					{
						newPoint.X = point.X + (newPoint.Y - point.Y) / num4 * y;
						if (double.IsNaN(newPoint.X) || double.IsInfinity(newPoint.X))
						{
							newPoint = action.NewPoint;
						}
						else
						{
							repair = false;
						}
					}
					polylineArch.Center = newPoint;
				}
				else
				{
					polylineCircle3.Center = action.NewPoint;
				}
			}
			if (polylineEntity is IPolylineEllipse)
			{
				IPolylineEllipse polylineEllipse5 = (IPolylineEllipse)polylineEntity;
				polylineEllipse5.Center = action.NewPoint;
			}
			break;
		case ChangedTarget.Direction:
			if (polylineEntity is IPolylineEllipse)
			{
				IPolylineEllipse polylineEllipse2 = (IPolylineEllipse)polylineEntity;
				polylineEllipse2.Direction = new Vector(action.NewPoint.X, action.NewPoint.Y);
			}
			break;
		case ChangedTarget.Radius:
			if (polylineEntity is IPolylineCircle)
			{
				IPolylineCircle polylineCircle2 = (IPolylineCircle)polylineEntity;
				polylineCircle2.Radius = action.NewDouble;
			}
			break;
		case ChangedTarget.LongRadius:
			if (polylineEntity is IPolylineEllipse)
			{
				IPolylineEllipse polylineEllipse4 = (IPolylineEllipse)polylineEntity;
				polylineEllipse4.LongRadius = action.NewDouble;
			}
			break;
		case ChangedTarget.ShortRadius:
			if (polylineEntity is IPolylineEllipse)
			{
				IPolylineEllipse polylineEllipse3 = (IPolylineEllipse)polylineEntity;
				polylineEllipse3.ShortRadius = action.NewDouble;
			}
			break;
		case ChangedTarget.IsReal:
			polylineEntity.IsReal = action.NewBool;
			GroupUpdate();
			break;
		case ChangedTarget.IsClockwise:
			if (polylineEntity is IPolylineCircle)
			{
				IPolylineCircle polylineCircle = (IPolylineCircle)polylineEntity;
				polylineCircle.IsClockwise = action.NewBool;
			}
			if (polylineEntity is IPolylineEllipse)
			{
				IPolylineEllipse polylineEllipse = (IPolylineEllipse)polylineEntity;
				polylineEllipse.IsClockwise = action.NewBool;
			}
			break;
		}
		if (repair)
		{
			RepairFor(polylineEntity);
		}
	}

	public IPolylineAction Copy(IEnumerable<IPolylineEntity> copies)
	{
		IPolylineAction result = new PolylineAction(copies.FirstOrDefault()?.ID ?? (-1), new IPolylineEntity[0], new IPolylineEntity[0]);
		StringBuilder stringBuilder = new StringBuilder();
		foreach (IPolylineEntity copy in copies)
		{
			copy.Save(stringBuilder);
			stringBuilder.Append("\n");
		}
		DataObject data = new DataObject(stringBuilder.ToString());
		try
		{
			Clipboard.SetDataObject(data, copy: true);
		}
		catch (ExternalException)
		{
			return result;
		}
		return result;
	}

	public IPolylineAction Cut(IEnumerable<IPolylineEntity> items)
	{
		Copy(items);
		return Remove(items);
	}

	public IPolylineAction Paste(int index)
	{
		ispaste = true;
		pasteitems.Clear();
		if (index - 1 >= 0)
		{
			pasteitems.Add(items[index - 1]);
		}
		IEnumerable<IPolylineEntity> clipboard = GetClipboard();
		ispaste = false;
		if (index - 1 >= 0)
		{
			pasteitems.RemoveAt(0);
		}
		if (clipboard == null)
		{
			return null;
		}
		return Insert(index, clipboard);
	}

	public IEnumerable<IPolylineEntity> GetClipboard()
	{
		IDataObject dataObject = null;
		string text = null;
		string[] array = null;
		List<IPolylineEntity> list = null;
		try
		{
			dataObject = Clipboard.GetDataObject();
		}
		catch (Exception)
		{
			return null;
		}
		try
		{
			text = dataObject.GetData(DataFormats.UnicodeText).ToString();
		}
		catch (Exception)
		{
			return null;
		}
		try
		{
			array = text.Split('\n');
			list = new List<IPolylineEntity>();
			string[] array2 = array;
			foreach (string text2 in array2)
			{
				if (text2.StartsWith("LINE,"))
				{
					IPolylineLine polylineLine = new PolylineLine(this, pasteitems.Count(), default(Point));
					pasteitems.Add(polylineLine);
					polylineLine.Load(text2);
				}
				else if (text2.StartsWith("CIRCLE,"))
				{
					IPolylineCircle polylineCircle = new PolylineCircle(this, pasteitems.Count(), default(Point), default(Point));
					pasteitems.Add(polylineCircle);
					polylineCircle.Load(text2);
				}
				else if (text2.StartsWith("ARCH,"))
				{
					IPolylineArch polylineArch = new PolylineArch(this, pasteitems.Count(), default(Point), default(Point));
					pasteitems.Add(polylineArch);
					polylineArch.Load(text2);
				}
			}
		}
		catch (Exception)
		{
			return null;
		}
		return pasteitems;
	}

	public void GroupUpdate()
	{
		groups.Clear();
		int num = -1;
		_invoke_selectupdate = false;
		for (int i = 0; i < items.Count(); i++)
		{
			items[i].ID = i;
			items[i].IsSelected = false;
			if (!items[i].IsReal)
			{
				items[i].Group = null;
				if (num < 0)
				{
					continue;
				}
				int count = i - num;
				IPolylineGroup polylineGroup = new PolylineGroup(this, num, groups.Count(), num, count);
				groups.Add(polylineGroup);
				for (int j = num; j < i; j++)
				{
					IPolylineGroup polylineGroup2 = items[j].Group;
					if (polylineGroup2 != null && polylineGroup2.HasRealName())
					{
						polylineGroup.Name = items[j].Group.Name;
					}
					items[j].Group = polylineGroup;
				}
				num = -1;
			}
			else if (num < 0)
			{
				num = i;
			}
		}
		if (num >= 0 && num < items.Count())
		{
			int count2 = items.Count() - num;
			IPolylineGroup polylineGroup3 = new PolylineGroup(this, num, groups.Count(), num, count2);
			groups.Add(polylineGroup3);
			for (int k = num; k < items.Count(); k++)
			{
				IPolylineGroup polylineGroup4 = items[k].Group;
				if (polylineGroup4 != null && polylineGroup4.HasRealName())
				{
					polylineGroup3.Name = items[k].Group.Name;
				}
				items[k].Group = polylineGroup3;
			}
		}
		_invoke_selectupdate = true;
		selectedstart = 0;
		selectedcount = 0;
	}

	public void GroupResize(IPolylineGroup group, int delta)
	{
		group.Count += delta;
		if (group.Count == 0)
		{
			groups.Remove(group);
			for (int i = group.GID; i < groups.Count(); i++)
			{
				groups[i].GID = i;
				groups[i].Start += delta;
			}
		}
		else
		{
			for (int j = group.GID + 1; j < groups.Count(); j++)
			{
				groups[j].Start += delta;
			}
		}
	}

	public void GroupResize(int itemindex, int delta)
	{
		foreach (IPolylineGroup group in groups)
		{
			if (itemindex >= group.Start && itemindex <= group.Start + group.Count)
			{
				GroupResize(group, delta);
			}
		}
	}

	public IPolylineAction GroupMerge(IList<IPolylineGroup> groups)
	{
		if (groups.Count() == 0)
		{
			return null;
		}
		Vector v = new Vector(0.0, 0.0);
		List<IPolylineEntity> list = new List<IPolylineEntity>();
		int start = groups.First().Start;
		int count = groups.Last().Start + groups.Last().Count - start;
		for (int i = 0; i < groups.Count(); i++)
		{
			IPolylineGroup polylineGroup = groups[i];
			IPolylineGroup polylineGroup2 = ((i > 0) ? groups[i - 1] : null);
			if (polylineGroup2 != null)
			{
				Point point = items[polylineGroup2.Start + polylineGroup2.Count].From;
				Point point2 = items[polylineGroup.Start].From;
				v -= point2 - point;
			}
			for (int j = polylineGroup.Start; j < polylineGroup.Start + polylineGroup.Count; j++)
			{
				IPolylineEntity polylineEntity = items[j];
				polylineEntity = polylineEntity.Move(v);
				list.Add(polylineEntity);
			}
		}
		IPolylineAction polylineAction = Replace(start, count, list);
		if (polylineAction != null)
		{
			polylineAction.Message = $"GroupMerge";
		}
		return polylineAction;
	}

	public IPolylineAction GroupSplit(IList<IPolylineEntity> reals)
	{
		if (reals.Count() == 0)
		{
			return null;
		}
		IPolylineActionCollection polylineActionCollection = new PolylineActionCollection();
		int iD = reals.First().ID;
		int iD2 = reals.Last().ID;
		if (iD2 + 1 < items.Count() && items[iD2 + 1].IsReal)
		{
			IPolylineLine polylineLine = new PolylineLine(this, iD2 + 1, reals.Last().To, _isreal: false);
			IPolylineAction item = new PolylineAction(polylineLine.ID, new IPolylineEntity[0], new IPolylineEntity[1] { polylineLine });
			polylineActionCollection.Items.Add(item);
			items.Insert(polylineLine.ID, polylineLine);
		}
		if (iD - 1 >= 0 && items[iD - 1].IsReal)
		{
			IPolylineLine polylineLine2 = new PolylineLine(this, iD, reals.First().From, _isreal: false);
			IPolylineAction item2 = new PolylineAction(polylineLine2.ID, new IPolylineEntity[0], new IPolylineEntity[1] { polylineLine2 });
			polylineActionCollection.Items.Add(item2);
			items.Insert(polylineLine2.ID, polylineLine2);
		}
		if (polylineActionCollection.Items.Count() == 0)
		{
			return null;
		}
		undos.Add(polylineActionCollection);
		redos.Clear();
		GroupUpdate();
		if (polylineActionCollection != null)
		{
			polylineActionCollection.Message = $"GroupSplit";
		}
		return polylineActionCollection;
	}

	public IPolylineAction GroupMove(IList<IPolylineEntity> moves, Vector v)
	{
		if (moves.Count() == 0)
		{
			return null;
		}
		List<IPolylineEntity> list = new List<IPolylineEntity>();
		int num = moves.First().ID;
		int num2 = moves.Count();
		if (num > 0)
		{
			num--;
			num2++;
			IPolylineEntity polylineEntity = items[num];
			polylineEntity = polylineEntity.Move(v);
			list.Add(polylineEntity);
		}
		foreach (IPolylineEntity move in moves)
		{
			list.Add(move.Move(v));
		}
		IPolylineAction polylineAction = Replace(num, num2, list);
		if (polylineAction != null)
		{
			polylineAction.Message = $"GroupMove";
		}
		return polylineAction;
	}

	public IPolylineAction GroupReverse(IList<IPolylineGroup> groups)
	{
		if (groups.Count() == 0)
		{
			return null;
		}
		List<IPolylineEntity> list = new List<IPolylineEntity>();
		int num = groups.First().Start;
		int num2 = groups.Last().Start + groups.Last().Count - num;
		if (num > 0)
		{
			IPolylineEntity polylineEntity = items[num - 1].Clone();
			polylineEntity.To = items[num + num2 - 1].To;
			list.Add(polylineEntity);
		}
		for (int num3 = num + num2 - 1; num3 >= num; num3--)
		{
			IPolylineEntity polylineEntity2 = items[num3];
			polylineEntity2 = polylineEntity2.Reverse();
			list.Add(polylineEntity2);
		}
		if (num > 0)
		{
			num--;
			num2++;
		}
		IPolylineAction polylineAction = Replace(num, num2, list);
		if (polylineAction != null)
		{
			polylineAction.Message = $"GroupReverse";
		}
		return polylineAction;
	}

	public IPolylineAction GroupMirror(IList<IPolylineGroup> groups, Point s, Vector v)
	{
		if (groups.Count() == 0)
		{
			return null;
		}
		List<IPolylineEntity> list = new List<IPolylineEntity>();
		int num = groups.First().Start;
		int num2 = groups.Last().Start + groups.Last().Count - num;
		if (num > 0)
		{
			IPolylineEntity polylineEntity = items[num - 1];
			polylineEntity = polylineEntity.Mirror(s, v);
			list.Add(polylineEntity);
		}
		for (int i = num; i < num + num2; i++)
		{
			IPolylineEntity polylineEntity2 = items[i];
			polylineEntity2 = polylineEntity2.Mirror(s, v);
			list.Add(polylineEntity2);
		}
		if (num > 0)
		{
			num--;
			num2++;
		}
		IPolylineAction polylineAction = Replace(num, num2, list);
		if (polylineAction != null)
		{
			polylineAction.Message = $"GroupMirror";
		}
		return polylineAction;
	}

	public IPolylineAction GroupRotate(IList<IPolylineGroup> groups, Point s, double a)
	{
		if (groups.Count() == 0)
		{
			return null;
		}
		List<IPolylineEntity> list = new List<IPolylineEntity>();
		int num = groups.First().Start;
		int num2 = groups.Last().Start + groups.Last().Count - num;
		if (num > 0)
		{
			IPolylineEntity polylineEntity = items[num - 1];
			polylineEntity = polylineEntity.Rotate(s, a);
			list.Add(polylineEntity);
		}
		for (int i = num; i < num + num2; i++)
		{
			IPolylineEntity polylineEntity2 = items[i];
			polylineEntity2 = polylineEntity2.Rotate(s, a);
			list.Add(polylineEntity2);
		}
		if (num > 0)
		{
			num--;
			num2++;
		}
		IPolylineAction polylineAction = Replace(num, num2, list);
		if (polylineAction != null)
		{
			polylineAction.Message = $"GroupRotate";
		}
		return polylineAction;
	}

	public IPolylineAction GroupScale(IList<IPolylineGroup> groups, Point s, double xs, double ys)
	{
		if (groups.Count() == 0)
		{
			return null;
		}
		List<IPolylineEntity> list = new List<IPolylineEntity>();
		int num = groups.First().Start;
		int num2 = groups.Last().Start + groups.Last().Count - num;
		if (num > 0)
		{
			IPolylineEntity polylineEntity = items[num - 1];
			polylineEntity = polylineEntity.Scale(s, xs, ys);
			list.Add(polylineEntity);
		}
		for (int i = num; i < num + num2; i++)
		{
			IPolylineEntity polylineEntity2 = items[i];
			polylineEntity2 = polylineEntity2.Scale(s, xs, ys);
			list.Add(polylineEntity2);
		}
		if (num > 0)
		{
			num--;
			num2++;
		}
		IPolylineAction polylineAction = Replace(num, num2, list);
		if (polylineAction != null)
		{
			polylineAction.Message = $"GroupScale";
		}
		return polylineAction;
	}

	protected void GroupMatrixOne(List<IPolylineEntity> news, int index, int count, Vector move)
	{
		Point to = items[index].From + move;
		IPolylineLine item = new PolylineLine(this, index - 1, to, _isreal: false);
		news.Add(item);
		for (int i = index; i < index + count; i++)
		{
			news.Add(items[i].Move(move));
		}
	}

	protected void GroupMatrixOne(List<IPolylineEntity> news, int index, int count, int ri, int ci, Point offset)
	{
		Vector move = new Vector(offset.X * (double)ci, offset.Y * (double)ri);
		GroupMatrixOne(news, index, count, move);
	}

	protected void GroupMatrixLineH(List<IPolylineEntity> news, int index, int count, int ri, int c1, int c2, Point offset)
	{
		if (c1 <= c2)
		{
			for (int i = c1; i <= c2; i++)
			{
				GroupMatrixOne(news, index, count, ri, i, offset);
			}
			return;
		}
		for (int num = c1; num >= c2; num--)
		{
			GroupMatrixOne(news, index, count, ri, num, offset);
		}
	}

	protected void GroupMatrixLineV(List<IPolylineEntity> news, int index, int count, int ci, int r1, int r2, Point offset)
	{
		if (r1 <= r2)
		{
			for (int i = r1; i <= r2; i++)
			{
				GroupMatrixOne(news, index, count, i, ci, offset);
			}
			return;
		}
		for (int num = r1; num >= r2; num--)
		{
			GroupMatrixOne(news, index, count, num, ci, offset);
		}
	}

	public IPolylineAction GroupMatrix(IList<IPolylineGroup> groups, int row, int column, Point offset, MatrixStrategy strategy, MatrixPriority priority)
	{
		if (groups.Count() == 0)
		{
			return null;
		}
		List<IPolylineEntity> list = new List<IPolylineEntity>();
		int num = groups.First().Start;
		int num2 = groups.Last().Start + groups.Last().Count - num;
		if (row < 1 || column < 1)
		{
			return null;
		}
		if (row == 1 && column == 1)
		{
			return null;
		}
		if (row == 1)
		{
			for (int i = 0; i < column; i++)
			{
				GroupMatrixOne(list, num, num2, 0, i, offset);
			}
		}
		else if (column == 1)
		{
			for (int j = 0; j < row; j++)
			{
				GroupMatrixOne(list, num, num2, j, 0, offset);
			}
		}
		else
		{
			switch (strategy)
			{
			case MatrixStrategy.Sprial:
				switch (priority)
				{
				case MatrixPriority.Horizontal:
				{
					Directions directions4 = Directions.Up;
					int num7 = 0;
					int num8 = column - 1;
					int num9 = 1;
					int num10 = row - 1;
					GroupMatrixLineH(list, num, num2, 0, 0, column - 1, offset);
					while (num7 <= num8 && num9 <= num10)
					{
						switch (directions4)
						{
						case Directions.Up:
							GroupMatrixLineV(list, num, num2, num8--, num9, num10, offset);
							directions4 = Directions.Left;
							break;
						case Directions.Left:
							GroupMatrixLineH(list, num, num2, num10--, num8, num7, offset);
							directions4 = Directions.Down;
							break;
						case Directions.Down:
							GroupMatrixLineV(list, num, num2, num7++, num10, num9, offset);
							directions4 = Directions.Right;
							break;
						case Directions.Right:
							GroupMatrixLineH(list, num, num2, num9++, num7, num8, offset);
							directions4 = Directions.Up;
							break;
						}
					}
					break;
				}
				case MatrixPriority.Vertical:
				{
					Directions directions3 = Directions.Right;
					int num3 = 1;
					int num4 = column - 1;
					int num5 = 0;
					int num6 = row - 1;
					GroupMatrixLineV(list, num, num2, 0, 0, row - 1, offset);
					while (num3 <= num4 && num5 <= num6)
					{
						switch (directions3)
						{
						case Directions.Right:
							GroupMatrixLineH(list, num, num2, num6--, num3, num4, offset);
							directions3 = Directions.Down;
							break;
						case Directions.Down:
							GroupMatrixLineV(list, num, num2, num4--, num6, num5, offset);
							directions3 = Directions.Left;
							break;
						case Directions.Left:
							GroupMatrixLineH(list, num, num2, num5++, num4, num3, offset);
							directions3 = Directions.Up;
							break;
						case Directions.Up:
							GroupMatrixLineV(list, num, num2, num3++, num5, num6, offset);
							directions3 = Directions.Right;
							break;
						}
					}
					break;
				}
				}
				break;
			case MatrixStrategy.Fold:
				switch (priority)
				{
				case MatrixPriority.Horizontal:
				{
					Directions directions2 = Directions.Right;
					GroupMatrixLineH(list, num, num2, 0, 0, column - 1, offset);
					for (int n = 1; n < row; n++)
					{
						switch (directions2)
						{
						case Directions.Right:
							GroupMatrixLineH(list, num, num2, n, column - 1, 0, offset);
							directions2 = Directions.Left;
							break;
						case Directions.Left:
							GroupMatrixLineH(list, num, num2, n, 0, column - 1, offset);
							directions2 = Directions.Right;
							break;
						}
					}
					break;
				}
				case MatrixPriority.Vertical:
				{
					Directions directions = Directions.Down;
					GroupMatrixLineH(list, num, num2, 0, 0, row - 1, offset);
					for (int m = 1; m < column; m++)
					{
						switch (directions)
						{
						case Directions.Down:
							GroupMatrixLineH(list, num, num2, m, row - 1, 0, offset);
							directions = Directions.Up;
							break;
						case Directions.Up:
							GroupMatrixLineH(list, num, num2, m, 0, row - 1, offset);
							directions = Directions.Down;
							break;
						}
					}
					break;
				}
				}
				break;
			case MatrixStrategy.ZipZap:
				switch (priority)
				{
				case MatrixPriority.Horizontal:
				{
					for (int l = 0; l < row; l++)
					{
						GroupMatrixLineH(list, num, num2, l, 0, column - 1, offset);
					}
					break;
				}
				case MatrixPriority.Vertical:
				{
					for (int k = 0; k < column; k++)
					{
						GroupMatrixLineV(list, num, num2, k, 0, row - 1, offset);
					}
					break;
				}
				}
				break;
			default:
				return null;
			}
		}
		if (num > 0)
		{
			num--;
			num2++;
		}
		IPolylineAction polylineAction = Replace(num, num2, list);
		if (polylineAction != null)
		{
			polylineAction.Message = "GroupMatrix";
		}
		return polylineAction;
	}

	public IPolylineAction GroupExpand(IList<IPolylineGroup> groups, double r, bool removeold)
	{
		IPolylineGroup polylineGroup = groups.FirstOrDefault();
		IPolylineAction polylineAction = null;
		IPolylineEntity polylineEntity = polylineGroup[0]?.Prev;
		List<IPolylineExpand> list = new List<IPolylineExpand>();
		List<IPolylineEntity> list2 = new List<IPolylineEntity>();
		int num = polylineGroup.ID;
		int num2 = polylineGroup.Count;
		if (polylineGroup.IsLeftInside())
		{
			r *= -1.0;
		}
		for (int i = 0; i < polylineGroup.Count; i++)
		{
			IPolylineEntity polylineEntity2 = polylineGroup[i];
			IPolylineExpand polylineExpand = polylineEntity2.Expand(r);
			IPolylineExpand item = polylineEntity2.ExpandPoint(r);
			if (!(polylineExpand is IPolylineExpandCircle) || !(((IPolylineExpandCircle)polylineExpand).R <= 0.0))
			{
				list.Add(polylineExpand);
				list.Add(item);
				list2.Add(polylineEntity2);
			}
		}
		if (list2.Count() <= 1)
		{
			return null;
		}
		for (int j = 0; j < list2.Count(); j++)
		{
			IPolylineEntity polylineEntity3 = list2[j];
			IPolylineEntity polylineEntity4 = ((j - 1 >= 0) ? list2[j - 1] : list2.LastOrDefault());
			IPolylineEntity polylineEntity5 = ((j + 1 < list2.Count()) ? list2[j + 1] : list2.FirstOrDefault());
			IPolylineExpand polylineExpand2 = list[j * 2];
			IPolylineExpand polylineExpand3 = list[j * 2 + 1];
			Vector? vector = polylineEntity4?.LawerTo(r);
			Vector? vector2 = polylineEntity3.LawerFrom(r);
			Vector? vector3 = polylineEntity3.LawerTo(r);
			Vector? vector4 = polylineEntity5?.LawerFrom(r);
			Vector? vector5 = polylineEntity3.HorizonFrom();
			Vector? vector6 = polylineEntity3.HorizonTo();
			if (vector.HasValue && vector2.HasValue)
			{
				polylineExpand2.AngleFrom = Vector.AngleBetween(vector2.Value, vector.Value) * (double)((!(Vector.CrossProduct(vector5.Value, vector2.Value) < 0.0)) ? 1 : (-1)) * Math.PI / 180.0;
			}
			if (vector3.HasValue && vector4.HasValue)
			{
				polylineExpand2.AngleTo = Vector.AngleBetween(vector3.Value, vector4.Value) * (double)((Vector.CrossProduct(vector6.Value, vector3.Value) < 0.0) ? 1 : (-1)) * Math.PI / 180.0;
			}
		}
		Point point = default(Point);
		IPolylineExpand polylineExpand4 = list.FirstOrDefault();
		if (polylineExpand4 == null)
		{
			return null;
		}
		Point? connect = list[list.Count() - 1].GetConnect(new Point(0.0, 0.0), list[0]);
		Point? connect2 = list[list.Count() - 2].GetConnect(new Point(0.0, 0.0), list[0]);
		if (polylineExpand4.AngleFrom > 0.0 && connect.HasValue)
		{
			point = connect.Value;
		}
		else
		{
			if (!(polylineExpand4.AngleFrom <= 0.0) || !connect2.HasValue)
			{
				return null;
			}
			point = connect2.Value;
		}
		list2.Clear();
		IPolylineEntity item2 = new PolylineLine(this, polylineGroup.ID - 1, point, _isreal: false);
		list2.Add(item2);
		if (polylineEntity != null)
		{
			num--;
			num2++;
		}
		for (int k = 0; k < list.Count(); k += 2)
		{
			IPolylineExpand polylineExpand5 = list[k];
			if (polylineExpand5.AngleTo > 1E-05)
			{
				IPolylineExpand polylineExpand6 = list[k + 1];
				IPolylineExpand that = ((k + 2 < list.Count()) ? list[k + 2] : list[0]);
				IPolylineEntity polylineEntity6 = polylineExpand5.ToEntity(point, polylineExpand6);
				if (polylineEntity6 != null)
				{
					list2.Add(polylineEntity6);
					point = polylineEntity6.To;
					if ((polylineEntity6 = polylineExpand6.ToEntity(point, that)) != null)
					{
						list2.Add(polylineEntity6);
						point = polylineEntity6.To;
					}
				}
			}
			else
			{
				IPolylineExpand that2 = ((k + 2 < list.Count()) ? list[k + 2] : list[0]);
				IPolylineEntity polylineEntity7 = polylineExpand5.ToEntity(point, that2);
				if (polylineEntity7 != null)
				{
					list2.Add(polylineEntity7);
					point = polylineEntity7.To;
				}
			}
		}
		if (!removeold)
		{
			num = polylineGroup.ID + polylineGroup.Count;
			num2 = 0;
		}
		polylineAction = Replace(num, num2, list2);
		if (polylineAction != null)
		{
			polylineAction.Message = $"EntityExpand";
		}
		return polylineAction;
	}

	public IPolylineAction GroupFill(IList<IPolylineGroup> groups, double r, FillStrategy strategy, bool removeold)
	{
		IPolylineGroup polylineGroup = groups.FirstOrDefault();
		IPolylineEntity polylineEntity = polylineGroup[0]?.Prev;
		IPolylineAction polylineAction = null;
		List<IPolylineExpandHit> list = new List<IPolylineExpandHit>();
		List<IPolylineExpand> list2 = polylineGroup.Select((IPolylineEntity en) => en.Expand(0.0)).ToList();
		List<IPolylineEntity> list3 = new List<IPolylineEntity>();
		int num = polylineGroup.ID;
		int num2 = polylineGroup.Count;
		double num3 = double.MaxValue;
		double num4 = double.MinValue;
		double num5 = double.MaxValue;
		double num6 = double.MinValue;
		bool flag = true;
		IPolylineExpandHit polylineExpandHit = null;
		IPolylineExpandHit polylineExpandHit2 = null;
		IPolylineExpandHit polylineExpandHit3 = null;
		Point point = default(Point);
		Point point2 = default(Point);
		for (int num7 = 0; num7 < polylineGroup.Count; num7++)
		{
			IPolylineEntity polylineEntity2 = polylineGroup[num7];
			Rect bounding = polylineEntity2.Bounding;
			num3 = Math.Min(num3, bounding.Left);
			num4 = Math.Max(num4, bounding.Right);
			num5 = Math.Min(num5, bounding.Top);
			num6 = Math.Max(num6, bounding.Bottom);
		}
		if ((strategy & FillStrategy.Horizontal) != FillStrategy.None)
		{
			for (double num8 = num3; num8 <= num4; num8 += r)
			{
				list.Clear();
				for (int num9 = 0; num9 < list2.Count(); num9++)
				{
					list2[num9].GetYs(num8, list);
				}
				list.Sort((IPolylineExpandHit h0, IPolylineExpandHit h1) => h0.P.Y.CompareTo(h1.P.Y));
				polylineExpandHit2 = null;
				int num10 = ((!flag) ? (list.Count() - 1) : 0);
				while (flag ? (num10 < list.Count()) : (num10 >= 0))
				{
					IPolylineExpandHit polylineExpandHit4 = list[num10];
					if (polylineExpandHit2 == null)
					{
						polylineExpandHit2 = polylineExpandHit4;
						point = polylineExpandHit?.P ?? polylineEntity?.From ?? new Point(0.0, 0.0);
						point2 = polylineExpandHit2.P;
						list3.Add(new PolylineLine(this, polylineGroup.ID + list3.Count(), point2, _isreal: false));
						if (polylineExpandHit == null && polylineEntity != null)
						{
							num--;
							num2++;
						}
						polylineExpandHit = polylineExpandHit2;
					}
					if (polylineExpandHit3 == null)
					{
						polylineExpandHit3 = polylineExpandHit4;
						if (polylineExpandHit4 != polylineExpandHit2)
						{
							list3.Add(new PolylineLine(this, polylineGroup.ID + list3.Count(), polylineExpandHit4.P, _isreal: false));
						}
					}
					else
					{
						polylineExpandHit3 = null;
						list3.Add(new PolylineLine(this, polylineGroup.ID + list3.Count(), polylineExpandHit4.P));
					}
					num10 = (flag ? (++num10) : (--num10));
				}
				flag = !flag;
			}
			if (!removeold)
			{
				num = polylineGroup.ID + polylineGroup.Count;
				num2 = 0;
			}
			polylineAction = Replace(num, num2, list3);
			if (polylineAction != null)
			{
				polylineAction.Message = "GroupFill";
			}
			return polylineAction;
		}
		if ((strategy & FillStrategy.Vertical) != FillStrategy.None)
		{
			for (double num11 = num5; num11 <= num6; num11 += r)
			{
				list.Clear();
				for (int num12 = 0; num12 < list2.Count(); num12++)
				{
					list2[num12].GetXs(num11, list);
				}
				list.Sort((IPolylineExpandHit h0, IPolylineExpandHit h1) => h0.P.X.CompareTo(h1.P.X));
				int num13 = ((!flag) ? (list.Count() - 1) : 0);
				while (flag ? (num13 < list.Count()) : (num13 >= 0))
				{
					IPolylineExpandHit polylineExpandHit5 = list[num13];
					if (polylineExpandHit2 == null)
					{
						polylineExpandHit2 = polylineExpandHit5;
						point = polylineExpandHit?.P ?? polylineEntity?.From ?? new Point(0.0, 0.0);
						point2 = polylineExpandHit2.P;
						list3.Add(new PolylineLine(this, polylineGroup.ID + list3.Count(), point2, _isreal: false));
						if (polylineExpandHit == null && polylineEntity != null)
						{
							num--;
							num2++;
						}
						polylineExpandHit = polylineExpandHit2;
					}
					if (polylineExpandHit3 == null)
					{
						polylineExpandHit3 = polylineExpandHit5;
						if (polylineExpandHit5 != polylineExpandHit2)
						{
							list3.Add(new PolylineLine(this, polylineGroup.ID + list3.Count(), polylineExpandHit5.P, _isreal: false));
						}
					}
					else
					{
						polylineExpandHit3 = null;
						list3.Add(new PolylineLine(this, polylineGroup.ID + list3.Count(), polylineExpandHit5.P));
					}
					num13 = (flag ? (++num13) : (--num13));
				}
				flag = !flag;
			}
			if (!removeold)
			{
				num = polylineGroup.ID + polylineGroup.Count;
				num2 = 0;
			}
			polylineAction = Replace(num, num2, list3);
			if (polylineAction != null)
			{
				polylineAction.Message = "GroupFill";
			}
			return polylineAction;
		}
		if ((strategy & FillStrategy.None) != FillStrategy.None)
		{
		}
		return null;
	}

	public IPolylineAction EntityBreak(IPolylineEntity entity, Point b)
	{
		IPolylineAction polylineAction = null;
		switch (entity.Type)
		{
		case PolylineType.Line:
		{
			IPolylineLine polylineLine = (IPolylineLine)entity;
			IPolylineLine polylineLine2 = (IPolylineLine)polylineLine.Clone();
			IPolylineLine polylineLine3 = (IPolylineLine)polylineLine.Clone();
			polylineLine2.To = b;
			IPolylineEntity[] newitems3 = new IPolylineEntity[2] { polylineLine2, polylineLine3 };
			polylineAction = Replace(polylineLine.ID, 1, newitems3);
			break;
		}
		case PolylineType.Circle:
		{
			IPolylineCircle polylineCircle = (IPolylineCircle)entity;
			IPolylineArch polylineArch4 = new PolylineArch(this, entity.ID, b, polylineCircle.Center, polylineCircle.IsClockwise);
			IPolylineArch polylineArch5 = new PolylineArch(this, entity.ID + 1, polylineCircle.From, polylineCircle.Center, polylineCircle.IsClockwise);
			IPolylineEntity[] newitems2 = new IPolylineEntity[2] { polylineArch4, polylineArch5 };
			polylineAction = Replace(polylineCircle.ID, 1, newitems2);
			break;
		}
		case PolylineType.Arch:
		{
			IPolylineArch polylineArch = (IPolylineArch)entity;
			IPolylineArch polylineArch2 = (IPolylineArch)polylineArch.Clone();
			IPolylineArch polylineArch3 = (IPolylineArch)polylineArch.Clone();
			polylineArch2.To = b;
			IPolylineEntity[] newitems = new IPolylineEntity[2] { polylineArch2, polylineArch3 };
			polylineAction = Replace(polylineArch.ID, 1, newitems);
			break;
		}
		}
		if (polylineAction != null)
		{
			polylineAction.Message = $"EntityBreak";
		}
		return polylineAction;
	}

	protected IPolylineLine EntityCornerEdge(IPolylineLine line)
	{
		if (line.Next is IPolylineLine && line.Next.IsReal)
		{
			return (IPolylineLine)line.Next;
		}
		if (line.Group == null)
		{
			return null;
		}
		if (line.ID != line.Group.Start + line.Group.Count - 1)
		{
			return null;
		}
		IPolylineEntity polylineEntity = line.Group.FirstOrDefault();
		IPolylineEntity polylineEntity2 = line.Group.LastOrDefault();
		if (line == polylineEntity2 && polylineEntity is IPolylineLine && IsEqualP(polylineEntity2.To, polylineEntity.From))
		{
			return (IPolylineLine)polylineEntity;
		}
		return null;
	}

	public IPolylineArch EntityRoundDemo(IPolylineEntity entity, double r)
	{
		IPolylineArch result = null;
		if (!(entity is IPolylineLine))
		{
			return result;
		}
		IPolylineLine polylineLine = (IPolylineLine)entity;
		IPolylineLine polylineLine2 = EntityCornerEdge(polylineLine);
		if (polylineLine2 == null)
		{
			return result;
		}
		Vector vector = polylineLine.To - polylineLine.From;
		Vector vector2 = polylineLine2.To - polylineLine2.From;
		double length = vector.Length;
		double length2 = vector2.Length;
		double num = Vector.CrossProduct(vector, vector2);
		if (r > length || r > length2)
		{
			return result;
		}
		vector *= (length - r) / length;
		vector2 *= r / length2;
		Point point = polylineLine.From + vector;
		Point point2 = polylineLine2.From + vector2;
		Vector vector3 = point2 - point;
		Vector vector4 = new Vector(vector.Y, 0.0 - vector.X) / vector.Length * ((!(num >= 0.0)) ? 1 : (-1));
		double d = Vector.AngleBetween(vector4, vector3) * Math.PI / 180.0;
		double num2 = Math.Cos(d);
		vector4 *= vector3.Length * 0.5 / num2;
		Point center = point + vector4;
		return new PolylineDemoArch(this, polylineLine.ID + 1, point, point2, center, num <= 0.0);
	}

	public IPolylineAction EntityRound(IPolylineEntity entity, double r)
	{
		IPolylineAction result = null;
		if (!(entity is IPolylineLine))
		{
			return result;
		}
		IPolylineLine polylineLine = (IPolylineLine)entity;
		IPolylineLine polylineLine2 = EntityCornerEdge(polylineLine);
		IPolylineArch polylineArch = EntityRoundDemo(entity, r);
		if (polylineArch == null)
		{
			return result;
		}
		IPolylineLine polylineLine3 = (IPolylineLine)polylineLine.Clone();
		IPolylineLine polylineLine4 = (IPolylineLine)polylineLine2.Clone();
		IPolylineArch polylineArch2 = new PolylineArch(this, polylineLine.ID + 1, polylineArch.To, polylineArch.Center, polylineArch.IsClockwise);
		polylineLine3.To = polylineArch.From;
		if (polylineLine.ID + 1 == polylineLine2.ID)
		{
			IPolylineEntity[] newitems = new IPolylineEntity[3] { polylineLine3, polylineArch2, polylineLine4 };
			result = Replace(polylineLine.ID, 2, newitems);
		}
		else
		{
			IPolylineEntity[] newitems2 = new IPolylineEntity[2] { polylineLine3, polylineArch2 };
			IPolylineAction item = Replace(polylineLine.ID, 1, newitems2);
			newitems2 = new IPolylineEntity[1]
			{
				new PolylineLine(this, polylineLine2.ID - 1, polylineArch.To, _isreal: false)
			};
			IPolylineAction item2 = ((polylineLine2.ID > 0) ? Replace(polylineLine2.ID - 1, 1, newitems2) : Insert(0, newitems2[0]));
			IPolylineActionCollection polylineActionCollection = new PolylineActionCollection();
			polylineActionCollection.Items.Add(item);
			polylineActionCollection.Items.Add(item2);
			result = polylineActionCollection;
		}
		if (result != null)
		{
			result.Message = $"EntityRound";
		}
		return result;
	}

	public IPolylineLine EntityBevelDemo(IPolylineEntity entity, double r)
	{
		IPolylineLine result = null;
		if (!(entity is IPolylineLine))
		{
			return result;
		}
		IPolylineLine polylineLine = (IPolylineLine)entity;
		IPolylineLine polylineLine2 = EntityCornerEdge(polylineLine);
		Vector vector = polylineLine.To - polylineLine.From;
		Vector vector2 = polylineLine2.To - polylineLine2.From;
		double length = vector.Length;
		double length2 = vector2.Length;
		double num = Vector.CrossProduct(vector, vector2);
		if (r > length || r > length2)
		{
			return result;
		}
		vector *= (length - r) / length;
		vector2 *= r / length2;
		Point point = polylineLine.From + vector;
		Point to = polylineLine2.From + vector2;
		return new PolylineDemoLine(this, polylineLine.ID + 1, point, to);
	}

	public IPolylineAction EntityBevel(IPolylineEntity entity, double r)
	{
		IPolylineAction result = null;
		if (!(entity is IPolylineLine))
		{
			return result;
		}
		IPolylineLine polylineLine = (IPolylineLine)entity;
		IPolylineLine polylineLine2 = EntityCornerEdge(polylineLine);
		IPolylineLine polylineLine3 = EntityBevelDemo(entity, r);
		if (polylineLine3 == null)
		{
			return result;
		}
		IPolylineLine polylineLine4 = (IPolylineLine)polylineLine.Clone();
		IPolylineLine polylineLine5 = (IPolylineLine)polylineLine2.Clone();
		IPolylineLine polylineLine6 = new PolylineLine(this, polylineLine.ID + 1, polylineLine3.To);
		polylineLine4.To = polylineLine3.From;
		if (polylineLine.ID + 1 == polylineLine2.ID)
		{
			IPolylineEntity[] newitems = new IPolylineEntity[3] { polylineLine4, polylineLine6, polylineLine5 };
			result = Replace(polylineLine.ID, 2, newitems);
		}
		else
		{
			IPolylineEntity[] newitems2 = new IPolylineEntity[2] { polylineLine4, polylineLine6 };
			IPolylineAction item = Replace(polylineLine.ID, 1, newitems2);
			newitems2 = new IPolylineEntity[1]
			{
				new PolylineLine(this, polylineLine2.ID - 1, polylineLine3.To, _isreal: false)
			};
			IPolylineAction item2 = ((polylineLine2.ID > 0) ? Replace(polylineLine2.ID - 1, 1, newitems2) : Insert(0, newitems2[0]));
			IPolylineActionCollection polylineActionCollection = new PolylineActionCollection();
			polylineActionCollection.Items.Add(item);
			polylineActionCollection.Items.Add(item2);
			result = polylineActionCollection;
		}
		if (result != null)
		{
			result.Message = $"EntityBevel";
		}
		return result;
	}

	public IPolylineCorner EntitySharpDemo(IPolylineEntity entity)
	{
		Point corner = default(Point);
		IPolylineCorner result = null;
		if (!(entity.Prev is IPolylineLine))
		{
			return result;
		}
		if (!(entity.Next is IPolylineLine))
		{
			return result;
		}
		IPolylineLine polylineLine = (IPolylineLine)entity.Prev;
		IPolylineLine polylineLine2 = (IPolylineLine)entity.Next;
		Point point = polylineLine.From;
		Point to = polylineLine.To;
		Point to2 = polylineLine2.To;
		Point point2 = polylineLine2.From;
		if (IsEqualX(point.X, to.X) && IsEqualX(point2.X, to2.X))
		{
			return result;
		}
		if (IsEqualY(point.Y, to.Y) && IsEqualY(point2.Y, to2.Y))
		{
			return result;
		}
		if (IsEqualX(point.X, to.X))
		{
			corner = point2 + (to2 - point2) / (to2.X - point2.X) * (to.X - point2.X);
		}
		else if (IsEqualX(point2.X, to2.X))
		{
			corner = point + (to - point) / (to.X - point.X) * (point2.X - point.X);
		}
		else
		{
			double num = (to.Y - point.Y) / (to.X - point.X);
			double num2 = point.Y - point.X * num;
			double num3 = (point2.Y - to2.Y) / (point2.X - to2.X);
			double num4 = to2.Y - to2.X * num3;
			if (num == num3)
			{
				return result;
			}
			corner.X = (num4 - num2) / (num - num3);
			corner.Y = num2 + num * corner.X;
		}
		return new PolylineDemoCorner(this, polylineLine.ID, point, corner, to2);
	}

	public IPolylineAction EntitySharp(IPolylineEntity entity)
	{
		IPolylineAction result = null;
		IPolylineCorner polylineCorner = EntitySharpDemo(entity);
		if (polylineCorner == null)
		{
			return result;
		}
		IPolylineLine polylineLine = (IPolylineLine)entity.Prev;
		IPolylineLine polylineLine2 = (IPolylineLine)entity.Next;
		IPolylineLine polylineLine3 = (IPolylineLine)polylineLine.Clone();
		IPolylineLine polylineLine4 = (IPolylineLine)polylineLine2.Clone();
		polylineLine3.To = polylineCorner.Corner;
		IPolylineEntity[] newitems = new IPolylineEntity[2] { polylineLine3, polylineLine4 };
		result = Replace(polylineLine.ID, 3, newitems);
		if (result != null)
		{
			result.Message = $"EntitySharp";
		}
		return result;
	}

	public IPolylineAction EntityFill0(IPolylineGroup group, double r)
	{
		return null;
	}

	public IPolylineAction EntityFill1(IPolylineGroup group, double r)
	{
		return null;
	}

	public void Select(int _selectedstart, int _selectedcount)
	{
		_invoke_selectupdate = false;
		foreach (IPolylineGroup group in groups)
		{
			group.IsSelected = false;
		}
		for (int i = 0; i < items.Count(); i++)
		{
			items[i].ID = i;
			items[i].IsSelected = i >= _selectedstart && i < _selectedstart + _selectedcount;
		}
		selectedstart = _selectedstart;
		selectedcount = _selectedcount;
		_invoke_selectupdate = true;
	}

	public void SelectUpdate(IPolylineEntity item)
	{
		if (!_invoke_selectupdate)
		{
			return;
		}
		int iD = item.ID;
		int num = ((item is IPolylineGroup) ? (((IPolylineGroup)item).Start + ((IPolylineGroup)item).Count - 1) : item.ID);
		if (item.IsSelected)
		{
			if (selectedcount == 0)
			{
				selectedstart = iD;
				selectedcount = num - iD + 1;
				return;
			}
			if (iD < selectedstart)
			{
				selectedcount += selectedstart - iD;
				selectedstart = iD;
			}
			if (num >= selectedstart + selectedcount)
			{
				selectedcount = num - selectedstart + 1;
			}
			return;
		}
		bool flag = false;
		if (iD == selectedstart && selectedcount > 0)
		{
			int num2 = selectedstart;
			while (num2 < selectedstart + selectedcount)
			{
				if (!items[num2].IsSelected)
				{
					IPolylineEntity polylineEntity = items[num2];
					if (polylineEntity == null || polylineEntity.Group?.IsSelected != true)
					{
						num2++;
						continue;
					}
				}
				selectedcount -= num2 - selectedstart;
				selectedstart = num2;
				flag = true;
				break;
			}
			if (!flag)
			{
				selectedstart = 0;
				selectedcount = 0;
			}
		}
		if (num != selectedstart + selectedcount - 1 || selectedcount <= 0)
		{
			return;
		}
		int num3 = selectedstart + selectedcount - 1;
		while (num3 >= selectedstart)
		{
			if (!items[num3].IsSelected)
			{
				IPolylineEntity polylineEntity2 = items[num3];
				if (polylineEntity2 == null || polylineEntity2.Group?.IsSelected != true)
				{
					num3--;
					continue;
				}
			}
			selectedcount -= selectedstart + selectedcount - num3 - 1;
			flag = true;
			break;
		}
		if (!flag)
		{
			selectedstart = 0;
			selectedcount = 0;
		}
	}

	public void ArchToLine()
	{
		items.CollectionChanged -= OnItemsChanged;
		ObservableCollection<IPolylineEntity> observableCollection = new ObservableCollection<IPolylineEntity>();
		foreach (IPolylineEntity item2 in items)
		{
			if (item2 is IPolylineCircle)
			{
				IPolylineCircle polylineCircle = (IPolylineCircle)item2;
				Vector vector = polylineCircle.From - polylineCircle.Center;
				Vector vector2 = polylineCircle.From - polylineCircle.Center;
				double num = Math.PI * 2.0;
				if (polylineCircle is IPolylineArch)
				{
					IPolylineArch polylineArch = (IPolylineArch)polylineCircle;
					vector2 = polylineArch.To - polylineArch.Center;
					num = Vector.AngleBetween(vector, vector2);
					if (polylineArch.IsClockwise)
					{
						num = 360.0 - num;
					}
					num *= Math.PI / 180.0;
				}
				for (; num < 0.0; num += Math.PI * 2.0)
				{
				}
				while (num > Math.PI * 2.0)
				{
					num -= Math.PI * 2.0;
				}
				double num2 = Math.Sin(Math.PI / 8.0);
				double num3 = Math.Cos(Math.PI / 8.0);
				Vector vector3 = vector;
				for (double num4 = 0.0; num4 < num; num4 += Math.PI / 8.0)
				{
					double num5 = num4 + Math.PI / 8.0;
					Vector vector4 = vector3;
					if (num5 > num)
					{
						num5 = num;
						vector4 = vector2;
					}
					else if (polylineCircle.IsClockwise)
					{
						vector4.X = vector3.X * num3 + vector3.Y * num2;
						vector4.Y = (0.0 - vector3.X) * num2 + vector3.Y * num3;
					}
					else
					{
						vector4.X = vector3.X * num3 - vector3.Y * num2;
						vector4.Y = vector3.X * num2 + vector3.Y * num3;
					}
					IPolylineLine item = new PolylineLine(this, observableCollection.Count(), polylineCircle.Center + vector4);
					observableCollection.Add(item);
					vector3 = vector4;
				}
			}
			else
			{
				IPolylineEntity polylineEntity = item2.Clone();
				polylineEntity.ID = observableCollection.Count();
				observableCollection.Add(polylineEntity);
			}
		}
		items = observableCollection;
		items.CollectionChanged += OnItemsChanged;
		undos.Clear();
		redos.Clear();
		GroupUpdate();
	}

	public void Rect4To(IPolylineLine topleft, IPolylineLine topright, IPolylineLine bottomright, IPolylineLine bottomleft, Rect rect)
	{
		topleft.To = rect.TopLeft;
		topright.To = rect.TopRight;
		bottomleft.To = rect.BottomLeft;
		bottomright.To = rect.BottomRight;
	}

	public IPolylineAction ReorderSpecific(IList<IPolylineReorderingGroup> regroups)
	{
		List<IPolylineEntity> list = new List<IPolylineEntity>();
		foreach (IPolylineReorderingGroup regroup in regroups)
		{
			if (regroup.ID > 0)
			{
				IPolylineEntity polylineEntity = items[regroup.ID - 1];
				polylineEntity = polylineEntity.Clone();
				list.Add(polylineEntity);
			}
			else
			{
				IPolylineEntity item = new PolylineLine(this, 0, new Point(0.0, 0.0), _isreal: false);
				list.Add(item);
			}
			foreach (IPolylineEntity item3 in regroup)
			{
				IPolylineEntity item2 = item3.Clone();
				list.Add(item2);
			}
		}
		return Replace(0, items.Count(), list);
	}

	public IPolylineAction ReorderIntelligence(ReorderingStrategy strategy)
	{
		IPolylineAction polylineAction = null;
		switch (strategy)
		{
		case ReorderingStrategy.MinimizeLength:
		{
			using (LengthOptimizeModule lengthOptimizeModule = new LengthOptimizeModule(this))
			{
				IList<IPolylineEntity> newitems2 = lengthOptimizeModule.Sort();
				polylineAction = Replace(0, items.Count(), newitems2);
			}
			break;
		}
		case ReorderingStrategy.FlatCorners:
		{
			using (CornerOptimizeModule cornerOptimizeModule = new CornerOptimizeModule(this))
			{
				IList<IPolylineEntity> newitems = cornerOptimizeModule.Sort();
				polylineAction = Replace(0, items.Count(), newitems);
			}
			break;
		}
		}
		return null;
	}

	public bool IsEqualP(Point p1, Point p2)
	{
		return IsEqualX(p1.X, p2.X) && IsEqualY(p1.Y, p2.Y);
	}

	public bool IsEqualX(double x1, double x2)
	{
		double num = width * 1E-08;
		return Math.Abs(x1 - x2) <= num;
	}

	public bool IsEqualY(double y1, double y2)
	{
		double num = height * 1E-08;
		return Math.Abs(y1 - y2) <= num;
	}

	public void RepairAll()
	{
		bool movefrom = false;
		foreach (IPolylineEntity item in items)
		{
			movefrom = RepairNext(item, movefrom);
			if (item.Next is IPolylineEllipse)
			{
				IPolylineEllipse polylineEllipse = (IPolylineEllipse)item.Next;
				double angle = Vector.AngleBetween(new Vector(1.0, 0.0), item.To - polylineEllipse.Center) * Math.PI / 180.0;
				item.To = polylineEllipse.GetCrossPoint(angle);
				polylineEllipse.To = polylineEllipse.From;
				bool flag = RepairBack(item, moveto: true);
				int num = item.ID - 1;
				while (num >= 0 && flag)
				{
					flag = RepairBack(items[num], flag);
					num--;
				}
			}
		}
	}

	public void RepairFor(IPolylineEntity entity)
	{
		RepairBack(entity, moveto: true);
		if (entity is IPolylineEllipse)
		{
			IPolylineEllipse polylineEllipse = (IPolylineEllipse)entity;
			IPolylineEntity prev = polylineEllipse.Prev;
			IPolylineEntity next = polylineEllipse.Next;
			if (entity.Prev != null)
			{
				double angle = Vector.AngleBetween(new Vector(1.0, 0.0), prev.To - polylineEllipse.Center) * Math.PI / 180.0;
				bool flag = true;
				prev.To = polylineEllipse.GetCrossPoint(angle);
				int num = polylineEllipse.ID - 1;
				while (num >= 0 && flag)
				{
					flag = RepairBack(items[num], flag);
					num--;
				}
			}
			polylineEllipse.To = polylineEllipse.From;
		}
		bool flag2 = true;
		for (int i = entity.ID + 1; i < items.Count(); i++)
		{
			if (!flag2)
			{
				break;
			}
			flag2 = RepairNext(items[i], flag2);
		}
	}

	public bool RepairBack(IPolylineEntity entity, bool moveto)
	{
		if (entity is IPolylineCircle && !(entity is IPolylineArch))
		{
			IPolylineCircle polylineCircle = (IPolylineCircle)entity;
			moveto = !IsEqualP(polylineCircle.From, polylineCircle.To);
			polylineCircle.Center += polylineCircle.To - polylineCircle.From;
			polylineCircle.From = polylineCircle.To;
			return moveto;
		}
		if (entity is IPolylineEllipse && !(entity is IPolylineEllipseArch))
		{
			IPolylineEllipse polylineEllipse = (IPolylineEllipse)entity;
			moveto = !IsEqualP(polylineEllipse.From, polylineEllipse.To);
			polylineEllipse.Center += polylineEllipse.To - polylineEllipse.From;
			polylineEllipse.From = polylineEllipse.To;
			return moveto;
		}
		if (entity is IPolylineArch && moveto)
		{
			IPolylineArch polylineArch = (IPolylineArch)entity;
			polylineArch.CenterFrom();
			return false;
		}
		return false;
	}

	public bool RepairNext(IPolylineEntity entity, bool movefrom)
	{
		if (entity is IPolylineCircle && !(entity is IPolylineArch))
		{
			IPolylineCircle polylineCircle = (IPolylineCircle)entity;
			movefrom = !IsEqualP(polylineCircle.From, polylineCircle.To);
			polylineCircle.Center += polylineCircle.From - polylineCircle.To;
			polylineCircle.To = polylineCircle.From;
			return movefrom;
		}
		if (entity is IPolylineEllipse && !(entity is IPolylineEllipseArch))
		{
			IPolylineEllipse polylineEllipse = (IPolylineEllipse)entity;
			movefrom = !IsEqualP(polylineEllipse.From, polylineEllipse.To);
			polylineEllipse.Center += polylineEllipse.From - polylineEllipse.To;
			polylineEllipse.To = polylineEllipse.From;
			return movefrom;
		}
		if (entity is IPolylineArch && movefrom)
		{
			IPolylineArch polylineArch = (IPolylineArch)entity;
			polylineArch.CenterTo();
			return false;
		}
		return false;
	}

	public void UserStart()
	{
		foreach (IPolylineUserFormat userfmt in userfmts)
		{
			userfmt.Temporary();
		}
	}

	public void UserYes(IList<IPolylineUserFormat> _users)
	{
		_invoke_userfmtschanged = false;
		IPolylineUserFormat[] array = userfmts.ToArray();
		IPolylineUserObject[] array2 = new IPolylineUserObject[array.Length];
		int[] array3 = new int[_users.Count()];
		userfmts.Clear();
		for (int i = 0; i < _users.Count(); i++)
		{
			userfmts.Add(_users[i]);
			array3[i] = -1;
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j] == _users[i])
				{
					array3[i] = j;
					break;
				}
			}
		}
		foreach (IPolylineEntity item2 in items)
		{
			item2.UserObjs.CopyTo(array2, 0);
			item2.UserObjs.Clear();
			for (int k = 0; k < _users.Count(); k++)
			{
				IList<IPolylineUserObject> userObjs = item2.UserObjs;
				object item;
				if (array3[k] < 0)
				{
					IPolylineUserObject polylineUserObject = new PolylineUserObject(_users[k]);
					item = polylineUserObject;
				}
				else
				{
					item = array2[array3[k]];
				}
				userObjs.Add((IPolylineUserObject)item);
			}
		}
		_invoke_userfmtschanged = true;
		this.UserFmtsChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
	}

	public void UserNo()
	{
		for (int i = 0; i < userfmts.Count(); i++)
		{
			userfmts[i].ID = i;
			userfmts[i].Restore();
		}
	}

	protected void InvokePropertyChanged(string propname)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
	}

	private void OnItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		if (!_invoke_itemschanged)
		{
			return;
		}
		this.ItemsChanged?.Invoke(this, e);
		if (e.NewItems == null)
		{
			return;
		}
		foreach (IPolylineEntity newItem in e.NewItems)
		{
			Rect bounding = newItem.Bounding;
			left = Math.Min(left, bounding.Left);
			top = Math.Min(top, bounding.Top);
			width = Math.Max(width, bounding.Right - left);
			height = Math.Max(height, bounding.Bottom - top);
		}
	}

	private void OnUserFmtsChanged(object sender, NotifyCollectionChangedEventArgs e)
	{
		if (!_invoke_userfmtschanged)
		{
			return;
		}
		for (int i = 0; i < userfmts.Count(); i++)
		{
			userfmts[i].ID = i;
		}
		if (e.Action == NotifyCollectionChangedAction.Reset)
		{
			foreach (IPolylineEntity item2 in items)
			{
				item2.RefreshUserObjs();
			}
		}
		else
		{
			if (e.OldItems != null && e.OldItems.Count > 0)
			{
				foreach (IPolylineEntity item3 in items)
				{
					for (int num = e.OldItems.Count - 1; num >= 0; num--)
					{
						IPolylineUserObject polylineUserObject = item3.UserObjs[e.OldStartingIndex];
						item3.UserObjs.RemoveAt(e.OldStartingIndex);
						polylineUserObject.Dispose();
					}
				}
			}
			if (e.NewItems != null && e.NewItems.Count > 0)
			{
				foreach (IPolylineEntity item4 in items)
				{
					for (int num2 = e.NewItems.Count - 1; num2 >= 0; num2--)
					{
						if (e.NewItems[num2] is IPolylineUserFormat)
						{
							IPolylineUserFormat format = (IPolylineUserFormat)e.NewItems[num2];
							IPolylineUserObject item = new PolylineUserObject(format);
							item4.UserObjs.Insert(e.NewStartingIndex, item);
						}
					}
				}
			}
		}
		this.UserFmtsChanged?.Invoke(this, e);
	}
}

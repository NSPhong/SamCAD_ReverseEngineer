using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using SamSoarII.Polyline.Entity.User;
using SamSoarII.Polyline.Export;

namespace SamSoarII.Polyline.Control;

public class PolylineDataGridMultiplyModifyCore : INotifyPropertyChanged
{
	private IPolylineImage core;

	private List<PolylineExportColumn> columns;

	private PolylineExportColumn selectedcolumn;

	private int startline;

	private int endline;

	private object modifyvalue;

	private bool ismodifyselected;

	public IPolylineImage Core => core;

	public IList<PolylineExportColumn> Columns => columns;

	public PolylineExportColumn SelectedColumn
	{
		get
		{
			return selectedcolumn;
		}
		set
		{
			selectedcolumn = value;
			InvokeProp("SelectedColumn");
		}
	}

	public int StartLine
	{
		get
		{
			return startline;
		}
		set
		{
			startline = value;
			InvokeProp("StartLine");
		}
	}

	public int EndLine
	{
		get
		{
			return endline;
		}
		set
		{
			endline = value;
			InvokeProp("EndLine");
		}
	}

	public object ModifyValue
	{
		get
		{
			return modifyvalue;
		}
		set
		{
			modifyvalue = value;
			InvokeProp("ModifyValue");
		}
	}

	public bool IsModifySelected
	{
		get
		{
			return ismodifyselected;
		}
		set
		{
			ismodifyselected = value;
			InvokeProp("IsModifySelected");
			InvokeProp("IsModifyLine");
		}
	}

	public bool IsModifyLine => !ismodifyselected;

	public event PropertyChangedEventHandler PropertyChanged;

	public PolylineDataGridMultiplyModifyCore(IPolylineImage _core)
	{
		core = _core;
		columns = core.UserFmts.Select((IPolylineUserFormat uf) => new PolylineExportColumn(PolylineExportColumnType.User)
		{
			UserFormat = uf
		}).ToList();
		selectedcolumn = null;
		startline = 0;
		endline = 0;
		modifyvalue = null;
	}

	protected void InvokeProp(string propname)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
	}
}

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SamSoarII.Polyline.Import;

public class PolylineImportCore
{
	private IPolylineImage image;

	private ObservableCollection<PolylineImportColumn> columns;

	private int rowstart;

	private int rowcount;

	private bool kksk;

	private bool toskdevice;

	public IPolylineImage Image => image;

	public IList<PolylineImportColumn> Columns => columns;

	public int RowStart
	{
		get
		{
			return rowstart;
		}
		set
		{
			rowstart = value;
			InvokeProp("RowStart");
		}
	}

	public int RowCount
	{
		get
		{
			return rowcount;
		}
		set
		{
			rowcount = value;
			InvokeProp("RowCount");
		}
	}

	public bool KKSK
	{
		get
		{
			return kksk;
		}
		set
		{
			kksk = value;
			InvokeProp("KKSK");
		}
	}

	public bool ToSKDevice
	{
		get
		{
			return toskdevice;
		}
		set
		{
			toskdevice = value;
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public PolylineImportCore(IPolylineImage _image)
	{
		image = _image;
		columns = new ObservableCollection<PolylineImportColumn>();
		rowstart = 0;
		rowcount = 100;
		kksk = false;
	}

	protected void InvokeProp(string propname)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
	}
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SamSoarII.Polyline.Export;

public class PolylineExportCore : INotifyPropertyChanged, IDisposable
{
	private PolylineImage image;

	private ObservableCollection<PolylineExportColumn> columns;

	private int rowstart;

	private int rowend;

	private bool kksk;

	private bool toskdevice;

	private PolylineExportSK sk;

	public PolylineImage Image => image;

	public IList<PolylineExportColumn> Columns => columns;

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

	public int RowEnd
	{
		get
		{
			return rowend;
		}
		set
		{
			rowend = value;
			InvokeProp("RowEnd");
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
			InvokeProp("ToSKDevice");
		}
	}

	public PolylineExportSK SK => sk;

	public event PropertyChangedEventHandler PropertyChanged;

	public PolylineExportCore(PolylineImage _image)
	{
		image = _image;
		columns = new ObservableCollection<PolylineExportColumn>();
		rowstart = 0;
		rowend = 100;
		kksk = false;
		toskdevice = true;
		sk = new PolylineExportSK();
		columns.Add(new PolylineExportColumn(PolylineExportColumnType.X));
		columns.Add(new PolylineExportColumn(PolylineExportColumnType.Y));
		columns.Add(new PolylineExportColumn(PolylineExportColumnType.Type));
		columns.Add(new PolylineExportColumn(PolylineExportColumnType.Radius));
		columns.Add(new PolylineExportColumn(PolylineExportColumnType.CenterX));
		columns.Add(new PolylineExportColumn(PolylineExportColumnType.CenterY));
	}

	public void Dispose()
	{
		image = null;
		columns = null;
	}

	protected void InvokeProp(string propname)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
	}
}

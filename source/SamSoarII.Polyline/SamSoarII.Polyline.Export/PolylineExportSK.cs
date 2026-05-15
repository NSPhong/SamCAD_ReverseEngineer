using System.ComponentModel;

namespace SamSoarII.Polyline.Export;

public class PolylineExportSK
{
	private string deciptname;

	private string description;

	private bool saveplc;

	private bool loadplc;

	public string DeciptName
	{
		get
		{
			return deciptname;
		}
		set
		{
			deciptname = value;
			InvokeProp("DeciptName");
		}
	}

	public string Description
	{
		get
		{
			return description;
		}
		set
		{
			description = value;
			InvokeProp("Description");
		}
	}

	public bool SavePLC
	{
		get
		{
			return saveplc;
		}
		set
		{
			saveplc = value;
			InvokeProp("SavePLC");
		}
	}

	public bool LoadPLC
	{
		get
		{
			return loadplc;
		}
		set
		{
			loadplc = value;
			InvokeProp("LoadPLC");
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public PolylineExportSK()
	{
		deciptname = "SamCAD graphics";
		description = string.Empty;
		saveplc = false;
		loadplc = false;
	}

	protected void InvokeProp(string propname)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
	}
}

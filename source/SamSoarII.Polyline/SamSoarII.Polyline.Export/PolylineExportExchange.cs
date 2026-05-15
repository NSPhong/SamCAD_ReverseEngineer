using System.ComponentModel;
using SamSoarII.Polyline.Control;

namespace SamSoarII.Polyline.Export;

public class PolylineExportExchange
{
	private PolylineExportColumn parent;

	private int from;

	private int to;

	public PolylineExportColumn Parent => parent;

	public int From => from;

	public int To
	{
		get
		{
			return to;
		}
		set
		{
			to = value;
			InvokeProp("To");
		}
	}

	public string From_S
	{
		get
		{
			PolylineExportColumnType? polylineExportColumnType = parent?.Type;
			PolylineExportColumnType? polylineExportColumnType2 = polylineExportColumnType;
			if (polylineExportColumnType2.HasValue)
			{
				PolylineExportColumnType valueOrDefault = polylineExportColumnType2.GetValueOrDefault();
				if (valueOrDefault == PolylineExportColumnType.Type)
				{
					return (from >= 0 && from < PolylineDataGridItem._Types_S.Length) ? PolylineDataGridItem._Types_S[from] : string.Empty;
				}
			}
			return from.ToString();
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public PolylineExportExchange(PolylineExportColumn _parent, int _from, int _to)
	{
		parent = _parent;
		from = _from;
		to = _to;
	}

	protected void InvokeProp(string propname)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
	}
}

using System.Collections.Generic;
using SamSoarII.Polyline.Control;
using SamSoarII.Polyline.Entity.User;

namespace SamSoarII.Polyline.Export;

public class PolylineExportColumn
{
	private PolylineExportColumnType type;

	private IPolylineUserFormat userformat;

	private List<PolylineExportExchange> exchanges;

	public PolylineExportColumnType Type
	{
		get
		{
			return type;
		}
		set
		{
			type = value;
		}
	}

	public IPolylineUserFormat UserFormat
	{
		get
		{
			return userformat;
		}
		set
		{
			userformat = value;
		}
	}

	public List<PolylineExportExchange> Exchanges => exchanges;

	public PolylineExportColumn(PolylineExportColumnType _type)
	{
		type = _type;
		exchanges = new List<PolylineExportExchange>();
		userformat = null;
		if (type == PolylineExportColumnType.Type)
		{
			for (int i = 0; i < PolylineDataGridItem._Types_S.Length; i++)
			{
				exchanges.Add(new PolylineExportExchange(this, i, i));
			}
		}
	}

	public override string ToString()
	{
		return type switch
		{
			PolylineExportColumnType.X => "X-axis coordinate", 
			PolylineExportColumnType.Y => "Y-axis coordinate", 
			PolylineExportColumnType.Radius => "Radius", 
			PolylineExportColumnType.Type => "线条Type", 
			PolylineExportColumnType.CenterX => "The x-coordinate of the center of the circle", 
			PolylineExportColumnType.CenterY => "The y-coordinate of the center of the circle", 
			PolylineExportColumnType.User => userformat?.Name ?? "<null>", 
			_ => base.ToString(), 
		};
	}
}

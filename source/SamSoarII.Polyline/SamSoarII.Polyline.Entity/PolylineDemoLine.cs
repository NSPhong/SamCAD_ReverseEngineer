using System.Windows;

namespace SamSoarII.Polyline.Entity;

public class PolylineDemoLine : PolylineLine
{
	private Point from;

	public override Point From
	{
		get
		{
			return from;
		}
		set
		{
			from = value;
		}
	}

	public PolylineDemoLine()
	{
	}

	public PolylineDemoLine(IPolylineImage _image, int _id, Point _from, Point _to)
		: base(_image, _id, _to)
	{
		from = _from;
	}
}

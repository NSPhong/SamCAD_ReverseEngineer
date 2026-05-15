using System;

namespace SamSoarII.Polyline.Entity;

public class PolylineActionEventArgs : EventArgs
{
	private IPolylineImage image;

	private IPolylineAction action;

	public IPolylineImage Image => image;

	public IPolylineAction Action => action;

	public PolylineActionEventArgs(IPolylineImage _image, IPolylineAction _action)
	{
		image = _image;
		action = _action;
	}
}

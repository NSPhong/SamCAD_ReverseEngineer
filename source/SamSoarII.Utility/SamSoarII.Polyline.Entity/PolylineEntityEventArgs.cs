using System;

namespace SamSoarII.Polyline.Entity;

public class PolylineEntityEventArgs : EventArgs
{
	private IPolylineEntity entity;

	public IPolylineEntity Entity => entity;

	public PolylineEntityEventArgs(IPolylineEntity _entity)
	{
		entity = _entity;
	}
}

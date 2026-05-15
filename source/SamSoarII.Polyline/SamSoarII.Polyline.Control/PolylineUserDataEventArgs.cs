using System;
using System.Collections.Generic;
using System.Linq;
using SamSoarII.Polyline.Entity.User;

namespace SamSoarII.Polyline.Control;

public class PolylineUserDataEventArgs : EventArgs
{
	private IList<IPolylineUserFormat> targets;

	public IList<IPolylineUserFormat> Targets => targets;

	public PolylineUserDataEventArgs(IEnumerable<IPolylineUserFormat> _targets)
	{
		targets = _targets.ToArray();
	}
}

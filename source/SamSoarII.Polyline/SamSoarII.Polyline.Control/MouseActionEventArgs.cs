using System;

namespace SamSoarII.Polyline.Control;

public class MouseActionEventArgs : EventArgs
{
	private MouseStatus status;

	public MouseStatus Status => status;

	public MouseActionEventArgs(MouseStatus _status)
	{
		status = _status;
	}
}

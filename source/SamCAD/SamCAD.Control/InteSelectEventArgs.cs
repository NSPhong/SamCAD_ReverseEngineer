using System;

namespace SamCAD.Control;

public class InteSelectEventArgs : EventArgs
{
	private Enum_InteSelectEvent ev;

	public Enum_InteSelectEvent Event => ev;

	public InteSelectEventArgs(Enum_InteSelectEvent _ev)
	{
		ev = _ev;
	}
}

using System;

namespace SamSoarII.Polyline.Control.TreeView;

public class ReorderEventArgs : EventArgs
{
	private ReorderCommands command;

	public ReorderCommands Command => command;

	public ReorderEventArgs(ReorderCommands _command)
	{
		command = _command;
	}
}

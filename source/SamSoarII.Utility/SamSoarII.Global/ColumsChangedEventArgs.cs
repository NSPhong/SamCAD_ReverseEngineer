using System;

namespace SamSoarII.Global;

public class ColumsChangedEventArgs : EventArgs
{
	private int delta;

	private bool isUndo;

	public int Delta => delta;

	public bool IsUndo => isUndo;

	public ColumsChangedEventArgs(int delta, bool isUndo = false)
	{
		this.delta = delta;
		this.isUndo = isUndo;
	}
}

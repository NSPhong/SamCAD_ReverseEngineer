using System;

namespace SamSoarII.Core.Models;

public class LadderDiagramArgumentExchangeEventArgs : EventArgs
{
	private int start;

	private int count;

	public int Start => start;

	public int Count => count;

	public LadderDiagramArgumentExchangeEventArgs(int _start, int _count)
	{
		start = _start;
		count = _count;
	}
}

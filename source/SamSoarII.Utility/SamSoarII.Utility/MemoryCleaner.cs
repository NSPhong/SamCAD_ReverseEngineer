using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace SamSoarII.Utility;

public class MemoryCleaner
{
	private int timespan;

	private byte tick = 0;

	private Timer timer;

	public int TimeSpan
	{
		get
		{
			return timespan;
		}
		set
		{
			timespan = value;
			timer.Change(0, timespan);
		}
	}

	[DllImport("Kernel32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool SetProcessWorkingSetSize(IntPtr hProcess, int lpMinimumWorkingSetSize, int lpMaximumWorkingSetSize);

	public MemoryCleaner(int _timespan = 500)
	{
		timespan = System.Math.Max(_timespan, 200);
		tick = 0;
		timer = new Timer(_Action, null, 0, timespan);
	}

	private void _Action(object obj)
	{
		if (tick == 0)
		{
		}
		int num = 0;
		int num2 = tick;
		while ((num2 & 1) != 0)
		{
			num++;
			num2 >>= 1;
		}
		GC.Collect(num, GCCollectionMode.Forced);
		tick++;
	}
}

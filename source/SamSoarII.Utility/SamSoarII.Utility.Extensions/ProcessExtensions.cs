using System;
using System.Diagnostics;

namespace SamSoarII.Utility.Extensions;

public static class ProcessExtensions
{
	public static Process GetCurrentParent()
	{
		return GetParent(Process.GetCurrentProcess());
	}

	public static Process GetParent(Process proc)
	{
		string indexedName = GetIndexedName(proc);
		if (indexedName == null)
		{
			return null;
		}
		return GetParentFromIndexedName(indexedName);
	}

	public static string GetIndexedName(Process proc)
	{
		int id = proc.Id;
		string processName = proc.ProcessName;
		string text = null;
		try
		{
			Process[] processesByName = Process.GetProcessesByName(processName);
			for (int i = 0; i < processesByName.Length; i++)
			{
				text = ((i == 0) ? processName : $"{processName}#{i}");
				PerformanceCounter performanceCounter = new PerformanceCounter("Process", "ID Process", text);
				int num = (int)performanceCounter.NextValue();
				if (num == id)
				{
					return text;
				}
			}
		}
		catch (Exception)
		{
		}
		return text;
	}

	public static Process GetParentFromIndexedName(string piname)
	{
		try
		{
			PerformanceCounter performanceCounter = new PerformanceCounter("Process", "Creating Process ID", piname);
			int processId = (int)performanceCounter.NextValue();
			return Process.GetProcessById(processId);
		}
		catch (Exception)
		{
			return null;
		}
	}
}

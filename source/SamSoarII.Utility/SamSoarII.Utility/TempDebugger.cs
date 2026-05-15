using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace SamSoarII.Utility;

public class TempDebugger
{
	private static Random random;

	public static string DebugPath;

	private static StreamWriter writer;

	private static object lockobject;

	private static Dictionary<Thread, StreamWriter> branchwriters;

	static TempDebugger()
	{
		random = new Random();
		DebugPath = FileHelper.AppRootPath + $"\\Temp\\Debug_{(ushort)(random.Next() % 26) + 97}{(ushort)(random.Next() % 26) + 97}{(ushort)(random.Next() % 26) + 97}{(ushort)(random.Next() % 26) + 97}.txt";
		lockobject = new object();
		branchwriters = new Dictionary<Thread, StreamWriter>();
		if (!Directory.Exists(FileHelper.AppRootPath + "\\Temp"))
		{
			Directory.CreateDirectory(FileHelper.AppRootPath + "\\Temp");
		}
		if (!File.Exists(DebugPath))
		{
			writer = new StreamWriter(File.Create(DebugPath));
		}
		else
		{
			writer = new StreamWriter(DebugPath, append: true);
		}
	}

	public static void WriteLineWithDate(string message)
	{
		WriteLine($"[{DateTime.Now.ToShortDateString()}:{DateTime.Now.ToShortTimeString()}:{DateTime.Now.Second}:{DateTime.Now.Millisecond}]{message}");
	}

	public static void WriteLine(string message)
	{
		StreamWriter value = writer;
		lock (branchwriters)
		{
			if (!branchwriters.TryGetValue(Thread.CurrentThread, out value))
			{
				value = writer;
			}
		}
		lock (value)
		{
			value.WriteLine(message);
			value.Flush();
		}
	}

	public static void WriteLine(object obj)
	{
		WriteLine(obj.ToString());
	}

	public static void WriteLine()
	{
		StreamWriter value = writer;
		lock (branchwriters)
		{
			if (!branchwriters.TryGetValue(Thread.CurrentThread, out value))
			{
				value = writer;
			}
		}
		lock (value)
		{
			value.WriteLine();
			value.Flush();
		}
	}

	public static void Close()
	{
		writer.Close();
	}

	public static void Dispose()
	{
		writer.Close();
		writer.Dispose();
	}

	public static void CreateBranch(string basename)
	{
		string text = null;
		do
		{
			text = FileHelper.AppRootPath + $"\\Temp\\{basename}_{(ushort)(random.Next() % 26) + 97}{(ushort)(random.Next() % 26) + 97}{(ushort)(random.Next() % 26) + 97}{(ushort)(random.Next() % 26) + 97}.txt";
		}
		while (File.Exists(text));
		lock (branchwriters)
		{
			if (!branchwriters.ContainsKey(Thread.CurrentThread))
			{
				StreamWriter value = new StreamWriter(text, append: true);
				branchwriters.Add(Thread.CurrentThread, value);
			}
		}
	}

	public static void CloseBranch()
	{
		lock (branchwriters)
		{
			if (branchwriters.ContainsKey(Thread.CurrentThread))
			{
				branchwriters[Thread.CurrentThread]?.Close();
				branchwriters.Remove(Thread.CurrentThread);
			}
		}
	}
}

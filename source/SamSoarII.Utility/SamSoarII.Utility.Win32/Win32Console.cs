using System;
using System.Runtime.InteropServices;
using System.Text;

namespace SamSoarII.Utility.Win32;

public class Win32Console : IDisposable
{
	[DllImport("kernel32.dll")]
	public static extern bool AllocConsole();

	[DllImport("kernel32.dll")]
	public static extern bool FreeConsole();

	public void Dispose()
	{
	}

	public void Write(string text)
	{
		Console.Write(text);
	}

	public void WriteLine(string text)
	{
		Console.WriteLine(text);
	}

	public void Write(string format, params object[] args)
	{
		Console.Write(format, args);
	}

	public void WriteLine(string format, params object[] args)
	{
		Console.WriteLine(format, args);
	}

	public void WriteDate(string text)
	{
		Console.WriteLine($"[{DateTime.Now.ToShortDateString()}:{DateTime.Now.ToShortTimeString()}:{DateTime.Now.Second}:{DateTime.Now.Millisecond}]{text}");
	}

	public void WriteComm(string text, byte[] data, int row0, int row1)
	{
		StringBuilder stringBuilder = new StringBuilder();
		int num = 0;
		int num2 = 0;
		stringBuilder.Append(text);
		foreach (byte b in data)
		{
			stringBuilder.Append($"{b:X2}");
			if (++num >= ((num2 == 0) ? row0 : row1))
			{
				num = 0;
				num2++;
				stringBuilder.Append("\n");
			}
			else
			{
				stringBuilder.Append(" ");
			}
		}
		WriteDate(stringBuilder.ToString());
	}

	public void WriteCommIn(byte[] data)
	{
		WriteComm("IN  :", data, 16, 20);
	}

	public void WriteCommOut(byte[] data)
	{
		WriteComm("OUT :", data, 16, 20);
	}
}

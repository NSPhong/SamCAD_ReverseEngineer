using System;
using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public sealed class HSCSTableHeader : BaseFileHeader
{
	[FieldOffset(0)]
	public int dwHeaderSize;

	[FieldOffset(4)]
	public int dwNetworkID;

	[FieldOffset(8)]
	public ushort wX;

	[FieldOffset(10)]
	public ushort wY;

	[FieldOffset(12)]
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
	public int[] spArgs = new int[1];

	public override int HeaderSize
	{
		get
		{
			return dwHeaderSize;
		}
		set
		{
			dwHeaderSize = value;
			spArgs = new int[ArgsCount];
		}
	}

	public override FileHeaderTypes HeaderType => FileHeaderTypes.HSCSTable;

	public int ArgsCount => Math.Max(1, (HeaderSize - 12) / 4);

	public override IFileHeader Create()
	{
		return new HSCSTableHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		if (id >= 0 && id < ArgsCount)
		{
			spArgs[id] = sp;
		}
		else
		{
			base.SetStrPtr(id, sp);
		}
	}

	public override void Save(IntPtr intptr)
	{
		base.Save(intptr);
		Marshal.Copy(spArgs, 0, intptr + 12, spArgs.Length);
	}

	public override void Load(IntPtr intptr)
	{
		base.Load(intptr);
		spArgs = new int[ArgsCount];
		Marshal.Copy(intptr + 12, spArgs, 0, spArgs.Length);
	}
}

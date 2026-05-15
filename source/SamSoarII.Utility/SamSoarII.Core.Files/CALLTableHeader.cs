using System;
using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public sealed class CALLTableHeader : BaseFileHeader
{
	[FieldOffset(0)]
	public ushort wHeaderSize;

	[FieldOffset(2)]
	public ushort wNetworkID;

	[FieldOffset(4)]
	public ushort wX;

	[FieldOffset(6)]
	public ushort wY;

	[FieldOffset(8)]
	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
	public int[] spArgs = new int[1];

	public override int HeaderSize
	{
		get
		{
			return wHeaderSize;
		}
		set
		{
			wHeaderSize = (ushort)value;
			spArgs = new int[ArgsCount];
		}
	}

	public override FileHeaderTypes HeaderType => FileHeaderTypes.CALLTable;

	public int ArgsCount => Math.Max(1, (HeaderSize - 8) / 4);

	public override IFileHeader Create()
	{
		return new CALLTableHeader();
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
		Marshal.Copy(spArgs, 0, intptr + 8, spArgs.Length);
	}

	public override void Load(IntPtr intptr)
	{
		base.Load(intptr);
		spArgs = new int[ArgsCount];
		Marshal.Copy(intptr + 8, spArgs, 0, spArgs.Length);
	}
}

using System;
using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public sealed class ValueBoardHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int lpReserved;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
	public int[] spValue = new int[1];

	public override int HeaderSize
	{
		get
		{
			return dwHeaderSize;
		}
		set
		{
			dwHeaderSize = value;
			spValue = new int[(dwHeaderSize - 8 - 1) / 4 + 1];
		}
	}

	public override FileHeaderTypes HeaderType => FileHeaderTypes.ValueBoard;

	public override IFileHeader Create()
	{
		return new ValueBoardHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		if (id >= 0 && id < spValue.Length)
		{
			spValue[id] = sp;
		}
		else
		{
			base.SetStrPtr(id, sp);
		}
	}

	public override void Save(IntPtr intptr)
	{
		base.Save(intptr);
		Marshal.Copy(spValue, 0, intptr + 8, spValue.Length);
	}

	public override void Load(IntPtr intptr)
	{
		base.Load(intptr);
		spValue = new int[(dwHeaderSize - 8 - 1) / 4 + 1];
		Marshal.Copy(intptr + 8, spValue, 0, spValue.Length);
	}
}

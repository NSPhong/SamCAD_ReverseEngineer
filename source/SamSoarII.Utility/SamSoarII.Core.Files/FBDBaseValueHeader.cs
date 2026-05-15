using System;
using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class FBDBaseValueHeader : BaseFileHeader
{
	public int dwHeaderSize;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
	public int[] spValues = new int[1];

	public override int HeaderSize
	{
		get
		{
			return dwHeaderSize;
		}
		set
		{
			dwHeaderSize = value;
			spValues = new int[Math.Max(1, (value - 5) / 4 + 1)];
		}
	}

	public override FileHeaderTypes HeaderType => FileHeaderTypes.FBDBaseValue;

	public override IFileHeader Create()
	{
		return new FBDBaseValueHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		spValues[id] = sp;
	}

	public override void Save(IntPtr intptr)
	{
		base.Save(intptr);
		if (HeaderSize > 4)
		{
			Marshal.Copy(spValues, 0, intptr + 4, spValues.Length);
		}
	}

	public override void Load(IntPtr intptr)
	{
		base.Load(intptr);
		if (HeaderSize > 4)
		{
			spValues = new int[Math.Max(1, (HeaderSize - 5) / 4 + 1)];
			Marshal.Copy(intptr + 4, spValues, 0, spValues.Length);
		}
	}
}

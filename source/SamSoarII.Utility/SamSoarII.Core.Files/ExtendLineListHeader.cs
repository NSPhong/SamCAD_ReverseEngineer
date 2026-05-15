using System;
using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ExtendLineListHeader : BaseFileHeader
{
	public int dwHeaderSize;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
	public int[] dwData;

	public override int HeaderSize
	{
		get
		{
			return dwHeaderSize;
		}
		set
		{
			dwHeaderSize = value;
			dwData = new int[(dwHeaderSize - 4 - 1) / 4 + 1];
		}
	}

	public override FileHeaderTypes HeaderType => FileHeaderTypes.ExtendLineList;

	public override IFileHeader Create()
	{
		return new ExtendLineListHeader();
	}

	public override void Load(IntPtr intptr)
	{
		base.Load(intptr);
		dwData = new int[(dwHeaderSize - 4 - 1) / 4 + 1];
		Marshal.Copy(intptr + 4, dwData, 0, dwData.Length);
	}

	public override void Save(IntPtr intptr)
	{
		base.Save(intptr);
		Marshal.Copy(dwData, 0, intptr + 4, dwData.Length);
	}
}

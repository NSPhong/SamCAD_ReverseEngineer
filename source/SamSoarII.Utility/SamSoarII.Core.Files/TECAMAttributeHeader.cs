using System;
using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class TECAMAttributeHeader : BaseFileHeader
{
	[MarshalAs(UnmanagedType.I4)]
	public int dwHeaderSize;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
	public int[] dwData = new int[1];

	public override int HeaderSize
	{
		get
		{
			return dwHeaderSize;
		}
		set
		{
			dwHeaderSize = value;
			dwData = new int[(value - 4 - 1) / 4 + 1];
		}
	}

	public override FileHeaderTypes HeaderType => FileHeaderTypes.TECAMAttributeParams;

	public override IFileHeader Create()
	{
		return new TECAMAttributeHeader();
	}

	public override void Save(IntPtr intptr)
	{
		base.Save(intptr);
		Marshal.Copy(dwData, 0, intptr + 4, dwData.Length);
	}

	public override void Load(IntPtr intptr)
	{
		base.Load(intptr);
		dwData = new int[(dwHeaderSize - 4 - 1) / 4 + 1];
		Marshal.Copy(intptr + 4, dwData, 0, dwData.Length);
	}
}

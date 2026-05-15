using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class TBLHeader : BaseFileHeader
{
	[MarshalAs(UnmanagedType.I4)]
	public int dwHeaderSize;

	[MarshalAs(UnmanagedType.I1)]
	public byte bMode;

	[MarshalAs(UnmanagedType.I4)]
	public int dwItemCount;

	[MarshalAs(UnmanagedType.I4)]
	public int lpItem;

	public override int HeaderSize
	{
		get
		{
			return dwHeaderSize;
		}
		set
		{
			dwHeaderSize = value;
		}
	}

	public override FileHeaderTypes HeaderType => FileHeaderTypes.TBL;

	public override IFileHeader Create()
	{
		return new TBLHeader();
	}
}

using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class PasswordHeader : BaseFileHeader
{
	[FieldOffset(0)]
	[MarshalAs(UnmanagedType.I4)]
	public int dwHeaderSize;

	[FieldOffset(4)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bDownloadEnable;

	[FieldOffset(5)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bUploadEnable;

	[FieldOffset(6)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bMonitorEnable;

	[FieldOffset(7)]
	[MarshalAs(UnmanagedType.I1)]
	public byte bReserved1;

	[FieldOffset(8)]
	[MarshalAs(UnmanagedType.I4)]
	public int spDownload;

	[FieldOffset(12)]
	[MarshalAs(UnmanagedType.I4)]
	public int spUpload;

	[FieldOffset(16)]
	[MarshalAs(UnmanagedType.I4)]
	public int spMonitor;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.PasswordParams;

	public override IFileHeader Create()
	{
		return new PasswordHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 0:
			spUpload = sp;
			break;
		case 1:
			spDownload = sp;
			break;
		case 2:
			spMonitor = sp;
			break;
		}
	}
}

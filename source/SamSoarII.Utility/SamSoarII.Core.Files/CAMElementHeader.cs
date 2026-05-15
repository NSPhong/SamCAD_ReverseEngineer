using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class CAMElementHeader : BaseFileHeader
{
	[MarshalAs(UnmanagedType.I4)]
	public int dwHeaderSize;

	[MarshalAs(UnmanagedType.I4)]
	public int dwTarget;

	[MarshalAs(UnmanagedType.I4)]
	public int spAddress;

	[MarshalAs(UnmanagedType.I1)]
	public byte bMode;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.CAMElement;

	public override IFileHeader Create()
	{
		return new CAMElementHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		if (id == 0)
		{
			spAddress = sp;
		}
	}
}

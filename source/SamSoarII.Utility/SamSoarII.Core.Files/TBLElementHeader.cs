using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class TBLElementHeader : BaseFileHeader
{
	[MarshalAs(UnmanagedType.I4)]
	public int dwHeaderSize;

	[MarshalAs(UnmanagedType.I4)]
	public int dwFrequency;

	[MarshalAs(UnmanagedType.I4)]
	public int dwNumber;

	[MarshalAs(UnmanagedType.I1)]
	public byte bWaitEvent;

	[MarshalAs(UnmanagedType.I4)]
	public int spCondition;

	[MarshalAs(UnmanagedType.I4)]
	public int dwJump;

	[MarshalAs(UnmanagedType.I4)]
	public int spEnd;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.TBLElement;

	public override IFileHeader Create()
	{
		return new TBLElementHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 0:
			spCondition = sp;
			break;
		case 1:
			spEnd = sp;
			break;
		}
	}
}

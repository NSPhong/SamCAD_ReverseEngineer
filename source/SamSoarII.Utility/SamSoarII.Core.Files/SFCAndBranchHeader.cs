using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class SFCAndBranchHeader : BaseFileHeader
{
	[MarshalAs(UnmanagedType.I4)]
	public int dwHeaderSize;

	[MarshalAs(UnmanagedType.I2)]
	public short wX;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.SFCAndBranch;

	public override IFileHeader Create()
	{
		return new SFCAndBranchHeader();
	}
}

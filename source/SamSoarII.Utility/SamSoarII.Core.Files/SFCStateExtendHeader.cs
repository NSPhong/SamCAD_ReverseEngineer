using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class SFCStateExtendHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int dwStateID;

	public int dwOutputCount;

	public int lpOutput;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.SFCStateExtend;

	public override IFileHeader Create()
	{
		return new SFCStateExtendHeader();
	}
}

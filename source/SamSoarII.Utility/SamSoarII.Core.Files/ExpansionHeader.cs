using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ExpansionHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int dwIsEnabled;

	public int dwUnitCount;

	public int lpUnits;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.ExpansionParams;

	public override IFileHeader Create()
	{
		return new ExpansionHeader();
	}
}

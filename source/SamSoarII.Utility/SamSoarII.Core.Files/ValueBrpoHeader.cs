using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ValueBrpoHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int dwItemCount;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.ValueBrpo;

	public override IFileHeader Create()
	{
		return new ValueBrpoHeader();
	}
}

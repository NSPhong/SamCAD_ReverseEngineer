using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class POLYLINEFHeader : POLYLINEHeader
{
	public int dwItemCount;

	public int lpItem;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.POLYLINEF;

	public override IFileHeader Create()
	{
		return new POLYLINEFHeader();
	}
}

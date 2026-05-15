using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class POLYLINEIHeader : POLYLINEHeader
{
	public int dwItemCount;

	public int lpItem;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.POLYLINEI;

	public override IFileHeader Create()
	{
		return new POLYLINEIHeader();
	}
}

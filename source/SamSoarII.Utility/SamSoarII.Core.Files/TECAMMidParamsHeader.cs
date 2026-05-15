using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class TECAMMidParamsHeader : TECAMParamsHeader
{
	public int dwMidPointCount;

	public int lpMidPoints;

	public int dwMiddleIndex;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.TECAMParams_Mid;

	public override IFileHeader Create()
	{
		return new TECAMMidParamsHeader();
	}
}

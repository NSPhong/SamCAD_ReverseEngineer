using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class TECAMFollowParamsHeader : TECAMParamsHeader
{
	public float fXTotalLen;

	public float fXAcceLen;

	public float fXSyncLen;

	public float fXDeceLen;

	public float fXPToNLen;

	public float fXNToPLen;

	public float fYTotalLen;

	public int dwCuttingTime;

	public float fXPreWaitLen;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.TECAMParams_Follow;

	public override IFileHeader Create()
	{
		return new TECAMFollowParamsHeader();
	}
}

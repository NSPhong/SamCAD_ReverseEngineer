using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class TECAMFreeKeyPointHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int dwX;

	public int dwY;

	public float fK;

	public int dwLineType;

	public int dwResolution;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.TECAMParams_Free_KeyPoint;

	public override IFileHeader Create()
	{
		return new TECAMFreeKeyPointHeader();
	}
}

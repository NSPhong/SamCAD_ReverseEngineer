using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public sealed class TECAMMidPointHeader : BaseFileHeader
{
	public int dwX;

	public int dwY;

	public override int HeaderSize
	{
		get
		{
			return 8;
		}
		set
		{
		}
	}

	public override FileHeaderTypes HeaderType => FileHeaderTypes.TECAMParams_Mid_MidPoint;

	public override IFileHeader Create()
	{
		return new TECAMMidPointHeader();
	}
}

using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class ProjectParamsHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int lpCom232;

	public int lpCom485;

	public int lpUsb;

	public int lpPassword;

	public int lpHolding;

	public int lpAnalog;

	public int lpExpansion;

	public int lpFilter;

	public int lpOther;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.ProjectParams;

	public override IFileHeader Create()
	{
		return new ProjectParamsHeader();
	}
}

using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public abstract class TECAMParamsHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int dwHeaderType;

	public int dwXRoundPls;

	public float fXRoundMov;

	public int dwYRoundPls;

	public float fYRoundMov;

	public int dwCurveResolution;

	public int dwOrder;

	public int dwYLimitedVelocity;

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
}

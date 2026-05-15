using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class AIOAnalogParamsHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int dwAICount;

	public int lpAI;

	public int dwAOCount;

	public int lpAO;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.AIOAnalogParams;

	public override IFileHeader Create()
	{
		return new AIOAnalogParamsHeader();
	}
}

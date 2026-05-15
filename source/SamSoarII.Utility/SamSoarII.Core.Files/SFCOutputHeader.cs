using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class SFCOutputHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int spCondition;

	public int spOutput;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.SFCOutput;

	public override IFileHeader Create()
	{
		return new SFCOutputHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 0:
			spCondition = sp;
			break;
		case 1:
			spOutput = sp;
			break;
		default:
			base.SetStrPtr(id, sp);
			break;
		}
	}
}

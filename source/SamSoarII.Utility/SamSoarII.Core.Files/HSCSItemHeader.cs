using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class HSCSItemHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int spTarget;

	public int spCallName;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.HSCSItem;

	public override IFileHeader Create()
	{
		return new HSCSItemHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 0:
			spTarget = sp;
			break;
		case 1:
			spCallName = sp;
			break;
		default:
			base.SetStrPtr(id, sp);
			break;
		}
	}
}

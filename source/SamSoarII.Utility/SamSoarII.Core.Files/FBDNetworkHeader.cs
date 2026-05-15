using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class FBDNetworkHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int spComment;

	public int dwWidth;

	public int dwHeight;

	public int dwUnitCount;

	public int lpUnits;

	public int lpBaseValue;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.FBDNetwork;

	public override IFileHeader Create()
	{
		return new FBDNetworkHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		if (id == 0)
		{
			spComment = sp;
		}
		else
		{
			base.SetStrPtr(id, sp);
		}
	}
}

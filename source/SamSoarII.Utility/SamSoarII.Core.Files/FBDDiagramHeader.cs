using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class FBDDiagramHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int spName;

	public int spComment;

	public int dwNetworkCount;

	public int lpNetworks;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.FBDDiagram;

	public override IFileHeader Create()
	{
		return new FBDDiagramHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 0:
			spName = sp;
			break;
		case 1:
			spComment = sp;
			break;
		default:
			base.SetStrPtr(id, sp);
			break;
		}
	}
}

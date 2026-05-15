using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class SFCTransformExtendHeader : BaseFileHeader
{
	public int dwHeaderSize;

	public int spExpr;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.SFCTransformExtend;

	public override IFileHeader Create()
	{
		return new SFCTransformExtendHeader();
	}

	public override void SetStrPtr(int id, int sp)
	{
		if (id == 0)
		{
			spExpr = sp;
		}
		else
		{
			base.SetStrPtr(id, sp);
		}
	}
}

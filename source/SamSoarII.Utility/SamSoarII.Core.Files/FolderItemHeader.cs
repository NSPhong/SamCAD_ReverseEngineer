using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Explicit)]
public class FolderItemHeader : BaseFileHeader
{
	[FieldOffset(0)]
	public byte bHeaderSize;

	[FieldOffset(1)]
	public byte bType;

	[FieldOffset(2)]
	public byte bFlag;

	[FieldOffset(3)]
	public byte bReserved;

	[FieldOffset(4)]
	public int spName;

	public override FileHeaderTypes HeaderType => FileHeaderTypes.FolderItem;

	public override int HeaderSize
	{
		get
		{
			return bHeaderSize;
		}
		set
		{
			bHeaderSize = (byte)value;
		}
	}

	public override IFileHeader Create()
	{
		return new FolderItemHeader();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 3,
			dwVersionModify = 22
		};
	}

	public override void SetStrPtr(int id, int sp)
	{
		if (id == 0)
		{
			spName = sp;
		}
		else
		{
			base.SetStrPtr(id, sp);
		}
	}
}

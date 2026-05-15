using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public class FolderHeader_Version_2_2_46 : BaseFileHeader
{
	public int dwHeaderSize;

	public int dwFolderType;

	public int spPath;

	public int spPassword;

	public int dwPasswordEnable;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.Folder;

	public override IFileHeader Create()
	{
		return new FolderHeader_Version_2_2_46();
	}

	public override FileVersion MinorSupportedVersion()
	{
		return new FileVersion
		{
			dwVersionMain = 2,
			dwVersionSub = 2,
			dwVersionModify = 46
		};
	}

	public override void SetStrPtr(int id, int sp)
	{
		switch (id)
		{
		case 0:
			spPath = sp;
			break;
		case 1:
			spPassword = sp;
			break;
		}
	}
}

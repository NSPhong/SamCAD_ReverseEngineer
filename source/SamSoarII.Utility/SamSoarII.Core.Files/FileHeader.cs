using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public sealed class FileHeader : BaseFileHeader
{
	public const int FLAG_PASSWORD_NONE = 0;

	public const int FLAG_PASSWORD_ISENABLED = 1;

	public const int FLAG_PASSWORD_CANREADWITHOUTACCEPT = 2;

	public const int FLAG_PASSWORD_CANWRITEWITHOUTACCEPT = 4;

	public const int FLAG_DIAGRAM_NONE = 0;

	public const int FLAG_DIAGRAM_ISMAIN = 1;

	public const int FLAG_DIAGRAM_ISEXPAND = 2;

	public const int FLAG_DIAGRAM_USEDBCD = 4;

	public const int FLAG_NETWORK_NONE = 0;

	public const int FLAG_NETWORK_ISMASKED = 1;

	public const int FLAG_NETWORK_ISEXPAND = 2;

	public const int FLAG_NETWORK_ISBRIEFEXPAND = 4;

	public const int FLAG_FUNCBLOCK_NONE = 0;

	public const int FLAG_FUNCBLOCK_ISLIBRARY = 1;

	public const int FLAG_SFCLADDER_NONE = 0;

	public const int FLAG_SFCLADDER_ISCHECKED = 1;

	public const int FLAG_SFCLADDER_ISCOMPILED = 2;

	public const int FLAG_ZRNR_NONE = 0;

	public const int FLAG_ZRNR_ISPOSITIVEUSED = 1;

	public const int FLAG_ZRNR_ISNEGATIVEUSED = 2;

	public const int FLAG_ZRNR_ISZEROUSED = 4;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
	public byte[] szToken = new byte[16];

	public int dwHeaderSize;

	public FileVersion stFileVersion;

	public int lpProject;

	public int lpReserved;

	public int lpFormattedMemory = 0;

	public int lpFreeMemory;

	[MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
	public int[] lpTinyString = new int[16];

	public int lpFreeString;

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

	public override FileHeaderTypes HeaderType => FileHeaderTypes.File;

	public override IFileHeader Create()
	{
		return new FileHeader();
	}
}

namespace SamSoarII.Utility;

public class VersionInfo
{
	public static readonly VersionInfo[] VersionInfos;

	private Version version;

	private string[] updatemessages;

	private string[] updatemessages_en;

	public Version Version => version;

	public string[] UpdateMessages => updatemessages;

	public string[] UpdateMessages_EN => updatemessages_en;

	static VersionInfo()
	{
		VersionInfos = new VersionInfo[7]
		{
			new VersionInfo(new Version(2, 0, 0), new string[3] { "Starting from V2.0.0, the pulse output instructions uniformly use two-word integers as parameters. Projects that previously used single-word integer pulse instructions will be forcibly converted to the corresponding two-word integer pulse instructions. Please be sure to pay attention to the adjustments to these changes.", "Starting from V2.0.0, library functions have been changed and uniformly imported and used. For projects that use library functions from the previous generation, you can choose to retain the previous library functions or try using the new version.", "The program password, upload password, download password and monitoring password are all lost. Please reset them." }, new string[3] { "Since version v2.0.0，it only accepted DWORD (4-Bytes) pulse-instruction and abandoned the WORD (2-Bytes) one. Project with lower version (v1.x.x) and WORD (2-Bytes) pulse-instruction, will be forced to be converted to DWORD pulse-instruction corrosponded. Please notice the modification and adjust your project. ", "Since version v2.0.0, the library functions had been modified, and imported and applied uniformly. Project with lower version (v1.x.x) with library function, could choose to reserve the old library function, or try to apply the new one. ", "The passwords of program, download, upload and monitor will be lost, please set them again." }),
			new VersionInfo(new Version(2, 2, 46), new string[1] { "Starting from V2.2.46, a password attribute has been added to the internal folders of the project to protect the contents inside. Therefore, for safety reasons, software versions V2.2.45 and earlier cannot open files from V2.2.46 and later. Please notify us as soon as possible to update to the latest version." }, new string[1] { "Since version v2.2.46, the folder in project had been added the password attribute, to protected the content inside it.For the safety to do that the SamSoarII App with version no more than v2.2.45, cannot open the preject file with version no less than v2.2.46.Please notice to upgrade to the latest version at once." }),
			new VersionInfo(new Version(2, 2, 49), new string[1] { "Starting from V2.2.49, encryption protection and password protection have been enhanced within the project. Therefore, for safety reasons, software versions V2.2.48 and earlier cannot open files from V2.2.49 and later. Please notify us as soon as possible to update to the latest version." }, new string[1] { "Since version v2.2.49, it had been enforced the encryption and password system to protect the project.For the safety to do that the SamSoarII App with version no more than v2.2.48, cannot open the preject file with version no less than v2.2.49.Please notice to upgrade to the latest version at once." }),
			new VersionInfo(new Version(2, 2, 54), new string[1] { "Starting from V2.2.54, the four Modbus function codes 0x03 for reading double characters, 0x04 for reading double characters, 0x06 for writing double characters, and 0x10 for writing multiple double characters have been removed. The original engineering files with two-character function codes will be automatically converted into the corresponding single-character function codes." }, new string[1] { "Since version v2.2.54, the MODBUS handle codes : 0x03 read DWORD, 0x04 read DWORD, 0x06 write DWORD, 0x10 write multiple DWORD, which have been canceled.The DWORD handle codes in project of early version, will be automatically transformed to the corrosponding WORD handle codes. " }),
			new VersionInfo(new Version(2, 3, 2), new string[1] { "Starting from V2.3.2, ZRN and ZRNR have added the parameters of acceleration time and deceleration time. For the project that originally used these two instructions, these two parameters are in the default state when opened. Please fill them in as soon as possible." }, new string[1] { "Since version v2.3.2, the ZRN and ZRNR instruction had been added 'Acc time' argument and 'Slow time' argument.The project file with earlier version which contains ZRN and ZRNR instructions, the 'Acc time' and 'Slow time' arguments will be missed defaultly, please finish them immediately." }),
			new VersionInfo(new Version(2, 3, 22), new string[2] { "Starting from V2.3.22, automatic sorting is no longer available in the Project Explorer. You can drag to adjust the order.", "Moreover, the number of expansion modules has been expanded to 16 (only valid for new hardware). When the old version opens the file saved by the current version, the data of the newly added expansion modules later will be lost." }, new string[1] { "Since version v2.3.22, the project mananger will never be sorted automatically, instead to drag item to manage order." }),
			new VersionInfo(new Version(2, 3, 23), new string[1] { "Starting from V2.3.23, the DVIT command has undergone significant changes, allowing for the setting of arbitrary interrupt signals (X/Y/M) and enable signals (X/Y/M)." }, new string[1] { "Since version v2.3.23, there had been modified the DVIT instruction, it is possible to set any interruption singal (X/Y/M) and enable signal (X/Y/M)." })
		};
	}

	public VersionInfo(Version _version, string[] _updatemessages, string[] _updatemessages_en)
	{
		version = _version;
		updatemessages = _updatemessages;
		updatemessages_en = _updatemessages_en;
	}
}

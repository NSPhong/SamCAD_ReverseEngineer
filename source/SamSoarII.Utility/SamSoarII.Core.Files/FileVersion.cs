using System;
using SamSoarII.Utility;

namespace SamSoarII.Core.Files;

public struct FileVersion : IComparable<FileVersion>
{
	public int dwVersionMain;

	public int dwVersionSub;

	public int dwVersionModify;

	public int CompareTo(FileVersion other)
	{
		int num = 0;
		num = dwVersionMain.CompareTo(other.dwVersionMain);
		if (num != 0)
		{
			return num;
		}
		num = dwVersionSub.CompareTo(other.dwVersionSub);
		if (num != 0)
		{
			return num;
		}
		num = dwVersionModify.CompareTo(other.dwVersionModify);
		if (num != 0)
		{
			return num;
		}
		return 0;
	}

	public SamSoarII.Utility.Version ToAppVersion()
	{
		return new SamSoarII.Utility.Version
		{
			Ver_Main = dwVersionMain,
			Ver_Sub = dwVersionSub,
			Ver_Modify = dwVersionModify
		};
	}
}

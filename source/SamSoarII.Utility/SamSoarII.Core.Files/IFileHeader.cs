using System;

namespace SamSoarII.Core.Files;

public interface IFileHeader
{
	int HeaderSize { get; set; }

	int MaxHeaderSize { get; }

	FileHeaderTypes HeaderType { get; }

	void SetStrPtr(int id, int sp);

	void SetFreePtr(int id, int lp);

	void Save(IntPtr intptr);

	void Load(IntPtr intptr);

	IFileHeader Create();

	FileVersion MinorSupportedVersion();
}

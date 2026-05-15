using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace SamSoarII.Core.Files;

[StructLayout(LayoutKind.Sequential)]
public sealed class FileHeaderEnumerable : BaseFileHeader, IEnumerable<IFileHeader>, IEnumerable
{
	public int dwCount;

	public int lpHeaders;

	public override int HeaderSize
	{
		get
		{
			return 8;
		}
		set
		{
		}
	}

	public override FileHeaderTypes HeaderType => FileHeaderTypes.FileHeaderEnumerable;

	public override IFileHeader Create()
	{
		return new FileHeaderEnumerable();
	}

	public IEnumerator<IFileHeader> GetEnumerator()
	{
		int lpHeader = lpHeaders;
		for (int i = 0; i < dwCount; i++)
		{
			TypeHeader hdTypeLP = (TypeHeader)FileFormat.GetHeader(lpHeader, FileHeaderTypes.TypeLP);
			yield return hdTypeLP.ToHeader();
			lpHeader += hdTypeLP.HeaderSize;
		}
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}
}

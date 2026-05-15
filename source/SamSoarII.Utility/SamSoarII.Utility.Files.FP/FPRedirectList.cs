using System.Collections.Generic;

namespace SamSoarII.Utility.Files.FP;

public class FPRedirectList : List<FPRedirectItem>
{
	public FPRedirectList(FPDataStream stream)
	{
		byte[] array = new byte[3];
		while (stream.Offset + 2 < stream.Data.Length)
		{
			stream.ReadBytes(array, 0, 3);
			int num = (array[0] << 16) + (array[1] << 8) + array[2];
			int offset = (int)stream.ReadU64();
			if (num == 0)
			{
				break;
			}
			FPRedirectItem item = new FPRedirectItem(num, offset);
			Add(item);
		}
	}
}

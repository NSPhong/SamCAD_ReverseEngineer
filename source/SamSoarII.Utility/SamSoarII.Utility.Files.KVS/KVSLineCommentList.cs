using System.Collections.Generic;
using System.Text;

namespace SamSoarII.Utility.Files.KVS;

public class KVSLineCommentList : List<KVSLineComment>
{
	public unsafe KVSLineCommentList(KVSFileStream s)
	{
		uint num = s.ReadUInt();
		uint num2 = s.ReadUInt();
		s.Skip(32);
		for (int i = 0; i < num2; i++)
		{
			uint num3 = s.ReadUInt();
			int index = (int)(s.Index + num3 - 4);
			KVSLineComment kVSLineComment = new KVSLineComment
			{
				Y = (int)s.ReadUInt()
			};
			fixed (byte* ptr = &s.Data[s.Index])
			{
				kVSLineComment.Comment = ValueConverter.ToString((sbyte*)ptr, (int)(num3 - 8), Encoding.Default);
			}
			Add(kVSLineComment);
			s.Index = index;
		}
	}
}

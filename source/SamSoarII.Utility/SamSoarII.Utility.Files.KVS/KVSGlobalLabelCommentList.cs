using System.Collections.Generic;
using System.Text;

namespace SamSoarII.Utility.Files.KVS;

public class KVSGlobalLabelCommentList : List<KVSGlobalLabelComment>
{
	public unsafe KVSGlobalLabelCommentList(KVSFileStream s)
	{
		uint num = s.ReadUInt();
		uint num2 = s.ReadUInt();
		s.Skip(32);
		for (int i = 0; i < num2; i++)
		{
			uint num3 = s.ReadUInt();
			int index = (int)(s.Index + num3 - 4);
			KVSGlobalLabelComment kVSGlobalLabelComment = new KVSGlobalLabelComment();
			fixed (byte* ptr = &s.Data[s.Index])
			{
				kVSGlobalLabelComment.Comment = ValueConverter.ToString((sbyte*)ptr, (int)(num3 - 4), Encoding.Default);
			}
			Add(kVSGlobalLabelComment);
			s.Index = index;
		}
	}
}

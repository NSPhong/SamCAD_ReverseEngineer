using System.Collections.Generic;
using System.Text;

namespace SamSoarII.Utility.Files.KVS;

public class KVSLocalLabelCommentList : List<KVSLocalLabelComment>
{
	public unsafe KVSLocalLabelCommentList(KVSFileStream s)
	{
		uint num = s.ReadUInt();
		uint num2 = s.ReadUInt();
		s.Skip(32);
		for (int i = 0; i < num2; i++)
		{
			uint num3 = s.ReadUInt();
			int index = (int)(s.Index + num3 - 4);
			KVSLocalLabelComment kVSLocalLabelComment = new KVSLocalLabelComment();
			fixed (byte* ptr = &s.Data[s.Index])
			{
				kVSLocalLabelComment.Comment = ValueConverter.ToString((sbyte*)ptr, (int)(num3 - 4), Encoding.Default);
			}
			Add(kVSLocalLabelComment);
			s.Index = index;
		}
	}
}

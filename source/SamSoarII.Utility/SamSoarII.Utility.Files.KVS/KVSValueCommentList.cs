using System.Collections.Generic;
using System.Text;

namespace SamSoarII.Utility.Files.KVS;

public class KVSValueCommentList : List<KVSValueComment>
{
	protected ushort code;

	public ushort Code
	{
		get
		{
			return code;
		}
		set
		{
			code = value;
		}
	}

	public ushort ElementCode => code switch
	{
		160 => 0, 
		161 => 6, 
		162 => 2, 
		163 => 3, 
		164 => 8, 
		165 => 17, 
		166 => 18, 
		169 => 1, 
		170 => 7, 
		171 => 5, 
		172 => 4, 
		174 => 27, 
		175 => 28, 
		167 => 23, 
		168 => 25, 
		_ => 0, 
	};

	public unsafe KVSValueCommentList(KVSFileStream s)
	{
		uint num = s.ReadUInt();
		uint num2 = s.ReadUInt();
		s.Skip(32);
		for (int i = 0; i < num2; i++)
		{
			uint num3 = s.ReadUInt();
			int index = (int)(s.Index + num3 - 4);
			KVSValueComment kVSValueComment = new KVSValueComment
			{
				Parent = this
			};
			kVSValueComment.Offset = (int)s.ReadUInt();
			fixed (byte* ptr = &s.Data[s.Index])
			{
				kVSValueComment.Comment = ValueConverter.ToString((sbyte*)ptr, (int)(num3 - 8), Encoding.Default);
			}
			Add(kVSValueComment);
			s.Index = index;
		}
	}
}

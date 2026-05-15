using System.Collections.Generic;
using System.Text;

namespace SamSoarII.Utility.Files.KVS;

public class KVSGlobalLabelList : List<KVSGlobalLabel>
{
	public unsafe KVSGlobalLabelList(KVSFileStream s)
	{
		uint num = s.ReadUInt();
		uint num2 = s.ReadUInt();
		s.Skip(32);
		for (int i = 0; i < num2; i++)
		{
			uint num3 = s.ReadUInt();
			int index = (int)(s.Index + num3 - 4);
			KVSGlobalLabel kVSGlobalLabel = new KVSGlobalLabel
			{
				ValueType = s.ReadUShort(),
				Offset = s.ReadUInt(),
				IsArray = s.ReadUShort(),
				ArrayCount = s.ReadUInt()
			};
			s.Skip(12);
			fixed (byte* ptr = &s.Data[s.Index])
			{
				kVSGlobalLabel.Name = ValueConverter.ToString((sbyte*)ptr, 34, Encoding.Default);
			}
			s.Skip(34);
			kVSGlobalLabel.CMID = (int)s.ReadUInt();
			Add(kVSGlobalLabel);
			s.Index = index;
		}
	}
}

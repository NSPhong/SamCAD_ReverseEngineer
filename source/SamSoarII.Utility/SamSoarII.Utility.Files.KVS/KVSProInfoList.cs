using System.Collections.Generic;
using System.Text;

namespace SamSoarII.Utility.Files.KVS;

public class KVSProInfoList : List<KVSProInfo>
{
	public unsafe KVSProInfoList(KVSFileStream s)
	{
		uint num = s.ReadUInt();
		uint num2 = s.ReadUInt();
		s.Skip(32);
		for (int i = 0; i < num2; i++)
		{
			uint num3 = s.ReadUInt();
			int index = (int)(s.Index + num3 - 4);
			KVSProInfo kVSProInfo = new KVSProInfo
			{
				Device = s.ReadUShort()
			};
			s.Skip(96);
			fixed (byte* ptr = &s.Data[s.Index])
			{
				kVSProInfo.Name = ValueConverter.ToString((sbyte*)ptr, 34, Encoding.Default);
				kVSProInfo.Comment = ValueConverter.ToString((sbyte*)(ptr + 34), (int)(num3 - 34 - 96 - 4 - 2), Encoding.Default);
			}
			Add(kVSProInfo);
			s.Index = index;
		}
	}
}

using System.Collections.Generic;
using System.Text;

namespace SamSoarII.Utility.Files.KVS;

public class KVSModInfoList : List<KVSModInfo>
{
	public unsafe KVSModInfoList(KVSFileStream s)
	{
		uint num = s.ReadUInt();
		uint num2 = s.ReadUInt();
		s.Skip(32);
		for (int i = 0; i < num2; i++)
		{
			uint num3 = s.ReadUInt();
			int index = (int)(s.Index + num3 - 4);
			KVSModInfo kVSModInfo = new KVSModInfo
			{
				Code = s.ReadUShort()
			};
			fixed (byte* ptr = &s.Data[s.Index])
			{
				kVSModInfo.Name = ValueConverter.ToString((sbyte*)ptr, 26, Encoding.Default);
			}
			Add(kVSModInfo);
			s.Index = index;
		}
	}
}

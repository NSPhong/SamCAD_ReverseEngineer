using System.Collections.Generic;
using System.Text;

namespace SamSoarII.Utility.Files.KVS;

public class KVSVarList : List<KVSVar>
{
	public unsafe KVSVarList(KVSFileStream s)
	{
		uint num = s.ReadUInt();
		uint num2 = s.ReadUInt();
		s.Skip(32);
		for (int i = 0; i < num2; i++)
		{
			uint num3 = s.ReadUInt();
			int index = (int)(s.Index + num3 - 4);
			s.Skip(6);
			uint num4 = s.ReadUInt();
			s.Skip(18);
			for (int j = 0; j < num4; j++)
			{
				KVSVar kVSVar = new KVSVar
				{
					Code = s.ReadUShort(),
					Offset = s.ReadUShort(),
					ValueType = s.ReadUShort()
				};
				fixed (byte* ptr = &s.Data[s.Index])
				{
					kVSVar.Title = ValueConverter.ToString((sbyte*)ptr, 14, Encoding.Default);
					kVSVar.Description = ValueConverter.ToString((sbyte*)(ptr + 14), 26, Encoding.Default);
				}
				s.Skip(40);
			}
			s.Index = index;
		}
	}
}

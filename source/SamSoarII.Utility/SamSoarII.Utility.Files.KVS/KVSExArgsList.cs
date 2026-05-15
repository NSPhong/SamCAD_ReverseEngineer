using System.Collections.Generic;

namespace SamSoarII.Utility.Files.KVS;

public class KVSExArgsList : List<KVSExArgs>
{
	public KVSExArgsList(KVSFileStream s)
	{
		uint num = s.ReadUInt();
		uint num2 = s.ReadUInt();
		s.Skip(32);
		for (int i = 0; i < num2; i++)
		{
			uint num3 = s.ReadUInt();
			int index = (int)(s.Index + num3 - 4);
			KVSExArgs item = new KVSExArgs();
			s.Skip(12);
			ushort num4 = s.ReadUShort();
			for (int j = 0; j < num4; j++)
			{
				KVSArg kVSArg = new KVSArg(null)
				{
					Code = s.ReadUShort(),
					Offset = s.ReadULong()
				};
			}
			Add(item);
			s.Index = index;
		}
		Sort((KVSExArgs a1, KVSExArgs a2) => a1.ID.CompareTo(a2.ID));
	}
}

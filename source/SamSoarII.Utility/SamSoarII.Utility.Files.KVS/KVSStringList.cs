using System.Collections.Generic;
using System.Text;

namespace SamSoarII.Utility.Files.KVS;

public class KVSStringList : List<KVSString>
{
	public unsafe KVSStringList(KVSFileStream s)
	{
		uint num = s.ReadUInt();
		uint num2 = s.ReadUInt();
		s.Skip(32);
		for (int i = 0; i < num2; i++)
		{
			uint num3 = s.ReadUInt();
			int index = (int)(s.Index + num3 - 4);
			KVSString kVSString = new KVSString
			{
				ID = (int)s.ReadUInt()
			};
			if (s.EndOfStream)
			{
				break;
			}
			fixed (byte* value = &s.Data[s.Index])
			{
				kVSString.Text = new string((sbyte*)value, 0, (int)(num3 - 4), Encoding.Default);
			}
			Add(kVSString);
			s.Index = index;
		}
		Sort((KVSString a1, KVSString a2) => a1.ID.CompareTo(a2.ID));
	}
}

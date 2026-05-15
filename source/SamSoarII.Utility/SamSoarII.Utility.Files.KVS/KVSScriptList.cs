using System.Collections.Generic;
using System.Text;

namespace SamSoarII.Utility.Files.KVS;

public class KVSScriptList : List<KVSScript>
{
	public unsafe KVSScriptList(KVSFileStream s)
	{
		uint num = s.ReadUInt();
		uint num2 = s.ReadUInt();
		s.Skip(32);
		for (int i = 0; i < num2; i++)
		{
			uint num3 = s.ReadUInt();
			int index = (int)(s.Index + num3 - 4);
			KVSScript kVSScript = new KVSScript
			{
				Y = (int)s.ReadUInt()
			};
			s.Skip(64);
			fixed (byte* ptr = &s.Data[s.Index])
			{
				kVSScript.Text = ValueConverter.ToString((sbyte*)ptr, (int)(num3 - 68), Encoding.Default);
			}
			Add(kVSScript);
			s.Index = index;
		}
	}
}

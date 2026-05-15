using System.Collections.Generic;
using System.Text;

namespace SamSoarII.Utility.Files.KVS;

public class KVSBase
{
	private List<string> names;

	private List<string> comments;

	public IList<string> Names => names;

	public IList<string> Comments => comments;

	public unsafe KVSBase(KVSFileStream s)
	{
		names = new List<string>();
		comments = new List<string>();
		uint num = s.ReadUInt();
		uint num2 = s.ReadUInt();
		s.Skip(32);
		for (int i = 0; i < num2; i++)
		{
			uint num3 = s.ReadUInt();
			int index = (int)(s.Index + num3 - 4);
			s.Skip(8);
			fixed (byte* ptr = &s.Data[s.Index])
			{
				string item = ValueConverter.ToString((sbyte*)ptr, 26, Encoding.Default);
				string item2 = ValueConverter.ToString((sbyte*)(ptr + 26), 42, Encoding.Default);
				names.Add(item);
				comments.Add(item2);
			}
			s.Index = index;
		}
	}
}

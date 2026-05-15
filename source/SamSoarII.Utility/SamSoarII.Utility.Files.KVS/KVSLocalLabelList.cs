using System.Collections.Generic;
using System.Text;

namespace SamSoarII.Utility.Files.KVS;

public class KVSLocalLabelList : List<KVSLocalLabel>
{
	public unsafe KVSLocalLabelList(KVSFileStream s)
	{
		uint num = s.ReadUInt();
		uint num2 = s.ReadUInt();
		s.Skip(32);
		for (int i = 0; i < num2; i++)
		{
			uint num3 = s.ReadUInt();
			int index = (int)(s.Index + num3 - 4);
			KVSLocalLabel kVSLocalLabel = new KVSLocalLabel
			{
				ValueType = s.ReadUShort()
			};
			ushort num4 = s.ReadUShort();
			uint num5 = s.ReadUInt();
			kVSLocalLabel.IsArray = s.ReadUShort();
			kVSLocalLabel.ArrayCount = s.ReadUInt();
			s.Skip(12);
			fixed (byte* ptr = &s.Data[s.Index])
			{
				kVSLocalLabel.Name = ValueConverter.ToString((sbyte*)ptr, 34, Encoding.Default);
			}
			s.Skip(34);
			kVSLocalLabel.CMID = (int)s.ReadUInt();
			ulong value = s.ReadULong();
			if (num4 != 48)
			{
				switch (kVSLocalLabel.ValueType)
				{
				case 4:
					kVSLocalLabel.ConstValue = (short)num5;
					break;
				case 2:
					kVSLocalLabel.ConstValue = (ushort)num5;
					break;
				case 5:
					kVSLocalLabel.ConstValue = (int)num5;
					break;
				case 3:
					kVSLocalLabel.ConstValue = num5;
					break;
				case 6:
					kVSLocalLabel.ConstValue = ValueConverter.UIntToFloat(num5);
					break;
				case 10:
					kVSLocalLabel.ConstValue = ValueConverter.Int64ToDouble((long)value);
					break;
				}
			}
			Add(kVSLocalLabel);
			s.Index = index;
		}
	}
}

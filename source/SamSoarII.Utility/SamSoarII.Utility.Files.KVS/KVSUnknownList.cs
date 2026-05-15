namespace SamSoarII.Utility.Files.KVS;

public class KVSUnknownList
{
	protected ushort code;

	protected byte[] data;

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

	public byte[] Data
	{
		get
		{
			return data;
		}
		set
		{
			data = value;
		}
	}

	public KVSUnknownList(KVSFileStream s)
	{
		uint num = s.ReadUInt();
		uint num2 = s.ReadUInt();
		s.Skip(32);
		if (s.Index + num <= s.Length)
		{
			data = new byte[num];
			s.ReadBytes(data, 0, (int)num);
		}
	}
}

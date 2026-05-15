using System;

namespace SamSoarII.Utility.Files.GX;

public class GXInstFormat
{
	private string name;

	private byte[] data;

	public string Name => name;

	public byte[] Data => data;

	public GXInstFormat(string _name, byte[] _data)
	{
		name = _name;
		data = _data;
	}

	public override string ToString()
	{
		return string.Format("GXInstFormat:{0:s}", name ?? "null");
	}

	public virtual GXInstFormat Pulselize()
	{
		string text = name;
		byte[] array = new byte[data.Length + 1];
		Array.Copy(data, array, data.Length);
		array[data.Length] = 2;
		return new GXInstFormat(text, array);
	}
}

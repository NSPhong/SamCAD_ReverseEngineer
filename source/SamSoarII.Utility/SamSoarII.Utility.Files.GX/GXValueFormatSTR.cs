using System.Text;

namespace SamSoarII.Utility.Files.GX;

public class GXValueFormatSTR : GXValueFormat
{
	public GXValueFormatSTR(string _name, byte[] _data)
		: base(_name, _data)
	{
	}

	public unsafe virtual string ToString(byte[] data)
	{
		fixed (byte* value = &data[0])
		{
			return $"\"{new string((sbyte*)value, 1, data.Length - 1, Encoding.Default)}\"";
		}
	}
}

namespace SamSoarII.Utility.Files.GX;

public class GXInitialize
{
	private string name;

	private byte bitnum;

	private byte type;

	private ulong binary;

	public string Name => name;

	public byte Bitnum => bitnum;

	public byte Type => type;

	public ulong Binary => binary;

	public GXInitialize(string _name, byte _bitnum, byte _type, ulong _binary)
	{
		name = _name;
		bitnum = _bitnum;
		type = _type;
		binary = _binary;
	}
}

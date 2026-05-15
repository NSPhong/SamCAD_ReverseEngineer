namespace SamSoarII.Core.Files;

public class LocatedStringByteData
{
	private byte[] data;

	private int localoffset;

	public byte[] Data => data;

	public int LocalOffset => localoffset;

	public LocatedStringByteData(byte[] _data, int _localoffset)
	{
		data = _data;
		localoffset = _localoffset;
	}
}

namespace SamSoarII.Core.Files;

public class LocatedFreeMemory
{
	private IFileHeader header;

	private int id;

	private int localoffset;

	public IFileHeader Header => header;

	public int ID => id;

	public int LocalOffset => localoffset;

	public LocatedFreeMemory(IFileHeader _header, int _id, int _localoffset)
	{
		header = _header;
		id = _id;
		localoffset = _localoffset;
	}
}

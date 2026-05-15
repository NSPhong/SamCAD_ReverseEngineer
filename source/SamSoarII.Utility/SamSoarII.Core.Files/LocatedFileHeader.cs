namespace SamSoarII.Core.Files;

public class LocatedFileHeader
{
	private IFileHeader header;

	private int lpHeader;

	public IFileHeader Header => header;

	public int LPHeader => lpHeader;

	public LocatedFileHeader(IFileHeader _header, int _lpHeader)
	{
		header = _header;
		lpHeader = _lpHeader;
	}

	public override string ToString()
	{
		return $"{{LocatedFileHeader:hd={header},lp={lpHeader}}}";
	}
}

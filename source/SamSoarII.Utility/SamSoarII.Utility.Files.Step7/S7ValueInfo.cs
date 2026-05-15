using System.Collections.Generic;

namespace SamSoarII.Utility.Files.Step7;

public class S7ValueInfo
{
	private Enum_S7BaseType bas;

	private int ofs;

	private List<S7ValueBase> values;

	public Enum_S7BaseType Bas => bas;

	public int Ofs => ofs;

	public IList<S7ValueBase> Values => values;

	public S7ValueInfo(Enum_S7BaseType _bas, int _ofs)
	{
		bas = _bas;
		ofs = _ofs;
		values = new List<S7ValueBase>();
	}
}

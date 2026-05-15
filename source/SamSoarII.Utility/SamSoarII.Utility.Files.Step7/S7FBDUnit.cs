namespace SamSoarII.Utility.Files.Step7;

public class S7FBDUnit : S7Unit
{
	private S7FBDUnitFormat format;

	public new S7FBDUnitFormat Format => format;

	public S7FBDUnit(S7Network _parent, int _dataindex)
		: base(_parent, _dataindex)
	{
		S7Translator.FBDFormats.TryGetValue(base.Code, out format);
	}

	public S7FBDUnit(S7Network _parent, S7Unit _source)
		: base(_parent, _source)
	{
	}
}

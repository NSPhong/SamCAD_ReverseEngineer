namespace SamSoarII.Utility.Files.KVS;

public class KVSHLine : KVSUnit
{
	public KVSHLine(KVSLadder _parent)
		: base(_parent)
	{
		parent = _parent;
		oargs = new KVSArg[3];
	}
}

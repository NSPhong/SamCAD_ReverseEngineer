namespace SamSoarII.Utility.Files.KVS;

public class KVSVLine : KVSUnit
{
	public KVSVLine(KVSLadder _parent)
		: base(_parent)
	{
		parent = _parent;
		oargs = new KVSArg[3];
	}
}

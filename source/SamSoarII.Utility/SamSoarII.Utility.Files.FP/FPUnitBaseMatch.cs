namespace SamSoarII.Utility.Files.FP;

public class FPUnitBaseMatch : FPItemConvert
{
	private int oid;

	private int eid;

	private int ofs;

	private bool istemp;

	public int OID => oid;

	public int EID => eid;

	public int OFS
	{
		get
		{
			return ofs;
		}
		set
		{
			ofs = value;
		}
	}

	public bool IsTemp
	{
		get
		{
			return istemp;
		}
		set
		{
			istemp = value;
		}
	}

	public FPUnitBaseMatch(int _oid, int _eid)
	{
		oid = _oid;
		eid = _eid;
		ofs = 0;
		istemp = false;
	}
}

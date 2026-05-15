namespace SamSoarII.Utility.Files.KVS;

public class KVSArgFormat
{
	protected bool isfollowsuffix = false;

	protected KVSValueType valuetype = KVSValueType.BOOL;

	protected int countback = 0;

	protected int countbase = 0;

	protected int countid = -1;

	protected int countfactor = 1;

	public bool IsFollowSuffix
	{
		get
		{
			return isfollowsuffix;
		}
		set
		{
			isfollowsuffix = value;
		}
	}

	public KVSValueType ValueType
	{
		get
		{
			return valuetype;
		}
		set
		{
			valuetype = value;
		}
	}

	public int CountBack
	{
		get
		{
			return countback;
		}
		set
		{
			countback = value;
		}
	}

	public int CountBase
	{
		get
		{
			return countbase;
		}
		set
		{
			countbase = value;
		}
	}

	public int CountID
	{
		get
		{
			return countid;
		}
		set
		{
			countid = value;
		}
	}

	public int CountFactor
	{
		get
		{
			return countfactor;
		}
		set
		{
			countfactor = value;
		}
	}
}

namespace SamSoarII.Utility.Files.KVS;

public class KVSSingleAction : KVSAction
{
	private KVSUnit in_;

	public KVSUnit In
	{
		get
		{
			return in_;
		}
		set
		{
			in_ = value;
		}
	}
}

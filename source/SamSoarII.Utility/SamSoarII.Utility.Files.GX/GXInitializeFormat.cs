namespace SamSoarII.Utility.Files.GX;

public class GXInitializeFormat
{
	private GXValueFormat register;

	private uint magicnumber;

	public GXValueFormat Register => register;

	public uint MagicNumber => magicnumber;

	public GXInitializeFormat(GXValueFormat _register, uint _magicnumber)
	{
		register = _register;
		magicnumber = _magicnumber;
	}
}

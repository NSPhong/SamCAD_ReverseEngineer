namespace SamSoarII.Utility.Files.FP;

public class FPUnitFormat : FPFormat
{
	private FPUnitConvert conv;

	public FPUnitConvert Conv
	{
		get
		{
			return conv;
		}
		set
		{
			conv = value;
		}
	}

	public FPUnitFormat(FPFormat _origin, string _name)
		: base(_origin.Offset, _origin.MainCode, _origin.PosiCode, _origin.IdenCode)
	{
		name = _name;
	}

	public FPUnitFormat(long _offset, uint _maincode, uint _posicode, uint _idencode, string _name)
		: base(_offset, _maincode, _posicode, _idencode)
	{
		name = _name;
	}

	public override string ToString()
	{
		return $"FPUnitFormat({name},{maincode:X8},{offset:X8})";
	}

	public FPUnitFormat ToRegi(uint regicode)
	{
		FPUnitFormat fPUnitFormat = new FPUnitFormat(offset, (maincode & 0xFFFF00FFu) | regicode, posicode, idencode, name);
		fPUnitFormat.IsUseMainRegi = base.IsUseMainRegi;
		fPUnitFormat.IsUseOffset = base.IsUseOffset;
		return fPUnitFormat;
	}
}

namespace SamSoarII.Utility.Files.FP;

public class FPValueFormat : FPFormat
{
	public FPValueFormat(uint _maincode, string _name)
		: base(0L, _maincode, 0u, 0u)
	{
		name = _name;
	}

	public static bool IsHexTail(uint _maincode)
	{
		switch (_maincode & 0xFF00)
		{
		case 0u:
		case 256u:
		case 512u:
		case 768u:
		case 1536u:
			return true;
		default:
			return false;
		}
	}

	public unsafe static string ToOffsetString(uint _maincode, long _offset)
	{
		if (IsHexTail(_maincode))
		{
			return (_offset < 16) ? $"{_offset:X1}" : $"{_offset >> 4}{_offset & 0xF:X1}";
		}
		if ((_maincode & 0xFF00) == 11008)
		{
			return $"{*(float*)(&_offset)}";
		}
		if ((_maincode & 0xFF00) == 11264)
		{
			return $"{*(double*)(&_offset)}";
		}
		return $"{_offset}";
	}
}

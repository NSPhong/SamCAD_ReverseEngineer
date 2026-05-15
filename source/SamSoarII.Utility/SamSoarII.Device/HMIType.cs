using System.Collections.Generic;

namespace SamSoarII.Device;

public class HMIType
{
	public static readonly HMIType EAH070B = new HMIType(EnumHMIType.EAH070B)
	{
		Name = "EA--7.0B(800*480 7.0')"
	};

	public static readonly HMIType EAH043A = new HMIType(EnumHMIType.EAH043A)
	{
		Name = "EA--4.3A(480*272 4.3')"
	};

	public static readonly HMIType EAH035A = new HMIType(EnumHMIType.EAH035A)
	{
		Name = "EA--3.5B(320*240 3.5')"
	};

	public static readonly HMIType EAH08C = new HMIType(EnumHMIType.EAHO8C)
	{
		Name = "EA--8C(800*600 8.0')"
	};

	public static readonly List<HMIType> List = new List<HMIType> { EAH070B, EAH043A, EAH035A, EAH08C };

	private EnumHMIType type;

	private string name;

	public EnumHMIType Type => type;

	public string Name
	{
		get
		{
			return name ?? type.ToString();
		}
		set
		{
			name = value;
		}
	}

	public static HMIType Create(EnumHMIType _hmitype)
	{
		return _hmitype switch
		{
			EnumHMIType.EAH070B => EAH070B, 
			EnumHMIType.EAH043A => EAH043A, 
			EnumHMIType.EAH035A => EAH035A, 
			EnumHMIType.EAHO8C => EAH08C, 
			_ => null, 
		};
	}

	public HMIType(EnumHMIType _type)
	{
		type = _type;
	}
}

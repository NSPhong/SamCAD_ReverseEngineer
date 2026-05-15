namespace SamSoarII.Core.Models;

public interface IFBDUnitType
{
	string Name { get; }

	Enum_FBDUnitType E { get; }

	Enum_FBDUnitShape Shape { get; }
}

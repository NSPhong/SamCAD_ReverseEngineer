namespace SamSoarII.Core.Models;

public interface IFBDValueFormat
{
	int ID { get; }

	string Name { get; }

	Enum_FBDValueIO IO { get; }

	int IOID { get; }

	bool ToPT { get; }

	int PTID { get; }

	bool CanRoute { get; }

	int X { get; }

	int Y { get; }

	bool IsCultural { get; }

	bool IsNameVisible { get; }

	bool IsInput();

	bool IsOutput();

	bool IsEn();
}

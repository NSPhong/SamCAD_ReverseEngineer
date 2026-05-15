namespace SamSoarII.Core.Models;

public interface IFBDValueModel
{
	IFBDUnitModel Parent { get; }

	int ID { get; }

	int X { get; }

	int Y { get; }

	string Text { get; }

	string Comment { get; }

	Enum_FBDLadderMode LadderMode { get; }

	string ShowName { get; }

	string ShowValue { get; }

	IFBDValueFormat Format { get; }

	IValueFormat VF { get; }

	IValueModel VM { get; }

	bool IsRouteOccupied { get; }

	object Error { get; }
}

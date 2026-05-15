namespace SamSoarII.Core.Models;

public interface IMonitorElement
{
	bool IsVisible { get; }

	IValueStore Store { get; }

	int SelectIndex { get; set; }

	string ShowName { get; }

	string CurrentValue { get; }

	int DataType { get; set; }
}

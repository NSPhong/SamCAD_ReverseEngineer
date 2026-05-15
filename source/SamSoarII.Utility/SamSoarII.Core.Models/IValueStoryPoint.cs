namespace SamSoarII.Core.Models;

public interface IValueStoryPoint
{
	int TimeMS { get; }

	int ValueI { get; }

	double ValueD { get; }

	bool IsInt { get; }

	bool IsFloat { get; }
}

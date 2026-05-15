namespace SamSoarII.Core.Models;

public interface IValueFormat
{
	int Position { get; }

	string Name { get; }

	bool CanRead { get; }

	bool CanWrite { get; }

	bool CanArgument { get; }
}

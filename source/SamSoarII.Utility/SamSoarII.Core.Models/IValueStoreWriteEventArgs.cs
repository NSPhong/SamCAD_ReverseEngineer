namespace SamSoarII.Core.Models;

public interface IValueStoreWriteEventArgs
{
	IValueStore Store { get; }

	bool IsWrite { get; }

	bool ToLock { get; }

	bool Unlock { get; }

	bool UnlockAll { get; }
}

namespace SamSoarII.Threads;

public interface ICoreThreadManager : IThreadManager
{
	IThreadManager AutoSave { get; }

	IThreadManager AutoInst { get; }

	IThreadManager FindReplace { get; }
}

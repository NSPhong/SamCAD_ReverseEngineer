using System.Windows;

namespace SamSoarII.Threads;

public interface IThreadManager
{
	bool IsAlive { get; }

	bool IsActive { get; }

	event RoutedEventHandler Started;

	event RoutedEventHandler Paused;

	event RoutedEventHandler Aborted;

	void Start();

	void Pause();

	void Abort();
}

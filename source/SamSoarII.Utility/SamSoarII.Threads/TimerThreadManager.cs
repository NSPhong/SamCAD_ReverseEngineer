namespace SamSoarII.Threads;

public abstract class TimerThreadManager : BaseThreadManager
{
	public TimerThreadManager(string _threadname, bool _isMTA, bool forceabort = false)
		: base(_threadname, _isMTA, forceabort)
	{
	}

	protected override void _Thread_Handle()
	{
		if (base.ThActive)
		{
			Handle();
		}
	}
}

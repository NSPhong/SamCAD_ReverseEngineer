using System.Threading;
using System.Windows;

namespace SamSoarII.Threads;

public abstract class BaseThreadManager : IThreadManager
{
	private Thread thread;

	private string threadname;

	private ThreadPriority threadpriority;

	private AutoResetEvent timerevent;

	private bool isalive;

	private bool isactive;

	private bool thalive;

	private bool thactive;

	private bool isMTA;

	private bool forceabort;

	private int timespan = 10;

	public bool IsAlive => isalive && thread != null && thread.IsAlive;

	public bool IsActive => isactive && thread != null;

	public bool ThAlive => thalive;

	public bool ThActive => thactive;

	public int TimeSpan
	{
		get
		{
			return timespan;
		}
		set
		{
			timespan = value;
		}
	}

	public event RoutedEventHandler Started = delegate
	{
	};

	public event RoutedEventHandler Paused = delegate
	{
	};

	public event RoutedEventHandler Aborted = delegate
	{
	};

	public BaseThreadManager(string _threadname, bool _isMTA, bool _forceabort = false, ThreadPriority _threadpriority = ThreadPriority.Normal)
	{
		threadname = _threadname;
		isMTA = _isMTA;
		forceabort = _forceabort;
		threadpriority = _threadpriority;
		timerevent = new AutoResetEvent(initialState: false);
	}

	private void _Thread_Run()
	{
		Before();
		_Invoke_Start();
		while (thalive)
		{
			_Invoke_Pause();
			if (!thalive)
			{
				break;
			}
			do
			{
				timerevent.WaitOne(timespan);
			}
			while (thalive && !thactive);
			if (!thalive)
			{
				break;
			}
			_Invoke_Start();
			if (!thalive)
			{
				break;
			}
			_Thread_Handle();
			if (!thalive)
			{
				break;
			}
		}
		After();
		_Invoke_Abort();
	}

	protected virtual void _Thread_Handle()
	{
		while (thalive && thactive)
		{
			isalive = true;
			isactive = true;
			Handle();
		}
	}

	protected abstract void Handle();

	protected virtual void Before()
	{
	}

	protected virtual void After()
	{
	}

	private void _Invoke_Start()
	{
		if (thalive)
		{
			isalive = true;
		}
		if (thactive)
		{
			isactive = true;
		}
		this.Started(this, new RoutedEventArgs());
	}

	private void _Invoke_Pause()
	{
		isactive = false;
		this.Paused(this, new RoutedEventArgs());
	}

	private void _Invoke_Abort()
	{
		isalive = false;
		isactive = false;
		this.Aborted(this, new RoutedEventArgs());
		thread.Abort();
	}

	public virtual void Abort()
	{
		timerevent.Set();
		if (IsAlive)
		{
			thalive = false;
			if (!isactive || forceabort)
			{
				_Invoke_Abort();
			}
		}
	}

	public virtual void Pause()
	{
		thactive = false;
	}

	public virtual void Start()
	{
		thalive = true;
		thactive = true;
		if (!IsAlive)
		{
			thread = new Thread(_Thread_Run);
			thread.Name = threadname;
			thread.Priority = threadpriority;
			if (isMTA)
			{
				thread.SetApartmentState(ApartmentState.MTA);
			}
			else
			{
				thread.SetApartmentState(ApartmentState.STA);
			}
			thread.Start();
		}
	}

	public virtual void Sleep(int timespan)
	{
		timerevent.WaitOne(timespan);
	}

	public virtual void Awake()
	{
		timerevent.Set();
	}
}

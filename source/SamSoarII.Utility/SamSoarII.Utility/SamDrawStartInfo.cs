using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SamSoarII.Utility;

public class SamDrawStartInfo
{
	private Process process;

	private IntPtr hipipe;

	private IntPtr hopipe;

	private SamDrawCode code;

	private string projectname;

	private SamDrawLanguage language;

	private string hmidevicename;

	private bool issktool;

	private bool hasopened;

	private Queue<SamDrawPipeTask> taskdelies = new Queue<SamDrawPipeTask>();

	public Process Process
	{
		get
		{
			return process;
		}
		set
		{
			process = value;
		}
	}

	public IntPtr HIPipe
	{
		get
		{
			return hipipe;
		}
		set
		{
			hipipe = value;
		}
	}

	public IntPtr HOPipe
	{
		get
		{
			return hopipe;
		}
		set
		{
			hopipe = value;
		}
	}

	public SamDrawCode Code
	{
		get
		{
			return code;
		}
		set
		{
			code = value;
		}
	}

	public string ProjectName
	{
		get
		{
			return projectname;
		}
		set
		{
			projectname = value;
		}
	}

	public SamDrawLanguage Language
	{
		get
		{
			return language;
		}
		set
		{
			language = value;
		}
	}

	public string HMIDeviceName
	{
		get
		{
			return hmidevicename;
		}
		set
		{
			hmidevicename = value;
		}
	}

	public bool IsSKTOOL
	{
		get
		{
			return issktool;
		}
		set
		{
			issktool = value;
		}
	}

	public bool HasOpened
	{
		get
		{
			return hasopened;
		}
		set
		{
			hasopened = value;
		}
	}

	public Queue<SamDrawPipeTask> TaskDelies => taskdelies;

	public string AppPath => Path.GetDirectoryName(process?.MainModule?.FileName);

	public string LadderPath => Path.Combine(AppPath, "ladder");

	public string CompilePath => Path.Combine(AppPath, "compile");
}

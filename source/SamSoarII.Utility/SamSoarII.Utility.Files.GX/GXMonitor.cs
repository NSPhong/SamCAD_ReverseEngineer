using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXMonitor
{
	private string name;

	private List<GXMonitorItem> items;

	private List<GXMonitorArgument> args;

	public string Name => name;

	public List<GXMonitorItem> Items => items;

	public List<GXMonitorArgument> Args => args;

	public GXMonitor(string _name)
	{
		name = _name;
		items = new List<GXMonitorItem>();
		args = new List<GXMonitorArgument>();
	}
}

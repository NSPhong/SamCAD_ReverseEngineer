using System.Collections.Generic;

namespace SamSoarII.Utility.Files.GX;

public class GXMonitorItem
{
	private int id;

	private List<GXMonitorArgument> args;

	public int ID => id;

	public List<GXMonitorArgument> Args => args;

	public GXMonitorItem(int _id)
	{
		id = _id;
		args = new List<GXMonitorArgument>();
	}
}

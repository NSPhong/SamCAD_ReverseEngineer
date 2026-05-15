using System.Collections.Generic;
using SamSoarII.Properties;

namespace SamSoarII.Device;

public class Series
{
	public static readonly Series PLC = new Series(EnumSeries.PLC);

	public static readonly Series PLC_HMI = new Series(EnumSeries.PLC_HMI);

	public static readonly List<Series> List = new List<Series> { PLC, PLC_HMI };

	public static readonly List<Series> List_PLCOnly = new List<Series> { PLC, PLC_HMI };

	private EnumSeries e;

	public EnumSeries E => e;

	public Series(EnumSeries _e)
	{
		e = _e;
	}

	public override string ToString()
	{
		return e switch
		{
			EnumSeries.PLC => Resources.Series_PLC, 
			EnumSeries.Motion => Resources.Series_Motion, 
			EnumSeries.PLC_HMI => Resources.Series_PLC_HMI, 
			_ => "<null>", 
		};
	}
}

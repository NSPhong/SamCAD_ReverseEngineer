using System.Collections.Generic;

namespace SamSoarII.Core.Helpers;

public class CSVValueLineI : ICSVValueLineI, ICSVValueLine
{
	public ICSVValueInfo ValueInfo { get; private set; }

	public IList<uint> Times { get; private set; }

	public IList<int> Values { get; private set; }

	public CSVValueLineI(ICSVValueInfo _valueinfo, IList<uint> _times, IList<int> _values)
	{
		ValueInfo = _valueinfo;
		Times = _times;
		Values = _values;
	}
}

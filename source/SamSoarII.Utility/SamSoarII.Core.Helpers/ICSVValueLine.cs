using System.Collections.Generic;

namespace SamSoarII.Core.Helpers;

public interface ICSVValueLine
{
	ICSVValueInfo ValueInfo { get; }

	IList<uint> Times { get; }
}

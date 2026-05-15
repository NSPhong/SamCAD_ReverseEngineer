using System.Collections.Generic;

namespace SamSoarII.Core.Helpers;

public interface ICSVValueLineI : ICSVValueLine
{
	IList<int> Values { get; }
}

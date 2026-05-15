using System.Collections.Generic;

namespace SamSoarII.Core.Helpers;

public interface ICSVValueLineF : ICSVValueLine
{
	IList<float> Values { get; }
}

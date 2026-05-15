using System.Collections.Generic;
using SamSoarII.Core.Models;

namespace SamSoarII.Shell.Windows;

public interface IProjectCompareViewModel
{
	IEnumerable<ICompareLadderPair> LadderPairs { get; }

	IEnumerable<ICompareTextPair> TextPairs { get; }

	IEnumerable<ICompareModbusPair> ModbusPairs { get; }

	IEnumerable<ICompareParamsPair> ParamsPairs { get; }
}

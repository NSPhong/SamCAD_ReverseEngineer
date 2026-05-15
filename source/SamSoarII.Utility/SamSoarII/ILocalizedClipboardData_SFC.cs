using System;
using System.Collections.Generic;
using SamSoarII.Core.Models;

namespace SamSoarII;

public interface ILocalizedClipboardData_SFC : ILocalizedClipboardData, IDisposable
{
	ISFCLadderModel SFCLadder { get; }

	IEnumerable<ISFCUnitModel> SFCUnits { get; }

	int XStart { get; }

	int YStart { get; }
}

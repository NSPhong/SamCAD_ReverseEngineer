using System;
using System.Collections.Generic;
using SamSoarII.Core.Models;

namespace SamSoarII;

public interface ILocalizedClipboardData_Units : ILocalizedClipboardData_Ladder, ILocalizedClipboardData, IDisposable
{
	ILadderNetworkModel Network { get; }

	IEnumerable<ILadderUnitModel> Units { get; }

	int XStart { get; }

	int XEnd { get; }

	int YStart { get; }

	int YEnd { get; }
}

using System;
using System.Collections.Generic;
using SamSoarII.Core.Models;

namespace SamSoarII;

public interface ILocalizedClipboardData_FBD : ILocalizedClipboardData, IDisposable
{
	IFBDDiagramModel Diagram { get; }

	IFBDNetworkModel Network { get; }

	IEnumerable<IFBDNetworkModel> Networks { get; }

	IEnumerable<IFBDUnitModel> Units { get; }
}

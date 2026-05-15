using System;
using System.Collections.Generic;
using SamSoarII.Core.Models;

namespace SamSoarII;

public interface ILocalizedClipboardData_Networks : ILocalizedClipboardData_Ladder, ILocalizedClipboardData, IDisposable
{
	ILadderDiagramModel Diagram { get; }

	IEnumerable<ILadderNetworkModel> Networks { get; }

	int NStart { get; }

	int NEnd { get; }
}

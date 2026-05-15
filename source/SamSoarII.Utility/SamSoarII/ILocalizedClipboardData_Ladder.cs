using System;
using SamSoarII.Shell.Models;

namespace SamSoarII;

public interface ILocalizedClipboardData_Ladder : ILocalizedClipboardData, IDisposable
{
	int LadderXCapacity { get; }

	SelectStatus SelectionStatus { get; }
}

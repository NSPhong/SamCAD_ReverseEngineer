using System;

namespace SamSoarII.Shell.Models;

public interface ILadderDiagramViewModel : IViewModel, IDisposable
{
	ISelectRectCore SelectRect { get; }

	ISelectAreaCore SelectArea { get; }
}

using System;

namespace SamSoarII.Core.Models;

public interface IFBDCommand : IDisposable
{
	bool IsRefreshView { get; }

	void Undo();

	void Undo(bool toview);

	void Redo();

	void Redo(bool toview);

	void SelectUndo();

	void SelectRedo();
}

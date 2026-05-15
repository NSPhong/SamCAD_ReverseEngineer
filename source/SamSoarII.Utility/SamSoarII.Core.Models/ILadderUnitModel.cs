using System;
using System.Collections.Generic;
using System.ComponentModel;
using SamSoarII.Shell.Models;

namespace SamSoarII.Core.Models;

public interface ILadderUnitModel : IModel, IDisposable, INotifyPropertyChanged
{
	bool IsDisposed { get; }

	bool IsUsed { get; }

	bool IsRisingEdge { get; }

	LadderModes LadderMode { get; }

	LadderUnitShapes Shape { get; }

	LadderUnitTypes Type { get; }

	string InstName { get; }

	int X { get; }

	int Y { get; }

	IEnumerable<IValueModel> Children { get; }

	ITextLineUnitInfo TextView { get; set; }

	IValueModel GetChild(int id);

	IValueModel GetNext(IValueModel vmodel);

	IValueModel GetPrev(IValueModel vmodel);

	int IndexOf(IValueModel child);
}

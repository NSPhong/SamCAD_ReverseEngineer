using System;
using System.ComponentModel;
using SamSoarII.Shell.Models;

namespace SamSoarII.Core.Models;

public interface ILadderNetworkModel : IModel, IDisposable, INotifyPropertyChanged
{
	bool IsDisposed { get; }

	int ID { get; set; }

	string Brief { get; set; }

	string Description { get; set; }

	bool IsExpand { get; set; }

	bool IsBriefExpand { get; set; }

	bool IsMasked { get; set; }

	LadderModes LadderMode { get; }

	bool IsCommentMode { get; }

	int RowCount { get; }

	ITextLineNetworkInfo TextInfo { get; set; }

	ILadderUnitModel GetChild(int x, int y);

	ILadderUnitModel GetVLine(int x, int y);

	ILadderRowModel GetRow(int y);
}

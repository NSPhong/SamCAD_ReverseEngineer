using System;
using System.Collections.Generic;
using System.ComponentModel;
using SamSoarII.Shell.Models;

namespace SamSoarII.Core.Models;

public interface ILadderDiagramModel : IModel, IDisposable, INotifyPropertyChanged
{
	bool IsDisposed { get; }

	int KeyID { get; }

	string Name { get; set; }

	string Brief { get; set; }

	string Description { get; set; }

	bool IsMainLadder { get; set; }

	bool IsExpand { get; set; }

	bool IsHeaderIsExpanded { get; set; }

	LadderTypes LadderType { get; }

	LadderModes LadderMode { get; }

	bool IsCommentMode { get; }

	IEnumerable<ILadderNetworkModel> Children { get; }

	IInstructionDiagramModel Inst { get; }

	ITextLineDiagramViewModel TextView { get; }

	ITextLineDiagramInfo TextInfo { get; set; }
}

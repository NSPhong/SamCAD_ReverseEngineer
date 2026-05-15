using System;
using System.ComponentModel;
using System.Windows.Media;

namespace SamSoarII.Core.Models;

public interface ILadderLineComment : ILadderUnitModel, IModel, IDisposable, INotifyPropertyChanged
{
	string Text { get; set; }

	FormattedText FmtText { get; set; }
}

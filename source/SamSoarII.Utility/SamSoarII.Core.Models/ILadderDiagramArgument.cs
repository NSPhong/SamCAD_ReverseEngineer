using System;
using System.ComponentModel;

namespace SamSoarII.Core.Models;

public interface ILadderDiagramArgument : IModel, IDisposable, INotifyPropertyChanged, INotifyPropertyChanging
{
	string Name { get; set; }

	string Comment { get; set; }

	IOAccessables IOAccessable { get; set; }

	ArgumentSpecials Special { get; set; }
}

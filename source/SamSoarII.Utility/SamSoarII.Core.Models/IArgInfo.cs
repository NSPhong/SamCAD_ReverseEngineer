using System;
using System.ComponentModel;

namespace SamSoarII.Core.Models;

public interface IArgInfo : IValueInfo, INotifyPropertyChanged, IDisposable
{
	ILadderDiagramModel Diagram { get; }

	ILadderDiagramArgument Argument { get; }
}

using System;
using System.ComponentModel;

namespace SamSoarII.Core.Models;

public interface ILocalInfo : IValueInfo, INotifyPropertyChanged, IDisposable
{
	ILadderDiagramModel Diagram { get; }
}

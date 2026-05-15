using System;
using System.ComponentModel;

namespace SamSoarII.Core.Models;

public interface IFBDDiagramModel : IModel, IDisposable, INotifyPropertyChanged
{
	string Name { get; }

	string Comment { get; }
}

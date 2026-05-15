using System;
using System.ComponentModel;
using SamSoarII.Core.Models;

namespace SamSoarII.Shell.Models;

public interface ISelectRectCore : IModel, IDisposable, INotifyPropertyChanged
{
	int X { get; }

	int Y { get; }

	ILadderUnitModel Current { get; set; }
}

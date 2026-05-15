using System;
using System.ComponentModel;

namespace SamSoarII.Core.Models;

public interface IPolylineAxisModel : IModel, IDisposable, INotifyPropertyChanged
{
	IValueModel PLS { get; }

	IValueModel DIR { get; }

	IValueModel WEI { get; }

	IValueModel LIM { get; }

	IValueModel CLM { get; }

	IValueModel ITV { get; }
}

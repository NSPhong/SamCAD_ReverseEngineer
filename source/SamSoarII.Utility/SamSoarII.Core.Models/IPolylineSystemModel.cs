using System;
using System.ComponentModel;

namespace SamSoarII.Core.Models;

public interface IPolylineSystemModel : IModel, IDisposable, INotifyPropertyChanged
{
	int ID { get; }

	int PLSID { get; set; }

	bool IsEnabled { get; set; }

	IPolylineAxisModel X { get; }

	IPolylineAxisModel Y { get; }

	bool IsHMIEnabled { get; set; }

	IValueModel HMI { get; }
}

using System;
using System.ComponentModel;

namespace SamSoarII.Core.Models;

public interface IValueStore : INotifyPropertyChanged, IDisposable
{
	IValueInfo Parent { get; }

	string ShowValue { get; }

	int Offset { get; }
}

using System;
using System.ComponentModel;

namespace SamSoarII.Core.Models;

public interface IValueModel : IModel, IDisposable, INotifyPropertyChanged
{
	bool IsLocal { get; }

	bool IsConstTime { get; }

	bool IsChild { get; }

	int Offset { get; }

	string Text { get; }

	string ShowText { get; }

	string Comment { get; }

	string FormatName { get; }

	object Value { get; }

	IValueStore Store { get; }

	IValueManager ValueManager { get; }
}

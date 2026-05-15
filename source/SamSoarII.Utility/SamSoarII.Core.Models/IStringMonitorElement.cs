using System;
using System.ComponentModel;

namespace SamSoarII.Core.Models;

public interface IStringMonitorElement : IDisposable, INotifyPropertyChanged
{
	IStringMonitorCore Parent { get; }

	string StartAddress { get; set; }

	int MaxLength { get; set; }

	int Length { get; set; }

	string Text { get; set; }
}

using System;
using System.ComponentModel;

namespace SamSoarII.Core.Models;

public interface IFBDLabelTableItem : IDisposable, INotifyPropertyChanged
{
	string Label { get; set; }

	string Address { get; set; }

	string Comment { get; set; }

	string ShowValue { get; set; }

	int RefCount { get; set; }
}

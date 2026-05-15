using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SamSoarII.Core.Models;

public interface IMonitorTable : IModel, IDisposable, INotifyPropertyChanged
{
	string Name { get; }

	IEnumerable<IMonitorElement> Children { get; }
}

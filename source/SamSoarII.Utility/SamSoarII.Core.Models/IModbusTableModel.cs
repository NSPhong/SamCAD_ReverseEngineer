using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SamSoarII.Core.Models;

public interface IModbusTableModel : IModel, IDisposable, INotifyPropertyChanged
{
	IEnumerable<IModbusModel> Children { get; }
}

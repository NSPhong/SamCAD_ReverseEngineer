using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SamSoarII.Core.Models;

public interface IModbusModel : IModel, IDisposable, INotifyPropertyChanged
{
	int KeyID { get; }

	string Name { get; set; }

	string Comment { get; set; }

	IEnumerable<IModbusItem> Children { get; }
}

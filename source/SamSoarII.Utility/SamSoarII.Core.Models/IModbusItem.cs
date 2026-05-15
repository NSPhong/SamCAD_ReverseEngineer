using System;
using System.ComponentModel;

namespace SamSoarII.Core.Models;

public interface IModbusItem : IModel, IDisposable, INotifyPropertyChanged
{
	string ItemID { get; set; }

	string SlaveID { get; set; }

	string HandleCode { get; set; }

	string SlaveRegister { get; set; }

	string SlaveCount { get; set; }

	string MasteRegister { get; set; }
}

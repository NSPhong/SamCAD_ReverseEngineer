using System;
using System.ComponentModel;

namespace SamSoarII.Core.Models;

public interface IFuncBlockModel : IModel, IDisposable, INotifyPropertyChanged
{
	int KeyID { get; }

	string Name { get; set; }

	string Code { get; set; }
}

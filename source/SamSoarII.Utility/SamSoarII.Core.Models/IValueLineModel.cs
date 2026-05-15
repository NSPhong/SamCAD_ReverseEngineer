using System;
using System.ComponentModel;

namespace SamSoarII.Core.Models;

public interface IValueLineModel : IModel, IDisposable, INotifyPropertyChanged
{
	void WriteInit();

	void WriteNext();

	void WriteTerminate();
}

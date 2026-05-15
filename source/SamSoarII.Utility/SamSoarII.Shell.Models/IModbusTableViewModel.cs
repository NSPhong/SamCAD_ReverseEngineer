using System;

namespace SamSoarII.Shell.Models;

public interface IModbusTableViewModel : IViewModel, IDisposable
{
	void UpdateButtonEnable();
}

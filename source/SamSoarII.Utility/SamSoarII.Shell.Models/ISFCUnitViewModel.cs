using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace SamSoarII.Shell.Models;

public interface ISFCUnitViewModel : IViewModel, IDisposable
{
	Canvas CVParent { get; set; }

	Dispatcher Dispatcher { get; }

	bool MouseTran { get; }

	bool MouseOutput { get; }

	int MouseTranFlag { get; }

	int ActualY { get; }

	void Update();

	void UpdateMouse(Point p);

	void UpdateValue();
}

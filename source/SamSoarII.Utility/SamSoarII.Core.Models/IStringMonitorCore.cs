using System;
using System.Collections.Generic;

namespace SamSoarII.Core.Models;

public interface IStringMonitorCore : IDisposable
{
	IProjectModel Parent { get; }

	IEnumerable<IStringMonitorElement> Items { get; }

	void UpdateLength();

	void InvokeText();
}

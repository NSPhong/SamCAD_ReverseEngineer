using System;
using SamSoarII.Core.Models;

namespace SamSoarII.Shell.Models;

public interface IViewModel : IDisposable
{
	IModel Core { get; set; }

	IViewModel ViewParent { get; }
}

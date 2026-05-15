using System;

namespace SamSoarII.Utility;

public interface IResource : IDisposable
{
	int ResourceID { get; set; }

	IResource Create(params object[] args);

	void Recreate(params object[] args);
}

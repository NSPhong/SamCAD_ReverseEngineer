using System.Collections.Generic;

namespace SamSoarII.Core.Models;

public interface ILadExSubCreator
{
	IEnumerable<ILadExSubCreate> GetCreates();
}

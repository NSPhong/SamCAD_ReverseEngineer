using System.Collections.Generic;

namespace SamSoarII.Core.Models;

public interface ILadderExtendLine
{
	int ID { get; set; }

	IEnumerable<ILadderUnitModel> Exs { get; }

	IEnumerable<ILadderUnitModel> Exvs { get; }

	ILadderExtendLineTarget To { get; }
}

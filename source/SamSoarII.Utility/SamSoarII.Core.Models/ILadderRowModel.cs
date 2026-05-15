using System.Collections.Generic;

namespace SamSoarII.Core.Models;

public interface ILadderRowModel
{
	int ID { get; }

	ILadderNetworkModel Parent { get; }

	ILadderLineComment LineCmt { get; set; }

	IEnumerable<ILadderUnitComment> UnitCmts { get; }

	IEnumerable<ILadderExtendLine> ExLines { get; }
}

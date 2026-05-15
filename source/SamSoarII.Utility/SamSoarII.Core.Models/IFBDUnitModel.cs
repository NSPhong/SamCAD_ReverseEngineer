using System.Collections.Generic;

namespace SamSoarII.Core.Models;

public interface IFBDUnitModel
{
	IFBDNetworkModel Parent { get; }

	IFBDUnitType Type { get; }

	int X { get; }

	int Y { get; }

	int Width { get; }

	int Height { get; }

	Enum_FBDLadderMode LadderMode { get; }

	Enum_FBDEditMode EditMode { get; }

	Enum_FBDShowMode ShowMode { get; }

	bool IsCommentMode { get; }

	IEnumerable<IFBDValueModel> Children { get; }
}

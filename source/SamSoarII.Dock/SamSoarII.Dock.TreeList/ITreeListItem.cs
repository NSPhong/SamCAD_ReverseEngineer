using System.Collections;
using System.Collections.Generic;

namespace SamSoarII.Dock.TreeList;

public interface ITreeListItem : IList<ITreeListItem>, ICollection<ITreeListItem>, IEnumerable<ITreeListItem>, IEnumerable
{
	TreeList Root { get; }

	ITreeListItem Parent { get; }

	ITreeListItemCtx Ctx { get; }

	int ID { get; }

	int Level { get; }

	int Height { get; }

	bool IsExpand { get; }

	bool IsDirectory { get; }

	bool IsSorted { get; }

	bool IsSelected { get; }
}

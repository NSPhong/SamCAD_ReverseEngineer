using System.Collections.Generic;
using System.Linq;

namespace SamSoarII.Core.Models;

public interface ILadExSubCreate
{
	string Description { get; }

	string DefaultName { get; }

	string Name { get; set; }

	string DirectoryPath { get; }

	ILookup<string, string> ValueLabels { get; }

	ILookup<string, string> ValueComments { get; }

	IEnumerable<string[]> GetStlList();
}

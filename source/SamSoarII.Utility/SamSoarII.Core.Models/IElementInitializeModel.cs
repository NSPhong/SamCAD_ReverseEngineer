using System.ComponentModel;
using System.IO;
using System.Xml.Linq;

namespace SamSoarII.Core.Models;

public interface IElementInitializeModel : INotifyPropertyChanged
{
	IValueInfo Parent { get; set; }

	string ShowName { get; }

	string ShowValue { get; set; }

	string[] ShowTypes { get; }

	int SelectIndex { get; set; }

	string Base { get; set; }

	uint Offset { get; set; }

	int DataType { get; set; }

	uint TempValue { get; set; }

	XElement CreateXElementByModel();

	void LoadByXElement(XElement rootNode);

	void Save(BinaryWriter writer);

	void Load(BinaryReader reader);
}

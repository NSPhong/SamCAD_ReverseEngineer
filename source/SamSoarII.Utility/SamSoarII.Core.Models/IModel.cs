using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Linq;
using SamSoarII.Shell.Models;

namespace SamSoarII.Core.Models;

public interface IModel : IDisposable, INotifyPropertyChanged
{
	IModel Parent { get; }

	IViewModel View { get; set; }

	void Save(XElement xele);

	void Load(XElement xele);

	void Save(BinaryWriter writer);

	void Load(BinaryReader reader);
}

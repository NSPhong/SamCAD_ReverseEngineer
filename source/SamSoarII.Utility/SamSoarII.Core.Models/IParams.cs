using System;
using System.ComponentModel;
using System.Xml.Linq;

namespace SamSoarII.Core.Models;

public interface IParams : IDisposable, INotifyPropertyChanged
{
	void Save(XElement xele);

	void Load(XElement xele);

	IParams Clone();

	void Load(IParams that);

	bool CheckParams();
}

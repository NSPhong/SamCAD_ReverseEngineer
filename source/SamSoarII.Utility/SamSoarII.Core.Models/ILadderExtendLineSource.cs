using System;
using System.ComponentModel;

namespace SamSoarII.Core.Models;

public interface ILadderExtendLineSource : ILadderUnitModel, IModel, IDisposable, INotifyPropertyChanged
{
	ILadderExtendLine From { get; set; }

	int ID { get; }
}

using System;
using System.ComponentModel;

namespace SamSoarII.Core.Models;

public interface IProjectPropertyParams : IParams, IDisposable, INotifyPropertyChanged
{
	IOtherParams Other { get; }
}

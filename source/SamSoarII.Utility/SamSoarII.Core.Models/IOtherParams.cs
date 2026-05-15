using System;
using System.ComponentModel;

namespace SamSoarII.Core.Models;

public interface IOtherParams : IParams, IDisposable, INotifyPropertyChanged
{
	bool IsUseWDG { get; set; }

	ushort WDG_Time { get; set; }

	bool IsRTCBCD { get; }
}

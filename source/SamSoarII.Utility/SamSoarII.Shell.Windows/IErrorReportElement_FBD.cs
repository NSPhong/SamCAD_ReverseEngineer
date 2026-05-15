using System;
using System.ComponentModel;
using SamSoarII.Core.Models;

namespace SamSoarII.Shell.Windows;

public interface IErrorReportElement_FBD : IDisposable, INotifyPropertyChanged
{
	ErrorReportStatus_FBD Status { get; }

	string Message { get; }

	IFBDNetworkModel Network { get; }

	IFBDUnitModel Unit { get; }

	int X { get; }

	int Y { get; }

	IErrorReportElement_FBD Clone();
}

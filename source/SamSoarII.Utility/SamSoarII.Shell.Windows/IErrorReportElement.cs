using System;
using System.ComponentModel;

namespace SamSoarII.Shell.Windows;

public interface IErrorReportElement : INotifyPropertyChanged, IDisposable
{
	IErrorReportElement Clone();
}

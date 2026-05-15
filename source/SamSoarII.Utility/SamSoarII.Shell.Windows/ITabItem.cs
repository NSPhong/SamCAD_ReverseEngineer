using System;
using System.ComponentModel;
using SamSoarII.Dock.Interface;

namespace SamSoarII.Shell.Windows;

public interface ITabItem : IDockContent, INotifyPropertyChanged, IDisposable
{
	IMainTabControl TabControl { get; }

	void Invoke(TabAction action);
}

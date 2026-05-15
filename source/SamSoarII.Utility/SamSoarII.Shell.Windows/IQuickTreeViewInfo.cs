using System;
using System.ComponentModel;
using SamSoarII.Core.Models;

namespace SamSoarII.Shell.Windows;

public interface IQuickTreeViewInfo : IModel, IDisposable, INotifyPropertyChanged
{
	string Path { get; }

	object RelativeObject { get; set; }

	bool IsExpanded { get; set; }
}

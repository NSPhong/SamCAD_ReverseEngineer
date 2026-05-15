using System;
using System.ComponentModel;
using SamSoarII.Core.Models;
using SamSoarII.Dock.Interface;

namespace SamSoarII.Shell.Windows;

public interface IMainTabDiagramItem : ITabItem, IDockContent, INotifyPropertyChanged, IDisposable
{
	ILadderDiagramModel Diagram { get; }

	void CreateFBDVModel(IFBDDiagramModel fbd);

	void RemoveFBDVModel();
}

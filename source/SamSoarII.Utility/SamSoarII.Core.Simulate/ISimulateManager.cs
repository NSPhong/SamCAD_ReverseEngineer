using System.ComponentModel;
using SamSoarII.Threads;

namespace SamSoarII.Core.Simulate;

public interface ISimulateManager : IThreadManager, INotifyPropertyChanged
{
	bool IsEnable { get; set; }

	IInteractionFacade IFParent { get; }

	ISimulateDllModel DllModel { get; }

	ISimulateViewer Viewer { get; }

	IBreakpointManager MNGBrpo { get; }

	IValueBrpoManager MNGVBrpo { get; }

	void JumpTo(int address);
}

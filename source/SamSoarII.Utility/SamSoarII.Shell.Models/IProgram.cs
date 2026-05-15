using System.ComponentModel;

namespace SamSoarII.Shell.Models;

public interface IProgram : INotifyPropertyChanged
{
	string ProgramName { get; set; }
}

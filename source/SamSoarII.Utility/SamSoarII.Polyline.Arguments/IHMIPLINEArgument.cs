using System.ComponentModel;

namespace SamSoarII.Polyline.Arguments;

public interface IHMIPLINEArgument : IPolylineArgument, INotifyPropertyChanged
{
	StartupConditions Condition { get; set; }

	string Value { get; set; }

	string Running { get; set; }

	string Finally { get; set; }

	string XOffset { get; set; }

	string YOffset { get; set; }

	string Velocity { get; set; }

	string Accelerate { get; set; }

	string Decelerate { get; set; }
}

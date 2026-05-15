using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace SamSoarII.Shell;

public class DefaultGridPenningSource : IGridPenningSource, INotifyPropertyChanged
{
	public static readonly DefaultGridPenningSource Default = new DefaultGridPenningSource();

	public double XStart => 0.0;

	public double YStart => 0.0;

	public double XLength => 100.0;

	public double YLength => 100.0;

	public string XUnit => "ms";

	public string YUnit => "Hz";

	public IList<string> XUnitEx => new string[0];

	public IList<string> YUnitEx => new string[0];

	public IList<int> XValueBase => new int[0];

	public IList<int> YValueBase => new int[0];

	public event PropertyChangedEventHandler PropertyChanged;

	public IEnumerable<IGridPenningEntity> GetEntities(Rect rect)
	{
		yield break;
	}
}

using System.Windows.Controls;

namespace SamSoarII.Shell;

public class CapslockTooltip : ToolTip
{
	public CapslockTooltip()
	{
		base.Content = new CapslockTooltipInner();
	}
}

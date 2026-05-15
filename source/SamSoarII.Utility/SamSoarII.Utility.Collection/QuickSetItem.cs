namespace SamSoarII.Utility.Collection;

public class QuickSetItem
{
	private QuickSetItem parent = null;

	private IQuickSetSupport item;

	public QuickSetItem Parent
	{
		get
		{
			return parent;
		}
		set
		{
			parent = value;
		}
	}

	public IQuickSetSupport Item
	{
		get
		{
			return item;
		}
		set
		{
			item = value;
		}
	}
}

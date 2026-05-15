namespace SamSoarII.Shell;

public class GridPenningAnimEventArgs
{
	private GridPenning.AnimStatus animstate;

	public GridPenning.AnimStatus AnimState => animstate;

	public GridPenningAnimEventArgs(GridPenning.AnimStatus _animstate)
	{
		animstate = _animstate;
	}
}

namespace SamSoarII.Shell;

public interface IGridPenningFunctionYX : IGridPenningEntity
{
	double YMin { get; }

	double YMax { get; }

	bool GetX(double y, ref double x);
}

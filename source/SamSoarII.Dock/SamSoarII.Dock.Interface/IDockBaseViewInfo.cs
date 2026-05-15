namespace SamSoarII.Dock.Interface;

public interface IDockBaseViewInfo
{
	DockManager Parent { get; }

	int DockID { get; }

	double Top { get; }

	double Left { get; }

	double Width { get; }

	double Height { get; }
}

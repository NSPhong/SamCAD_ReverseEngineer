namespace SamSoarII.Dock.Interface;

internal interface IDockCollection : IDockView
{
	bool IsChildChanging { get; }

	int Count { get; }

	void AddChild(IDockBaseView baseview);

	void RemoveChild(IDockBaseView baseview);

	void ResetChild();
}

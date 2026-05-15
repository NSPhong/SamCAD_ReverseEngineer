namespace SamSoarII.Dock.Interface;

internal interface IUserFocus
{
	bool IsUserFocused { get; }

	void InvokeIsUserFocusedChanged();
}

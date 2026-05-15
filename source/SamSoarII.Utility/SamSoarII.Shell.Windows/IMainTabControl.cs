using SamSoarII.Core.Models;

namespace SamSoarII.Shell.Windows;

public interface IMainTabControl : IWindow
{
	ITabItem SelectedItem { get; set; }

	void Reset();

	ITabItem ToTabItem(IModel model);

	void Add(ITabItem tab);

	void Remove(ITabItem tab);

	void Invoke(ITabItem tab, TabAction action);

	void ShowItem(IModel model);

	void ShowItem(ITabItem tab);

	void CloseItem(IModel model);

	void CloseItem(ITabItem tab);
}

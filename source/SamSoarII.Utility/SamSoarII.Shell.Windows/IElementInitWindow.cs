using SamSoarII.Core.Models;

namespace SamSoarII.Shell.Windows;

public interface IElementInitWindow : IWindow
{
	void AddElement(IElementInitializeModel element);

	IElementInitializeModel GetElement(string Base, uint Offset);

	IElementInitializeModel GenerateElementModel(bool isBit, string Base, uint Offset, int DataType);
}

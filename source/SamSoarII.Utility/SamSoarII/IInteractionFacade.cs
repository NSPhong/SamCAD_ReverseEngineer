using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows;
using SamSoarII.Core.Models;
using SamSoarII.Core.Simulate;
using SamSoarII.Shell.Dialogs;
using SamSoarII.Shell.Models;
using SamSoarII.Shell.Windows;
using SamSoarII.Threads;
using Samkoon.Protocol;

namespace SamSoarII;

public interface IInteractionFacade : IDisposable, INotifyPropertyChanged
{
	IMainWindow MainWindow { get; }

	IProjectModel Project { get; }

	IValueManager ValueManager { get; }

	ICoreThreadManager CoreThread { get; }

	IViewThreadManager ViewThread { get; }

	ISimulateManager SimulateManager { get; }

	ICommunicationManager CommunicationManager { get; }

	IMainTabControl MainTab { get; }

	IViewModel Current { get; }

	bool IsWaitForKey { get; }

	bool IsLoading { get; }

	bool IsSaveing { get; }

	bool IsRenaming { get; }

	bool IsSktool { get; }

	IEnumerable<ICompareLadderPair> CompareLadderPairs { get; }

	IEnumerable<ICompareTextPair> CompareTextPairs { get; }

	bool IsRT1061 { get; }

	event IWindowEventHandler PostIWindowEvent;

	event EventHandler LanguageChanged;

	LocalizedMessageResult ShowYesNo(string message);

	LocalizedMessageResult ShowYesNo(string message, string title);

	LocalizedMessageResult ShowYesNo(string message, string title, Window owner);

	LocalizedMessageResult ShowError(string message);

	LocalizedMessageResult ShowInfo(string message);

	LocalizedMessageResult ShowWarning(string message);

	bool ResizeXCapacity(int xcapacity);

	void UpdateTextViewAll();

	void UpdateTextViewProperties();

	void Navigate(string text);

	void Navigate(ILadderNetworkModel network, int x, int y);

	void Navigate(ILadderUnitModel unit);

	void Navigate(IFuncBlockModel funcblock, int offset);

	void Navigate(IFuncBlockModel funcblock, int line, int column);

	void Select(ILadderNetworkModel network, int x1, int x2, int y1, int y2);

	void Select(ILadderDiagramModel diagram, int netstart, int netend);

	void SelectToBrpoTable(ILadderUnitModel unit);

	bool IsDocked(IWindow window);

	bool IsFloated(IWindow window);

	void Dock(IWindow window);

	void Show(IWindow window);

	void Hide(IWindow window);

	bool IsFindWindowDocked();

	bool IsFindWindowFloated();

	void DockFindWindow();

	void ShowFindWindow();

	bool IsReplaceWindowDocked();

	bool IsReplaceWindowFloated();

	void DockReplaceWindow();

	void ShowReplaceWindow();

	void ShowErrorList(IEnumerable<IErrorReportElement_SFC> sfcerrors);

	void ShowErrorList(IEnumerable<IErrorReportElement_FBD> fbderrors);

	FrameworkElement CreateIcon(FrameworkElement fele, int iconid);

	FrameworkElement CreateIcon(FrameworkElement fele, int iconid, int width, int height);

	bool ShowInstructionInputDialog(string text, ISelectRectCore core);

	bool ShowInstructionInputDialog(string text, ISelectAreaCore core);

	void ShowElementPropertyDialog(ILadderUnitModel unit);

	bool ShowElementPropertyDialog(Enum type, ISelectRectCore core);

	bool ShowElementPropertyDialog(Enum type, ISelectAreaCore core);

	bool ShowElementPropertyDialog(Enum type, ISelectRectCore core, bool cover);

	bool ShowElementPropertyDialog(Enum type, ISelectAreaCore core, bool cover);

	bool ShowElementPropertyDialog(IFuncModel func, ISelectRectCore core);

	bool ShowElementPropertyDialog(IModbusModel modbus, ISelectRectCore core);

	void ShowValueModifyDialog(IEnumerable<IValueModel> values);

	void ShowValueModifyDialog(IEnumerable<IValueModel> values, int defaultindex);

	void ShowEditDiagramCommentDialog(ILadderDiagramModel diagram);

	void ShowEditNetworkCommentDialog(ILadderNetworkModel network);

	void ShowEditLineCommentDialog(ILadderNetworkModel network, int y);

	void ShowEditUnitCommentDialog(ILadderNetworkModel network, int x, int y);

	void ShowSFCUnitDialog(ISFCLadderModel ladder, int x, int y);

	void ShowSFCOutputDialog(ISFCUnitModel unit);

	void ShowTextRenameDialog(IFuncBlockViewModel funcblock, string word);

	void ShowEPIDMonitorDialog(ILadderUnitModel unit);

	void ShowHelpDocument();

	void ShowHelpDocument(Enum type);

	void ShowHelpDocument(string url);

	void UpdateHardFaultDebugInfo(string filename);

	void Copy(ILadderDiagramViewModel diagram, ILadderUnitModel unit);

	void Copy(ILadderDiagramViewModel diagram, ILadderNetworkModel network);

	void Copy(ILadderDiagramViewModel diagram, IEnumerable<ILadderUnitModel> units, int xstart, int xend, int ystart, int yend);

	void Copy(ILadderDiagramViewModel diagram, IEnumerable<ILadderNetworkModel> networks);

	void Copy(IEnumerable<ISFCUnitModel> units, int x, int y);

	void Copy(IFBDNetworkModel network, IEnumerable<IFBDUnitModel> units);

	void Copy(IFBDDiagramModel diagram, IEnumerable<IFBDNetworkModel> networks);

	ILocalizedClipboardData_Ladder Paste(ILadderNetworkModel receivenet);

	ILocalizedClipboardData_SFC Paste(ISFCLadderModel sfcladder);

	ILocalizedClipboardData_FBD Paste(IFBDNetworkModel network);

	bool Clipboard_AssertNetwork();

	bool Clipboard_AssertSFC();

	bool Clipboard_AssertFBD();

	byte? GetAddrType(Enum type, uint offset);

	List<byte> GetData(IPLSBlockModel plsblock);

	IQuickTreeViewInfo[] GetTreeItems(IDataObject data);

	IErrorReportElement CreateUnitError(ILadderUnitModel unit, int status, string message);

	IErrorReportElement_SFC CreateSFCError(ISFCUnitModel unit);

	IErrorReportElement_FBD CreateFBDError(string message, IFBDNetworkModel network, int x, int y);

	IErrorReportElement_FBD CreateFBDError(string message, IFBDUnitModel unit, int x, int y);

	ISFCUnitViewModel CreateSFCUnit(ISFCUnitModel unit);

	void DisposeSFCUnit(ISFCUnitViewModel unitview);

	void RemoveLadderCompare(ICompareLadderPair pair);

	void UpdateLadderCompare();

	void MeasureExpansionModule(IParams core);

	string GetOutlineName(int oid);

	Bitmap GetOutlineBitmap(int oid);

	void LoadFBDDemo();

	void LoadFBDFont();

	void SaveFBDFont();

	void UpdateFBDViewSize();

	void BookLabelRemoveAll();

	void BookLabelUp(object obj);

	void BookLabelDown(object obj);

	int GetReceiveLength(IBaseCommand cmd);

	void ReadInitData();
}

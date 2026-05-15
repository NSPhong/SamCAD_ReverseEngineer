using System.Collections.Generic;
using System.Windows.Input;

namespace SamSoarII.Global;

public static class GlobalCommand
{
	public static HashSet<RoutedUICommand> commands;

	public static RoutedUICommand AddNewFuncBlockCommand { get; set; }

	public static RoutedUICommand AddNewSubRoutineCommand { get; set; }

	public static RoutedUICommand AddNewModbusCommand { get; set; }

	public static RoutedUICommand InsertRowCommand { get; set; }

	public static RoutedUICommand DeleteRowCommand { get; set; }

	public static RoutedUICommand InsertColCommand { get; set; }

	public static RoutedUICommand DeleteColCommand { get; set; }

	public static RoutedUICommand InsertNetCommand { get; set; }

	public static RoutedUICommand DeleteNetCommand { get; set; }

	public static RoutedUICommand ZoomInCommand { get; set; }

	public static RoutedUICommand ZoomOutCommand { get; set; }

	public static RoutedUICommand ShowProjectTreeViewCommand { get; set; }

	public static RoutedUICommand ShowCommunicationSettingDialogCommand { get; set; }

	public static RoutedUICommand CompileCommand { get; set; }

	public static RoutedUICommand MonitorCommand { get; set; }

	public static RoutedUICommand EditCommand { get; set; }

	public static RoutedUICommand DownloadCommand { get; set; }

	public static RoutedUICommand UploadCommand { get; set; }

	public static RoutedUICommand ShowMainMonitorCommand { get; set; }

	public static RoutedUICommand ShowErrorListCommand { get; set; }

	public static RoutedUICommand ShowElemListCommand { get; set; }

	public static RoutedUICommand ShowElemInitCommand { get; set; }

	public static RoutedUICommand ShowBreakpointCommand { get; set; }

	public static RoutedUICommand ShowValueBrpoCommand { get; set; }

	public static RoutedUICommand ShowProjCompCommand { get; set; }

	public static RoutedUICommand ShowValueLineCommand { get; set; }

	public static RoutedUICommand ShowStrMoniCommand { get; set; }

	public static RoutedUICommand ShowDiagramArgumentCommand { get; set; }

	public static RoutedUICommand LadderModeToggleCommand { get; set; }

	public static RoutedUICommand InstModeToggleCommand { get; set; }

	public static RoutedUICommand CommentModeToggleCommand { get; set; }

	public static RoutedUICommand TextModeToggleCommand { get; set; }

	public static RoutedUICommand ShowPropertyDialogCommand { get; set; }

	public static RoutedUICommand InstShortCutOpenCommand { get; set; }

	public static RoutedUICommand ShowOptionDialogCommand { get; set; }

	public static RoutedUICommand CheckNetworkErrorCommand { get; set; }

	public static RoutedUICommand CheckFuncBlockCommand { get; set; }

	public static RoutedUICommand CloseProjectCommand { get; set; }

	public static RoutedUICommand SimulateCommand { get; set; }

	public static RoutedUICommand SimuStartCommand { get; set; }

	public static RoutedUICommand SimuPauseCommand { get; set; }

	public static RoutedUICommand SimuStopCommand { get; set; }

	public static RoutedUICommand BrpoStepCommand { get; set; }

	public static RoutedUICommand BrpoCallCommand { get; set; }

	public static RoutedUICommand BrpoNowCommand { get; set; }

	public static RoutedUICommand BrpoOutCommand { get; set; }

	public static RoutedUICommand EleListOpenCommand { get; set; }

	public static RoutedUICommand EleInitializeCommand { get; set; }

	public static RoutedUICommand ChangeToChineseCommand { get; set; }

	public static RoutedUICommand ChangeToEnglishCommand { get; set; }

	public static RoutedUICommand ShowHelpDocumentCommand { get; set; }

	public static RoutedUICommand ShowAboutCommand { get; set; }

	public static RoutedUICommand FileConvertCommand { get; set; }

	public static RoutedUICommand PolylineSettingCommand { get; set; }

	public static RoutedUICommand ImageImportCommand { get; set; }

	public static RoutedUICommand RecentOpenCommand { get; set; }

	public static RoutedUICommand BackupRecoveryCommand { get; set; }

	public static RoutedUICommand ExpansionModuleCheckCommand { get; set; }

	public static RoutedUICommand PLCResetCommand { get; set; }

	public static RoutedUICommand CloseCompareCommand { get; set; }

	public static RoutedUICommand OpenCompareCommand { get; set; }

	public static RoutedUICommand UploadCompareCommand { get; set; }

	public static RoutedUICommand USBFileCommand { get; set; }

	public static RoutedUICommand IapDownloadCommand { get; set; }

	public static RoutedUICommand SwitchToRunCommand { get; set; }

	public static RoutedUICommand SwitchToStopCommand { get; set; }

	public static RoutedUICommand WriteRealTimeCommand { get; set; }

	public static RoutedUICommand PLCVersionInfoCommand { get; set; }

	public static RoutedUICommand PLCHardFaultCommand { get; set; }

	public static RoutedUICommand BookLabelChange { get; set; }

	public static RoutedUICommand BookLabelRemoveAll { get; set; }

	public static RoutedUICommand BookLabelUp { get; set; }

	public static RoutedUICommand BookLabelDown { get; set; }

	public static RoutedUICommand FBDInsertRow { get; set; }

	public static RoutedUICommand FBDRemoveRow { get; set; }

	public static RoutedUICommand FBDInsertColumn { get; set; }

	public static RoutedUICommand FBDRemoveColumn { get; set; }

	public static RoutedUICommand FBDInsertNetwork { get; set; }

	public static RoutedUICommand FBDRemoveNetwork { get; set; }

	public static RoutedUICommand FBDBranch { get; set; }

	public static RoutedUICommand FBDLineDown { get; set; }

	public static RoutedUICommand FBDLineUp { get; set; }

	public static RoutedUICommand FBDLineRight { get; set; }

	public static RoutedUICommand FBDAnd { get; set; }

	public static RoutedUICommand FBDOr { get; set; }

	public static RoutedUICommand FBDRect { get; set; }

	public static RoutedUICommand FBDRectAdd { get; set; }

	public static RoutedUICommand FBDRectRemove { get; set; }

	public static RoutedUICommand FBDShowAddress { get; set; }

	public static RoutedUICommand FBDShowLabel { get; set; }

	public static RoutedUICommand FBDShowBoth { get; set; }

	public static RoutedUICommand FBDShowLabelTable { get; set; }

	public static RoutedUICommand FBDShowDiagramComment { get; set; }

	public static RoutedUICommand FBDShowNetworkComment { get; set; }

	public static RoutedUICommand FBDConvertInv { get; set; }

	public static RoutedUICommand FBDConvertImme { get; set; }

	public static RoutedUICommand PageSetup { get; set; }

	static GlobalCommand()
	{
		commands = new HashSet<RoutedUICommand>();
		AddNewFuncBlockCommand = new RoutedUICommand();
		commands.Add(AddNewFuncBlockCommand);
		AddNewSubRoutineCommand = new RoutedUICommand();
		commands.Add(AddNewSubRoutineCommand);
		AddNewModbusCommand = new RoutedUICommand();
		commands.Add(AddNewModbusCommand);
		InsertRowCommand = new RoutedUICommand();
		commands.Add(InsertRowCommand);
		DeleteRowCommand = new RoutedUICommand();
		commands.Add(DeleteRowCommand);
		InsertColCommand = new RoutedUICommand();
		commands.Add(InsertColCommand);
		DeleteColCommand = new RoutedUICommand();
		commands.Add(DeleteColCommand);
		InsertNetCommand = new RoutedUICommand();
		commands.Add(InsertNetCommand);
		DeleteNetCommand = new RoutedUICommand();
		commands.Add(DeleteNetCommand);
		ZoomInCommand = new RoutedUICommand();
		commands.Add(ZoomInCommand);
		ZoomOutCommand = new RoutedUICommand();
		commands.Add(ZoomOutCommand);
		ShowProjectTreeViewCommand = new RoutedUICommand();
		commands.Add(ShowProjectTreeViewCommand);
		ShowCommunicationSettingDialogCommand = new RoutedUICommand();
		commands.Add(ShowCommunicationSettingDialogCommand);
		ShowMainMonitorCommand = new RoutedUICommand();
		commands.Add(ShowMainMonitorCommand);
		ShowErrorListCommand = new RoutedUICommand();
		commands.Add(ShowErrorListCommand);
		ShowElemListCommand = new RoutedUICommand();
		commands.Add(ShowElemListCommand);
		ShowElemInitCommand = new RoutedUICommand();
		commands.Add(ShowElemInitCommand);
		ShowBreakpointCommand = new RoutedUICommand();
		commands.Add(ShowBreakpointCommand);
		ShowValueBrpoCommand = new RoutedUICommand();
		commands.Add(ShowValueBrpoCommand);
		ShowProjCompCommand = new RoutedUICommand();
		commands.Add(ShowProjCompCommand);
		CompileCommand = new RoutedUICommand();
		commands.Add(CompileCommand);
		MonitorCommand = new RoutedUICommand();
		commands.Add(MonitorCommand);
		EditCommand = new RoutedUICommand();
		commands.Add(EditCommand);
		DownloadCommand = new RoutedUICommand();
		commands.Add(DownloadCommand);
		UploadCommand = new RoutedUICommand();
		commands.Add(UploadCommand);
		ShowPropertyDialogCommand = new RoutedUICommand();
		commands.Add(ShowPropertyDialogCommand);
		InstShortCutOpenCommand = new RoutedUICommand();
		commands.Add(InstShortCutOpenCommand);
		ShowOptionDialogCommand = new RoutedUICommand();
		commands.Add(ShowOptionDialogCommand);
		LadderModeToggleCommand = new RoutedUICommand();
		commands.Add(LadderModeToggleCommand);
		InstModeToggleCommand = new RoutedUICommand();
		commands.Add(InstModeToggleCommand);
		CommentModeToggleCommand = new RoutedUICommand();
		commands.Add(CommentModeToggleCommand);
		TextModeToggleCommand = new RoutedUICommand();
		commands.Add(TextModeToggleCommand);
		CheckNetworkErrorCommand = new RoutedUICommand();
		commands.Add(CheckNetworkErrorCommand);
		CheckFuncBlockCommand = new RoutedUICommand();
		commands.Add(CheckFuncBlockCommand);
		CloseProjectCommand = new RoutedUICommand();
		commands.Add(CloseProjectCommand);
		SimulateCommand = new RoutedUICommand();
		commands.Add(SimulateCommand);
		SimuStartCommand = new RoutedUICommand();
		commands.Add(SimuStartCommand);
		SimuStopCommand = new RoutedUICommand();
		commands.Add(SimuStopCommand);
		SimuPauseCommand = new RoutedUICommand();
		commands.Add(SimuPauseCommand);
		BrpoStepCommand = new RoutedUICommand();
		commands.Add(BrpoStepCommand);
		BrpoCallCommand = new RoutedUICommand();
		commands.Add(BrpoCallCommand);
		BrpoNowCommand = new RoutedUICommand();
		commands.Add(BrpoNowCommand);
		BrpoOutCommand = new RoutedUICommand();
		commands.Add(BrpoOutCommand);
		ChangeToChineseCommand = new RoutedUICommand();
		commands.Add(ChangeToChineseCommand);
		ChangeToEnglishCommand = new RoutedUICommand();
		commands.Add(ChangeToEnglishCommand);
		ShowHelpDocumentCommand = new RoutedUICommand();
		commands.Add(ShowHelpDocumentCommand);
		ShowAboutCommand = new RoutedUICommand();
		commands.Add(ShowAboutCommand);
		FileConvertCommand = new RoutedUICommand();
		commands.Add(FileConvertCommand);
		PolylineSettingCommand = new RoutedUICommand();
		commands.Add(PolylineSettingCommand);
		ImageImportCommand = new RoutedUICommand();
		commands.Add(ImageImportCommand);
		RecentOpenCommand = new RoutedUICommand();
		commands.Add(RecentOpenCommand);
		BackupRecoveryCommand = new RoutedUICommand();
		commands.Add(BackupRecoveryCommand);
		ExpansionModuleCheckCommand = new RoutedUICommand();
		commands.Add(ExpansionModuleCheckCommand);
		PLCResetCommand = new RoutedUICommand();
		commands.Add(PLCResetCommand);
		CloseCompareCommand = new RoutedUICommand();
		commands.Add(CloseCompareCommand);
		ShowValueLineCommand = new RoutedUICommand();
		commands.Add(ShowValueLineCommand);
		OpenCompareCommand = new RoutedUICommand();
		commands.Add(OpenCompareCommand);
		UploadCompareCommand = new RoutedUICommand();
		commands.Add(UploadCompareCommand);
		USBFileCommand = new RoutedUICommand();
		commands.Add(USBFileCommand);
		IapDownloadCommand = new RoutedUICommand();
		commands.Add(IapDownloadCommand);
		SwitchToRunCommand = new RoutedUICommand();
		commands.Add(SwitchToRunCommand);
		SwitchToStopCommand = new RoutedUICommand();
		commands.Add(SwitchToStopCommand);
		WriteRealTimeCommand = new RoutedUICommand();
		commands.Add(WriteRealTimeCommand);
		PLCVersionInfoCommand = new RoutedUICommand();
		commands.Add(PLCVersionInfoCommand);
		PLCHardFaultCommand = new RoutedUICommand();
		commands.Add(PLCHardFaultCommand);
		ShowStrMoniCommand = new RoutedUICommand();
		commands.Add(ShowStrMoniCommand);
		ShowDiagramArgumentCommand = new RoutedUICommand();
		commands.Add(ShowDiagramArgumentCommand);
		BookLabelChange = new RoutedUICommand();
		commands.Add(BookLabelChange);
		BookLabelUp = new RoutedUICommand();
		commands.Add(BookLabelUp);
		BookLabelDown = new RoutedUICommand();
		commands.Add(BookLabelDown);
		BookLabelRemoveAll = new RoutedUICommand();
		commands.Add(BookLabelRemoveAll);
		FBDInsertRow = new RoutedUICommand();
		commands.Add(FBDInsertRow);
		FBDRemoveRow = new RoutedUICommand();
		commands.Add(FBDRemoveRow);
		FBDInsertColumn = new RoutedUICommand();
		commands.Add(FBDInsertColumn);
		FBDRemoveColumn = new RoutedUICommand();
		commands.Add(FBDRemoveColumn);
		FBDInsertNetwork = new RoutedUICommand();
		commands.Add(FBDInsertNetwork);
		FBDRemoveNetwork = new RoutedUICommand();
		commands.Add(FBDRemoveNetwork);
		FBDBranch = new RoutedUICommand();
		commands.Add(FBDBranch);
		FBDLineDown = new RoutedUICommand();
		commands.Add(FBDLineDown);
		FBDLineUp = new RoutedUICommand();
		commands.Add(FBDLineUp);
		FBDLineRight = new RoutedUICommand();
		commands.Add(FBDLineRight);
		FBDAnd = new RoutedUICommand();
		commands.Add(FBDAnd);
		FBDOr = new RoutedUICommand();
		commands.Add(FBDOr);
		FBDRect = new RoutedUICommand();
		commands.Add(FBDRect);
		FBDRectAdd = new RoutedUICommand();
		commands.Add(FBDRectAdd);
		FBDRectRemove = new RoutedUICommand();
		commands.Add(FBDRectRemove);
		FBDShowAddress = new RoutedUICommand();
		commands.Add(FBDShowAddress);
		FBDShowLabel = new RoutedUICommand();
		commands.Add(FBDShowLabel);
		FBDShowBoth = new RoutedUICommand();
		commands.Add(FBDShowBoth);
		FBDShowLabelTable = new RoutedUICommand();
		commands.Add(FBDShowLabelTable);
		FBDShowDiagramComment = new RoutedUICommand();
		commands.Add(FBDShowDiagramComment);
		FBDShowNetworkComment = new RoutedUICommand();
		commands.Add(FBDShowNetworkComment);
		FBDConvertInv = new RoutedUICommand();
		commands.Add(FBDConvertInv);
		FBDConvertImme = new RoutedUICommand();
		commands.Add(FBDConvertImme);
		PageSetup = new RoutedUICommand();
		commands.Add(PageSetup);
	}
}

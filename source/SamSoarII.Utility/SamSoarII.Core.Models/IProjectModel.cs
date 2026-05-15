using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using SamSoarII.Device;
using SamSoarII.Shell.Models;
using SamSoarII.Shell.Windows;
using SamSoarII.Utility;

namespace SamSoarII.Core.Models;

public interface IProjectModel : IModel, IDisposable, INotifyPropertyChanged
{
	IInteractionFacade IFParent { get; }

	IValueManager ValueManager { get; }

	SamSoarII.Utility.Version Version { get; }

	IPLCDevice PLCDevice { get; }

	HMIType HMIType { get; }

	string Password { get; set; }

	bool PasswordIsEnable { get; set; }

	int LadderXCapacity { get; }

	bool IsRT1061 { get; }

	Enum_FBDLadderMode FBDLadderMode { get; set; }

	Enum_FBDShowMode FBDShowMode { get; set; }

	ILadderDiagramModel MainDiagram { get; }

	IFuncBlockModel LibFuncBlock { get; }

	IEnumerable<ILadderDiagramModel> Diagrams { get; }

	IEnumerable<IFuncBlockModel> FuncBlocks { get; }

	IEnumerable<IFuncModel> Funcs { get; }

	IEnumerable<IPLSBlockModel> PLSBlocks { get; }

	IEnumerable<IPolylineSystemModel> Polylines { get; }

	IModbusTableModel Modbus { get; }

	IProjectPropertyParams Params { get; }

	IMonitorDiagramModel PulseChart { get; }

	IValueBrpoModel ValueBrpo { get; }

	IValueLineModel ValueLine { get; }

	IDictionary<string, IFBDLabelTableItem> LabelTable { get; }

	LadderModes LadderMode { get; }

	bool IsCommentMode { get; }

	bool Nolibrary { get; }

	bool IsModified { get; }

	event NotifyCollectionChangedEventHandler DiagramChanged;

	event NotifyCollectionChangedEventHandler FuncBlockChanged;

	IFBDLabelTableItem CreateLabel(string labelname);

	IFBDLabelTableItem RemoveLabel(string labelname);

	IFBDLabelTableItem RemoveLabel(string labelname, int refdelta);

	bool MoveLabel_Assert_Label(IFBDLabelTableItem item, string label);

	bool MoveLabel_Assert_Address(IFBDLabelTableItem item, string address);

	void MoveLabel_Begin(IFBDLabelTableItem item);

	void MoveLabel_End(IFBDLabelTableItem item);

	void InvokeModify(IModel model);

	void InvokeModify(IModel model, bool undo);

	void InvokeModify(IWindow window);

	void InvokeModify(IParams paras);

	void LoadUnitSp(ILadderUnitModel unit);
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace SamSoarII.Core.Models;

public interface IValueManager : IDisposable, IModel, INotifyPropertyChanged, IEnumerable<IValueInfo>, IEnumerable
{
	IInteractionFacade IFParent { get; }

	IValueInfo EmptyInfo { get; }

	IValueInfo this[int index] { get; }

	IValueInfo this[string text] { get; }

	IValueInfo this[IValueModel value] { get; }

	IValueModel LastAnalyzer { get; }

	ILadderDiagramModel LocalDiagram { get; }

	IValueModel TempModel { get; }

	IValueModel WBitModel { get; }

	IValueModel BDWordModel { get; }

	IValueModel LocalModel { get; }

	event IValueStoreWriteEventHandler PostValueStoreEvent;

	int GetOffset(IValueModel value);

	int IndexOf(IValueModel value);

	void SetTempLocalEnvironment(ILadderDiagramModel _localdiagram, bool _iscreatelocalinfo);

	void Add(IValueModel value);

	void Remove(IValueModel value);

	void Add(ILadderUnitModel unit);

	void AddZRNR(ILadderUnitModel unit);

	void Remove(ILadderUnitModel unit);

	void RemoveZRNR(ILadderUnitModel unit);

	void Add(ILadderNetworkModel network);

	void Remove(ILadderNetworkModel network);

	void Add(ILadderDiagramModel diagram);

	void Remove(ILadderDiagramModel diagram);

	void RemoveAlias(IValueInfo info);

	string AllocAlias(IValueInfo info);

	IEnumerable<ILocalInfo> GetLocalInfos();

	void InvokePostValueStoreEvent(object sender, IValueStoreWriteEventArgs e);

	void NotifyVisRefChanged();

	void AsyncRead(ILadderUnitModel unit);

	void AsyncWrite(ILadderUnitModel unit);

	void AsyncRead(ILadderUnitModel unit, IAsyncReadAttribute attr);

	void AsyncWrite(ILadderUnitModel unit, IAsyncWriteAttribute attr);

	void OpenSelfTuning(ILadderUnitModel unit);

	void AddArg(ILadderDiagramModel diagram, ILadderDiagramArgument argument);

	void AddArg(IArgInfo arginfo);

	void RemoveArg(ILadderDiagramModel diagram, string name);

	void DisposeArg(ILadderDiagramModel diagram, string name);

	void ClearArgs(ILadderDiagramModel diagram);

	ILadderDiagramArgument GetArg(ILadderDiagramModel diagram, string name);

	ILadderDiagramArgument GetArg(string name);

	IArgInfo GetArgInfo(ILadderDiagramModel diagram, string name);

	IArgInfo GetArgInfo(string name);
}

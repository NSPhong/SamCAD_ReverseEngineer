using System;
using System.ComponentModel;
using System.Windows.Input;

namespace SamSoarII.Shell.Models;

public interface ITextLineDiagramViewModel : IDisposable, INotifyPropertyChanged
{
	void InvokeViewCreated(ITextLineBaseInfo info);

	void InvokeViewRemoved(ITextLineBaseInfo info);

	void InvokeMouseDown(ITextLineColumnInfo cinfo, MouseButtonEventArgs e);

	void InvokeMouseUp(ITextLineColumnInfo cinfo, MouseButtonEventArgs e);

	void InvokeMouseMove(ITextLineColumnInfo cinfo, MouseEventArgs e);

	void InvokeMouseLeave(ITextLineColumnInfo cinfo, MouseEventArgs e);

	void InvokeMouseEnter(ITextLineColumnInfo cinfo, MouseEventArgs e);

	void InvokeMouseWheel(ITextLineColumnInfo cinfo, MouseWheelEventArgs e);

	void InvokeDoubleClick(ITextLineColumnInfo cinfo, MouseEventArgs e);
}

using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using SamSoarII.Polyline.Entity.User;

namespace SamSoarII.Polyline.Control;

public class PolylineDataGridUserColumn : DataGridTemplateColumn
{
	public static DataTemplate ShowTemplateT;

	public static DataTemplate EditTemplateT;

	public static DataTemplate ShowTemplateB;

	public static DataTemplate EditTemplateB;

	protected static readonly DependencyProperty CoreProperty = DependencyProperty.Register("Core", typeof(IPolylineUserFormat), typeof(PolylineDataGridUserColumn), new PropertyMetadata(null, OnPropertyChanged_Core));

	public IPolylineUserFormat Core
	{
		get
		{
			return (IPolylineUserFormat)GetValue(CoreProperty);
		}
		set
		{
			SetValue(CoreProperty, value);
		}
	}

	private static void OnPropertyChanged_Core(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineDataGridUserColumn)
		{
			((PolylineDataGridUserColumn)d).OnCoreChanged(e);
		}
	}

	protected virtual void OnCoreChanged(DependencyPropertyChangedEventArgs e)
	{
		if (e.OldValue is IPolylineUserFormat)
		{
			IPolylineUserFormat polylineUserFormat = (IPolylineUserFormat)e.OldValue;
			polylineUserFormat.PropertyChanged -= OnCorePropertyChanged;
		}
		if (e.NewValue is IPolylineUserFormat)
		{
			IPolylineUserFormat polylineUserFormat2 = (IPolylineUserFormat)e.NewValue;
			polylineUserFormat2.PropertyChanged += OnCorePropertyChanged;
		}
		if (Core != null)
		{
			base.Header = Core.Name;
			if (Core.DataType == PolylineUserDataType.Bool)
			{
				base.CellTemplate = ShowTemplateB;
				base.CellEditingTemplate = EditTemplateB;
			}
			else
			{
				base.CellTemplate = ShowTemplateT;
				base.CellEditingTemplate = EditTemplateT;
			}
		}
	}

	private void OnCorePropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		string propertyName = e.PropertyName;
		string text = propertyName;
		if (text == "Name")
		{
			base.Header = Core.Name;
		}
	}

	protected override object PrepareCellForEdit(FrameworkElement editingElement, RoutedEventArgs editingEventArgs)
	{
		return base.PrepareCellForEdit(editingElement, editingEventArgs);
	}

	protected override bool CommitCellEdit(FrameworkElement editingElement)
	{
		return base.CommitCellEdit(editingElement);
	}

	protected override void CancelCellEdit(FrameworkElement editingElement, object uneditedValue)
	{
		base.CancelCellEdit(editingElement, uneditedValue);
	}
}

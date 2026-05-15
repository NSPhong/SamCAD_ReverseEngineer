using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace SamSoarII.Shell;

public class CustomItemBox : ItemsControl, IComponentConnector
{
	private CustomItemsPanel itemspanel;

	private bool _contentLoaded;

	public CustomItemBox()
	{
		InitializeComponent();
	}

	public override void OnApplyTemplate()
	{
		base.OnApplyTemplate();
		DependencyObject templateChild = GetTemplateChild("PART_ItemsPanel");
		if (templateChild is CustomItemsPanel)
		{
			itemspanel = (CustomItemsPanel)templateChild;
			itemspanel.ViewParent = this;
			itemspanel.ItemTemplate = base.ItemTemplate;
		}
	}

	protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		base.OnPropertyChanged(e);
		if (e.Property == ItemsControl.ItemsSourceProperty)
		{
			itemspanel?.InvalidateLogicItems(base.Items);
		}
		if (e.Property == ItemsControl.ItemTemplateProperty && itemspanel != null)
		{
			itemspanel.ItemTemplate = base.ItemTemplate;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamSoarII.Utility;component/controls/customitembox.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		_contentLoaded = true;
	}
}

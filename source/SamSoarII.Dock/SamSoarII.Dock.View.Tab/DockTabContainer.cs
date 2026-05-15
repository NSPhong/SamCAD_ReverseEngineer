using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using SamSoarII.Dock.Interface;

namespace SamSoarII.Dock.View.Tab;

internal class DockTabContainer : DockCollectionContainer
{
	private FormattedText headertext;

	public override IDockBaseView ViewContent
	{
		get
		{
			return base.ViewContent;
		}
		set
		{
			if (ViewContent != null)
			{
				ViewContent.DockContent.PropertyChanged -= OnDockContentPropertyChanged;
			}
			base.ViewContent = value;
			if (ViewContent != null)
			{
				ViewContent.DockContent.PropertyChanged += OnDockContentPropertyChanged;
				headertext = new FormattedText(ViewContent.DockContent.Header, Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, DockTabBarDrawer.TextTypeface, DockTabBarDrawer.TextSize, DockTabBarDrawer.TextBrush_Unselected);
				headertext.MaxLineCount = 1;
			}
		}
	}

	public FormattedText HeaderText => headertext;

	public DockTabContainer(DockTab _parent, IDockBaseView _baseview)
		: base(_parent, _baseview)
	{
	}

	private void OnDockContentPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		string propertyName = e.PropertyName;
		string text = propertyName;
		if (text == "Header")
		{
			headertext = new FormattedText(ViewContent.DockContent.Header, Thread.CurrentThread.CurrentUICulture, FlowDirection.LeftToRight, DockTabBarDrawer.TextTypeface, DockTabBarDrawer.TextSize, DockTabBarDrawer.TextBrush_Unselected);
			headertext.MaxLineCount = 1;
			((DockTab)base.ViewParent).IsChanged = true;
		}
	}
}

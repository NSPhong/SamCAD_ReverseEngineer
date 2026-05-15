using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using SamSoarII.Dock.Global;
using SamSoarII.Dock.Interface;
using SamSoarII.Dock.View;
using SamSoarII.Dock.View.Tab;

namespace SamSoarII.Dock.Float;

internal class FloatTab : Grid, IDisposable, IDockBaseView, IUserFocus, IDockView
{
	private class HeightConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is double num))
			{
				return double.NaN;
			}
			return num + HeaderDrawer.HeaderHeight;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!(value is double num))
			{
				return double.NaN;
			}
			return num - HeaderDrawer.HeaderHeight;
		}
	}

	public class VirtualContent : IDockContent, INotifyPropertyChanged
	{
		public string Header => string.Empty;

		public ushort DockID => 0;

		public ImageSource Icon => null;

		public event PropertyChangedEventHandler PropertyChanged;

		public CultureInfo GetCurrentCultureInfo()
		{
			return CultureInfo.CurrentUICulture;
		}
	}

	private bool isdisposed = false;

	private FloatWindow parent;

	private DockTab content;

	private HeaderContainer header;

	protected RowDefinition rdefhd;

	protected RowDefinition rdefin;

	protected DockBaseInner inner;

	private Binding bdWidth;

	private Binding bdHeight;

	private Binding bdMinWidth;

	private Binding bdMinHeight;

	private VirtualContent virtualcontent;

	private IDockContainer dockcontainer;

	public bool IsDisposed => isdisposed;

	public ViewCommon.BaseViewTypes Type => ViewCommon.BaseViewTypes.Anchor;

	public FloatWindow ViewParent => parent;

	IDockView IDockView.ViewParent => ViewParent;

	DockManager IDockBaseView.DockManager => parent?.ViewParent;

	public DockTab ViewContent => content;

	public HeaderContainer ViewHeader => header;

	IDockContent IDockBaseView.DockContent => virtualcontent;

	public IDockContainer DockContainer
	{
		get
		{
			return dockcontainer;
		}
		set
		{
			if (dockcontainer != value)
			{
				IDockContainer dockContainer = dockcontainer;
				dockcontainer = null;
				if (dockContainer != null && dockContainer.ViewContent != null)
				{
					dockContainer.ViewContent = null;
				}
				dockcontainer = value;
				if (dockcontainer != null && dockcontainer.ViewContent != this)
				{
					dockcontainer.ViewContent = this;
				}
			}
		}
	}

	public ViewCommon.HeaderTypes HeaderType
	{
		get
		{
			if (dockcontainer is Window)
			{
				Window window = (Window)dockcontainer;
				return (window.WindowState == WindowState.Maximized) ? ViewCommon.HeaderTypes.FloatMaximized : ViewCommon.HeaderTypes.Float;
			}
			return ViewCommon.HeaderTypes.Float;
		}
	}

	public bool IsUserFocused => UserFocusManager.IsUserFocused(this);

	public FloatTab(FloatWindow _parent, DockTab _content)
	{
		parent = _parent;
		content = _content;
		header = new HeaderContainer(this);
		inner = new DockBaseInner(this);
		virtualcontent = new VirtualContent();
		rdefhd = new RowDefinition
		{
			MinHeight = HeaderDrawer.HeaderHeight,
			MaxHeight = HeaderDrawer.HeaderHeight
		};
		rdefin = new RowDefinition
		{
			Height = new GridLength(1.0, GridUnitType.Star)
		};
		bdWidth = new Binding("Width")
		{
			Source = content
		};
		bdHeight = new Binding("Height")
		{
			Source = content,
			Converter = new HeightConverter()
		};
		bdMinWidth = new Binding("MinWidth")
		{
			Source = content
		};
		bdMinHeight = new Binding("MinHeight")
		{
			Source = content,
			Converter = new HeightConverter()
		};
		Grid.SetRow(header, 0);
		Grid.SetRow(inner, 1);
		base.RowDefinitions.Add(rdefhd);
		base.RowDefinitions.Add(rdefin);
		base.Children.Add(header);
		base.Children.Add(inner);
		inner.Content = content;
		inner.SizeChanged += OnInnerSizeChanged;
		SetBinding(FrameworkElement.WidthProperty, bdWidth);
		SetBinding(FrameworkElement.HeightProperty, bdHeight);
		SetBinding(FrameworkElement.MinWidthProperty, bdMinWidth);
		SetBinding(FrameworkElement.MinHeightProperty, bdMinHeight);
	}

	public void Dispose()
	{
		if (!isdisposed)
		{
			isdisposed = true;
			inner.SizeChanged -= OnInnerSizeChanged;
			inner.Content = null;
			base.Children.Clear();
			BindingOperations.ClearBinding(this, FrameworkElement.WidthProperty);
			BindingOperations.ClearBinding(this, FrameworkElement.HeightProperty);
			BindingOperations.ClearBinding(this, FrameworkElement.MinWidthProperty);
			BindingOperations.ClearBinding(this, FrameworkElement.MinHeightProperty);
			parent = null;
			header = null;
			content = null;
			inner = null;
			rdefhd = null;
			rdefin = null;
			bdWidth = null;
			bdHeight = null;
			bdMinWidth = null;
			bdMinHeight = null;
		}
	}

	void IUserFocus.InvokeIsUserFocusedChanged()
	{
		header.Update();
	}

	private void OnInnerSizeChanged(object sender, SizeChangedEventArgs e)
	{
		if (content != null)
		{
			content.Width = inner.ActualWidth;
			content.Height = inner.ActualHeight;
		}
	}
}

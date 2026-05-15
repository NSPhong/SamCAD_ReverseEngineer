using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SamSoarII.Shell;

public class CustomDialog : Window
{
	public static RoutedUICommand CloseWindowCommand;

	public static readonly DependencyProperty ShowDefaultHeaderProperty;

	public static readonly DependencyProperty ShowResizeGripProperty;

	public static readonly DependencyProperty CanCloseProperty;

	public static readonly DependencyProperty CanResizeProperty;

	public static readonly DependencyProperty HeaderProperty;

	public static readonly DependencyProperty HeaderTemplateProperty;

	public static readonly DependencyProperty HeaderTemplateSelectorProperty;

	public static readonly DependencyProperty IsFullScreenMaximizeProperty;

	public static RoutedUICommand MaximizeWindowCommand { get; private set; }

	public static RoutedUICommand MinimizeWindowCommand { get; private set; }

	public static RoutedUICommand RestoreWindowCommand { get; private set; }

	public bool ShowDefaultHeader
	{
		get
		{
			return (bool)GetValue(ShowDefaultHeaderProperty);
		}
		set
		{
			SetValue(ShowDefaultHeaderProperty, value);
		}
	}

	public bool CanClose
	{
		get
		{
			return (bool)GetValue(CanCloseProperty);
		}
		set
		{
			SetValue(CanCloseProperty, value);
		}
	}

	public bool CanResize
	{
		get
		{
			return (bool)GetValue(CanResizeProperty);
		}
		set
		{
			SetValue(CanResizeProperty, value);
		}
	}

	public bool ShowResizeGrip
	{
		get
		{
			return (bool)GetValue(ShowResizeGripProperty);
		}
		set
		{
			SetValue(ShowResizeGripProperty, value);
		}
	}

	public object Header
	{
		get
		{
			return GetValue(HeaderProperty);
		}
		set
		{
			SetValue(HeaderProperty, value);
		}
	}

	public DataTemplate HeaderTemplate
	{
		get
		{
			return (DataTemplate)GetValue(HeaderTemplateProperty);
		}
		set
		{
			SetValue(HeaderTemplateProperty, value);
		}
	}

	public DataTemplateSelector HeaderTemplateSelector
	{
		get
		{
			return (DataTemplateSelector)GetValue(HeaderTemplateSelectorProperty);
		}
		set
		{
			SetValue(HeaderTemplateSelectorProperty, value);
		}
	}

	public bool IsFullScreenMaximize
	{
		get
		{
			return (bool)GetValue(IsFullScreenMaximizeProperty);
		}
		set
		{
			SetValue(IsFullScreenMaximizeProperty, value);
		}
	}

	static CustomDialog()
	{
		ShowDefaultHeaderProperty = DependencyProperty.Register("ShowDefaultHeader", typeof(bool), typeof(CustomDialog), new FrameworkPropertyMetadata(true));
		ShowResizeGripProperty = DependencyProperty.Register("ShowResizeGrip", typeof(bool), typeof(CustomDialog), new FrameworkPropertyMetadata(true));
		CanCloseProperty = DependencyProperty.Register("CanClose", typeof(bool), typeof(CustomDialog), new FrameworkPropertyMetadata(true));
		CanResizeProperty = DependencyProperty.Register("CanResize", typeof(bool), typeof(CustomDialog), new FrameworkPropertyMetadata(true));
		HeaderProperty = DependencyProperty.Register("Header", typeof(object), typeof(CustomDialog), new FrameworkPropertyMetadata(null, OnheaderChanged));
		HeaderTemplateProperty = DependencyProperty.Register("HeaderTemplate", typeof(DataTemplate), typeof(CustomDialog), new FrameworkPropertyMetadata(null));
		HeaderTemplateSelectorProperty = DependencyProperty.Register("HeaderTemplateSelector", typeof(DataTemplateSelector), typeof(CustomDialog), new FrameworkPropertyMetadata(null));
		IsFullScreenMaximizeProperty = DependencyProperty.Register("IsFullScreenMaximize", typeof(bool), typeof(CustomDialog), new FrameworkPropertyMetadata(false));
		CloseWindowCommand = new RoutedUICommand("Close", "CloseWindow", typeof(CustomDialog));
		MaximizeWindowCommand = new RoutedUICommand("Maximize", "MaximizeWindow", typeof(CustomDialog));
		MinimizeWindowCommand = new RoutedUICommand("Minimize", "MinimizeWindow", typeof(CustomDialog));
		RestoreWindowCommand = new RoutedUICommand("Restore", "RestoreWindow", typeof(CustomDialog));
	}

	public CustomDialog()
	{
		base.Resources.MergedDictionaries.Add(new ResourceDictionary
		{
			Source = new Uri("/SamSoarII.Utility;component/Resources/ResourceDictionary/Themes.xaml", UriKind.Relative)
		});
		base.Style = (Style)base.Resources["CustomDialogStyle"];
		base.ShowInTaskbar = false;
	}

	private static void OnheaderChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
	{
		CustomDialog customDialog = sender as CustomDialog;
		customDialog.RemoveLogicalChild(e.OldValue);
		customDialog.AddLogicalChild(e.NewValue);
	}

	protected void ChangeWindowState(WindowState state)
	{
		if (state == WindowState.Maximized)
		{
			if (!IsFullScreenMaximize && IsLocationOnPrimaryScreen())
			{
				base.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
				base.MaxWidth = SystemParameters.MaximizedPrimaryScreenWidth;
			}
			else
			{
				base.MaxHeight = double.PositiveInfinity;
				base.MaxWidth = double.PositiveInfinity;
			}
		}
		base.WindowState = state;
	}

	private bool IsLocationOnPrimaryScreen()
	{
		return base.Left < SystemParameters.PrimaryScreenWidth && base.Top < SystemParameters.PrimaryScreenHeight;
	}

	protected override void OnInitialized(EventArgs e)
	{
		base.CommandBindings.Add(new CommandBinding(CloseWindowCommand, CommandBinding_Executed, CommandBinding_CanExecute));
		base.CommandBindings.Add(new CommandBinding(MaximizeWindowCommand, CommandBinding_Executed, CommandBinding_CanExecute));
		base.CommandBindings.Add(new CommandBinding(MinimizeWindowCommand, CommandBinding_Executed, CommandBinding_CanExecute));
		base.CommandBindings.Add(new CommandBinding(RestoreWindowCommand, CommandBinding_Executed, CommandBinding_CanExecute));
		base.OnInitialized(e);
	}

	protected virtual void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
	{
		bool flag = true;
		if (e.Command == CloseWindowCommand)
		{
			flag &= base.IsLoaded && base.IsVisible;
		}
		if (e.Command == MaximizeWindowCommand)
		{
			flag &= base.WindowState != WindowState.Maximized;
		}
		if (e.Command == MinimizeWindowCommand)
		{
			flag &= base.WindowState != WindowState.Minimized;
		}
		if (e.Command == RestoreWindowCommand)
		{
			flag &= base.WindowState != WindowState.Normal;
		}
		e.CanExecute = flag;
	}

	protected virtual void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
	{
		if (e.Command == CloseWindowCommand)
		{
			Close();
		}
		if (e.Command == MaximizeWindowCommand)
		{
			base.WindowState = WindowState.Maximized;
		}
		if (e.Command == MinimizeWindowCommand)
		{
			base.WindowState = WindowState.Minimized;
		}
		if (e.Command == RestoreWindowCommand)
		{
			base.WindowState = WindowState.Normal;
		}
	}
}

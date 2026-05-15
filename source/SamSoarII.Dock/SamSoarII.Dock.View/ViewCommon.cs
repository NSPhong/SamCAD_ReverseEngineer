using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using SamSoarII.Dock.Dock;
using SamSoarII.Dock.Float;
using SamSoarII.Dock.Interface;
using SamSoarII.Dock.View.Tab;

namespace SamSoarII.Dock.View;

public abstract class ViewCommon
{
	public enum BaseViewTypes
	{
		Null,
		Document,
		Anchor,
		Toolbar
	}

	public enum BaseViewRelations
	{
		Null,
		Top,
		Left,
		Bottom,
		Right,
		Inside,
		TopSide,
		LeftSide,
		BottomSide,
		RightSide,
		Float,
		TopAutoHide,
		LeftAutoHide,
		BottomAutoHide,
		RightAutoHide,
		DockAsDocument,
		TopFromStackPanel,
		LeftFromStackPanel,
		BottomFromStackPanel,
		RightFromStackPanel
	}

	public enum HeaderTypes
	{
		Null,
		AutoHide,
		Dock,
		Float,
		FloatMaximized,
		FloatInner
	}

	public enum HeaderButtonTypes
	{
		Null,
		Menu,
		AutoHide,
		Dock,
		Minimize,
		Maximize,
		Restore,
		Close
	}

	public enum TabGroupStyles
	{
		Single,
		Horizontal,
		Vertical
	}

	public static readonly Brush Black = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 0,
		G = 0,
		B = 0
	});

	public static readonly Brush White = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 252,
		G = 252,
		B = 252
	});

	public static readonly Brush Gray = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 172,
		G = 174,
		B = 187
	});

	public static readonly Brush DarkGray = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 172,
		G = 174,
		B = 187
	});

	public static readonly Brush Blue = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 0,
		G = 122,
		B = 204
	});

	public static readonly Brush LightBlue = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 28,
		G = 151,
		B = 234
	});

	private static readonly HeaderButtonTypes[][] headerbuttontypes = new HeaderButtonTypes[6][]
	{
		new HeaderButtonTypes[3],
		new HeaderButtonTypes[3]
		{
			HeaderButtonTypes.Menu,
			HeaderButtonTypes.Dock,
			HeaderButtonTypes.Close
		},
		new HeaderButtonTypes[3]
		{
			HeaderButtonTypes.Menu,
			HeaderButtonTypes.AutoHide,
			HeaderButtonTypes.Close
		},
		new HeaderButtonTypes[3]
		{
			HeaderButtonTypes.Menu,
			HeaderButtonTypes.Maximize,
			HeaderButtonTypes.Close
		},
		new HeaderButtonTypes[3]
		{
			HeaderButtonTypes.Menu,
			HeaderButtonTypes.Restore,
			HeaderButtonTypes.Close
		},
		new HeaderButtonTypes[3]
		{
			HeaderButtonTypes.Null,
			HeaderButtonTypes.Menu,
			HeaderButtonTypes.Close
		}
	};

	internal static FrameworkElement GetFrameworkElement(IDockBaseView baseview)
	{
		if (baseview?.DockContent is FrameworkElement)
		{
			return (FrameworkElement)baseview.DockContent;
		}
		if (baseview is FrameworkElement)
		{
			return (FrameworkElement)baseview;
		}
		return null;
	}

	internal static IDockCollection GetDockCollection(ref FrameworkElement fele)
	{
		FrameworkElement frameworkElement = fele;
		do
		{
			fele = frameworkElement;
			if (frameworkElement is IDockBaseView && ((IDockBaseView)frameworkElement).DockContainer?.ViewParent is DockTab)
			{
				frameworkElement = (DockTab)((IDockBaseView)frameworkElement).DockContainer.ViewParent;
			}
			else if (frameworkElement != null)
			{
				frameworkElement = (FrameworkElement)frameworkElement.Parent;
			}
		}
		while (frameworkElement != null && !(frameworkElement is IDockCollection));
		return (frameworkElement is IDockCollection) ? ((IDockCollection)frameworkElement) : null;
	}

	internal static ISideStackPanel GetSideStackPanel(ref FrameworkElement fele)
	{
		FrameworkElement frameworkElement = fele;
		do
		{
			fele = frameworkElement;
			if (frameworkElement is IDockBaseView && ((IDockBaseView)frameworkElement).DockContainer?.ViewParent is DockTab)
			{
				frameworkElement = (DockTab)((IDockBaseView)frameworkElement).DockContainer.ViewParent;
			}
			else if (frameworkElement != null)
			{
				frameworkElement = (FrameworkElement)frameworkElement.Parent;
			}
		}
		while (frameworkElement != null && !(frameworkElement is ISideStackPanel));
		return (frameworkElement is ISideStackPanel) ? ((ISideStackPanel)frameworkElement) : null;
	}

	internal static IDockView GetDockView(ref FrameworkElement fele)
	{
		FrameworkElement frameworkElement = fele;
		do
		{
			fele = frameworkElement;
			if (frameworkElement is IDockBaseView && ((IDockBaseView)frameworkElement).DockContainer?.ViewParent is DockTab)
			{
				frameworkElement = (DockTab)((IDockBaseView)frameworkElement).DockContainer.ViewParent;
			}
			else if (frameworkElement != null)
			{
				frameworkElement = (FrameworkElement)frameworkElement.Parent;
			}
		}
		while (frameworkElement != null && !(frameworkElement is IDockView));
		return (frameworkElement is IDockView) ? ((IDockView)frameworkElement) : null;
	}

	internal static IDockBaseView FirstOrDefault(FrameworkElement fele, Func<IDockBaseView, bool> func)
	{
		if (fele is FloatWindow)
		{
			FloatWindow floatWindow = (FloatWindow)fele;
			if (floatWindow.ViewContent == null)
			{
				return null;
			}
			if (func(floatWindow.ViewContent))
			{
				return floatWindow.ViewContent;
			}
			if (floatWindow.ViewContent is FrameworkElement)
			{
				return FirstOrDefault((FrameworkElement)floatWindow.ViewContent, func);
			}
		}
		if (fele is FloatStackPanel)
		{
			FloatStackPanel floatStackPanel = (FloatStackPanel)fele;
			return FirstOrDefault(floatStackPanel.ViewContent, func);
		}
		if (fele is FloatTab)
		{
			FloatTab floatTab = (FloatTab)fele;
			return FirstOrDefault(floatTab.ViewContent, func);
		}
		if (fele is DockStackPanel)
		{
			DockStackPanel dockStackPanel = (DockStackPanel)fele;
			foreach (FrameworkElement viewChild in dockStackPanel.ViewChildren)
			{
				IDockBaseView dockBaseView = FirstOrDefault(viewChild, func);
				if (dockBaseView != null)
				{
					return dockBaseView;
				}
			}
		}
		if (fele is DockTab)
		{
			DockTab dockTab = (DockTab)fele;
			return dockTab.ViewChildren.FirstOrDefault(func);
		}
		return null;
	}

	internal static bool HandleIn(FrameworkElement fele, Func<FrameworkElement, bool> handle)
	{
		Queue<FrameworkElement> queue = new Queue<FrameworkElement>();
		List<FrameworkElement> list = new List<FrameworkElement>();
		queue.Enqueue(fele);
		while (queue.Count() > 0)
		{
			fele = queue.Dequeue();
			if (fele is FloatStackPanel)
			{
				FloatStackPanel floatStackPanel = (FloatStackPanel)fele;
				queue.Enqueue(floatStackPanel.ViewContent);
			}
			else if (fele is FloatTab)
			{
				FloatTab floatTab = (FloatTab)fele;
				queue.Enqueue(floatTab.ViewContent);
			}
			else if (fele is DockStackPanel)
			{
				DockStackPanel dockStackPanel = (DockStackPanel)fele;
				foreach (FrameworkElement viewChild in dockStackPanel.ViewChildren)
				{
					if (viewChild is IDockBaseView)
					{
						list.Add(viewChild);
					}
					else
					{
						queue.Enqueue(viewChild);
					}
				}
			}
			else
			{
				if (!(fele is DockTab))
				{
					continue;
				}
				DockTab dockTab = (DockTab)fele;
				foreach (IDockBaseView viewChild2 in dockTab.ViewChildren)
				{
					if (viewChild2 is FrameworkElement)
					{
						list.Add((FrameworkElement)viewChild2);
					}
				}
			}
		}
		bool flag = true;
		foreach (FrameworkElement item in list)
		{
			flag &= handle(item);
		}
		return flag;
	}

	internal static bool HasType(FrameworkElement fele, BaseViewTypes type)
	{
		return FirstOrDefault(fele, (IDockBaseView i) => i.Type == type) != null;
	}

	internal static HeaderButtonTypes GetHeaderButtonType(HeaderTypes htype, int id)
	{
		return headerbuttontypes[(int)htype][id];
	}
}

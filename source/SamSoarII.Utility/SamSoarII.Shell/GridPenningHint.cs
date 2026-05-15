using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;

namespace SamSoarII.Shell;

public class GridPenningHint : Border, IComponentConnector
{
	private bool isdisposed = false;

	private GridPenning parent;

	private IGridPenningEntity core;

	private bool ishintx;

	private bool ishinty;

	private Rect actualrect;

	internal TextBlock TB_Text;

	private bool _contentLoaded;

	public bool IsDisposed => isdisposed;

	public GridPenning ViewParent => parent;

	public IGridPenningEntity Core
	{
		get
		{
			return core;
		}
		set
		{
			if (core != value)
			{
				core = value;
				Update();
			}
		}
	}

	public IGridPenningFunctionXY CoreXY => (core is IGridPenningFunctionXY) ? ((IGridPenningFunctionXY)core) : null;

	public IGridPenningFunctionYX CoreYX => (core is IGridPenningFunctionYX) ? ((IGridPenningFunctionYX)core) : null;

	public IGridPenningBoolRuler CoreBL => (core is IGridPenningBoolRuler) ? ((IGridPenningBoolRuler)core) : null;

	public bool IsHintX
	{
		get
		{
			return ishintx;
		}
		set
		{
			ishintx = value;
			Update();
		}
	}

	public bool IsHintY
	{
		get
		{
			return ishinty;
		}
		set
		{
			ishinty = value;
			Update();
		}
	}

	public bool IsHint => core != null && ((ishintx && parent.IsHintX) || (ishinty && parent.IsHintY));

	public Rect ActualRect => actualrect;

	public GridPenningHint(GridPenning _parent)
	{
		InitializeComponent();
		parent = _parent;
		ishintx = true;
		ishinty = true;
		actualrect = new Rect(0.0, 0.0, 100.0, 100.0);
		parent.PropertyChanged += OnParentPropertyChanged;
		Update();
	}

	public void Dispose()
	{
		if (!isdisposed)
		{
			isdisposed = true;
			parent.PropertyChanged -= OnParentPropertyChanged;
			parent = null;
		}
	}

	public void SetPosition(double x, double y)
	{
		Canvas.SetTop(this, y);
		Canvas.SetLeft(this, x);
		actualrect.X = x;
		actualrect.Y = y;
	}

	public void Update()
	{
		base.Visibility = ((!IsHint) ? Visibility.Hidden : Visibility.Visible);
		if (IsHint)
		{
			if (CoreXY != null)
			{
				string y = "???";
				CoreXY.GetYString(parent.HintX, ref y, 0);
				TB_Text.Text = $"{CoreXY.Name:s} = {y:s}";
			}
			if (CoreBL != null)
			{
				bool y2 = false;
				CoreBL.GetY(parent.HintX, ref y2, 0);
				TB_Text.Text = string.Format("{0:s} = {1:s}", CoreBL.Name, y2 ? "ON" : "OFF");
			}
			if (Core != null)
			{
				uint num = GridPenningPanel.RGBA_Pens[Core.ColorID];
				TB_Text.Foreground = new SolidColorBrush(new Color
				{
					A = (byte)((num >> 24) & 0xFF),
					R = (byte)((num >> 16) & 0xFF),
					G = (byte)((num >> 8) & 0xFF),
					B = (byte)(num & 0xFF)
				});
			}
		}
	}

	private void OnParentPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		switch (e.PropertyName)
		{
		case "IsHintX":
		case "IsHintY":
		case "HintX":
		case "HintY":
			Update();
			break;
		}
	}

	protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
	{
		base.OnRenderSizeChanged(sizeInfo);
		actualrect.Width = sizeInfo.NewSize.Width;
		actualrect.Height = sizeInfo.NewSize.Height;
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamSoarII.Utility;component/controls/gridpenning/gridpenninghint.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		if (connectionId == 1)
		{
			TB_Text = (TextBlock)target;
		}
		else
		{
			_contentLoaded = true;
		}
	}
}

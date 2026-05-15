using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SamSoarII.Polyline;
using SamSoarII.Polyline.Entity;

namespace SamCAD.Control;

public class GroupReorderListView : UserControl, IComponentConnector
{
	protected static readonly ImageSource Image_Move = new BitmapImage(new Uri("pack://application:,,,/SamCAD;component/Resources/Image/Move.png"));

	protected static readonly ImageSource Image_Auto = new BitmapImage(new Uri("pack://application:,,,/SamCAD;component/Resources/Image/Auto.png"));

	protected static Brush Foreground_GroupName = new SolidColorBrush(new Color
	{
		A = byte.MaxValue,
		R = 66,
		G = 130,
		B = 106
	});

	internal Image IM_Icon;

	internal TextBlock TB_Text;

	private bool _contentLoaded;

	public IPolylineReorderingGroup Group => (base.DataContext is IPolylineReorderingGroup) ? ((IPolylineReorderingGroup)base.DataContext) : null;

	public ReorderingStrategy Stratery => (base.DataContext is ReorderingStrategy) ? ((ReorderingStrategy)base.DataContext) : ReorderingStrategy.None;

	public GroupReorderListView()
	{
		InitializeComponent();
	}

	protected void UpdateText()
	{
		if (Group != null)
		{
			TB_Text.Text = string.Empty;
			TB_Text.Inlines.Clear();
			TB_Text.Inlines.Add(new Run
			{
				Text = "will"
			});
			TB_Text.Inlines.Add(new Run
			{
				Text = Group.Name,
				Foreground = Foreground_GroupName,
				FontWeight = FontWeights.Bold
			});
			TB_Text.Inlines.Add(new Run
			{
				Text = "Move to"
			});
			TB_Text.Inlines.Add(new Run
			{
				Text = Group.NewGID.ToString(),
				Foreground = Foreground_GroupName,
				FontWeight = FontWeights.Bold
			});
			TB_Text.Inlines.Add(new Run
			{
				Text = "item"
			});
		}
	}

	protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		base.OnPropertyChanged(e);
		if (e.Property == FrameworkElement.DataContextProperty)
		{
			if (e.OldValue is IPolylineReorderingGroup)
			{
				IPolylineReorderingGroup polylineReorderingGroup = (IPolylineReorderingGroup)e.OldValue;
				polylineReorderingGroup.PropertyChanged -= OnGroupPropertyChanged;
			}
			if (e.NewValue is IPolylineReorderingGroup)
			{
				IPolylineReorderingGroup polylineReorderingGroup2 = (IPolylineReorderingGroup)e.NewValue;
				polylineReorderingGroup2.PropertyChanged += OnGroupPropertyChanged;
			}
			if (Group != null)
			{
				IM_Icon.Source = Image_Move;
				UpdateText();
			}
			else if (Stratery == ReorderingStrategy.MinimizeLength)
			{
				IM_Icon.Source = Image_Auto;
				TB_Text.Text = "Dotted line最短化";
			}
			else if (Stratery == ReorderingStrategy.FlatCorners)
			{
				IM_Icon.Source = Image_Auto;
				TB_Text.Text = "Dotted line拐角平滑化";
			}
		}
	}

	private void OnGroupPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		string propertyName = e.PropertyName;
		string text = propertyName;
		if (text == "Name" || text == "NewGID")
		{
			UpdateText();
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamCAD;component/control/groupreorderlistview.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		switch (connectionId)
		{
		case 1:
			IM_Icon = (Image)target;
			break;
		case 2:
			TB_Text = (TextBlock)target;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}

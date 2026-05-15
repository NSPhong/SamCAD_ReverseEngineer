using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using SamSoarII.Dock.Interface;
using SamSoarII.Polyline.Arguments;
using SamSoarII.Polyline.Entity;

namespace SamSoarII.Polyline.Control;

public class PolylineArgumentWindow : UserControl, IDockContent, INotifyPropertyChanged, IComponentConnector
{
	protected static readonly DependencyProperty ProjectProperty = DependencyProperty.Register("Project", typeof(IPolylineProject), typeof(PolylineArgumentWindow), new PropertyMetadata(null, OnPropertyChanged_Project));

	protected static readonly DependencyProperty SelectedImageProperty = DependencyProperty.Register("SelectedImage", typeof(IPolylineImage), typeof(PolylineArgumentWindow), new PropertyMetadata(null, OnPropertyChanged_SelectedImage));

	protected static readonly DependencyProperty SelectedEntityProperty = DependencyProperty.Register("SelectedEntity", typeof(IPolylineEntity), typeof(PolylineArgumentWindow), new PropertyMetadata(null, OnPropertyChanged_SelectedEntity));

	private IPolylineArgument oldarg;

	internal PolylineArgumentWindow This;

	internal TextBox TX_Name;

	internal PointWidget UIP_To;

	internal PointWidget UIP_Center;

	internal PointWidget UIP_Direct;

	internal TextBox TX_Radius;

	internal TextBox TX_LongRadius;

	internal TextBox TX_ShortRadius;

	internal CheckBox CK_Real;

	internal CheckBox CK_Clock;

	internal Expander EP_HMIPLINE;

	internal ComboBox CB_Cond;

	internal TextBox TB_Value;

	internal TextBox TB_Run;

	internal TextBox TB_End;

	internal TextBox TB_EndSrc;

	internal TextBox TB_EndDst;

	internal TextBox TB_Velo;

	internal TextBox TB_Acce;

	internal TextBox TB_Dece;

	internal TextBox TB_XOfs;

	internal TextBox TB_YOfs;

	internal Button BN_Multi;

	internal Button BN_Undo;

	private bool _contentLoaded;

	ushort IDockContent.DockID => 14;

	string IDockContent.Header => "Graphic element attribute";

	ImageSource IDockContent.Icon => null;

	public IPolylineProject Project
	{
		get
		{
			return (IPolylineProject)GetValue(ProjectProperty);
		}
		set
		{
			SetValue(ProjectProperty, value);
		}
	}

	public IPolylineImage SelectedImage
	{
		get
		{
			return (IPolylineImage)GetValue(SelectedImageProperty);
		}
		set
		{
			SetValue(SelectedImageProperty, value);
		}
	}

	public IPolylineEntity SelectedEntity
	{
		get
		{
			return (IPolylineEntity)GetValue(SelectedEntityProperty);
		}
		set
		{
			SetValue(SelectedEntityProperty, value);
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public event RoutedEventHandler MultiModify;

	public event PolylineActionEventHandler EntityChanged;

	public PolylineArgumentWindow()
	{
		InitializeComponent();
	}

	private static void OnPropertyChanged_Project(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineArgumentWindow)
		{
			((PolylineArgumentWindow)d).OnProjectChanged(e);
		}
	}

	protected virtual void OnProjectChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_SelectedImage(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineArgumentWindow)
		{
			((PolylineArgumentWindow)d).OnSelctedImageChanged(e);
		}
	}

	protected virtual void OnSelctedImageChanged(DependencyPropertyChangedEventArgs e)
	{
	}

	private static void OnPropertyChanged_SelectedEntity(DependencyObject d, DependencyPropertyChangedEventArgs e)
	{
		if (d is PolylineArgumentWindow)
		{
			((PolylineArgumentWindow)d).OnSelctedEntityChanged(e);
		}
	}

	protected virtual void OnSelctedEntityChanged(DependencyPropertyChangedEventArgs e)
	{
		oldarg = SelectedEntity?.Argument?.Clone();
		TextBox tX_Name = TX_Name;
		IPolylineEntity selectedEntity = SelectedEntity;
		tX_Name.Text = ((selectedEntity != null && selectedEntity.HasRealName()) ? SelectedEntity.Name : string.Empty);
		EP_HMIPLINE.Visibility = ((!(SelectedEntity?.Argument is IHMIPLINEArgument)) ? Visibility.Collapsed : Visibility.Visible);
	}

	public void Submit()
	{
		UIP_To.Submit();
		UIP_Center.Submit();
	}

	protected void InvokePropertyChanged(string propname)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propname));
	}

	protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
	{
		base.OnPropertyChanged(e);
		if (e.Property == FrameworkElement.DataContextProperty && e.NewValue is IPolylineEntity)
		{
			IPolylineEntity polylineEntity = (IPolylineEntity)e.NewValue;
			oldarg = polylineEntity.Argument.Clone();
		}
	}

	private void TextBox_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
	{
		if (sender is TextBox)
		{
			TextBox textBox = (TextBox)sender;
			textBox.SelectAll();
		}
	}

	private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
	{
		switch (e.Key)
		{
		case Key.Return:
		case Key.Down:
			e.Handled = true;
			if (sender == TB_Value)
			{
				Keyboard.Focus(TB_Run);
			}
			if (sender == TB_Run)
			{
				Keyboard.Focus(TB_End);
			}
			if (sender == TB_End)
			{
				Keyboard.Focus(TB_Velo);
			}
			if (sender == TB_Velo)
			{
				Keyboard.Focus(TB_Acce);
			}
			if (sender == TB_Acce)
			{
				Keyboard.Focus(TB_Dece);
			}
			if (sender == TB_Dece)
			{
				Keyboard.Focus(TB_XOfs);
			}
			if (sender == TB_XOfs)
			{
				Keyboard.Focus(TB_YOfs);
			}
			if (sender == TB_YOfs)
			{
				Keyboard.Focus(TB_Value);
			}
			break;
		case Key.Up:
			e.Handled = true;
			if (sender == TB_Value)
			{
				Keyboard.Focus(TB_YOfs);
			}
			if (sender == TB_Run)
			{
				Keyboard.Focus(TB_Value);
			}
			if (sender == TB_End)
			{
				Keyboard.Focus(TB_Run);
			}
			if (sender == TB_Velo)
			{
				Keyboard.Focus(TB_End);
			}
			if (sender == TB_Acce)
			{
				Keyboard.Focus(TB_Velo);
			}
			if (sender == TB_Dece)
			{
				Keyboard.Focus(TB_Acce);
			}
			if (sender == TB_XOfs)
			{
				Keyboard.Focus(TB_Dece);
			}
			if (sender == TB_YOfs)
			{
				Keyboard.Focus(TB_XOfs);
			}
			break;
		}
	}

	private void BN_Multi_Click(object sender, RoutedEventArgs e)
	{
		if (MessageBox.Show("要will所有选中图元统一更改为当前的工艺参数吗？", "Batch modification", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
		{
			this.MultiModify?.Invoke(this, new RoutedEventArgs());
		}
	}

	private void BN_Undo_Click(object sender, RoutedEventArgs e)
	{
		SelectedEntity?.Argument?.Load(oldarg);
	}

	private void PointWidget_PointChanged(object sender, PointWidgetEventArgs e)
	{
		if (SelectedEntity == null)
		{
			return;
		}
		IPolylineAction action = null;
		Point oldPoint = e.OldPoint;
		Point newPoint = e.NewPoint;
		if (!SelectedImage.IsEqualP(oldPoint, newPoint))
		{
			if (sender == UIP_To)
			{
				action = new PolylineAction(SelectedEntity.ID, ChangedTarget.To, e.Flag, oldPoint, newPoint);
			}
			if (sender == UIP_Center)
			{
				action = new PolylineAction(SelectedEntity.ID, ChangedTarget.Center, e.Flag, oldPoint, newPoint);
			}
			if (sender == UIP_Direct)
			{
				action = new PolylineAction(SelectedEntity.ID, ChangedTarget.Direction, oldPoint, newPoint);
			}
			PolylineActionEventArgs e2 = new PolylineActionEventArgs(SelectedImage, action);
			this.EntityChanged?.Invoke(this, e2);
		}
	}

	private void CheckBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
	{
		if (sender is CheckBox)
		{
			CheckBox checkBox = (CheckBox)sender;
			IPolylineAction action = null;
			bool flag = checkBox.IsChecked == true;
			bool flag2 = !flag;
			e.Handled = true;
			checkBox.IsChecked = flag2;
			if (sender == CK_Real)
			{
				action = new PolylineAction(SelectedEntity.ID, ChangedTarget.IsReal, flag, flag2);
			}
			if (sender == CK_Clock)
			{
				action = new PolylineAction(SelectedEntity.ID, ChangedTarget.IsClockwise, flag, flag2);
			}
			PolylineActionEventArgs e2 = new PolylineActionEventArgs(SelectedImage, action);
			this.EntityChanged?.Invoke(this, e2);
		}
	}

	private void TX_Radius_PreviewKeyDown(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Return)
		{
			IPolylineAction polylineAction = null;
			double num = ((SelectedEntity is IPolylineCircle) ? ((IPolylineCircle)SelectedEntity).Radius : ((SelectedEntity is IPolylineEllipse && sender == TX_LongRadius) ? ((IPolylineEllipse)SelectedEntity).LongRadius : ((SelectedEntity is IPolylineEllipse && sender == TX_ShortRadius) ? ((IPolylineEllipse)SelectedEntity).ShortRadius : 0.0)));
			double result = num;
			e.Handled = true;
			if (SelectedEntity is IPolylineCircle && double.TryParse(TX_Radius.Text, out result))
			{
				IPolylineCircle polylineCircle = (IPolylineCircle)SelectedEntity;
				polylineCircle.Radius = result;
				polylineAction = new PolylineAction(SelectedEntity.ID, ChangedTarget.Radius, num, result);
				PolylineActionEventArgs e2 = new PolylineActionEventArgs(SelectedImage, polylineAction);
				this.EntityChanged?.Invoke(this, e2);
			}
			else if (SelectedEntity is IPolylineEllipse && sender == TX_LongRadius && double.TryParse(TX_LongRadius.Text, out result))
			{
				IPolylineEllipse polylineEllipse = (IPolylineEllipse)SelectedEntity;
				polylineEllipse.LongRadius = result;
				polylineAction = new PolylineAction(SelectedEntity.ID, ChangedTarget.LongRadius, num, result);
				PolylineActionEventArgs e3 = new PolylineActionEventArgs(SelectedImage, polylineAction);
				this.EntityChanged?.Invoke(this, e3);
			}
			else if (SelectedEntity is IPolylineEllipse && sender == TX_ShortRadius && double.TryParse(TX_ShortRadius.Text, out result))
			{
				IPolylineEllipse polylineEllipse2 = (IPolylineEllipse)SelectedEntity;
				polylineEllipse2.ShortRadius = result;
				polylineAction = new PolylineAction(SelectedEntity.ID, ChangedTarget.ShortRadius, num, result);
				PolylineActionEventArgs e4 = new PolylineActionEventArgs(SelectedImage, polylineAction);
				this.EntityChanged?.Invoke(this, e4);
			}
		}
	}

	private void TX_Radius_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
	{
		IPolylineAction polylineAction = null;
		double num = ((SelectedEntity is IPolylineCircle) ? ((IPolylineCircle)SelectedEntity).Radius : ((SelectedEntity is IPolylineEllipse && sender == TX_LongRadius) ? ((IPolylineEllipse)SelectedEntity).LongRadius : ((SelectedEntity is IPolylineEllipse && sender == TX_ShortRadius) ? ((IPolylineEllipse)SelectedEntity).ShortRadius : 0.0)));
		double result = num;
		if (SelectedEntity is IPolylineCircle && double.TryParse(TX_Radius.Text, out result))
		{
			IPolylineCircle polylineCircle = (IPolylineCircle)SelectedEntity;
			polylineCircle.Radius = result;
			polylineAction = new PolylineAction(SelectedEntity.ID, ChangedTarget.Radius, num, result);
			PolylineActionEventArgs e2 = new PolylineActionEventArgs(SelectedImage, polylineAction);
			this.EntityChanged?.Invoke(this, e2);
		}
		else if (SelectedEntity is IPolylineEllipse && sender == TX_LongRadius && double.TryParse(TX_LongRadius.Text, out result))
		{
			IPolylineEllipse polylineEllipse = (IPolylineEllipse)SelectedEntity;
			polylineEllipse.LongRadius = result;
			polylineAction = new PolylineAction(SelectedEntity.ID, ChangedTarget.LongRadius, num, result);
			PolylineActionEventArgs e3 = new PolylineActionEventArgs(SelectedImage, polylineAction);
			this.EntityChanged?.Invoke(this, e3);
		}
		else if (SelectedEntity is IPolylineEllipse && sender == TX_ShortRadius && double.TryParse(TX_ShortRadius.Text, out result))
		{
			IPolylineEllipse polylineEllipse2 = (IPolylineEllipse)SelectedEntity;
			polylineEllipse2.ShortRadius = result;
			polylineAction = new PolylineAction(SelectedEntity.ID, ChangedTarget.ShortRadius, num, result);
			PolylineActionEventArgs e4 = new PolylineActionEventArgs(SelectedImage, polylineAction);
			this.EntityChanged?.Invoke(this, e4);
		}
	}

	private void TX_Name_PreviewKeyDown(object sender, KeyEventArgs e)
	{
		if (e.Key == Key.Return)
		{
			e.Handled = true;
			if (SelectedEntity != null)
			{
				SelectedEntity.Name = TX_Name.Text;
			}
		}
	}

	private void TX_Name_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
	{
		if (SelectedEntity != null)
		{
			SelectedEntity.Name = TX_Name.Text;
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	public void InitializeComponent()
	{
		if (!_contentLoaded)
		{
			_contentLoaded = true;
			Uri resourceLocator = new Uri("/SamSoarII.Polyline;component/control/polylineargumentwindow.xaml", UriKind.Relative);
			Application.LoadComponent(this, resourceLocator);
		}
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	internal Delegate _CreateDelegate(Type delegateType, string handler)
	{
		return Delegate.CreateDelegate(delegateType, this, handler);
	}

	[DebuggerNonUserCode]
	[GeneratedCode("PresentationBuildTasks", "4.0.0.0")]
	[EditorBrowsable(EditorBrowsableState.Never)]
	void IComponentConnector.Connect(int connectionId, object target)
	{
		switch (connectionId)
		{
		case 1:
			This = (PolylineArgumentWindow)target;
			break;
		case 2:
			TX_Name = (TextBox)target;
			TX_Name.PreviewKeyDown += TX_Name_PreviewKeyDown;
			TX_Name.LostKeyboardFocus += TX_Name_LostKeyboardFocus;
			break;
		case 3:
			UIP_To = (PointWidget)target;
			break;
		case 4:
			UIP_Center = (PointWidget)target;
			break;
		case 5:
			UIP_Direct = (PointWidget)target;
			break;
		case 6:
			TX_Radius = (TextBox)target;
			TX_Radius.PreviewKeyDown += TX_Radius_PreviewKeyDown;
			TX_Radius.LostKeyboardFocus += TX_Radius_LostKeyboardFocus;
			break;
		case 7:
			TX_LongRadius = (TextBox)target;
			TX_LongRadius.PreviewKeyDown += TX_Radius_PreviewKeyDown;
			TX_LongRadius.LostKeyboardFocus += TX_Radius_LostKeyboardFocus;
			break;
		case 8:
			TX_ShortRadius = (TextBox)target;
			TX_ShortRadius.PreviewKeyDown += TX_Radius_PreviewKeyDown;
			TX_ShortRadius.LostKeyboardFocus += TX_Radius_LostKeyboardFocus;
			break;
		case 9:
			CK_Real = (CheckBox)target;
			CK_Real.PreviewMouseDown += CheckBox_PreviewMouseDown;
			break;
		case 10:
			CK_Clock = (CheckBox)target;
			CK_Clock.PreviewMouseDown += CheckBox_PreviewMouseDown;
			break;
		case 11:
			EP_HMIPLINE = (Expander)target;
			break;
		case 12:
			CB_Cond = (ComboBox)target;
			break;
		case 13:
			TB_Value = (TextBox)target;
			TB_Value.GotKeyboardFocus += TextBox_GotKeyboardFocus;
			TB_Value.PreviewKeyDown += TextBox_PreviewKeyDown;
			break;
		case 14:
			TB_Run = (TextBox)target;
			TB_Run.GotKeyboardFocus += TextBox_GotKeyboardFocus;
			TB_Run.PreviewKeyDown += TextBox_PreviewKeyDown;
			break;
		case 15:
			TB_End = (TextBox)target;
			TB_End.GotKeyboardFocus += TextBox_GotKeyboardFocus;
			TB_End.PreviewKeyDown += TextBox_PreviewKeyDown;
			break;
		case 16:
			TB_EndSrc = (TextBox)target;
			TB_EndSrc.GotKeyboardFocus += TextBox_GotKeyboardFocus;
			TB_EndSrc.PreviewKeyDown += TextBox_PreviewKeyDown;
			break;
		case 17:
			TB_EndDst = (TextBox)target;
			TB_EndDst.GotKeyboardFocus += TextBox_GotKeyboardFocus;
			TB_EndDst.PreviewKeyDown += TextBox_PreviewKeyDown;
			break;
		case 18:
			TB_Velo = (TextBox)target;
			TB_Velo.GotKeyboardFocus += TextBox_GotKeyboardFocus;
			TB_Velo.PreviewKeyDown += TextBox_PreviewKeyDown;
			break;
		case 19:
			TB_Acce = (TextBox)target;
			TB_Acce.GotKeyboardFocus += TextBox_GotKeyboardFocus;
			TB_Acce.PreviewKeyDown += TextBox_PreviewKeyDown;
			break;
		case 20:
			TB_Dece = (TextBox)target;
			TB_Dece.GotKeyboardFocus += TextBox_GotKeyboardFocus;
			TB_Dece.PreviewKeyDown += TextBox_PreviewKeyDown;
			break;
		case 21:
			TB_XOfs = (TextBox)target;
			TB_XOfs.GotKeyboardFocus += TextBox_GotKeyboardFocus;
			TB_XOfs.PreviewKeyDown += TextBox_PreviewKeyDown;
			break;
		case 22:
			TB_YOfs = (TextBox)target;
			TB_YOfs.GotKeyboardFocus += TextBox_GotKeyboardFocus;
			TB_YOfs.PreviewKeyDown += TextBox_PreviewKeyDown;
			break;
		case 23:
			BN_Multi = (Button)target;
			BN_Multi.Click += BN_Multi_Click;
			break;
		case 24:
			BN_Undo = (Button)target;
			BN_Undo.Click += BN_Undo_Click;
			break;
		default:
			_contentLoaded = true;
			break;
		}
	}
}

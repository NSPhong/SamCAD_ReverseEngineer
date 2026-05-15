using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SamSoarII.Shell;

public class CollectionStackPanel : StackPanel
{
	public static readonly DependencyProperty ItemsSourceProperty;

	public static readonly DependencyProperty SelectItemProperty;

	public IEnumerable<TextBlock> ItemsSource
	{
		get
		{
			return (IEnumerable<TextBlock>)GetValue(ItemsSourceProperty);
		}
		set
		{
			SetValue(ItemsSourceProperty, value);
		}
	}

	public TextBlock SelectItem
	{
		get
		{
			return (TextBlock)GetValue(SelectItemProperty);
		}
		set
		{
			SetValue(SelectItemProperty, value);
		}
	}

	static CollectionStackPanel()
	{
		FrameworkPropertyMetadata frameworkPropertyMetadata = new FrameworkPropertyMetadata();
		frameworkPropertyMetadata.PropertyChangedCallback = (PropertyChangedCallback)Delegate.Combine(frameworkPropertyMetadata.PropertyChangedCallback, new PropertyChangedCallback(OnItemsSourcePropertyChanged));
		ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(IEnumerable<TextBlock>), typeof(CollectionStackPanel), frameworkPropertyMetadata);
		FrameworkPropertyMetadata frameworkPropertyMetadata2 = new FrameworkPropertyMetadata();
		frameworkPropertyMetadata2.PropertyChangedCallback = (PropertyChangedCallback)Delegate.Combine(frameworkPropertyMetadata2.PropertyChangedCallback, new PropertyChangedCallback(OnSelectItemPropertyChanged));
		SelectItemProperty = DependencyProperty.Register("SelectItem", typeof(TextBlock), typeof(CollectionStackPanel), frameworkPropertyMetadata2);
	}

	public static void OnItemsSourcePropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
	{
		CollectionStackPanel collectionStackPanel = source as CollectionStackPanel;
		collectionStackPanel.Children.Clear();
		foreach (TextBlock item in collectionStackPanel.ItemsSource)
		{
			collectionStackPanel.Children.Add(item);
		}
	}

	public static void OnSelectItemPropertyChanged(DependencyObject source, DependencyPropertyChangedEventArgs args)
	{
		CollectionStackPanel collectionStackPanel = source as CollectionStackPanel;
		if (collectionStackPanel.SelectItem != null)
		{
			TextBlock selectItem = collectionStackPanel.SelectItem;
			Color color = new Color
			{
				A = byte.MaxValue,
				R = 26,
				G = 134,
				B = 243
			};
			selectItem.Background = new SolidColorBrush(color);
			color.A = byte.MaxValue;
			color.R = byte.MaxValue;
			color.G = byte.MaxValue;
			color.B = byte.MaxValue;
			selectItem.Foreground = new SolidColorBrush(color);
			selectItem.FontWeight = FontWeights.Heavy;
		}
	}
}

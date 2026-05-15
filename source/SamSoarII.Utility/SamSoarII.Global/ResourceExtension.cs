using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using SamSoarII.Properties;

namespace SamSoarII.Global;

public class ResourceExtension : MarkupExtension, INotifyPropertyChanged
{
	[ConstructorArgument("Key")]
	public string Key { get; set; }

	public string Value => Resources.ResourceManager.GetString(Key, Resources.Culture);

	public static event EventHandler ResourceLanaguageChanged;

	public event PropertyChangedEventHandler PropertyChanged = delegate
	{
	};

	public ResourceExtension()
	{
		ResourceLanaguageChanged += LanaguageChanged;
	}

	public ResourceExtension(string key)
		: this()
	{
		Key = key;
	}

	private void LanaguageChanged(object sender, EventArgs e)
	{
		this.PropertyChanged(this, new PropertyChangedEventArgs("Value"));
	}

	public static void RaiseLanaguageChangedEvent()
	{
		ResourceExtension.ResourceLanaguageChanged(null, new EventArgs());
	}

	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		IProvideValueTarget provideValueTarget = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
		if (provideValueTarget.TargetObject is Setter)
		{
			return new Binding("Value")
			{
				Source = this,
				Mode = BindingMode.OneWay
			};
		}
		Binding binding = new Binding("Value")
		{
			Source = this,
			Mode = BindingMode.OneWay
		};
		return binding.ProvideValue(serviceProvider);
	}

	static ResourceExtension()
	{
		ResourceExtension.ResourceLanaguageChanged = delegate
		{
		};
	}
}

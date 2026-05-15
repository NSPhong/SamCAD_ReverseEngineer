using System.ComponentModel;

namespace SamSoarII.Core.Print;

public class FontEx : INotifyPropertyChanged
{
	private Enum_PrintFont e;

	private float marginup;

	private float margindown;

	private float marginleft;

	public Enum_PrintFont E => e;

	public float MarginUp
	{
		get
		{
			return marginup;
		}
		set
		{
			if (marginup != value)
			{
				marginup = value;
				IvProp("MarginUp");
			}
		}
	}

	public float MarginDown
	{
		get
		{
			return margindown;
		}
		set
		{
			if (margindown != value)
			{
				margindown = value;
				IvProp("MarginDown");
			}
		}
	}

	public float MarginLeft
	{
		get
		{
			return marginleft;
		}
		set
		{
			if (marginleft != value)
			{
				marginleft = value;
				IvProp("MarginLeft");
			}
		}
	}

	public event PropertyChangedEventHandler PropertyChanged;

	public FontEx(Enum_PrintFont _e)
	{
		e = _e;
		marginup = 0f;
		margindown = 0f;
		marginleft = 0f;
	}

	public override string ToString()
	{
		return e switch
		{
			Enum_PrintFont.Default => "Main text", 
			Enum_PrintFont.Title => "Main title", 
			Enum_PrintFont.Title2 => "Subtitle", 
			Enum_PrintFont.Comment => "Program comment", 
			Enum_PrintFont.Unit => "Ladder diagram", 
			Enum_PrintFont.FuncBlock => "Function function block", 
			Enum_PrintFont.Modbus => "Modbus form", 
			Enum_PrintFont.ValueList => "Table of Soft Components", 
			Enum_PrintFont.PlsTable => "Pulse parameter table", 
			Enum_PrintFont.ChartRuler => "Chart ruler", 
			Enum_PrintFont.PageUp => "Header", 
			Enum_PrintFont.PageDown => "Footer/page number", 
			Enum_PrintFont.CoverTitle => "Cover title", 
			Enum_PrintFont.CoverTitle2 => "Cover Author/Date", 
			Enum_PrintFont.Menu => "Directory", 
			_ => e.ToString(), 
		};
	}

	protected void IvProp(string name)
	{
		this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
	}
}

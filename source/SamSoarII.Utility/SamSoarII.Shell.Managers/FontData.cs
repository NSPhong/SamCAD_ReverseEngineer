using System;
using System.ComponentModel;
using System.Windows.Media;
using SamSoarII.Properties;
using SamSoarII.Utility;

namespace SamSoarII.Shell.Managers;

public class FontData : INotifyPropertyChanged
{
	private FontType type;

	private IntRange fontRange;

	private uint fontsize;

	private FontFamily fontfamily;

	private Brush fontcolor;

	public FontType Type
	{
		get
		{
			return type;
		}
		set
		{
			type = value;
			this.PropertyChanged(this, new PropertyChangedEventArgs("Type"));
			this.PropertyChanged(this, new PropertyChangedEventArgs("Name"));
		}
	}

	public string Name => type switch
	{
		FontType.Title => Resources.Ladder_Title, 
		FontType.Ladder => Resources.Element, 
		FontType.Func => Resources.FuncBlock, 
		FontType.Comment => Resources.Comment, 
		FontType.Text_Title => Resources.Ladder_Title, 
		FontType.Text_Ladder => Resources.Element, 
		FontType.Text_Comment => Resources.Comment, 
		FontType.FBD_Network => Resources.Text_FBDNetwork, 
		FontType.FBD_Value => Resources.Text_FBDValue, 
		FontType.FBD_Comment => Resources.Text_FBDComment, 
		FontType.FBD_Table => Resources.Text_FBDTable, 
		FontType.FBD_Monitor => Resources.Text_FBDMonitor, 
		_ => string.Empty, 
	};

	public IntRange FontRange => fontRange;

	public uint FontSize
	{
		get
		{
			return fontsize;
		}
		set
		{
			uint val = value;
			val = Math.Max(val, fontRange.Start);
			val = Math.Min(val, fontRange.End - 1);
			fontsize = val;
			this.PropertyChanged(this, new PropertyChangedEventArgs("FontSize"));
		}
	}

	public FontFamily FontFamily
	{
		get
		{
			return fontfamily;
		}
		set
		{
			fontfamily = value;
			this.PropertyChanged(this, new PropertyChangedEventArgs("FontFamily"));
		}
	}

	public Brush FontColor
	{
		get
		{
			return fontcolor;
		}
		set
		{
			fontcolor = value;
			this.PropertyChanged(this, new PropertyChangedEventArgs("FontColor"));
		}
	}

	public event PropertyChangedEventHandler PropertyChanged = delegate
	{
	};

	public FontData(FontType type)
	{
		Type = type;
		switch (type)
		{
		case FontType.Title:
			fontRange = new IntRange(40u, 65u);
			break;
		case FontType.Ladder:
			fontRange = new IntRange(35u, 65u);
			break;
		case FontType.Comment:
			fontRange = new IntRange(25u, 65u);
			break;
		case FontType.Func:
			fontRange = new IntRange(10u, 36u);
			break;
		case FontType.Text_Title:
		case FontType.Text_Ladder:
		case FontType.Text_Comment:
			fontRange = new IntRange(8u, 18u);
			break;
		case FontType.FBD_Value:
		case FontType.FBD_Comment:
		case FontType.FBD_Table:
		case FontType.FBD_Monitor:
			fontRange = new IntRange(6u, 20u);
			break;
		case FontType.FBD_Network:
			fontRange = new IntRange(6u, 24u);
			break;
		default:
			fontRange = default(IntRange);
			break;
		}
	}

	public void Setup(FontData that)
	{
		Type = that.Type;
		FontSize = that.FontSize;
		FontFamily = that.FontFamily;
		FontColor = that.FontColor;
	}
}

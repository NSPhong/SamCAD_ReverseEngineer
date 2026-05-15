using System;
using System.Drawing;
using System.Xml.Linq;

namespace SamSoarII.Core.Print;

public class PrintFontData
{
	private string familyname;

	private float fontsize;

	private FontStyle fontstyle;

	private Color color;

	private float marginup;

	private float margindown;

	private float marginleft;

	public string FamliyName => familyname;

	public float FontSize => fontsize;

	public FontStyle FontStyle => fontstyle;

	public Color Color => color;

	public float MarginUp => marginup;

	public float MarginDown => margindown;

	public float MarginLeft => marginleft;

	public PrintFontData()
		: this(SystemFonts.DefaultFont, Brushes.Black, new FontEx(Enum_PrintFont.Default))
	{
	}

	public PrintFontData(Font _font, Brush _brush, FontEx _fontex)
	{
		familyname = _font.FontFamily.Name;
		fontsize = _font.Size;
		fontstyle = _font.Style;
		color = ((_brush is SolidBrush) ? ((SolidBrush)_brush).Color : default(Color));
		marginup = _fontex.MarginUp;
		margindown = _fontex.MarginDown;
		marginleft = _fontex.MarginLeft;
	}

	public void Save(XElement xe)
	{
		xe.SetAttributeValue("FamilyName", familyname);
		xe.SetAttributeValue("FontSize", fontsize);
		xe.SetAttributeValue("FontStyle", fontstyle);
		xe.SetAttributeValue("R", color.R);
		xe.SetAttributeValue("G", color.G);
		xe.SetAttributeValue("B", color.B);
		xe.SetAttributeValue("MarginUp", marginup);
		xe.SetAttributeValue("MarginDown", margindown);
		xe.SetAttributeValue("MarginLeft", marginleft);
	}

	public void Load(XElement xe)
	{
		familyname = xe.Attribute("FamilyName").Value;
		fontsize = float.Parse(xe.Attribute("FontSize").Value);
		fontstyle = (FontStyle)Enum.Parse(typeof(FontStyle), xe.Attribute("FontStyle").Value);
		int red = int.Parse(xe.Attribute("R").Value);
		int green = int.Parse(xe.Attribute("G").Value);
		int blue = int.Parse(xe.Attribute("B").Value);
		color = Color.FromArgb(red, green, blue);
		marginup = float.Parse(xe.Attribute("MarginUp").Value);
		margindown = float.Parse(xe.Attribute("MarginDown").Value);
		marginleft = float.Parse(xe.Attribute("MarginLeft").Value);
	}

	public void ToValue(Enum_PrintFont _e, out Font _font, out Brush _bsh, out FontEx _fex)
	{
		_font = new Font(familyname, fontsize, fontstyle);
		_bsh = new SolidBrush(color);
		_fex = new FontEx(_e);
		_fex.MarginUp = marginup;
		_fex.MarginDown = margindown;
		_fex.MarginLeft = marginleft;
	}
}

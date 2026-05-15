namespace SamSoarII.Shell.Managers;

public class DemoFontManager
{
	private static FontData Title = new FontData(FontType.Title);

	private static FontData Ladder = new FontData(FontType.Ladder);

	private static FontData Comment = new FontData(FontType.Comment);

	private static FontData Func = new FontData(FontType.Func);

	private static FontData Text_Title = new FontData(FontType.Text_Title);

	private static FontData Text_Ladder = new FontData(FontType.Text_Ladder);

	private static FontData Text_Comment = new FontData(FontType.Text_Comment);

	private static FontData FBD_Network = new FontData(FontType.FBD_Network);

	private static FontData FBD_Value = new FontData(FontType.FBD_Value);

	private static FontData FBD_Comment = new FontData(FontType.FBD_Comment);

	private static FontData FBD_Table = new FontData(FontType.FBD_Table);

	private static FontData FBD_Monitor = new FontData(FontType.FBD_Monitor);

	public static FontData GetTitle()
	{
		return Title;
	}

	public static FontData GetLadder()
	{
		return Ladder;
	}

	public static FontData GetComment()
	{
		return Comment;
	}

	public static FontData GetFunc()
	{
		return Func;
	}

	public static FontData GetTextTitle()
	{
		return Text_Title;
	}

	public static FontData GetTextLadder()
	{
		return Text_Ladder;
	}

	public static FontData GetTextComment()
	{
		return Text_Comment;
	}

	public static FontData GetFBDNetwork()
	{
		return FBD_Network;
	}

	public static FontData GetFBDValue()
	{
		return FBD_Value;
	}

	public static FontData GetFBDComment()
	{
		return FBD_Comment;
	}

	public static FontData GetFBDTable()
	{
		return FBD_Table;
	}

	public static FontData GetFBDMonitor()
	{
		return FBD_Monitor;
	}
}

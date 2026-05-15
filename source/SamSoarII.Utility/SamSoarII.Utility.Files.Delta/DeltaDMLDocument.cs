using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SamSoarII.Utility.Files.Delta;

public class DeltaDMLDocument
{
	protected static Dictionary<string, string> NamePairs;

	private string text;

	private List<DeltaDMLElement> items;

	public string Text => text;

	public IList<DeltaDMLElement> Items => items;

	static DeltaDMLDocument()
	{
		NamePairs = new Dictionary<string, string>();
		NamePairs.Add("TASK_E", "TASK_S");
		NamePairs.Add("DEVICE_CMT_END", "DEVICE_CMT_START");
		NamePairs.Add("PROPERTIES_END", "PROPERTIES_START");
		NamePairs.Add("NETWORK_END", "NETWORK_START");
		NamePairs.Add("ROOTLINK_END", "ROOTLINK_START");
		NamePairs.Add("OUTLINK_END", "OUTLINK_START");
		NamePairs.Add("END_LD_NODE", "LD_NODE");
		NamePairs.Add("VAR_NODE_E", "VAR_NODE_S");
	}

	public DeltaDMLDocument(string _text)
	{
		text = _text;
		items = new List<DeltaDMLElement>();
		Initialize();
	}

	protected void Initialize()
	{
		char c = '\0';
		int num = -1;
		bool flag = false;
		bool flag2 = false;
		string text = null;
		string namepair = null;
		List<DeltaDMLElement> list = new List<DeltaDMLElement>();
		StringBuilder stringBuilder = new StringBuilder();
		DeltaDMLElement deltaDMLElement = null;
		DeltaDMLElement deltaDMLElement2 = null;
		for (int i = 0; i < this.text.Length; i++)
		{
			switch (this.text[i])
			{
			case '\n':
				if (deltaDMLElement != null && deltaDMLElement.FirstChar == '\0')
				{
					if (c == '\0' && num >= 0)
					{
						deltaDMLElement.Value = stringBuilder.ToString();
					}
					deltaDMLElement = null;
					num = -1;
					stringBuilder.Clear();
				}
				flag2 = false;
				break;
			case '<':
			case '[':
				if (flag2)
				{
					if (num < 0)
					{
						num = i;
					}
					stringBuilder.Append(this.text[i]);
				}
				else if (c == '\0')
				{
					if (deltaDMLElement != null && deltaDMLElement.LastName == null)
					{
						deltaDMLElement.Value = stringBuilder.ToString();
					}
					deltaDMLElement = null;
					c = this.text[i];
					num = i;
					flag = false;
					flag2 = false;
					stringBuilder.Clear();
				}
				break;
			case '>':
			case ']':
				if (flag2)
				{
					if (num < 0)
					{
						num = i;
					}
					stringBuilder.Append(this.text[i]);
				}
				else if ((c == '<' && this.text[i] == '>') || (c == '[' && this.text[i] == ']'))
				{
					text = stringBuilder.ToString();
					namepair = null;
					if (NamePairs.ContainsKey(text))
					{
						namepair = NamePairs[text];
					}
					else if (flag)
					{
						namepair = text;
					}
					if (namepair != null && (deltaDMLElement2 = list.LastOrDefault((DeltaDMLElement _item) => _item.LastName == null && _item.FirstName.Equals(namepair))) != null)
					{
						deltaDMLElement2.LastName = text;
						int num2 = list.IndexOf(deltaDMLElement2);
						for (int num3 = num2 + 1; num3 < list.Count(); num3++)
						{
							list[num3].Parent = deltaDMLElement2;
							deltaDMLElement2.Items.Add(list[num3]);
						}
						list.RemoveRange(num2 + 1, list.Count() - num2 - 1);
						deltaDMLElement = deltaDMLElement2;
					}
					else
					{
						deltaDMLElement = new DeltaDMLElement();
						deltaDMLElement.Name = stringBuilder.ToString();
						deltaDMLElement.FirstChar = c;
						list.Add(deltaDMLElement);
					}
					c = '\0';
					num = -1;
					flag2 = false;
					stringBuilder.Clear();
				}
				else
				{
					stringBuilder.Append(this.text[i]);
				}
				break;
			case '/':
				if (flag2)
				{
					if (num < 0)
					{
						num = i;
					}
					stringBuilder.Append(this.text[i]);
				}
				else if ((c == '<' || c == '[') && stringBuilder.Length == 0)
				{
					flag = true;
				}
				break;
			case '=':
				if (c == '\0' && num >= 0 && !flag2)
				{
					deltaDMLElement = new DeltaDMLElement();
					deltaDMLElement.Name = stringBuilder.ToString();
					deltaDMLElement.FirstChar = '\0';
					list.Add(deltaDMLElement);
					num = -1;
					flag2 = true;
					stringBuilder.Clear();
				}
				else
				{
					if (num < 0)
					{
						num = i;
					}
					stringBuilder.Append(this.text[i]);
				}
				break;
			default:
				if (num < 0)
				{
					num = i;
				}
				stringBuilder.Append(this.text[i]);
				break;
			case '\t':
			case '\r':
			case ' ':
				break;
			}
		}
		items = list;
	}
}

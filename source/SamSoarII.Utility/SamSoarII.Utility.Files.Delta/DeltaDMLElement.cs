using System.Collections.Generic;
using System.Linq;

namespace SamSoarII.Utility.Files.Delta;

public class DeltaDMLElement
{
	protected string name;

	protected string lastname;

	protected char firstchar = '\0';

	protected string value;

	protected DeltaDMLElement parent;

	protected List<DeltaDMLElement> items = new List<DeltaDMLElement>();

	public string Name
	{
		get
		{
			return name;
		}
		set
		{
			name = value;
		}
	}

	public string FirstName
	{
		get
		{
			return Name;
		}
		set
		{
			Name = value;
		}
	}

	public string LastName
	{
		get
		{
			return lastname;
		}
		set
		{
			lastname = value;
		}
	}

	public char FirstChar
	{
		get
		{
			return firstchar;
		}
		set
		{
			firstchar = value;
		}
	}

	public string Value
	{
		get
		{
			return value;
		}
		set
		{
			this.value = value;
		}
	}

	public DeltaDMLElement Parent
	{
		get
		{
			return parent;
		}
		set
		{
			parent = value;
		}
	}

	public IList<DeltaDMLElement> Items => items;

	public override string ToString()
	{
		return $"{Name}={Value}[{Items.Count()}]";
	}
}

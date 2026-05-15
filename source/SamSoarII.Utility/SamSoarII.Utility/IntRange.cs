namespace SamSoarII.Utility;

public struct IntRange
{
	public uint Start { get; set; }

	public uint End { get; set; }

	public int Count => (int)(End - Start);

	public IntRange(uint start, uint end)
	{
		Start = start;
		End = end;
	}

	public bool AssertValue(uint input)
	{
		return input < End && input >= Start;
	}
}

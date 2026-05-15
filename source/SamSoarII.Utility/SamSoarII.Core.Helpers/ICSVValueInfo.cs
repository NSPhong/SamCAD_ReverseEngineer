namespace SamSoarII.Core.Helpers;

public interface ICSVValueInfo
{
	byte BaseAddress { get; }

	ushort Offset { get; }

	byte IntraAddress { get; }

	byte IntraOffset { get; }

	byte Flag { get; }

	byte DataType { get; }
}

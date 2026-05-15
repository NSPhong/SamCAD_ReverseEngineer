namespace Samkoon.Protocol;

public interface IBaseCommand
{
	byte[] ResponseCommand { get; set; }

	bool IsComplete { get; set; }

	bool IsSuccess { get; set; }

	int ResponseLength { get; }

	byte FunctionCode { get; }

	byte[] GetCommand(bool isNetTransport = false);
}

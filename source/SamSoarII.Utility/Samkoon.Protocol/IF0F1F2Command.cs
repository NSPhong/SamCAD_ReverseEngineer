namespace Samkoon.Protocol;

public interface IF0F1F2Command
{
	bool IsComplete { get; }

	bool IsSuccess { get; }

	byte[] SendData { get; }

	byte[] RecvData { get; }

	byte SendCode { get; }

	byte RecvCode { get; }

	int RecvIndex { get; }

	int RecvCount { get; }

	void RecvClear();

	void RecvAppend(byte[] data, int start, int count);

	void RecvReadStart();

	void RecvReadEnd();

	byte RecvReadByte();

	ushort RecvReadUShort();

	int RecvReadInt();

	byte[] RecvReadBytes(int count);

	void RecvRead(byte[] data, int start, int count);
}

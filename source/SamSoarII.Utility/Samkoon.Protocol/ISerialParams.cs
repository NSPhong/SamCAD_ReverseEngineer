namespace Samkoon.Protocol;

public interface ISerialParams
{
	string PortName { get; set; }

	int BaudRate { get; set; }

	int DataBits { get; set; }

	int StopBits { get; set; }

	int Timeout { get; set; }

	string Parity { get; set; }
}

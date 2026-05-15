namespace Samkoon.Protocol;

public interface ITransportManager
{
	bool IsOpened { get; }

	int Timeout { get; set; }

	CommuicationError Error { get; }

	int Open();

	int Close();

	int Send(IBaseCommand command);

	int Send(IF0F1F2Command command);

	int Receive(IBaseCommand command);

	int Receive(IF0F1F2Command command);
}

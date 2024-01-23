namespace ChessChaos.Domain;

public class ChessHandlerBuilder
{
	private Func<Request, Func<ChessMessage, Task<ChessMessageResult>>, Task<RequestResult>> _metaLevelHandler = null!;
	private Func<ChessMessage, Task<ChessMessageResult>> _messageLevelHandler = null!;

	public ChessHandlerBuilder Use(Func<Request, Func<ChessMessage, Task<ChessMessageResult>>, Task<RequestResult>> function)
	{
		_metaLevelHandler = function;

		return this;
	}

	public ChessHandlerBuilder Use(Func<ChessMessage, Task<ChessMessageResult>> function)
	{
		_messageLevelHandler = function;

		return this;
	}

	public ChessHandler Build()
	{
		Func<Request, Task<RequestResult>> builtFunction = (request) => _metaLevelHandler(request, _messageLevelHandler);

		return new ChessHandler(builtFunction);
	}
}
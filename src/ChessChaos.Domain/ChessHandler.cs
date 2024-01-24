namespace ChessChaos.Domain;

public class ChessHandler
{
	private readonly Func<Request, Task<RequestResult>> _requestHandler;

	public ChessHandler(Func<Request, Task<RequestResult>> requestHandler)
	{
		_requestHandler = requestHandler;
	}

	public async Task<RequestResult> HandleAsync(Request request)
		=> await _requestHandler(request);
}
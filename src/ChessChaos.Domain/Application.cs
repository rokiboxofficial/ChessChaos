using ChessChaos.Core;

namespace ChessChaos.Domain;

public class Application
{
	private readonly ChessHandler _chessHandler;
	private readonly ChessBoard _chessBoard;
	private readonly ChessBoardValidator _chessBoardValidator;

	public Application(ChessBoard chessBoard, ChessBoardValidator chessBoardValidator)
	{
		var chessHandlerBuilder = new ChessHandlerBuilder()
			.Use(HandleRequest)
			.Use(HandleChessMessage);

		_chessHandler = chessHandlerBuilder.Build();
		_chessBoard = chessBoard;
		_chessBoardValidator = chessBoardValidator;
	}

	// TODO: extract methods to classes
	// TODO: single thread
	public async Task<RequestResult> HandleAsync(Request request)
		=> await _chessHandler.HandleAsync(request);

	private async Task<RequestResult> HandleRequest(Request request, Func<ChessMessage, Task<ChessMessageResult>> next)
	{
		try
		{
			// TODO: Add timers
			// from another side get back
			if (request is ChessMoveRequest)
			{
				var chessMoveRequest = (ChessMoveRequest)request;

				// TODO: Add mapper?
				var chessMessage = new ChessMessage()
				{
					From = chessMoveRequest.From,
					To = chessMoveRequest.To,
					PlayerColor = chessMoveRequest.PlayerColor
				};

				var chessMessageResult = await next(chessMessage);
			}
		}
		catch
		{

		}

		throw new NotImplementedException();
	}

	private Task<ChessMessageResult> HandleChessMessage(ChessMessage chessMessage)
	{
		try
		{
			// TODO: extract to class?
			// points exists  | throw exc on board level
			// from.piece color is equal to player color | on checmate check it be possible to also check color 
			// move is possible ( and kind will be safe ) | throw exc on board level OR handle checks on that level

			// TODO: add try/catch and custom exceptions
			_chessBoard.FromPoints(chessMessage.From, chessMessage.To)
				.ValidateMove(move => { if (move.Self.Color != chessMessage.PlayerColor) throw new Exception(); })
				.ValidteBoard(chessGameStateReader => _chessBoardValidator.ValidateBoard(chessGameStateReader, chessMessage.PlayerColor))
				.Apply();
		}
		catch
		{

		}

		throw new NotImplementedException();
	}
}
using ChessChaos.Common;

namespace ChessChaos.Core;

internal class ChessBoardValidator : IChessBoardValidator
{
	private readonly BoardProvider _boardProvider;
	private readonly IChessMove _chessMove;

	public ChessBoardValidator(BoardProvider boardProvider, IChessMove chessMove)
	{
		_boardProvider = boardProvider;
		_chessMove = chessMove;
	}

	public IValidatedBoard ValidteBoard(Action<IChessGameStateReader> accessor)
	{
		_boardProvider.AccessBoard(accessor);

		return new ValidatedBoard(_boardProvider, _chessMove);
	}

	public IValidatedBoard ValidateBoard(Action<IChessGameStateReader> accessor, IChessMove chessMove)
	{
		_boardProvider.AccessBoard(chessMove, accessor);

		return new ValidatedBoard(_boardProvider, _chessMove);
	}
}
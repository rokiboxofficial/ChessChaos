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

	public IValidatedBoard ValidateBoard(Action<IChessGameStateReader> accessor)
	{
		_boardProvider.AccessBoard(_chessMove, accessor);

		return new ValidatedBoard(_boardProvider, _chessMove);
	}
}
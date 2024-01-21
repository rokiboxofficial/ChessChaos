using ChessChaos.Common;

namespace ChessChaos.Core;

internal class ChessMoveValidator : IChessMoveValidator
{
	private readonly BoardProvider _boardProvider;
	private readonly IChessMove _chessMove;

	public ChessMoveValidator(BoardProvider boardProvider, IChessMove chessMove)
	{
		_boardProvider = boardProvider;
		_chessMove = chessMove;
	}

	public IChessBoardValidator ValidateMove(Action<IMove> moveProvider)
	{
		moveProvider?.Invoke(_chessMove);

		return new ChessBoardValidator(_boardProvider, _chessMove);
	}
}
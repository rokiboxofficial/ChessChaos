using ChessChaos.Common;

namespace ChessChaos.Core;

internal class ValidatedBoard : IValidatedBoard
{
	private readonly BoardProvider _boardProvider;
	private readonly IChessMove _chessMove;

	public ValidatedBoard(BoardProvider boardProvider, IChessMove chessMove)
	{
		_boardProvider = boardProvider;
		_chessMove = chessMove;
	}

	public void Apply()
		=> _boardProvider.Apply(_chessMove);
}

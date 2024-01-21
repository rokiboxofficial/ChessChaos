using ChessChaos.Common;
using ChessChaos.Common.Pieces;

namespace ChessChaos.Core;

public class ChessBoard
{
	private readonly MoveProvider _moveProvider;
	private readonly BoardProvider _boardProvider;

	public ChessBoard(HashSet<Point> realPoints, IList<(Point point, Piece piece)> pieces)
	{
		var chessGameState = new ChessGameState(realPoints, pieces);
		_boardProvider = new BoardProvider(chessGameState, chessGameState);
		_moveProvider = new MoveProvider(chessGameState);
	}

	public IChessMoveValidator FromPoints(Point from, Point to)
	{
		IChessMove chessMove = _moveProvider.GetMove(from, to);

		return new ChessMoveValidator(_boardProvider, chessMove);
	}

	public void AccessBoard(Action<IChessGameStateReader> accessor)
		=> _boardProvider.AccessBoard(accessor);
}
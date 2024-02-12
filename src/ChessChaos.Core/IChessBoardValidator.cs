using ChessChaos.Common;

namespace ChessChaos.Core;

public interface IChessBoardValidator
{
	public IValidatedBoard ValidteBoard(Action<IChessGameStateReader> accessor);

	public IValidatedBoard ValidateBoard(Action<IChessGameStateReader> accessor, IChessMove chessMove);
}
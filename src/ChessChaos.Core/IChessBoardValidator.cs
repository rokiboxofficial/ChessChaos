using ChessChaos.Common;

namespace ChessChaos.Core;

public interface IChessBoardValidator
{
	public IValidatedBoard ValidateBoard(Action<IChessGameStateReader> accessor);
}
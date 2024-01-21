using ChessChaos.Common;

namespace ChessChaos.Core;

public interface IChessBoardValidator
{
	public IValidatedBoard ValdiateBoard(Action<IChessGameStateReader> accessor);
}
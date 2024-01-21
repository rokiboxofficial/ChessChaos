using ChessChaos.Common;

namespace ChessChaos.Core;

public interface IChessMoveValidator
{
	public IChessBoardValidator ValidateMove(Action<IMove> moveProvider);
}
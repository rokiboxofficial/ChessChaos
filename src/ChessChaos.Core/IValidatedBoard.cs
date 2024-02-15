using ChessChaos.Common;

namespace ChessChaos.Core;

public interface IValidatedBoard
{
	public IChessGameStateReader Apply();
}
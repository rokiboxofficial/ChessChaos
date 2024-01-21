namespace ChessChaos.Common;

public interface ICommand
{
	public void Execute(IChessGameStateWriter chessGameStateWriter);
	public void Revert(IChessGameStateWriter chessGameState);
}
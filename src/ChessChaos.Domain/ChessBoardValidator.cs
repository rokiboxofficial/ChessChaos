using ChessChaos.Common;

namespace ChessChaos.Domain;

public class ChessBoardValidator
{
	public void ValidateBoard(IChessGameStateReader chessGameStateReader, SideColor currentTurn)
	{
		// check that king with color==currentTurn is not checked and etc.
	} 
}
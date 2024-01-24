namespace ChessChaos.Common.Pieces;

public abstract class Piece
{
	public SideColor Color { get; set; }

	public abstract IChessMove GetMove(IChessGameStateReader chessGameStateReader, Point from, Point to);
}
using ChessChaos.Common.Pieces;

namespace ChessChaos.Common;

public interface IChessGameStateWriter
{
	public void Move(Point from, Point to);
	public void Reset(Point point);
	public void Set(Point point, Piece piece);
}
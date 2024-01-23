using ChessChaos.Common.Pieces;

namespace ChessChaos.Common;

public interface IChessGameStateReader
{
	public Piece? this[Point point] { get; }
	public bool IsRealPoint(Point point);
}
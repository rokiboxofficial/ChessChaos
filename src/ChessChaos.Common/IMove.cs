using ChessChaos.Common.Pieces;

namespace ChessChaos.Common;

public interface IMove
{
	public Piece Self { get; }
	public Piece? Target { get; }
	public Point From { get; }
	public Point To { get; }
}
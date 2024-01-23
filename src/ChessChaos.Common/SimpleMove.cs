using ChessChaos.Common.Pieces;

namespace ChessChaos.Common;

public sealed class SimpleMove : IChessMove
{
	public SimpleMove(Point from, Point to, Piece self, Piece? target)
	{
		From = from;
		To = to;
		Self = self;
		Target = target;
	}

	public Point From { get; }
	public Point To { get; }
	public Piece Self { get; }
	public Piece? Target { get; }

	public void Execute(IChessGameStateWriter chessGameStateWriter)
	{
		chessGameStateWriter.Reset(From);
		chessGameStateWriter.Set(To, Self);
	}

	public void Revert(IChessGameStateWriter chessGameStateWriter)
	{
		chessGameStateWriter.Reset(To);
		if (Target is not null)
			chessGameStateWriter.Set(To, Target);
		chessGameStateWriter.Set(From, Self);
	}
}
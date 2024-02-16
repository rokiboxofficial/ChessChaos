namespace ChessChaos.Common.Pieces;

public sealed class Bishop : Piece
{
	private readonly HashSet<Point> _moveVectors = new()
	{
		new Point(-1, 1), new Point(1, 1),
		new Point(-1, -1), new Point(1, -1)
	};

	internal Bishop(SideColor color)
	{
		Color = color;
	}

	public override IChessMove GetMove(IChessGameStateReader chessGameStateReader, Point from, Point to)
	{
		var target = chessGameStateReader[to];
		if (target?.Color == Color)
			throw new Exception();

		// Check that has no pieces between from and to points
		// TODO: ADD DIFF METHOD
		if (_moveVectors.Contains(new Point(to.X - from.X / Math.Abs(to.X - from.X),
				to.Y - from.Y / Math.Abs(to.Y - from.Y))))
			return new SimpleMove(from, to, this, target);

		// TODO: Add custom exceptions
		throw new Exception();
	}
}
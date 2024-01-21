namespace ChessChaos.Common.Pieces;

public sealed class King : Piece
{
	private readonly HashSet<Point> _moveOffsets = new ()
	{
		new Point(-1, 1), new Point(0, 1), new Point(1, 1),
		new Point(-1, 0), new Point(1, 0),
		new Point(-1, -1), new Point(0, -1), new Point(1, -1)
	};

	internal King(SideColor color)
	{
		Color = color;
	}

	public override IChessMove GetMove(IChessGameStateReader chessGameStateReader, Point from, Point to)
	{
		var target = chessGameStateReader[to];
		if (target?.Color == Color)
			throw new Exception();


		// TODO: ADD DIFF METHOD
		if (_moveOffsets.Contains(new Point(to.X - from.X, to.Y - from.Y)))
			return new SimpleMove(from, to, this, target);

		// TODO: Add custom exceptions
		throw new Exception();
	}
}
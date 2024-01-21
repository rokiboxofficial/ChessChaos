using ChessChaos.Common;
using ChessChaos.Common.Pieces;

namespace ChessChaos.Core;

internal class ChessGameState : IChessGameStateReader, IChessGameStateWriter
{
	private readonly Dictionary<Point, Piece?> _pieceByPoint = new();
	private readonly HashSet<Point> _realPoints = null!;

	public ChessGameState(HashSet<Point> realPoints, IList<(Point point, Piece piece)> pieces)
	{
		_realPoints = realPoints;

		foreach (var (point, piece) in pieces)
			_pieceByPoint[point] = piece;
	}

	public Piece? this[Point point]
		=> _pieceByPoint.ContainsKey(point) ? _pieceByPoint[point] : null;

	public void Move(Point from, Point to)
	{
		if (!_realPoints.Contains(from) || !_realPoints.Contains(to))
			throw new InvalidOperationException("Is not possible to use not real points");

		var initialPiece = _pieceByPoint[from];
		_pieceByPoint.Remove(from);
		_pieceByPoint[to] = initialPiece;
	}

	public void Reset(Point point)
	{
		if (!_realPoints.Contains(point))
			throw new InvalidOperationException("Is not possible to reset not real point");

		_pieceByPoint.Remove(point);
	}

	public void Set(Point point, Piece piece)
	{
		if (!_realPoints.Contains(point))
			throw new InvalidOperationException("Is not possible to set not real point");

		_pieceByPoint[point] = piece;
	}

	public bool IsRealPoint(Point point)
		=> _realPoints.Contains(point);
}
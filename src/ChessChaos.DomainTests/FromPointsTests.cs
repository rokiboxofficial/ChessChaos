using ChessChaos.Common;
using ChessChaos.Common.Pieces;
using ChessChaos.Core;
using FluentAssertions;

namespace ChessChaos.DomainTests;

[TestClass]
public class FromPointsTests
{
	[TestMethod]
	public void WhenMovingPoints_AndMoveIsNotExist_ThenThrowException()
	{
		// Arrange.
		var king = new PieceProvider().GetInstance(PieceKind.King, SideColor.White);
		var piceOnPoint = new List<(Point point, Piece piece)>();
		var truePoints = new HashSet<Point>();
		var from = new Point(1, 1);
		var to = new Point(21, 6);

		truePoints.Add(to);
		truePoints.Add(new Point(1, 0));
		truePoints.Add(new Point(0, 1));
		truePoints.Add(new Point(-1, 0));
		truePoints.Add(new Point(0, -1));
		truePoints.Add(new Point(-1, -1));
		truePoints.Add(new Point(-1, 1));
		truePoints.Add(new Point(1, -1));

		piceOnPoint.Add((from, king));

		// Act.
		var chessBoard = new ChessBoard(truePoints, piceOnPoint);
		Action act = () => chessBoard.FromPoints(from, to);

		// Assert.
		act.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPoints_AndMoveIsNotCorrect_ThenThrowException()
	{
		// Arrange.
		var king = new PieceProvider().GetInstance(PieceKind.King, SideColor.White);
		var piceOnPoint = new List<(Point point, Piece piece)>();
		var truePoints = new HashSet<Point>();
		var from = new Point(1, 1);
		var to = new Point(3, 3);

		truePoints.Add(to);
		truePoints.Add(from);
		truePoints.Add(new Point(1, 0));
		truePoints.Add(new Point(0, 1));
		truePoints.Add(new Point(-1, 0));
		truePoints.Add(new Point(0, -1));
		truePoints.Add(new Point(-1, -1));
		truePoints.Add(new Point(-1, 1));
		truePoints.Add(new Point(1, -1));

		piceOnPoint.Add((from, king));

		// Act.
		var chessBoard = new ChessBoard(truePoints, piceOnPoint);
		Action act = () => chessBoard.FromPoints(from, to);

		// Assert.
		act.Should().Throw<Exception>();
	}
}
using ChessChaos.Common;
using ChessChaos.Common.Pieces;
using ChessChaos.Core;
using FluentAssertions;

namespace ChessChaos.CoreTests;

[TestClass]
public class FromPointsTests
{
	[TestMethod]
	public void WhenMovingPieces_AndToPointIsNotExist_ThenThrowException()
	{
		// Arrange.
		var pieceKing = new PieceProvider()
			.GetInstance(PieceKind.King, SideColor.White);
		var piceOnPoint = new List<(Point point, Piece piece)>();
		var realPointsOnTheBoard = new HashSet<Point>();

		var from = new Point(1, 1);
		var to = new Point(21, 6);

		realPointsOnTheBoard.Add(to);
		realPointsOnTheBoard.Add(new Point(1, 0));
		realPointsOnTheBoard.Add(new Point(0, 1));
		realPointsOnTheBoard.Add(new Point(-1, 0));
		realPointsOnTheBoard.Add(new Point(0, -1));
		realPointsOnTheBoard.Add(new Point(-1, -1));
		realPointsOnTheBoard.Add(new Point(-1, 1));
		realPointsOnTheBoard.Add(new Point(1, -1));

		piceOnPoint.Add((from, pieceKing));

		// Act.
		var chessBoard = new ChessBoard(realPointsOnTheBoard, piceOnPoint);
		Action act = () => chessBoard.FromPoints(from, to);

		// Assert.
		act.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPieces_AndPieceMoveIsNotCorrect_ThenThrowException()
	{
		// Arrange.
		var pieceKing = new PieceProvider()
			.GetInstance(PieceKind.King, SideColor.White);
		var pieceOnPoint = new List<(Point point, Piece piece)>();
		var realPointsOnTheBoard = new HashSet<Point>();
		var from = new Point(1, 1);
		var to = new Point(3, 3);

		realPointsOnTheBoard.Add(to);
		realPointsOnTheBoard.Add(from);
		realPointsOnTheBoard.Add(new Point(1, 0));
		realPointsOnTheBoard.Add(new Point(0, 1));
		realPointsOnTheBoard.Add(new Point(-1, 0));
		realPointsOnTheBoard.Add(new Point(0, -1));
		realPointsOnTheBoard.Add(new Point(-1, -1));
		realPointsOnTheBoard.Add(new Point(-1, 1));
		realPointsOnTheBoard.Add(new Point(1, -1));

		pieceOnPoint.Add((from, pieceKing));

		// Act.
		var chessBoard = new ChessBoard(realPointsOnTheBoard, pieceOnPoint);
		Action act = () => chessBoard.FromPoints(from, to);

		// Assert.
		act.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPieces_AndMovePointFromAndMovePointToCorrect_MoveShouldBeCorrect()
	{
		// Arrange.
		var pieceKing = new PieceProvider()
			.GetInstance(PieceKind.King, SideColor.White);
		var pieceOnPoint = new List<(Point point, Piece piece)>();
		var realPointsOnTheBoard = new HashSet<Point>();

		var from = new Point(0, 0);
		var to = new Point(1, 1);

		realPointsOnTheBoard.Add(to);
		realPointsOnTheBoard.Add(from);
		realPointsOnTheBoard.Add(new Point(1, 4));
		realPointsOnTheBoard.Add(new Point(3, 2));
		realPointsOnTheBoard.Add(new Point(4, 4));
		realPointsOnTheBoard.Add(new Point(1, -1));

		pieceOnPoint.Add((from, pieceKing));

		// Act.
		var fromPoints = new ChessBoard(realPointsOnTheBoard, pieceOnPoint)
			.FromPoints(from, to);

		var move = fromPoints
			.ValidateMove(move =>
			{
				if (move.From != from || move.To != to)
					throw new Exception();
			});

		// Assert.
		move.Should().NotBe(null);
	}

	[TestMethod]
	public void WhenMovingPieces_AndMoveIsCorrect_WhenBoardShouldBeChanged()
	{
		// Arrange.
		var pieceKing = new PieceProvider()
			.GetInstance(PieceKind.King, SideColor.White);
		var pieceBishop = new PieceProvider()
			.GetInstance(PieceKind.Bishop, SideColor.Black);

		var pieceOnPoint = new List<(Point point, Piece piece)>();
		var realPointsOnTheBoard = new HashSet<Point>();

		var from = new Point(6, 6);
		var to = new Point(7, 7);

		realPointsOnTheBoard.Add(to);
		realPointsOnTheBoard.Add(from);
		realPointsOnTheBoard.Add(new Point(1, 4));
		realPointsOnTheBoard.Add(new Point(3, 2));
		realPointsOnTheBoard.Add(new Point(4, 4));
		realPointsOnTheBoard.Add(new Point(1, -1));

		pieceOnPoint.Add((from, pieceKing));
		pieceOnPoint.Add((to, pieceBishop));

		// Act.
		var fromPoint = new ChessBoard(realPointsOnTheBoard, pieceOnPoint)
			.FromPoints(from, to)
			.ValidateMove(move =>
			{
				if (move.From != from || move.To != to)
					throw new Exception();
			});

		var boardState = fromPoint
			.ValidateBoard(board => { }
			, new SimpleMove(from, to, pieceKing, pieceBishop));

		// Assert.
		boardState.Should().NotBe(null);
	}

	[TestMethod]
	public void WhenMovingPieces_AndMoveIsCorrect_WhenBoardIsValid()
	{
		// Arrange.
		var pieceKing = new PieceProvider()
			.GetInstance(PieceKind.King, SideColor.White);
		var pieceBishop = new PieceProvider()
			.GetInstance(PieceKind.King, SideColor.Black);
		var pieceOnPoint = new List<(Point point, Piece piece)>();
		var realPointsOnTheBoard = new HashSet<Point>();

		var from = new Point(1, 1);
		var to = new Point(2, 2);

		realPointsOnTheBoard.Add(to);
		realPointsOnTheBoard.Add(from);
		realPointsOnTheBoard.Add(new Point(1, 4));
		realPointsOnTheBoard.Add(new Point(3, 2));
		realPointsOnTheBoard.Add(new Point(4, 4));
		realPointsOnTheBoard.Add(new Point(1, -1));

		pieceOnPoint.Add((from, pieceKing));
		pieceOnPoint.Add((to, pieceBishop));

		// Act.
		var fromPoint = new ChessBoard(realPointsOnTheBoard, pieceOnPoint)
			.FromPoints(from, to)
			.ValidateMove(move =>
			{
				if (move.From != from || move.To != to)
					throw new Exception();
			});

		var boardState = fromPoint
			.ValidateBoard(board => { },
			new SimpleMove(from, to, pieceKing, pieceBishop));

		// Assert.
		boardState.Should().NotBe(null);
	}
}
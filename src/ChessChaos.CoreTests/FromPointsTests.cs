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

		var from = new Point(1, 1);
		var to = new Point(21, 6);

		var piceOnPoint = new List<(Point point, Piece piece)>()
		{ (from, pieceKing) };

		var realPointsOnTheBoard = new HashSet<Point>()
		{ from, to, new Point(1,0), new Point(0,1),
			new Point(-1,0), new Point(0,-1), new Point(-1,-1),
			new Point(-1,1), new Point(1,-1) };

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

		var from = new Point(1, 1);
		var to = new Point(3, 3);

		var pieceOnPoint = new List<(Point point, Piece piece)>()
		{ (from, pieceKing) };

		var realPointsOnTheBoard = new HashSet<Point>()
		{ from, to, new Point(1,0), new Point(0,1),
			new Point(-1,0), new Point(0,-1), new Point(-1,-1),
			new Point(-1,1), new Point(1,-1) };

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

		var from = new Point(0, 0);
		var to = new Point(1, 1);

		var pieceOnPoint = new List<(Point point, Piece piece)>()
		{ (from, pieceKing) };

		var realPointsOnTheBoard = new HashSet<Point>()
		{ from, to, new Point(1,4), new Point(3,2),
			new Point(4,4), new Point(1,-1) };

		// Act.
		var fromPoints = new ChessBoard(realPointsOnTheBoard, pieceOnPoint)
			.FromPoints(from, to)
			.ValidateMove(move =>
			{
				if (move.From != from || move.To != to)
					throw new Exception();
			});

		// Assert.
		fromPoints.Should().NotBe(null);
	}

	[TestMethod]
	public void WhenMovingPieces_AndPointOnTheBoardIsNotReal_WhenBoardShouldBeChanged()
	{
		// Arrange.
		var pieceKing = new PieceProvider()
			.GetInstance(PieceKind.King, SideColor.White);
		var pieceBishop = new PieceProvider()
			.GetInstance(PieceKind.Bishop, SideColor.Black);

		var from = new Point(6, 6);
		var to = new Point(7, 7);

		var pieceOnPoint = new List<(Point point, Piece piece)>()
		{ (from,pieceKing), (to,pieceBishop) };

		var realPointsOnTheBoard = new HashSet<Point>()
		{ to, from, new Point(1,4), new Point(3,2),
			new Point(4,4), new Point(1,-1) };

		// Act.
		var fromPoint = new ChessBoard(realPointsOnTheBoard, pieceOnPoint)
			.FromPoints(from, to)
			.ValidateMove(move =>
			{
				if (move.From != from || move.To != to)
					throw new Exception();
			})
			.ValidateBoard(board =>
			{
				if (board.IsRealPoint(from) == board.IsRealPoint(new Point(21, 28)))
					throw new Exception();
			});

		// Assert.
		fromPoint.Should().NotBe(null);
	}

	[TestMethod]
	public void WhenMovingPieces_AndMoveIsCorrect_WhenBoardIsValid()
	{
		// Arrange.
		var pieceKing = new PieceProvider()
			.GetInstance(PieceKind.King, SideColor.White);
		var pieceBishop = new PieceProvider()
			.GetInstance(PieceKind.Bishop, SideColor.Black);

		var from = new Point(1, 1);
		var to = new Point(2, 2);

		var pieceOnPoint = new List<(Point point, Piece piece)>()
		{ (from,pieceKing), (to,pieceBishop) };

		var realPointsOnTheBoard = new HashSet<Point>()
		{ to, from, new Point(1,4), new Point(3,2),
			new Point(4,4), new Point(1,-1) };

		// Act.
		var boardChek = new ChessBoard(realPointsOnTheBoard, pieceOnPoint)
			.FromPoints(from, to)
			.ValidateMove(move =>
			{
				if (move.From != from || move.To != to)
					throw new Exception();
			})
			.ValidateBoard(board =>
			{
				if (board.IsRealPoint(from) != board.IsRealPoint(to))
					throw new Exception();
			});

		// Assert.
		boardChek.Should().NotBe(null);
	}

	[TestMethod]
	public void WhenMovingPieces_AndBoardStateIsNotValid_ThrowException()
	{
		// Arrange.
		var pieceKing = new PieceProvider()
			.GetInstance(PieceKind.King, SideColor.White);
		var pieceBishop = new PieceProvider()
			.GetInstance(PieceKind.Bishop, SideColor.Black);

		var from = new Point(1, 1);
		var to = new Point(2, 2);

		var pieceOnPoint = new List<(Point point, Piece piece)>()
		{ (from,pieceKing), (to,pieceBishop) };

		var realPointsOnTheBoard = new HashSet<Point>()
		{ to, from, new Point(1,4), new Point(3,2),
			new Point(4,4), new Point(1,-1) };

		// Act.
		Action boardChek = () => new ChessBoard(realPointsOnTheBoard, pieceOnPoint)
			.FromPoints(from, to)
			.ValidateMove(move =>
			{
				if (move.From != from || move.To != to)
					throw new Exception();
			})
			.ValidateBoard(board =>
			{
				if (board[from] != board[to])
					throw new Exception();
			});

		// Assert.
		boardChek.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPieces_AndBoardsIsNotValid_ThrowException()
	{
		// Arrange.
		var pieceKing = new PieceProvider()
			.GetInstance(PieceKind.King, SideColor.White);
		var pieceBishop = new PieceProvider()
			.GetInstance(PieceKind.Bishop, SideColor.Black);

		var from = new Point(1, 1);
		var to = new Point(2, 2);

		var pieceOnPoint = new List<(Point point, Piece piece)>()
		{ (from,pieceKing), (to,pieceBishop) };

		var realPointsOnTheBoard = new HashSet<Point>()
		{ to, from, new Point(1,4), new Point(3,2),
			new Point(4,4), new Point(1,-1) };

		// Act.
		Action firstBoardCheck = () => new ChessBoard(realPointsOnTheBoard, pieceOnPoint)
			.FromPoints(from, to)
			.ValidateMove(move =>
			{
				if (move.From != from || move.To != to)
					throw new Exception();
			})
			.ValidateBoard(board =>
			{
				if (board[from] != board[to])
					throw new Exception();
			}).Apply();

		Action secondBoardCheck = () => new ChessBoard(realPointsOnTheBoard, pieceOnPoint)
			.FromPoints(from, to)
			.ValidateMove(move =>
			{
				if (move.From != from || move.To != to)
					throw new Exception();
			})
			.ValidateBoard(board =>
			{
				if (board[from] != board[to])
					throw new Exception();
			});

		// Assert.
		firstBoardCheck.Should().NotBeSameAs(secondBoardCheck);
	}
}
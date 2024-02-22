using ChessChaos.Common;
using ChessChaos.Common.Pieces;
using ChessChaos.Core;
using FluentAssertions;

namespace ChessChaos.CoreTests;

[TestClass]
public class GetPieceMoveTests
{
	[TestMethod]
	public void WhenMovingPiece_AndBoardNotExistPoint_ThrowException()
	{
		// Arrange.
		var provider = new PieceProvider();
		var whiteKing = provider.GetInstance(PieceKind.King, SideColor.White);
		var from = new Point(0, 0);
		var to = new Point(1, 0);
		var chessBoard = new ChessBoard(
			new HashSet<Point>() { new Point(5, 6), to },
			new List<(Point, Piece)> { (from, whiteKing) });

		// Act.
		Action notCorrectMove = () => chessBoard.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board =>
			{
				board[from]?.GetMove(board, from, to);
			});

		//Assert.
		notCorrectMove.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPiece_AndPieceIsNull_ThrowException()
	{
		// Arrange.
		var from = new Point(0, 0);
		var to = new Point(1, 0);
		var chessBoard = new ChessBoard(
			new HashSet<Point>() { from, to },
			new List<(Point, Piece)> { (from, null) });

		// Act.
		Action notCorrectMove = () => chessBoard.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board =>
			{
				board[from]?.GetMove(board, from, to);
			});

		// Assert.
		notCorrectMove.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPiece_AndBoardNotExistPointAndPieceIsNull_ThrowException()
	{
		// Arrange.
		var from = new Point(0, 0);
		var to = new Point(1, 0);
		var chessBoard = new ChessBoard(
			new HashSet<Point>() { from, to },
			new List<(Point, Piece)> { (new Point(6, 11), null) });

		// Act.
		Action notCorrectMove = () => chessBoard.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board =>
			{
				board[from]?.GetMove(board, from, to);
			});

		// Assert.
		notCorrectMove.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPiece_AndBoardExistPieceAndPieceNotNull_ThenMoveIsTrue()
	{
		// Arrange.
		var provider = new PieceProvider();
		var whiteKing = provider.GetInstance(PieceKind.King, SideColor.White);
		var from = new Point(0, 0);
		var to = new Point(1, 0);
		var chessBoard = new ChessBoard(
			new HashSet<Point>() { from, to },
			new List<(Point, Piece)>() { (from, whiteKing) });

		// Act, Assert.
		var correctMove = chessBoard.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board =>
			{
				var isValidMove = board[from]?.GetMove(board, from, to) == null;
				isValidMove.Should().BeTrue();
			});
	}
}
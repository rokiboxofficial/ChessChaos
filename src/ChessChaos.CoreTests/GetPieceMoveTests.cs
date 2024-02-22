
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
		var board = new HashSet<Point>()
		{
			from,to
		};
		var pieces = new List<(Point, Piece)>()
		{
			(new Point(5,2),whiteKing)
		};
		var chessBoard = new ChessBoard(board, pieces);

		// Act.
		Action notCorrectMove = () => chessBoard.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board =>
			{
				if (board[from].GetMove(board, from, to) is null)
					throw new Exception();
			});

		//Assert.
		notCorrectMove.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPiece_AndPieceIsNotExist_ThrowException()
	{
		// Arrange.
		var from = new Point(0, 0);
		var to = new Point(1, 0);
		var board = new HashSet<Point>()
		{
			from,to
		};
		var pieces = new List<(Point, Piece)>()
		{
			(from,null)
		};
		var chessBoard = new ChessBoard(board, pieces);

		// Act.
		Action notCorrectMove = () => chessBoard.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board =>
			{
				if (board[from].GetMove(board, from, to) is null)
					throw new Exception();
			});

		// Assert.
		notCorrectMove.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPiece_AndBoardNotExistPointAndPieceNotExis_ThrowException()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.King, SideColor.White);
		var from = new Point(0, 0);
		var to = new Point(1, 0);
		var board = new HashSet<Point>()
		{
			from,to
		};
		var pieces = new List<(Point, Piece)>()
		{
			(new Point(6,11),null)
		};
		var chessBoard = new ChessBoard(board, pieces);

		// Act.
		Action notCorrectMove = () => chessBoard.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board =>
			{
				if (board[from].GetMove(board, from, to) is null)
					throw new Exception();
			});

		// Assert.
		notCorrectMove.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPiece_AndBoardExistPieceAndPieceExist_ThenMoveIsTrue()
	{
		// Arrange.
		var provider = new PieceProvider();
		var whiteKing = provider.GetInstance(PieceKind.King, SideColor.White);
		var from = new Point(0, 0);
		var to = new Point(1, 0);
		var board = new HashSet<Point>()
		{
			from,to
		};
		var pieces = new List<(Point, Piece)>()
		{
			(from,whiteKing)
		};
		var chessBoard = new ChessBoard(board, pieces);

		// Act, Assert.
		Action correctMove = () => chessBoard.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board =>
			{
				var isValidMove = board[from].GetMove(board, from, to) is not null;
				isValidMove.Should().BeTrue();
			}).Apply();
	}
}
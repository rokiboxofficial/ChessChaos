using ChessChaos.Common;
using ChessChaos.Common.Pieces;
using ChessChaos.Core;
using FluentAssertions;

namespace ChessChaos.CoreTests;

[TestClass]
public class FromPointsTests
{
	[TestMethod]
	public void WhenMovingPieces_AndKingMoveIsDontCorrected_ThrowException()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.King, SideColor.White);
		var from = new Point(0, 0);
		var to = new Point(5, 3);
		var points = new HashSet<Point>()
		{
			from, to
		};
		var pieces = new List<(Point, Piece)>()
		{
			(from, whiteKing)
		};
		var board = new ChessBoard(points, pieces);

		// Act.
		Action whiteKingMove = () =>
			board.FromPoints(from, to);

		// Assert.
		whiteKingMove.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPieces_AndBishopMoveIsNotCorrected_ThrowException()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.Bishop, SideColor.White);
		var from = new Point(0, 0);
		var to = new Point(1, 0);
		var points = new HashSet<Point>()
		{
			from, to
		};
		var pieces = new List<(Point, Piece)>()
		{
			(from, whiteKing)
		};
		var board = new ChessBoard(points, pieces);

		// Act.
		Action whiteKingMove = () =>
			board.FromPoints(from, to);

		// Assert.
		whiteKingMove.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPieces_AndMoveIsCorrected_ThenMoveShouldBeTrue()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.King, SideColor.White);
		var from = new Point(0, 0);
		var to = new Point(1, 0);
		var points = new HashSet<Point>()
		{
			from, to
		};
		var pieces = new List<(Point, Piece)>()
		{
			(from, whiteKing)
		};
		var board = new ChessBoard(points, pieces);

		// Act.
		var whiteKingMove = board.FromPoints(from, to)
			.ValidateMove(move =>
			{
				var isCorrectMove = move.From == from
					&& move.Self == whiteKing
					&& move.Target == null
					&& move.To == to;

				isCorrectMove.Should().BeTrue();
			});
	}

	[TestMethod]
	public void WhenMovingPieces_AndMoveIsDontApplied_ThenPointShouldBeReverted()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteBishop = pieceProvider.GetInstance(PieceKind.Bishop, SideColor.White);
		var from = new Point(0, 0);
		var to = new Point(1, 1);
		var points = new HashSet<Point>()
		{
			from,new Point(1, 1)
		};
		var pieces = new List<(Point, Piece)>()
		{
			(from,whiteBishop)
		};
		var board = new ChessBoard(points, pieces);
		board.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board => { });

		// Act.
		board.AccessBoard(state =>
		{
			var isCorrectMove = state[to] == whiteBishop
				&& state[from] == null;

			isCorrectMove.Should().BeFalse();
		});
	}

	[TestMethod]
	public void WhenMovingPieces_AndMoveIsApplied_ThenMoveShouldBeSaved()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.King, SideColor.White);
		var from = new Point(0, 0);
		var to = new Point(0, 1);
		var points = new HashSet<Point>()
		{
			from, to
		};
		var pieces = new List<(Point, Piece)>()
		{
			(from,whiteKing)
		};
		var board = new ChessBoard(points, pieces);
		board.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board => { })
			.Apply();

		// Act.
		board.AccessBoard(state =>
		{
			var isCorrectMove = state[from] == null
				&& state[to] == whiteKing;

			isCorrectMove.Should().BeTrue();
		});
	}

	[TestMethod]
	public void WhenMovingPieces_AndBoardThrowedException_MoveShouldBeReverted()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.King, SideColor.White);
		var from = new Point(0, 0);
		var to = new Point(0, 1);
		var points = new HashSet<Point>()
		{
			from, to
		};
		var pieces = new List<(Point, Piece)>()
		{
			(from,whiteKing)
		};
		var board = new ChessBoard(points, pieces);
		Action boardIsValid = ()
			=> board.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board => { throw new Exception(); })
			.Apply();

		// Act.
		boardIsValid.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPieces_AndMoveThrowedException_MoveShouldBeReverted()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.King, SideColor.White);
		var from = new Point(0, 0);
		var to = new Point(0, 1);
		var points = new HashSet<Point>()
		{
			from, to
		};
		var pieces = new List<(Point, Piece)>()
		{
			(from,whiteKing)
		};
		var board = new ChessBoard(points, pieces);
		Action boardIsValid = ()
			=> board.FromPoints(from, to)
			.ValidateMove(move => { throw new Exception(); })
			.ValidateBoard(board => { })
			.Apply();

		// Act.
		boardIsValid.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPieces_AndBoardDontExistToPoint_ThenThrowException()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.King, SideColor.White);
		var from = new Point(0, 0);
		var to = new Point(1, 0);
		var points = new HashSet<Point>()
		{
			from
		};
		var pieces = new List<(Point, Piece)>()
		{
			(from,whiteKing)
		};
		var board = new ChessBoard(points, pieces);

		// Act.
		Action boardIsValid = ()
			=> board.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board => { });

		// Assert.
		boardIsValid.Should().Throw<Exception>();
	}
}
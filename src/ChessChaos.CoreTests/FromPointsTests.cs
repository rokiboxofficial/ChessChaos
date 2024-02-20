using ChessChaos.Common;
using ChessChaos.Common.Pieces;
using ChessChaos.Core;
using FluentAssertions;

namespace ChessChaos.CoreTests;

[TestClass]
public class FromPointsTests
{
	[TestMethod]
	public void WhenMovingPieces_AndKingMoveIsDoesNotCorrect_ThenThrowException()
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
	public void WhenMovingPieces_AndBishopMoveIsDoesNotCorrect_ThenThrowException()
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
	public void WhenMovingPieces_AndMoveIsCorrect_ThenMoveShouldBeTrue()
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

		// Act, Assert.
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
	public void WhenMovingPieces_AndMoveIsDoesNotApplied_ThenPointShouldBeReverted()
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

		// Act.
		board.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board => { });

		//Assert.
		board.AccessBoard(state =>
		{
			var isCorrectMove = state[to] == whiteBishop
				&& state[from] == null;

			isCorrectMove.Should().BeFalse();
		});
	}

	[TestMethod]
	public void WhenMovingPieces_AndMoveIsApplied_ThenMoveShouldBeApplied()
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

		// Act.
		board.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board => { })
			.Apply();

		// Assert.
		board.AccessBoard(state =>
		{
			var isCorrectMove = state[from] == null
				&& state[to] == whiteKing;

			isCorrectMove.Should().BeTrue();
		});
	}

	[TestMethod]
	public void WhenMovingPieces_AndBoardThrowedException_ThrowExceptionAndMoveShouldBeReverted()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.King, SideColor.White);
		var from = new Point(0, 0);
		var to = new Point(0, 1);
		var points = new HashSet<Point>()
		{
			from,
		};
		var pieces = new List<(Point, Piece)>()
		{
			(from,whiteKing)
		};
		var board = new ChessBoard(points, pieces);

		// Act.
		Action boardIsValid = () =>
		{
			board.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board => { })
			.Apply();
		};

		// Assert.
		board.AccessBoard(state =>
		{
			var isRevertMove = state[from] == whiteKing
				&& state[to] == null;
			isRevertMove.Should().BeTrue();
		});

		boardIsValid.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPieces_AndMoveThrowedException_ThrowExceptionAndMoveShouldBeReverted()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.King, SideColor.White);
		var from = new Point(0, 0);
		var to = new Point(0, 1);
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
		Action moveIsValid = () =>
		{
			board.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board => { })
			.Apply();
		};

		// Assert.
		board.AccessBoard(state =>
		{
			var isRevertMove = state[from] == whiteKing
				&& state[to] == null;
			isRevertMove.Should().BeTrue();
		});

		moveIsValid.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPieces_AndBoardDoesNotExistToPoint_ThenThrowException()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.King, SideColor.White);
		var from = new Point(0, 0);
		var to = new Point(13, 7);
		var points = new HashSet<Point>()
		{
			from,to
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
using ChessChaos.Common;
using ChessChaos.Common.Pieces;
using ChessChaos.Core;
using FluentAssertions;

namespace ChessChaos.CoreTests;

[TestClass]
public class FromPointsTests
{
	[TestMethod]
	public void WhenMovingPieces_AndPieceKingMoveIsNotCorrect_ThrowException()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.King, SideColor.White);

		var points = new HashSet<Point>()
		{
			new Point(0, 0), new Point(5, 3)
		};

		var pieces = new List<(Point, Piece)>()
		{
			(new Point(0, 0), whiteKing)
		};

		var board = new ChessBoard(points, pieces);

		// Act.
		Action whiteKingMove = () =>
			board.FromPoints(new Point(0, 0), new Point(5, 3));

		// Assert.
		whiteKingMove.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPieces_AndPieceBishopMoveIsNotCorrect_ThrowException()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.Bishop, SideColor.White);

		var points = new HashSet<Point>()
		{
			new Point(0,0), new Point(1,0)
		};

		var pieces = new List<(Point, Piece)>()
		{
			(new Point(0,0), whiteKing)
		};

		var board = new ChessBoard(points, pieces);

		// Act.
		Action whiteKingMove = () =>
			board.FromPoints(new Point(0, 0), new Point(1, 0));

		// Assert.
		whiteKingMove.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPieces_AndMoveIsCorrect_ThenMoveShouldBeTrue()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.King, SideColor.White);

		var points = new HashSet<Point>()
		{
			new Point(0, 0), new Point(1, 0)
		};

		var pieces = new List<(Point, Piece)>()
		{
			(new Point(0, 0), whiteKing)
		};

		var board = new ChessBoard(points, pieces);

		// Act.
		var isCorrectMove = false;
		var whiteKingMove = board.FromPoints(new Point(0, 0), new Point(1, 0))
			.ValidateMove(move =>
			{
				isCorrectMove = move.From == new Point(0, 0)
				&& move.Self == whiteKing
				&& move.Target == null
				&& move.To == new Point(1, 0);

				isCorrectMove.Should().BeTrue();
			});
	}

	[TestMethod]
	public void WhenMovingPieces_AndMovingPieceIsNotApply_ThenPointShouldBeComeBack()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteBishop = pieceProvider.GetInstance(PieceKind.Bishop, SideColor.White);

		var points = new HashSet<Point>()
		{
			new Point(0, 0),new Point(1, 1)
		};

		var pieces = new List<(Point, Piece)>()
		{
			(new Point(0, 0),whiteBishop)
		};

		var board = new ChessBoard(points, pieces);

		board.FromPoints(new Point(0, 0), new Point(1, 1))
			.ValidateMove(move => { })
			.ValidateBoard(board => { });

		// Act.
		board.AccessBoard(state =>
		{
			var expectedMove = state[new Point(1, 1)] == whiteBishop
				&& state[new Point(0, 0)] == null;

			expectedMove.Should().BeFalse();
		});
	}

	[TestMethod]
	public void WhenMovingPieces_AndMovingPieceIsApply_ThenMoveShouldBeSaved()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.King, SideColor.White);

		var points = new HashSet<Point>()
		{
			new Point(0, 0),new Point(0, 1)
		};

		var pieces = new List<(Point, Piece)>()
		{
			(new Point(0, 0),whiteKing)
		};

		var board = new ChessBoard(points, pieces);

		board.FromPoints(new Point(0, 0), new Point(0, 1))
			.ValidateMove(move => { })
			.ValidateBoard(board => { })
			.Apply();

		// Act.
		board.AccessBoard(state =>
		{
			var expectedMove = state[new Point(0, 0)] == null
				&& state[new Point(0, 1)] == whiteKing;

			expectedMove.Should().BeTrue();
		});
	}

	[TestMethod]
	public void WhenMovingPieces_AndBoardThrowException_RevertMove()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.King, SideColor.White);

		var points = new HashSet<Point>()
		{
			new Point(0, 0),new Point(0, 1)
		};

		var pieces = new List<(Point, Piece)>()
		{
			(new Point(0, 0),whiteKing)
		};

		var board = new ChessBoard(points, pieces);

		Action boardIsValid = ()
			=> board.FromPoints(new Point(0, 0), new Point(0, 1))
			.ValidateMove(move => { })
			.ValidateBoard(board =>
			{
				throw new Exception();
			})
			.Apply();

		// Act.
		boardIsValid.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPieces_AndMoveThrowException_RevertMove()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.King, SideColor.White);

		var points = new HashSet<Point>()
		{
			new Point(0, 0),new Point(0, 1)
		};

		var pieces = new List<(Point, Piece)>()
		{
			(new Point(0, 0),whiteKing)
		};

		var board = new ChessBoard(points, pieces);

		Action boardIsValid = ()
			=> board.FromPoints(new Point(0, 0), new Point(0, 1))
			.ValidateMove(move =>
			{
				throw new Exception();
			})
			.ValidateBoard(board => { })
			.Apply();

		// Act.
		boardIsValid.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPieces_AndBoardNotExistToPoint_ThenThrowException()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.King, SideColor.White);

		var points = new HashSet<Point>()
		{
			new Point(0, 0)
		};

		var pieces = new List<(Point, Piece)>()
		{
			(new Point(0, 0),whiteKing)
		};

		var board = new ChessBoard(points, pieces);

		// Act.
		Action boardIsValid = ()
			=> board.FromPoints(new Point(0, 0), new Point(0, 1))
			.ValidateMove(move => { })
			.ValidateBoard(board => { });

		// Assert.
		boardIsValid.Should().Throw<Exception>();
	}
}
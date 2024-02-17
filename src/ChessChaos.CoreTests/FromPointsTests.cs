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
		var from = new Point(0, 0);
		var to = new Point(5, 3);

		var pieceProvider = new PieceProvider();
		var boardProvider = new ChessBoard(
			new HashSet<Point>()
			{
				from, to
			},
			new List<(Point, Piece)>
			{
				(from, pieceProvider.GetInstance(PieceKind.King, SideColor.White))
			}
		);

		// Act.
		Action whiteKingMove = () =>
			boardProvider.FromPoints(from, to);

		// Assert.
		whiteKingMove.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPieces_AndPieceBishopMoveIsNotCorrect_ThrowException()
	{
		// Arrange.
		var from = new Point(0, 0);
		var to = new Point(1, 0);

		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.Bishop, SideColor.White);
		var boardProvider = new ChessBoard(
			new HashSet<Point>()
			{
				from, to
			},
			new List<(Point, Piece)>
			{
				(from, whiteKing)
			}
		);

		// Act.
		Action whiteKingMove = () =>
			boardProvider.FromPoints(from, to);

		// Assert.
		whiteKingMove.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPieces_AndMoveIsCorrect_WhenMoveShouldBeTrue()
	{
		// Arrange.
		var from = new Point(0, 0);
		var to = new Point(1, 0);

		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.King, SideColor.White);
		var boardProvider = new ChessBoard(
			new HashSet<Point>()
			{
				from, to
			},
			new List<(Point, Piece)>
			{
				(from, whiteKing)
			}
		);

		// Act.
		var isCorrectMove = false;

		var whiteKingMove = boardProvider.FromPoints(from, to)
			.ValidateMove(move =>
			{
				isCorrectMove = move.From == from
				&& move.Self == whiteKing
				&& move.Target == null
				&& move.To == to;
			});

		// Assert.
		isCorrectMove.Should().BeTrue();
	}

	[TestMethod]
	public void WhenMovingPieces_AndMovingPieceIsNotApply_WhenPointShouldBeComeBack()
	{
		// Arrange.
		var from = new Point(0, 0);
		var to = new Point(1, 1);

		var pieceProvider = new PieceProvider();
		var whiteBishop = pieceProvider.GetInstance(PieceKind.Bishop, SideColor.White);
		var chessBoard = new ChessBoard(
			new HashSet<Point>
			{
				from, to
			},
			new List<(Point, Piece)>
			{
				(from, whiteBishop)
			}
		);

		chessBoard.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board => { });

		// Act.
		var isSaveMove = true;

		chessBoard.AccessBoard(state =>
		{
			var fromPointState = state[from];
			var toPointState = state[to];

			isSaveMove = toPointState != null
				&& fromPointState != whiteBishop;
		});

		// Assert.
		isSaveMove.Should().BeFalse();
	}

	[TestMethod]
	public void WhenMovingPieces_AndMovingPieceIsApply_WhenMoveShouldBeSaved()
	{
		// Arrange.
		var from = new Point(0, 0);
		var to = new Point(0, 1);

		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.King, SideColor.White);
		var chessBoard = new ChessBoard(
			new HashSet<Point>
			{
				from, to
			},
			new List<(Point, Piece)>
			{
				(from, whiteKing)
			}
		);

		chessBoard.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board => { })
			.Apply();

		// Act.
		var isSaveMove = false;

		chessBoard.AccessBoard(state =>
		{
			var fromPointState = state[from];
			var toPointState = state[to];

			isSaveMove = fromPointState == null
				&& toPointState == whiteKing;
		});

		// Assert.
		isSaveMove.Should().BeTrue();
	}

	[TestMethod]
	public void WhenMovingPieces_AndBoardThrowException_ThrowExceptionAndRevertMove()
	{
		// Arrange.
		var from = new Point(0, 0);
		var to = new Point(0, 1);

		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.King, SideColor.White);
		var chessBoard = new ChessBoard(
			new HashSet<Point>
			{
				from, to
			},
			new List<(Point, Piece)>
			{
				(from, whiteKing)
			}
		);

		Action validateBoard = () => chessBoard.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board => { throw new Exception(); })
			.Apply();

		// Act.
		var isRevertMove = false;

		chessBoard.AccessBoard(state =>
		{
			var fromPointState = state[from];
			var toPointState = state[to];

			isRevertMove = fromPointState == whiteKing
				&& toPointState == null;
		});

		// Assert.
		validateBoard.Should().Throw<Exception>();
		isRevertMove.Should().BeTrue();
	}

	[TestMethod]
	public void WhenMovingPieces_AndBoardNotExistToPoint_ThenThrowException()
	{
		// Arrange.
		var from = new Point(0, 0);
		var to = new Point(0, 1);

		var provider = new PieceProvider();

		var chessBoard = new ChessBoard(
			new HashSet<Point>()
			{
				from
			},
			new List<(Point, Piece)>
			{
				(from, provider.GetInstance(PieceKind.King, SideColor.White))
			});

		// Act.
		Action validateBoard = () => chessBoard.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board => { });

		// Assert.
		validateBoard.Should().Throw<Exception>();
	}
}
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
			boardProvider.FromPoints(from, to)
			.ValidateMove(move => { });

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
		var boardProvider = new ChessBoard(
			new HashSet<Point>()
			{
				from, to
			},
			new List<(Point, Piece)>
			{
				(from, pieceProvider.GetInstance(PieceKind.Bishop, SideColor.White))
			}
		);

		// Act.
		Action whiteKingMove = () =>
			boardProvider.FromPoints(from, to)
			.ValidateMove(move => { });

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
		var isCorrectMove = false;
		var whiteKingMove = boardProvider.FromPoints(from, to)
			.ValidateMove(move =>
			{
				if (move.To == to && move.From == from)
					isCorrectMove = true;
			});

		// Assert.
		isCorrectMove.Should().BeTrue();
	}

	[TestMethod]
	public void WhenMovingPieces_AndMovePieceOnNotExistPoint_ThrowException()
	{
		// Arrange.
		var from = new Point(0, 0);
		var to = new Point(4, 4);
		var pieceProvider = new PieceProvider();
		var boardProvider = new ChessBoard(
			new HashSet<Point>()
			{
				new Point(0,0), to
			},
			new List<(Point, Piece)>
			{
				(from, pieceProvider.GetInstance(PieceKind.Bishop, SideColor.White))
			}
		);

		// Act.
		Action whiteKingMove = () =>
			boardProvider.FromPoints(from, to)
			.ValidateMove(move => { throw new Exception(); });

		// Assert.
		whiteKingMove.Should().Throw<Exception>();
	}


	[TestMethod]
	public void WhenMovingPieces_AndMovingPieceIsNotApply_WhenPointShouldBeComeBack()
	{
		// Arrange.
		var from = new Point(0, 0);
		var to = new Point(1, 1);
		var pieceProvider = new PieceProvider();
		var chessBoard = new ChessBoard(
			new HashSet<Point>
			{
				from, to
			},
			new List<(Point, Piece)>
			{
				(from, pieceProvider.GetInstance(PieceKind.Bishop,SideColor.White))
			}
		);

		chessBoard.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board => { });

		var isSaveMove = false;
		chessBoard.AccessBoard(state =>
		{
			var fromPointState = state[new Point(0, 0)];
			var toPointState = state[new Point(1, 1)];

			if (fromPointState != null
			&& fromPointState == pieceProvider.GetInstance(PieceKind.Bishop, SideColor.White))
			{
				isSaveMove = true;
			}
		});

		isSaveMove.Should().BeTrue();
	}

	[TestMethod]
	public void WhenMovingPieces_AndMovingPieceIsApply_WhenMoveShouldBeSaved()
	{
		// Arrange.
		var from = new Point(0, 0);
		var to = new Point(0, 1);
		var pieceProvider = new PieceProvider();
		var chessBoard = new ChessBoard(
			new HashSet<Point>
			{
				from, to
			},
			new List<(Point, Piece)>
			{
				(from, pieceProvider.GetInstance(PieceKind.King,SideColor.White))
			}
		);

		chessBoard.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board => { })
			.Apply();

		var isSaveMove = false;
		chessBoard.AccessBoard(state =>
		{
			var fromPointState = state[new Point(0, 0)];
			var toPointState = state[new Point(0, 1)];

			if (fromPointState == null
			&& toPointState == pieceProvider.GetInstance(PieceKind.King, SideColor.White))
			{
				isSaveMove = true;
			}
		});

		isSaveMove.Should().BeTrue();
	}

	[TestMethod]
	public void WhenMovingPieces_AndBoardNotExistToPoint_ThenThrowException()
	{
		// Arrange.
		var from = new Point(0, 0);
		var to = new Point(4, 1);
		var provider = new PieceProvider();

		var chessBoard = new ChessBoard(
			new HashSet<Point>()
			{
				from, to
			},
			new List<(Point, Piece)>
			{
				(from, provider.GetInstance(PieceKind.King, SideColor.White))
			});

		// Act.
		Action act = () => chessBoard.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board => { });

		// Assert.
		act.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPieces_AndWhiteBishopKillBlackBishop_WhenBlackBishopDisappeared()
	{
		// Arrange.
		var from = new Point(0, 0);
		var to = new Point(1, 1);
		var provider = new PieceProvider();

		var chessBoard = new ChessBoard(
			new HashSet<Point>()
			{
				from, to
			},
			new List<(Point, Piece)>
			{
				(from, provider.GetInstance(PieceKind.Bishop, SideColor.White)),
				(to, provider.GetInstance(PieceKind.Bishop, SideColor.Black))
			});

		chessBoard.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board => { })
			.Apply();

		var isSaveMove = false;
		chessBoard.AccessBoard(state =>
		{
			var fromPointState = state[new Point(0, 0)];
			var toPointState = state[new Point(1, 1)];

			if (fromPointState == null
			&& toPointState == provider.GetInstance(PieceKind.Bishop, SideColor.White))
			{
				isSaveMove = true;
			}
		});

		isSaveMove.Should().BeTrue();
	}
}
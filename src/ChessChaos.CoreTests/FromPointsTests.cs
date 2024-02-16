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
		var boardProvider = new ChessBoard(
			new HashSet<Point>()
			{
				new Point(0,0),
				new Point(5,3),
			},
			new List<(Point, Piece)>
			{
				(new Point(0,0), pieceProvider.GetInstance(PieceKind.King, SideColor.White))
			}
		);

		// Act.
		Action whiteKingMove = () =>
			boardProvider.FromPoints(new Point(0, 0), new Point(5, 3))
			.ValidateMove(move => { });

		// Assert.
		whiteKingMove.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPieces_AndPieceBishopMoveIsNotCorrect_ThrowException()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var boardProvider = new ChessBoard(
			new HashSet<Point>()
			{
				new Point(0,0),
				new Point(1,0),
			},
			new List<(Point, Piece)>
			{
				(new Point(0,0), pieceProvider.GetInstance(PieceKind.Bishop, SideColor.White))
			}
		);

		// Act.
		Action whiteKingMove = () =>
			boardProvider.FromPoints(new Point(0, 0), new Point(1, 0))
			.ValidateMove(move => { });

		// Assert.
		whiteKingMove.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPieces_AndMovePieceOnNotExistPoint_ThrowException()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var boardProvider = new ChessBoard(
			new HashSet<Point>()
			{
				new Point(0,0),
				new Point(4,4),
			},
			new List<(Point, Piece)>
			{
				(new Point(3,3), pieceProvider.GetInstance(PieceKind.Bishop, SideColor.White))
			}
		);

		// Act.
		Action whiteKingMove = () =>
			boardProvider.FromPoints(new Point(3, 3), new Point(4, 4))
			.ValidateMove(move => { throw new Exception(); });

		// Assert.
		whiteKingMove.Should().Throw<Exception>();
	}


	[TestMethod]
	public void WhenMovingPieces_AndMovingPieceIsNotApply_WhenPointShouldBeComeBack()
	{
		var pieceProvider = new PieceProvider();
		var chessBoard = new ChessBoard(
			new HashSet<Point>
			{
			new Point(0, 0), new Point(1, 1),
			},
			new List<(Point, Piece)>
			{
			(new Point(0,0), pieceProvider.GetInstance(PieceKind.Bishop,SideColor.White))
			}
		);

		chessBoard.FromPoints(new Point(0, 0), new Point(1, 1))
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
		var pieceProvider = new PieceProvider();
		var chessBoard = new ChessBoard(
			new HashSet<Point>
			{
			new Point(0, 0), new Point(0, 1),
			},
			new List<(Point, Piece)>
			{
			(new Point(0,0), pieceProvider.GetInstance(PieceKind.King,SideColor.White))
			}
		);

		chessBoard.FromPoints(new Point(0, 0), new Point(0, 1))
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
		// Arrange
		var provider = new PieceProvider();

		var chessBoard = new ChessBoard(
			new HashSet<Point>()
			{
				new Point(0,0),
				new Point(4,1)
			},
			new List<(Point, Piece)>
			{
				(new Point(0,0), provider.GetInstance(PieceKind.King, SideColor.White))
			});

		// Act.
		Action act = () => chessBoard.FromPoints(new Point(0, 0), new Point(1, 0))
			.ValidateMove(move => { })
			.ValidateBoard(board => { });

		// Assert.
		act.Should().Throw<Exception>();
	}
}
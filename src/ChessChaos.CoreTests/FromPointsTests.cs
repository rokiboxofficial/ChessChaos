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
		var whiteKing = new PieceProvider()
			.GetInstance(PieceKind.King, SideColor.White);

		var from = new Point(1, 1);
		var to = new Point(21, 6);

		var piecesOnThePoints = new List<(Point point, Piece piece)>()
		{
			(from, whiteKing)
		};

		var realPoints = new HashSet<Point>()
		{
			from, to,
			new Point(1,0),
			new Point(0,1),
			new Point(-1,0),
			new Point(0,-1),
			new Point(-1,-1),
			new Point(-1,1),
			new Point(1,-1)
		};

		var chessBoard = new ChessBoard(realPoints, piecesOnThePoints);

		// Act.
		Action act = () => chessBoard.FromPoints(from, to);

		// Assert.
		act.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPieces_AndPieceMoveIsNotCorrect_ThenThrowException()
	{
		// Arrange.
		var whiteKing = new PieceProvider()
			.GetInstance(PieceKind.King, SideColor.White);

		var from = new Point(1, 1);
		var to = new Point(3, 3);

		var piecesOnThePoints = new List<(Point point, Piece piece)>()
		{
			(from, whiteKing)
		};

		var realPoints = new HashSet<Point>()
		{
			from, to,
			new Point(1,0),
			new Point(0,1),
			new Point(-1,0),
			new Point(0,-1),
			new Point(-1,-1),
			new Point(-1,1),
			new Point(1,-1)
		};

		var chessBoard = new ChessBoard(realPoints, piecesOnThePoints);

		// Act.
		Action act = () => chessBoard.FromPoints(from, to);

		// Assert.
		act.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPieces_AndPieceMoveCorrect_ThenMoveIsCorrect()
	{
		// Arrange.
		var whiteKing = new PieceProvider()
			.GetInstance(PieceKind.King, SideColor.White);

		var from = new Point(0, 0);
		var to = new Point(1, 1);

		var piecesOnThePoints = new List<(Point point, Piece piece)>()
		{
			(from, whiteKing)
		};

		var realPoints = new HashSet<Point>()
		{
			from,to,
			new Point(1,4),
			new Point(3,2),
			new Point(4,4),
			new Point(1,-1)
		};

		// Act.
		var validateMove = new ChessBoard(realPoints, piecesOnThePoints)
			.FromPoints(from, to)
			.ValidateMove(move => { });

		// Assert.
		validateMove.Should().NotBe(null);
	}

	[TestMethod]
	public void WhenMovingPieces_AndMoveIsCorrect_WhenBoardIsValid()
	{
		// Arrange.
		var whiteKing = new PieceProvider()
			.GetInstance(PieceKind.King, SideColor.White);
		var blackBishop = new PieceProvider()
			.GetInstance(PieceKind.Bishop, SideColor.Black);

		var from = new Point(1, 1);
		var to = new Point(2, 2);

		var piecesOnThePoints = new List<(Point point, Piece piece)>()
		{
			(from,whiteKing),
			(to,blackBishop)
		};

		var realPoints = new HashSet<Point>()
		{
			to, from,
			new Point(1,4),
			new Point(3,2),
			new Point(4,4),
			new Point(1,-1)
		};

		// Act.
		var validateBoard = new ChessBoard(realPoints, piecesOnThePoints)
			.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board => { });

		// Assert.
		validateBoard.Should().NotBe(null);
	}

	[TestMethod]
	public void WhenMovingPieces_AndBoardStateIsNotValid_ThrowException()
	{
		// Arrange.
		var whiteKing = new PieceProvider()
			.GetInstance(PieceKind.King, SideColor.White);
		var blackBishop = new PieceProvider()
			.GetInstance(PieceKind.Bishop, SideColor.Black);

		var from = new Point(1, 1);
		var to = new Point(2, 2);

		var piecesOnThePoints = new List<(Point point, Piece piece)>()
		{
			(from,whiteKing),
			(to,blackBishop)
		};

		var realPoints = new HashSet<Point>()
		{
			to, from,
			new Point(1,4),
			new Point(3,2),
			new Point(4,4),
			new Point(1,-1)
		};

		// Act.
		Action validateBoard = () => new ChessBoard(realPoints, piecesOnThePoints)
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
		validateBoard.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPieces_AndBoardStateIsApply_TheBoardIsApply()
	{
		// Arrange.
		var whiteBishop = new PieceProvider()
			.GetInstance(PieceKind.Bishop, SideColor.White);

		var from = new Point(-1, 1);
		var to = new Point(-2, 2);

		var piecesOnThePoints = new List<(Point point, Piece piece)>()
		{
			(from,whiteBishop)
		};

		var realPoints = new HashSet<Point>()
		{
			to, from,
			new Point(1,4),
			new Point(3,2),
			new Point(4,4),
			new Point(1,-1)
		};

		// Act.
		var t = new ChessBoard(realPoints, piecesOnThePoints)
			.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board => { }).Apply();

		var isPointMove = t[to].ToString();

		// Assert.
		isPointMove.Should().Be("ChessChaos.Common.Pieces.Bishop");
	}

	[TestMethod]
	public void WhenMovingPieces_AndBoardStateNotIsApply_TheBoardNotChanged()
	{
		// Arrange.
		var whiteBishop = new PieceProvider()
			.GetInstance(PieceKind.Bishop, SideColor.White);

		var from = new Point(-1, 1);
		var to = new Point(-2, 2);

		var piecesOnThePoints = new List<(Point point, Piece piece)>()
		{
			(from,whiteBishop)
		};

		var realPoints = new HashSet<Point>()
		{
			to, from,
			new Point(1,4),
			new Point(3,2),
			new Point(4,4),
			new Point(1,-1)
		};

		// Act.
		Action validateBoard = () => new ChessBoard(realPoints, piecesOnThePoints)
			.FromPoints(from, to)
			.ValidateMove(move => { })
			.ValidateBoard(board =>
			{
				if (board[from].ToString() == "ChessChaos.Common.Pieces.Bishop")
					throw new Exception();
			});

		// Assert.
		validateBoard.Should().Throw<Exception>();
	}
}
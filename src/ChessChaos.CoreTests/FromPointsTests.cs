﻿using ChessChaos.Common;
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

		var boardPoints = new HashSet<Point>()
		{
			new Point(0, 0),new Point(5, 3)
		};

		var piecesOnBoard = new List<(Point, Piece)>()
		{
			(new Point(0, 0),whiteKing)
		};

		var boardProvider = new ChessBoard(
			new HashSet<Point>() { new Point(0, 0), new Point(5, 3) },
			new List<(Point, Piece)> { (new Point(0, 0), whiteKing) });

		// Act.
		Action whiteKingMove = () =>
			boardProvider.FromPoints(new Point(0, 0), new Point(5, 3));

		// Assert.
		whiteKingMove.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPieces_AndPieceBishopMoveIsNotCorrect_ThrowException()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.Bishop, SideColor.White);

		var boardPoints = new HashSet<Point>()
		{
			new Point(0,0),new Point(1,0)
		};

		var piecesOnBoard = new List<(Point, Piece)>()
		{
			(new Point(0,0),whiteKing)
		};

		var boardProvider = new ChessBoard(boardPoints, piecesOnBoard);

		// Act.
		Action whiteKingMove = () =>
			boardProvider.FromPoints(new Point(0, 0), new Point(1, 0));

		// Assert.
		whiteKingMove.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPieces_AndMoveIsCorrect_WhenMoveShouldBeTrue()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.King, SideColor.White);

		var boardPoints = new HashSet<Point>()
		{
			new Point(0, 0),new Point(1, 0)
		};

		var piecesOnBoard = new List<(Point, Piece)>()
		{
			(new Point(0, 0),whiteKing)
		};

		var boardProvider = new ChessBoard(boardPoints, piecesOnBoard);

		// Act.
		var isCorrectMove = false;
		var whiteKingMove = boardProvider.FromPoints(new Point(0, 0), new Point(1, 0))
			.ValidateMove(move =>
			{
				isCorrectMove = move.From == new Point(0, 0)
				&& move.Self == whiteKing
				&& move.Target == null
				&& move.To == new Point(1, 0);
			});

		// Assert.
		isCorrectMove.Should().BeTrue();
	}

	[TestMethod]
	public void WhenMovingPieces_AndMovingPieceIsNotApply_WhenPointShouldBeComeBack()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteBishop = pieceProvider.GetInstance(PieceKind.Bishop, SideColor.White);

		var boardPoints = new HashSet<Point>()
		{
			new Point(0, 0),new Point(1, 1)
		};

		var piecesOnBoard = new List<(Point, Piece)>()
		{
			(new Point(0, 0),whiteBishop)
		};

		var chessBoard = new ChessBoard(boardPoints, piecesOnBoard);
		chessBoard.FromPoints(new Point(0, 0), new Point(1, 1))
			.ValidateMove(move => { }).ValidateBoard(board => { });

		// Act.
		chessBoard.AccessBoard(state =>
		{
			var isSaveMove = state[new Point(1, 1)] == whiteBishop
				&& state[new Point(0, 0)] == null;
			isSaveMove.Should().BeFalse();
		});
	}

	[TestMethod]
	public void WhenMovingPieces_AndMovingPieceIsApply_WhenMoveShouldBeSaved()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.King, SideColor.White);

		var boardPoints = new HashSet<Point>()
		{
			new Point(0, 0),new Point(0, 1)
		};

		var piecesOnBoard = new List<(Point, Piece)>()
		{
			(new Point(0, 0),whiteKing)
		};

		var chessBoard = new ChessBoard(boardPoints, piecesOnBoard);
		chessBoard.FromPoints(new Point(0, 0), new Point(0, 1))
			.ValidateMove(move => { }).ValidateBoard(board => { }).Apply();

		// Act.
		chessBoard.AccessBoard(state =>
		{
			var isSaveMove = state[new Point(0, 0)] == null
				&& state[new Point(0, 1)] == whiteKing;
			isSaveMove.Should().BeTrue();
		});
	}

	[TestMethod]
	public void WhenMovingPieces_AndBoardThrowException_RevertMove()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.King, SideColor.White);

		var boardPoints = new HashSet<Point>()
		{
			new Point(0, 0),new Point(0, 1)
		};

		var piecesOnBoard = new List<(Point, Piece)>()
		{
			(new Point(0, 0),whiteKing)
		};

		var chessBoard = new ChessBoard(boardPoints, piecesOnBoard);
		Action validateBoard = () => chessBoard.FromPoints(new Point(0, 0), new Point(0, 1))
			.ValidateMove(move => { }).ValidateBoard(board => { throw new Exception(); }).Apply();

		// Act.
		chessBoard.AccessBoard(state =>
		{
			var isRevertMove = state[new Point(0, 0)] == whiteKing
				&& state[new Point(0, 1)] == null;
			isRevertMove.Should().BeTrue();
		});
	}

	[TestMethod]
	public void WhenMovingPieces_AndMoveThrowException_RevertMove()
	{
		// Arrange.
		var pieceProvider = new PieceProvider();
		var whiteKing = pieceProvider.GetInstance(PieceKind.King, SideColor.White);

		var boardPoints = new HashSet<Point>()
		{
			new Point(0, 0),new Point(0, 1)
		};

		var piecesOnBoard = new List<(Point, Piece)>()
		{
			(new Point(0, 0),whiteKing)
		};

		var chessBoard = new ChessBoard(boardPoints, piecesOnBoard);
		Action validateBoard = () => chessBoard.FromPoints(new Point(0, 0), new Point(0, 1))
			.ValidateMove(move => { throw new Exception(); }).ValidateBoard(board => { }).Apply();

		// Act.
		validateBoard.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenMovingPieces_AndBoardNotExistToPoint_ThenThrowException()
	{
		// Arrange.
		var provider = new PieceProvider();
		var whiteKing = provider.GetInstance(PieceKind.King, SideColor.White);

		var boardPoints = new HashSet<Point>()
		{
			new Point(0, 0)
		};

		var piecesOnBoard = new List<(Point, Piece)>()
		{
			(new Point(0, 0),whiteKing)
		};

		var chessBoard = new ChessBoard(boardPoints, piecesOnBoard);

		// Act.
		Action validateBoard = () => chessBoard.FromPoints(new Point(0, 0), new Point(0, 1))
		.ValidateMove(move => { }).ValidateBoard(board => { });

		// Assert.
		validateBoard.Should().Throw<Exception>();
	}
}
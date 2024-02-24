using ChessChaos.Common;
using ChessChaos.Common.Pieces;
using ChessChaos.Core;
using FluentAssertions;
using Moq;

namespace ChessChaos.CoreTests;

[TestClass]
public class GetPieceMoveTests
{
	[TestMethod]
	public void WhenMovingPiece_AndPointNotExistOnTheBoard_ThrowException()
	{
		// Arrange.
		var provider = new PieceProvider();
		var whiteKing = provider.GetInstance(PieceKind.King, SideColor.White);

		// Act.
		var gameStateMock = new Mock<IChessGameStateReader>();

		gameStateMock.Setup(point => point[new Point(6, 6)])
			.Returns(whiteKing);

		var moveIsNotCorrect = () => new MoveProvider(gameStateMock.Object)
			.GetMove(new Point(0, 0), new Point(1, 0));

		// Assert.
		moveIsNotCorrect.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenGetMoving_AndPieceIsNotExist_ThrowException()
	{
		// Arrange.
		var from = new Point(0, 0);
		var to = new Point(1, 0);

		// Act.
		var gameStateMock = new Mock<IChessGameStateReader>();

		gameStateMock.Setup(point => point[from])
			.Returns(null as Piece);

		var moveIsNotCorrect = () => new MoveProvider(gameStateMock.Object)
			.GetMove(from, to);

		// Assert.
		moveIsNotCorrect.Should().Throw<Exception>();
	}

	[TestMethod]
	public void WhenGetMovingPiece_AndPointExistAndPieceIsExist_MoveShouldBeTrue()
	{
		// Arrange.
		var from = new Point(0, 0);
		var to = new Point(1, 0);
		var provider = new PieceProvider();
		var whiteKing = provider.GetInstance(PieceKind.King, SideColor.White);

		// Act.
		var gameStateMock = new Mock<IChessGameStateReader>();

		gameStateMock.Setup(p => p[from])
			.Returns(whiteKing);

		var moveIsCorrect = new MoveProvider(gameStateMock.Object)
			.GetMove(from, to) != null;

		// Assert.
		moveIsCorrect.Should().BeTrue();
	}
}
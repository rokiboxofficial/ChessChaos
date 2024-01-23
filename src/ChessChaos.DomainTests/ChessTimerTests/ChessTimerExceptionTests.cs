using FluentAssertions;

namespace ChessChaos.DomainTests.ChessTimerTests;

[TestClass]
public class ChessTimerExceptionTests
{
	[TestMethod]
	public void WhenCallingConstructor_AndInitialTimeIsNegative_ThenConstructorShouldThrowArgumentOutOfRangeException()
	{
		// Arrange.
		const int initialTimeInMilliseconds = -1;

		// Act.
		Action act = () => Utils.GetChessTimer(initialTimeInMilliseconds);

		// Assert.
		act.Should().Throw<ArgumentOutOfRangeException>();
	}

	[TestMethod]
	public void WhenAddingTime_AndAdditionalTimeIsNegative_ThenMethodAddShouldThrowArgumentOutOfRangeException()
	{
		// Arrange.
		const int additionalTime = -1;
		var chessTimer = Utils.GetChessTimer(initialTimeInMilliseconds: Utils.MillisecondsInSecond);
		chessTimer.Start();

		// Act.
		Action act = () => chessTimer.Add(additionalTime);

		// Assert.
		act.Should().Throw<ArgumentOutOfRangeException>();
	}

	[TestMethod]
	public void WhenStopping_AndStartIsNeverCalledYet_ThenMethodStopShouldThrowInvalidOperationException()
	{
		// Arrange.
		var chessTimer = Utils.GetChessTimer(initialTimeInMilliseconds: Utils.MillisecondsInSecond);

		// Act.
		Action act = chessTimer.Stop;

		// Assert.
		act.Should().Throw<InvalidOperationException>();
	}

	[TestMethod]
	public void WhenStopping_AndTimerIsAlreadyStopped_ThenMethodStopShouldThrowInvalidOperationException()
	{
		// Arrange.
		var chessTimer = Utils.GetChessTimer(initialTimeInMilliseconds: Utils.MillisecondsInSecond);
		chessTimer.Start();
		chessTimer.Stop();

		// Act.
		Action act = chessTimer.Stop;

		// Assert.
		act.Should().Throw<InvalidOperationException>();
	}

	[TestMethod]
	public void WhenStarting_AndTimerIsAlreadyStarted_ThenMethodStartShouldThrowInvalidOperationException()
	{
		// Arrange.
		var chessTimer = Utils.GetChessTimer(initialTimeInMilliseconds: Utils.MillisecondsInSecond);
		chessTimer.Start();

		// Act.
		Action act = chessTimer.Start;

		// Assert.
		act.Should().Throw<InvalidOperationException>();
	}
}
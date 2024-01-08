using FluentAssertions;

namespace ChessChaos.DomainTests.ChessTimerTests;

[TestClass]
public class ChessTimerTimeLeftTests
{
	[TestMethod]
	public void WhenGettingTimeLeft_AndInitialTimeIs5SecondsAndTimerStoppedAfter4Seconds_ThenTimeLeftShouldBe1Second()
	{
		// Arrange.
		var fakeDateTime = DateTime.MinValue;

		var chessTimer = Utils.GetChessTimer(() => fakeDateTime, initialTimeInMilliseconds: 5 * Utils.MillisecondsInSecond);

		chessTimer.Start();
		fakeDateTime = fakeDateTime.AddSeconds(4);
		chessTimer.Stop();

		// Act.
		var timeLeft = chessTimer.TimeLeftInMilliseconds;

		// Assert.
		timeLeft.Should().Be(Utils.MillisecondsInSecond);
	}

	[TestMethod]
	public void WhenGettingTimeLeft_AndTimeLeftIs2SecondsAnd2SecondsWasAddedAndTimerIsRunning_ThenTimeLeftShouldBe4Seconds()
	{
		// Arrange.
		var fakeDateTime = DateTime.MinValue;
		var chessTimer = Utils.GetChessTimer(() => fakeDateTime, initialTimeInMilliseconds: 5 * Utils.MillisecondsInSecond);

		fakeDateTime = DateTime.MinValue;
		chessTimer.Start();
		fakeDateTime = fakeDateTime.AddSeconds(3);
		chessTimer.Add(2 * Utils.MillisecondsInSecond);

		// Act.
		var timeLeft = chessTimer.TimeLeftInMilliseconds;

		// Assert.
		timeLeft.Should().Be(4 * Utils.MillisecondsInSecond);
	}

	[TestMethod]
	public void WhenGettingTimeLeft_AndTimeLeftIs2SecondsAndTimerWasStoppedAnd7SecondsWasAdded_ThenTimeLeftShouldBe3400Milliseconds()
	{
		// Arrange.
		var fakeDateTime = DateTime.MinValue;
		var chessTimer = Utils.GetChessTimer(() => fakeDateTime, initialTimeInMilliseconds: 5 * Utils.MillisecondsInSecond);

		fakeDateTime = DateTime.MinValue;
		chessTimer.Start();
		fakeDateTime = fakeDateTime.AddSeconds(3);
		chessTimer.Stop();
		chessTimer.Add(7 * Utils.MillisecondsInSecond);

		// Act.
		var timeLeft = chessTimer.TimeLeftInMilliseconds;

		// Assert.
		timeLeft.Should().Be(9 * Utils.MillisecondsInSecond);
	}
}
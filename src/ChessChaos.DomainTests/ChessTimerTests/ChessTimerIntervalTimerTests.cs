using ChessChaos.Domain.Timers;
using FluentAssertions;

namespace ChessChaos.DomainTests.ChessTimerTests;

[TestClass]
public class ChessTimerIntervalTimerTests
{
	[TestMethod]
	public void WhenGettingIntervalTimerInterval_AndChessTimerIsStarted_ThenIntervalShouldBeEqualInitialTime()
	{
		// Arrange.
		const int initialTimeInMilliseconds = Utils.MillisecondsInSecond;
		var (chessTimer, intervalTimer) = Utils.GetChessTimerAndIntervalTimer(initialTimeInMilliseconds);
		chessTimer.Start();

		// Act.
		var interval = intervalTimer.Interval;

		// Assert.
		interval.Should().Be(initialTimeInMilliseconds);
	}

	[TestMethod]
	public void WhenGettingIntervalTimerInterval_AndChessTimerIsStartedAndInitialTimeIs1SecondAnd2SecondsWasAdded_ThenIntervalShouldBe3Seconds()
	{
		// Arrange.
		var (chessTimer, intervalTimer) = Utils.GetChessTimerAndIntervalTimer(initialTimeInMilliseconds: Utils.MillisecondsInSecond);
		chessTimer.Start();
		chessTimer.Add(2 * Utils.MillisecondsInSecond);

		// Act.
		var interval = intervalTimer.Interval;

		// Assert.
		interval.Should().Be(3 * Utils.MillisecondsInSecond);
	}

	[TestMethod]
	public void WhenGettingIntervalTimerInterval_AndChessTimerIsStartedAndInitialTimeIs6SecondAnd4SecondWasWaitedAnd2SecondsWasAdded_ThenIntervalShouldBe4Seconds()
	{
		// Arrange.
		var fakeDateTime = DateTime.MinValue;
		var (chessTimer, intervalTimer) = Utils.GetChessTimerAndIntervalTimer(() => fakeDateTime, initialTimeInMilliseconds: 6 * Utils.MillisecondsInSecond);

		chessTimer.Start();
		fakeDateTime = fakeDateTime.AddSeconds(4);
		chessTimer.Add(2 * Utils.MillisecondsInSecond);

		// Act.
		var interval = intervalTimer.Interval;

		// Assert.
		interval.Should().Be(4 * Utils.MillisecondsInSecond);
	}

	[TestMethod]
	public void WhenGettingIntervalTimerEnabled_AndChessTimerIsStarted_ThenEnabledShouldBeTrue()
	{
		// Arrange.
		var (chessTimer, intervalTimer) = Utils.GetChessTimerAndIntervalTimer(initialTimeInMilliseconds: Utils.MillisecondsInSecond);
		chessTimer.Start();

		// Act.
		var enabled = intervalTimer.Enabled;

		// Assert.
		enabled.Should().BeTrue();
	}

	[TestMethod]
	public void WhenGettingIntervalTimerEnabled_AndChessTimerIsStartedAndStopped_ThenEnabledShouldBeFalse()
	{
		// Arrange.
		var (chessTimer, intervalTimer) = Utils.GetChessTimerAndIntervalTimer(initialTimeInMilliseconds: Utils.MillisecondsInSecond);
		chessTimer.Start();
		chessTimer.Stop();

		// Act.
		var enabled = intervalTimer.Enabled;

		// Assert.
		enabled.Should().BeFalse();
	}

	[TestMethod]
	public void WhenGettingIntervalTimerEnabled_AndChessTimerIsNotStartedYet_ThenEnabledShouldBeFalse()
	{
		// Arrange.
		var (_, intervalTimer) = Utils.GetChessTimerAndIntervalTimer(initialTimeInMilliseconds: Utils.MillisecondsInSecond);

		// Act.
		var enabled = intervalTimer.Enabled;

		// Assert.
		enabled.Should().BeFalse();
	}

	[TestMethod]
	public void WhenGettingIntervalTimerEnabled_AndChessTimerIsStartedAndTimeWasAdded_ThenEnabledShouldBeTrue()
	{
		// Arrange.
		var (chessTimer, intervalTimer) = Utils.GetChessTimerAndIntervalTimer(initialTimeInMilliseconds: Utils.MillisecondsInSecond);

		chessTimer.Start();
		chessTimer.Add(Utils.MillisecondsInSecond);

		// Act.
		var enabled = intervalTimer.Enabled;

		// Assert.
		enabled.Should().BeTrue();
	}

	[TestMethod]
	public void WhenGettingIntervalTimerEnabled_AndChessTimerIsStartedAndStoppedAndTimeWasAdded_ThenEnabledShouldBeFalse()
	{
		// Arrange.
		var (chessTimer, intervalTimer) = Utils.GetChessTimerAndIntervalTimer(initialTimeInMilliseconds: Utils.MillisecondsInSecond);

		chessTimer.Start();
		chessTimer.Stop();
		chessTimer.Add(Utils.MillisecondsInSecond);

		// Act.
		var enabled = intervalTimer.Enabled;

		// Assert.
		enabled.Should().BeFalse();
	}

	[TestMethod]
	public void WhenElaspsingIntervalTimer_AndChessTimerIsRunning_ThenChessTimerElapsedShouldBeRaise()
	{
		// Arrange.
		var (chessTimer, intervalTimer, intervalTimerMock) = Utils.GetChessTimerAndIntervalTimerAndMock(initialTimeInMilliseconds: Utils.MillisecondsInSecond);
		using var monitoredChessTimer = chessTimer.Monitor();
		chessTimer.Start();

		// Act.
		intervalTimerMock.Raise(intervalTimer => intervalTimer.Elapsed += null);

		// Assert.
		monitoredChessTimer.Should().Raise(nameof(ChessTimer.Elapsed));
	}
}
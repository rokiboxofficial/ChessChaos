using ChessChaos.Domain.Timers;
using Moq;
using ChessChaos.Domain.Abstractions;

namespace ChessChaos.DomainTests.ChessTimerTests;

public static class Utils
{
	public const int MillisecondsInSecond = 1000;

	public static ChessTimer GetChessTimer(int initialTimeInMilliseconds)
		=> GetChessTimer(() => DateTime.MinValue, initialTimeInMilliseconds);

	public static (ChessTimer, IIntervalTimer) GetChessTimerAndIntervalTimer(int initialTimeInMilliseconds)
		=> GetChessTimerAndIntervalTimer(() => DateTime.MinValue, initialTimeInMilliseconds);

	public static ChessTimer GetChessTimer(Func<DateTime> fakeDateTimeProvider, int initialTimeInMilliseconds)
	{
		var (chessTimer, _) = GetChessTimerAndIntervalTimer(fakeDateTimeProvider, initialTimeInMilliseconds);

		return chessTimer;
	}

	public static (ChessTimer chessTimer, IIntervalTimer intervalTimer) GetChessTimerAndIntervalTimer(Func<DateTime> fakeDateTimeProvider, int initialTimeInMilliseconds)
	{
		var dateTimeProvider = GetMockDateTimeNowProvider(fakeDateTimeProvider);
		var intervalTimer = GetMockIntervalTimer();
		var chessTimer = new ChessTimer(intervalTimer, dateTimeProvider, initialTimeInMilliseconds);

		return (chessTimer, intervalTimer);
	}

	private static IDateTimeNowProvider GetMockDateTimeNowProvider(Func<DateTime> fakeDateTimeProvider)
	{
		var dateTimeNowProviderMock = new Mock<IDateTimeNowProvider>();
		dateTimeNowProviderMock
			.Setup(dateTimeProvider => dateTimeProvider.Now)
			.Returns(fakeDateTimeProvider);

		var dateTimeProvider = dateTimeNowProviderMock.Object;

		return dateTimeProvider;
	}

	private static IIntervalTimer GetMockIntervalTimer()
	{
		var enabled = false;
		var intervalTimerMock = new Mock<IIntervalTimer>();
		intervalTimerMock.SetupProperty(intervalTimer => intervalTimer.Interval);
		intervalTimerMock.Setup(intervalTimer => intervalTimer.Start()).Callback(() => enabled = true);
		intervalTimerMock.Setup(intervalTimer => intervalTimer.Stop()).Callback(() => enabled = false);
		intervalTimerMock.SetupGet(intervalTimer => intervalTimer.Enabled).Returns(() => enabled);
		var intervalTimer = intervalTimerMock.Object;

		return intervalTimer;
	}
}
using ChessChaos.Domain.Abstractions;

namespace ChessChaos.Domain.Timers;

public sealed class ChessTimer : IChessTimer
{
	public event Action Elapsed = null!;
	private readonly IIntervalTimer _intervalTimer;
	private readonly IDateTimeNowProvider _dateTimeNowProvider;
	private DateTime _lastStartingTime;
	private bool _isRunning = false;

	public ChessTimer(
		IIntervalTimer intervalTimer,
		IDateTimeNowProvider dateTimeNowProvider,
		int initialTimeInMilliseconds)
	{
		if (initialTimeInMilliseconds < 0)
			throw new ArgumentOutOfRangeException(nameof(initialTimeInMilliseconds), $"{nameof(initialTimeInMilliseconds)} can't be negative");

		_intervalTimer = intervalTimer;
		_dateTimeNowProvider = dateTimeNowProvider;
		TimeLeftInMilliseconds = initialTimeInMilliseconds;
	}

	public int TimeLeftInMilliseconds { get; private set; }

	public void Add(int milliseconds)
	{
		if (milliseconds < 0)
			throw new ArgumentOutOfRangeException(nameof(milliseconds), $"{nameof(milliseconds)} can't be negative");

		if (!_isRunning)
		{
			TimeLeftInMilliseconds += milliseconds;
			return;
		}

		Stop();
		TimeLeftInMilliseconds += milliseconds;
		Start();
	}

	public void Start()
	{
		if (_isRunning)
			throw new InvalidOperationException("Timer is already running");

		_lastStartingTime = _dateTimeNowProvider.Now;
		_intervalTimer.Interval = TimeLeftInMilliseconds;
		_intervalTimer.Start();
		_isRunning = true;
	}

	public void Stop()
	{
		if (!_isRunning)
			throw new InvalidOperationException("Timer is already stopped");

		_intervalTimer.Stop();
		var iterationTime = _dateTimeNowProvider.Now - _lastStartingTime;
		var iterationTimeInMilliseconds = (int)iterationTime.TotalMilliseconds;
		TimeLeftInMilliseconds -= iterationTimeInMilliseconds;
		_isRunning = false;
	}
}
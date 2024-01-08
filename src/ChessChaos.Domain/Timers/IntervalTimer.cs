namespace ChessChaos.Domain.Timers;

public class IntervalTimer : IIntervalTimer, IDisposable
{
	public event Action Elapsed = null!;
	private readonly System.Timers.Timer _internalTimer;

	public IntervalTimer(int interval)
	{
		_internalTimer = new System.Timers.Timer(interval);
		_internalTimer.Elapsed += (_, _) => Elapsed?.Invoke();
	}

	public int Interval
	{
		get => (int)_internalTimer.Interval;
		set => _internalTimer.Interval = value;
	}

	public bool Enabled => _internalTimer.Enabled;

	public void Start()
		=> _internalTimer.Start();

	public void Stop()
		=> _internalTimer.Stop();

	public void Dispose()
		=> _internalTimer.Dispose();
}
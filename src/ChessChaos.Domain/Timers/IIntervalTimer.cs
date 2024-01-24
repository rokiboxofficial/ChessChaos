namespace ChessChaos.Domain.Timers;

public interface IIntervalTimer
{
	public event Action Elapsed;
	public int Interval { get; set; }
	public bool Enabled { get; }
	public void Start();
	public void Stop();
}
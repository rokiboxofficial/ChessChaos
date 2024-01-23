namespace ChessChaos.Domain.Timers;

public interface IChessTimer
{
	public event Action Elapsed;
	public void Add(int milliseconds);
	public void Start();
	public void Stop();
}
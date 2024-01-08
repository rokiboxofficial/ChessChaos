namespace ChessChaos.Domain.Abstractions;

public interface IDateTimeNowProvider
{
	public DateTime Now { get; }
}
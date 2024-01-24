using ChessChaos.Common;

namespace ChessChaos.Domain;

public class ChessMessage
{
	public Point From { get; set; }
	public Point To { get; set; }
	public SideColor PlayerColor { get; set; }
}
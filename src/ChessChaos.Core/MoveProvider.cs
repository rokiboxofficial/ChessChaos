using ChessChaos.Common;

namespace ChessChaos.Core;

public class MoveProvider
{
	private readonly IChessGameStateReader _chessGameStateReader;

	public MoveProvider(IChessGameStateReader chessGameStateReader)
	{
		_chessGameStateReader = chessGameStateReader;
	}

	// TODO: throw execption if not null
	public IChessMove GetMove(Point from, Point to)
		=> _chessGameStateReader[from]?.GetMove(_chessGameStateReader, from, to);
}
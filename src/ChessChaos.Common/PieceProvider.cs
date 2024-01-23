namespace ChessChaos.Common;

using ChessChaos.Common.Pieces;
using System.Collections.Concurrent;

public class PieceProvider
{
	private readonly ConcurrentDictionary<SideColor, Piece> _pieceByKindAndColor = new();
	private readonly IReadOnlyDictionary<PieceKind, Func<SideColor, Piece>> _pieceFactoryByPieceKind;

	public PieceProvider()
	{
		_pieceFactoryByPieceKind = new Dictionary<PieceKind, Func<SideColor, Piece>>()
		{
			[PieceKind.Bishop] = (color) => new Bishop(color),
			[PieceKind.King] = (color) => new King(color),
		};
	}

	public Piece GetInstance(PieceKind kind, SideColor color)
	{
		if (!_pieceByKindAndColor.ContainsKey(color))
		{
			var piece = _pieceFactoryByPieceKind[kind](color);
			_pieceByKindAndColor.TryAdd(color, piece);
		}

		return _pieceByKindAndColor[color];
	}
}
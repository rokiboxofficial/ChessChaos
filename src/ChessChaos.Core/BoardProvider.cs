using ChessChaos.Common;

namespace ChessChaos.Core;

internal class BoardProvider
{
	private readonly Stack<Action> _revertActions = new();
	private readonly IChessGameStateReader _chessGameStateReader;
	private readonly IChessGameStateWriter _chessGameStateWriter;

	public BoardProvider(IChessGameStateReader chessGameStateReader, IChessGameStateWriter chessGameStateWriter)
	{
		_chessGameStateReader = chessGameStateReader;
		_chessGameStateWriter = chessGameStateWriter;
	}

	public void AccessBoard(Action<IChessGameStateReader> accessor)
		=> accessor?.Invoke(_chessGameStateReader);

	internal void AccessBoard(ICommand move, Action<IChessGameStateReader> accessor)
	{
		var revertAction = ExecuteActionAndGetRevertAction(move);
		using var boardContext = new BoardContext(_chessGameStateReader, revertAction);
		accessor?.Invoke(boardContext.ChessGameState);
	}

	internal void Apply(ICommand move)
	{
		var revertAction = ExecuteActionAndGetRevertAction(move);
		_revertActions.Push(revertAction);
	}

	private Action ExecuteActionAndGetRevertAction(ICommand move)
	{
		move.Execute(_chessGameStateWriter);
		// TODO: probably extract to service
		// box to reverting action (fields)
		Action revertAction = () =>
		{
			move.Revert(_chessGameStateWriter);
			// BASE COPY OF FIELDS
		};

		return revertAction;
	}

	private class BoardContext : IDisposable
	{
		private readonly Action _revertAction;

		public BoardContext(IChessGameStateReader chessGameStateReader, Action revertAction)
		{
			ChessGameState = chessGameStateReader;
			_revertAction = revertAction;
		}

		public BoardContext(IChessGameStateReader chessGameStateReader)
		{
			ChessGameState = chessGameStateReader;
			_revertAction = null!;
		}

		public IChessGameStateReader ChessGameState { get; }

		public void Dispose()
			=> _revertAction?.Invoke();
	}
}
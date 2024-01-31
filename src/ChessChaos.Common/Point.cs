using System.Diagnostics.CodeAnalysis;

namespace ChessChaos.Common;

public readonly struct Point
{
	public Point(int x, int y)
	{
		X = x;
		Y = y;
	}

	public int X { get; }
	public int Y { get; }

	public override int GetHashCode()
	  => HashCode.Combine(X, Y);

	public override bool Equals([NotNullWhen(true)] object? obj)
	  => obj is Point && Equals((Point)obj);

	public bool Equals(Point other)
	  => this == other;

	public static bool operator ==(Point p1, Point p2)
	  => p1.X == p2.X && p1.Y == p2.Y;

	public static bool operator !=(Point p1, Point p2)
	  => !(p1 == p2);

	public Point Offset(Point offset)
	  => new Point(offset.X + X, offset.Y + Y);

	public static Point operator -(Point p1, Point p2)
	  => new Point(p1.X - p2.X, p1.Y - p2.Y);

	public static Point GetDirection(Point from, Point to)
	{
		ThrowIfNotValid(from, to);

		var firstPointDirection = YPointDirection(from);
		var secondPointDirection = XPointDirection(to);

		return IsDiagonalDirection(secondPointDirection, firstPointDirection)
			? GetDiagonalDirection(from, to)
			: GetHorizontalOrVerticalDirection(from, to);
	}

	private static Point GetHorizontalOrVerticalDirection(Point from, Point to)
	{
		var firstPointDirection = XPointDirection(to);
		var secondPointDirection = YPointDirection(from);

		return IsHorizontalDirection(firstPointDirection, secondPointDirection)
			? GetHorizontalDirection(secondPointDirection, firstPointDirection)
			: GetVerticalDirection(secondPointDirection, firstPointDirection);
	}

	private static Point GetVerticalDirection(int secondPointDirection, int firstPointDirection)
	{
		return firstPointDirection < 0
			? new Point(secondPointDirection, firstPointDirection / firstPointDirection * -1)
			: new Point(secondPointDirection, firstPointDirection / firstPointDirection);
	}

	private static Point GetHorizontalDirection(int secondPointDirection, int firstPointDirection)
	{
		return secondPointDirection < 0
			? new Point(secondPointDirection / secondPointDirection * -1, firstPointDirection)
			: new Point(secondPointDirection / secondPointDirection, firstPointDirection);
	}

	private static Point GetDiagonalDirection(Point from, Point to)
	{
		var x = to.X - from.X;
		var y = to.Y - from.Y;

		return new Point((x) / Math.Abs(x), (y) / Math.Abs(y));
	}

	private static int YPointDirection(Point from)
	{
		return from.X + from.Y;
	}

	private static int XPointDirection(Point to)
	{
		return to.X + to.Y;
	}

	private static void ThrowIfNotValid(Point source, Point target)
	{
		if (Math.Abs(source.X) == Math.Abs(source.Y)
			&& Math.Abs(target.X) == Math.Abs(target.Y))
		{
			throw new ArgumentException("Inalid input");
		}

		if (Math.Abs(source.X) == Math.Abs(target.X)
			&& Math.Abs(source.Y) == Math.Abs(target.Y))
		{
			throw new ArgumentException("Inalid input");
		}
	}

	private static bool IsDiagonalDirection(int first, int second)
	{
		return first != 0 && second != 0;
	}

	private static bool IsHorizontalDirection(int first, int second)
	{
		return second != 0 && first == 0;
	}
}
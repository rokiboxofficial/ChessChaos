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

		return IsDiagonalDirection(from, to)
			? GetDiagonalDirection(from, to)
			: GetHorizontalOrVerticalDirection(from, to);
	}

	private static Point GetHorizontalOrVerticalDirection(Point from, Point to)
	{
		return IsHorizontalDirection(from, to)
			? GetHorizontalDirection(from, to)
			: GetVerticalDirection(from, to);
	}

	private static Point GetVerticalDirection(Point from, Point to)
	{
		var fromPointX = from.X;
		var toPointY = to.Y;

		return fromPointX < 0
			? new Point(toPointY - toPointY, fromPointX / fromPointX * -1)
			: new Point(toPointY - toPointY, fromPointX / fromPointX);
	}

	private static Point GetHorizontalDirection(Point from, Point to)
	{
		var toPointX = to.X;
		var fromPointY = from.Y;

		return toPointX < 0
			? new Point(toPointX / toPointX * -1, fromPointY - fromPointY)
			: new Point(toPointX / toPointX, fromPointY - fromPointY);
	}

	private static Point GetDiagonalDirection(Point from, Point to)
	{
		var x = to.X - from.X;
		var y = to.Y - from.Y;

		return new Point((x) / Math.Abs(x), (y) / Math.Abs(y));
	}
	private static void ThrowIfNotValid(Point source, Point target)
	{
		if (Math.Abs(source.X) == Math.Abs(source.Y)
			&& Math.Abs(target.X) == Math.Abs(target.Y)
			|| Math.Abs(source.X) == Math.Abs(target.X)
			&& Math.Abs(source.Y) == Math.Abs(target.Y))
		{
			throw new ArgumentException("Inalid input");
		}
	}

	private static bool IsDiagonalDirection(Point from, Point to)
	{
		var fromPointSum = from.X != to.X;
		var secondPointSum = from.Y != to.Y;

		return fromPointSum && secondPointSum;
	}

	private static bool IsHorizontalDirection(Point from, Point to)
	{
		var x = Math.Abs(to.X) > Math.Abs(from.X);
		var y = Math.Abs(to.Y) == Math.Abs(from.Y);

		return x && y;
	}
}
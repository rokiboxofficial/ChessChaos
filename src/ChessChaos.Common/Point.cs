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
		return from.X == to.X
			? GetVerticalDirection(from, to)
			: GetHorizontalDirection(from, to);
	}

	private static Point GetVerticalDirection(Point from, Point to)
	{
		return from.Y > to.Y
			? new Point(0, -1)
			: new Point(0, 1);
	}

	private static Point GetHorizontalDirection(Point from, Point to)
	{
		return from.X > to.X
			? new Point(-1, 0)
			: new Point(1, 0);
	}

	private static Point GetDiagonalDirection(Point from, Point to)
	{
		var differenceBetweenX = to.X - from.X;
		var differenceBetweenY = to.Y - from.Y;

		return new Point(differenceBetweenX / Math.Abs(differenceBetweenX),
				differenceBetweenY / Math.Abs(differenceBetweenY));
	}

	private static void ThrowIfNotValid(Point from, Point to)
	{
		if ((from.X == to.X && from.Y == to.Y)
			|| (from.X != to.X && from.Y != to.Y
			&& (Math.Abs(Math.Abs(from.X) - Math.Abs(to.X))
			!= Math.Abs(Math.Abs(from.Y) - Math.Abs(to.Y)))))
		{
			throw new ArgumentException("The direction is not valid");
		}
	}

	private static bool IsDiagonalDirection(Point from, Point to)
	{
		var differenceBetweenX = Math.Abs(from.X) - Math.Abs(to.X);
		var differenceBetweenY = Math.Abs(from.Y) - Math.Abs(to.Y);

		return Math.Abs(differenceBetweenX) == Math.Abs(differenceBetweenY);
	}
}
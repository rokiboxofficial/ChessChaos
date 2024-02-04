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
		var subtractPointsXVertical = Math.Abs(from.X) - Math.Abs(to.X);

		return subtractPointsXVertical == 0 && from.X == to.X
			? GetVerticalDirection(from, to)
			: GetHorizontalDirection(from, to);
	}

	private static Point GetVerticalDirection(Point from, Point to)
	{
		var normalizationY = from.Y / from.Y;

		if (from.X != to.X)
		{
			throw new ArgumentException("Source point X is not equals Target point X");
		}

		return from.Y > to.Y
			? new Point(0, normalizationY * -1)
			: new Point(0, normalizationY);
	}

	private static Point GetHorizontalDirection(Point from, Point to)
	{
		var normalizationX = from.X / from.X;

		if (from.Y != to.Y)
		{
			throw new ArgumentException("Source point Y is not equals Target point Y");
		}

		return from.X > to.X
			? new Point(normalizationX * -1, 0)
			: new Point(normalizationX, 0);
	}

	private static Point GetDiagonalDirection(Point from, Point to)
	{
		var subtractPointsX = to.X - from.X;
		var subtractPointsY = to.Y - from.Y;

		return new Point((subtractPointsX) / Math.Abs(subtractPointsX),
				(subtractPointsY) / Math.Abs(subtractPointsY));
	}

	private static void ThrowIfNotValid(Point from, Point to)
	{
		if (from.X == to.X && from.Y == to.Y
			|| from.X == 0 && to.X == 0
			&& from.Y == 0 && to.Y == 0)
		{
			throw new ArgumentException();
		}
	}

	private static bool IsDiagonalDirection(Point from, Point to)
	{
		var isFromAndToPointsXNotEquals = Math.Abs(from.X) - Math.Abs(to.X);
		var isFromAndToPointsYNotEquals = Math.Abs(from.Y) - Math.Abs(to.Y);

		return Math.Abs(isFromAndToPointsXNotEquals) == Math.Abs(isFromAndToPointsYNotEquals);
	}
}
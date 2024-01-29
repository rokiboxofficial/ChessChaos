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

	public Point GetDirection(Point from, Point to)
	{
		ThrowIfNotValid(from, to);

		return IsDioganalDirection(GetSumSecondPoint(to), GetSumFirstPoint(from))
			? GetDiagonalDirection(from, to)
			: GetHorizontalOrVerticalDirection(from, to);
	}

	private Point GetHorizontalOrVerticalDirection(Point from, Point to)
	{
		var sumXYFirstPoint = GetSumSecondPoint(to);
		var sumXYSecondPoint = GetSumFirstPoint(from);

		return IsHorizontalDirection(sumXYFirstPoint, sumXYSecondPoint)
			? GetHorizontalDirection(sumXYSecondPoint, sumXYFirstPoint)
			: GetVerticalDirection(sumXYSecondPoint, sumXYFirstPoint);
	}

	private Point GetVerticalDirection(int sumXYSecondPoint, int sumXYFirstPoint)
	{
		return sumXYFirstPoint < 0
			? new Point(sumXYSecondPoint, sumXYFirstPoint / sumXYFirstPoint * -1)
			: new Point(sumXYSecondPoint, sumXYFirstPoint / sumXYFirstPoint);
	}

	private Point GetHorizontalDirection(int sumXYSecondPoint, int sumXYFirstPoint)
	{
		return sumXYSecondPoint < 0
				? new Point(sumXYSecondPoint / sumXYSecondPoint * -1, sumXYFirstPoint)
				: new Point(sumXYSecondPoint / sumXYSecondPoint, sumXYFirstPoint);
	}

	private Point GetDiagonalDirection(Point from, Point to)
	{
		return new Point((to.X - from.X) / Math.Abs(to.X - from.X),
					(to.Y - from.Y) / Math.Abs(to.Y - from.Y));
	}

	private int GetSumFirstPoint(Point from)
	{
		return from.X + from.Y;
	}

	private int GetSumSecondPoint(Point to)
	{
		return to.X + to.Y;
	}

	private void ThrowIfNotValid(Point source, Point target)
	{
		if (Math.Abs(source.X) == Math.Abs(source.Y)
			&& Math.Abs(target.X) == Math.Abs(target.Y))
		{
			throw new ArgumentException("Inalid input");
		}
	}

	private bool IsDioganalDirection(int first, int second)
	{
		return first != 0 && second != 0;
	}

	private bool IsHorizontalDirection(int first, int second)
	{
		return second != 0 && first == 0;
	}
}
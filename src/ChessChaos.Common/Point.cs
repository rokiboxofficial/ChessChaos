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

	public Point GetNormalizationVectors(Point from, Point to)
	{
		ThrowIfNotValid(from, to);

		if (from.X + from.Y != 0 && to.X + to.Y != 0)
		{
			return GetDiagonalDirection(from, to);
		}

		return GetHorizontalOrVerticalDirection(from, to);
	}

	private Point GetHorizontalOrVerticalDirection(Point from, Point to)
	{
		var sumXYFirstPoint = to.X + to.Y;
		var sumXYSecondPoint = from.X + from.Y;

		if (sumXYSecondPoint != 0 && sumXYFirstPoint == 0)
		{
			return sumXYSecondPoint < 0
				? new Point(sumXYSecondPoint / sumXYSecondPoint * -1, sumXYFirstPoint)
				: new Point(sumXYSecondPoint / sumXYSecondPoint, sumXYFirstPoint);
		}

		else if (sumXYSecondPoint == 0 && sumXYFirstPoint != 0)
		{
			return sumXYFirstPoint < 0
				? new Point(sumXYSecondPoint, sumXYFirstPoint / sumXYFirstPoint * -1)
				: new Point(sumXYSecondPoint, sumXYFirstPoint / sumXYFirstPoint);
		}

		throw new ArgumentException("Invalid coordinates");
	}

	private Point GetDiagonalDirection(Point from, Point to)
	{
		return new Point((to.X - from.X) / Math.Abs(to.X - from.X),
					(to.Y - from.Y) / Math.Abs(to.Y - from.Y));
	}

	private void ThrowIfNotValid(Point source, Point target)
	{
		if (Math.Abs(source.X) == Math.Abs(source.Y)
			&& Math.Abs(target.X) == Math.Abs(target.Y))
		{
			throw new ArgumentException("Inalid vector coordinates");
		}
	}
}
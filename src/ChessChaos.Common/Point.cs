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
		GetValidatingVectors(from, to);

		var calcTo = to.X + to.Y;
		var calcFrom = from.X + from.Y;

		if (calcFrom != 0 && calcTo != 0)
		{
			return GetDioganalDirection(from, to);
		}

		return GetHorizontalOrVerticalDirection(calcFrom, calcTo);
	}

	public Point GetHorizontalOrVerticalDirection(int from, int to)
	{
		if (from != 0 && to == 0)
		{
			if (from < 0)
			{
				return new Point(from / from * -1, to);
			}
			return new Point(from / from, to);
		}

		else if (from == 0 && to != 0)
		{
			if (to < 0)
			{
				return new Point(from, to / to * -1);
			}
			return new Point(from, to / to);
		}
		throw new ArgumentException("EROR1");
	}

	public Point GetDioganalDirection(Point from, Point to)
	{
		try
		{
			return new Point((to.X - from.X) / Math.Abs(to.X - from.X),
						(to.Y - from.Y) / Math.Abs(to.Y - from.Y));
		}
		catch
		{
			throw new ArgumentException();
		}
	}

	private void GetValidatingVectors(Point source, Point target)
	{
		if ((Math.Abs(source.X) == Math.Abs(source.Y)
			&& Math.Abs(target.X) == Math.Abs(target.Y)))
		{
			throw new ArgumentException("EROR2");
		}
	}
}
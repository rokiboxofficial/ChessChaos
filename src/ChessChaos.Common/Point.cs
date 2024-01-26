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

	public Point GetDirection(Point source, Point target)
	{
		var validVector = ValidationVectors(source, target);
		var resultPoint = new Point();

		if (validVector)
		{
			resultPoint = new Point((source.X - target.X) / Math.Abs(source.X - target.X),
							(source.Y - target.Y) / Math.Abs(source.Y - target.Y));
		}

		else
		{
			throw new ArgumentException();
		}

		return resultPoint;
	}

	private bool ValidationVectors(Point source, Point target)
	{
		if (Math.Abs(source.X) == Math.Abs(source.Y)
			&& Math.Abs(target.X) == Math.Abs(target.Y))
		{
			double x = (double)source.X / target.X;
			double y = (double)source.Y / target.Y;

			if (x == y)
			{
				return true;
			}
		}

		return false;
	}
}
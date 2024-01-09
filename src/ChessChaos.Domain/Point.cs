using System.Diagnostics.CodeAnalysis;

namespace ChessChaos.Domain;

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

	public Point Offset(int dx, int dy)
	{
		return new Point(X + dx, Y + dy);
	}

	public Point Offset(Point offset)
		=> Offset(offset.X, offset.Y);
}
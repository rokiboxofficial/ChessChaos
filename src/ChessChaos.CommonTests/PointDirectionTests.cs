using ChessChaos.Common;
using FluentAssertions;

namespace ChessChaos.CommonTests;

[TestClass]
public class PointDirectionTests
{
	[TestMethod]
	[DynamicData(nameof(GetHorizontalDirectionPointTests), DynamicDataSourceType.Method)]
	[DynamicData(nameof(GetVerticalDirectionTests), DynamicDataSourceType.Method)]
	[DynamicData(nameof(GetDiagonalDirectionPointTests), DynamicDataSourceType.Method)]
	public void WhenDirecting_AndPointsAreGood_ThenDirectionShouldBeCorrect(
		Point firstPoint, Point secondPoint, Point direction)
	{
		// Act.
		var resultPoint = Point.GetDirection(firstPoint, secondPoint);

		// Assert.
		resultPoint.Should().Be(direction);
	}

	[TestMethod]
	[DynamicData(nameof(GetExсeptionPointTests), DynamicDataSourceType.Method)]
	public void WhenDirecting_AndPointsAreBad_ThenShouldThrowArgumentException(
		Point firstPoint, Point secondPoint)
	{
		// Act.
		Action act = () => Point.GetDirection(firstPoint, secondPoint);

		// Assert.
		act.Should().Throw<ArgumentException>();
	}

	private static IEnumerable<object?[]> GetHorizontalDirectionPointTests()
	{
		yield return new object?[] { new Point(8, -5), new Point(12, -5), new Point(1, 0) };
		yield return new object?[] { new Point(8, -5), new Point(-2, -5), new Point(-1, 0) };
	}

	private static IEnumerable<object?[]> GetVerticalDirectionTests()
	{
		yield return new object?[] { new Point(8, -5), new Point(8, -1), new Point(0, 1) };
		yield return new object?[] { new Point(8, -5), new Point(8, -9), new Point(0, -1) };
	}

	private static IEnumerable<object?[]> GetDiagonalDirectionPointTests()
	{
		yield return new object?[] { new Point(8, -5), new Point(12, -1), new Point(1, 1) };
		yield return new object?[] { new Point(8, -5), new Point(4, -9), new Point(-1, -1) };
		yield return new object?[] { new Point(8, -5), new Point(12, -9), new Point(1, -1) };
		yield return new object?[] { new Point(8, -5), new Point(4, -1), new Point(-1, 1) };
	}

	private static IEnumerable<object?[]> GetExсeptionPointTests()
	{
		yield return new object?[] { new Point(0, 4), new Point(0, 4) };
		yield return new object?[] { new Point(0, 0), new Point(0, 0) };

		yield return new object?[] { new Point(8, -5), new Point(8000, -8) };
		yield return new object?[] { new Point(23, -7), new Point(-450, -8) };
		yield return new object?[] { new Point(8, -5), new Point(9, -108) };
		yield return new object?[] { new Point(17, 1), new Point(18, 400) };

		yield return new object?[] { new Point(19, 21), new Point(231, 423) };
		yield return new object?[] { new Point(23, 11), new Point(322, 120) };
		yield return new object?[] { new Point(-29, -24), new Point(-231, 423) };
		yield return new object?[] { new Point(-45, -11), new Point(-322, -120) };
	}
}
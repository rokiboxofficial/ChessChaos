using ChessChaos.Common;
using FluentAssertions;

namespace ChessChaos.CommonTests;

[TestClass]
public class PointSubtractPointTests
{
	[TestMethod]
	[DynamicData(nameof(GetSubtractPoint), DynamicDataSourceType.Method)]
	public void WhenSubtracting_AndPontsCorrect_ThenSubtractPointsShouldBeTrue(
		Point from, Point to, Point resultPoint)
	{
		// Act.
		var result = from - to;

		// Assert
		result.Should().Be(resultPoint);
	}

	public static IEnumerable<object?[]> GetSubtractPoint()
	{
		yield return new object?[] { new Point(45, 24), new Point(45, 24), new Point(0, 0) };
		yield return new object?[] { new Point(7, -9), new Point(-5, 4), new Point(12, -13) };
		yield return new object?[] { new Point(0, 0), new Point(0, 0), new Point(0, 0) };
	}
}
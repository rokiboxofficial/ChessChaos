using ChessChaos.Common;
using FluentAssertions;

namespace ChessChaos.CommonTests;

[TestClass]
public class OffsetPointTests
{
	[TestMethod]
	public void WhenOffsetingPoint_AndOffsetEquals0_ThenOffsetedPointShouldBeEqualToInitialPoint()
	{
		// Arrange.
		var initialPoint = new Point(15, 12);

		var offset = new Point(0, 0);

		var offsetedPoint = initialPoint.Offset(offset);

		// Act.
		var isPointChanged = initialPoint == offsetedPoint;

		// Assert.
		isPointChanged.Should().BeTrue();
	}

	[TestMethod]
	public void WhenOffsetingPoint_AndOffsetPointIsNot0_ThenOffsetedPointShouldBeChanged()
	{
		// Arrange.
		var initialPoint = new Point(6, 2);

		var offset = new Point(2, 5);

		var offsetedPoint = initialPoint.Offset(offset);

		// Act.
		var isPointChanged = offsetedPoint.X == 8 && offsetedPoint.Y == 7;

		// Assert.
		isPointChanged.Should().BeTrue();
	}

	[TestMethod]
	public void WhenOffsetingPoint_AndOffsetXAndYAreNegative_ThenOffsetedPointShouldBeChanged()
	{
		// Arrange.
		var initialPoint = new Point(5, 5);

		var offset = new Point(-2, -3);

		var offsetedPoint = initialPoint.Offset(offset);

		// Act.
		var isPointChanged = offsetedPoint.X == 3 && offsetedPoint.Y == 2;

		// Assert.
		isPointChanged.Should().BeTrue();
	}

	[TestMethod]
	public void WhenOffsetingPoint_AndOffsetPointIsNot0_ThenInitialPointNotChanged()
	{
		// Arrange.
		var initialPoint = new Point(2, 3);

		var offset = new Point(2, 4);

		initialPoint.Offset(offset);

		// Act.
		var isPointChanged = initialPoint == new Point(2, 3);

		// Assert.
		isPointChanged.Should().BeTrue();
	}
}
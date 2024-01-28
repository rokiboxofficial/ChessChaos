using ChessChaos.Common;
using FluentAssertions;

namespace ChessChaos.CommonTests;

[TestClass]
public class DirectionHorizontalPointTests
{
	[TestMethod]
	public void WhenDirectingPoints_AndPointSourceX0YNegative2AndPointTargetX0Y0_ThenDirectionShouldBeHorizontalLeft()
	{
		// Arange.
		var source = new Point(0, -2);
		var target = new Point(0, 0);
		var resultPoint = new Point();

		// Act.
		resultPoint = resultPoint.GetDirection(source, target);

		// Assert.
		resultPoint.Should().Be(new Point(-1, 0));
	}

	[TestMethod]
	public void WhenDirectingPoints_AndPointSourceX9Y0AndPointTargetX0Y0_ThenDirectionShouldBeHorizontalRight()
	{
		// Arange.
		var source = new Point(9, 0);
		var target = new Point(0, 0);
		var resultPoint = new Point();

		// Act.
		resultPoint = resultPoint.GetDirection(source, target);

		// Assert.
		resultPoint.Should().Be(new Point(1, 0));
	}
}
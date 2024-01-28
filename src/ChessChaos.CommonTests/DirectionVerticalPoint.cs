using ChessChaos.Common;
using FluentAssertions;

namespace ChessChaos.CommonTests;

[TestClass]
internal class DirectionVerticalPoint
{
	[TestMethod]
	public void WhenDirectingPoints_AndPointSourceX0Y0AndPointTargetX4Y0_ThenDirectionShouldBeVerticalDown()
	{
		// Arange.
		var source = new Point(0, 0);
		var target = new Point(4, 0);
		var resultPoint = new Point();

		// Act.
		resultPoint = resultPoint.GetNormalizationVectors(source, target);

		// Assert.
		resultPoint.Should().Be(new Point(0, 1));
	}

	[TestMethod]
	public void WhenDirectingPoints_AndPointSourceX0YNegative2AndPointTargetX0Y0_ThenDirectionShouldBeVerticalUp()
	{
		// Arange.
		var source = new Point(0, 0);
		var target = new Point(-5, 0);
		var resultPoint = new Point();

		// Act.
		resultPoint = resultPoint.GetNormalizationVectors(source, target);

		// Assert.
		resultPoint.Should().Be(new Point(0, -1));
	}
}
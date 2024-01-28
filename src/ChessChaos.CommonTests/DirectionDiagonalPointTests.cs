using ChessChaos.Common;
using FluentAssertions;

namespace ChessChaos.CommonTests;

[TestClass]
public class DirectionDiagonalPointTests
{
	[TestMethod]
	public void WhenDirectingPoints_AndPointSourceXNegative7YNegative4AndPointTargetX8Y2_ThenDirectionShouldBeDiagonalRightDown()
	{
		// Arange.
		var source = new Point(-7, -4);
		var target = new Point(8, 2);
		var resultPoint = new Point();

		// Act.
		resultPoint = resultPoint.GetNormalizationVectors(source, target);

		// Assert.
		resultPoint.Should().Be(new Point(1, 1));
	}

	[TestMethod]
	public void WhenDirectingPoints_AndPointSourceX8Y6AndPointTargetX3Y4_ThenDirectionShouldBeDiagonalLeftUp()
	{
		// Arange.
		var source = new Point(8, 6);
		var target = new Point(3, 4);
		var resultPoint = new Point();

		// Act.
		resultPoint = resultPoint.GetNormalizationVectors(source, target);

		// Assert.
		resultPoint.Should().Be(new Point(-1, -1));
	}

	[TestMethod]
	public void WhenDirectingPoints_AndPointSourceX2Y6AndPointTargetX3Y4_ThenDirectionShouldBeDiagonalRightUp()
	{
		// Arange.
		var source = new Point(2, 6);
		var target = new Point(3, 4);
		var resultPoint = new Point();

		// Act.
		resultPoint = resultPoint.GetNormalizationVectors(source, target);

		// Assert.
		resultPoint.Should().Be(new Point(1, -1));
	}

	[TestMethod]
	public void WhenDirectingPoints_AndPointSourceX7Y1AndPointTargetX5Y2_ThenDirectionShouldBeDiagonalLeftDown()
	{
		// Arange.
		var source = new Point(7, 1);
		var target = new Point(5, 2);
		var resultPoint = new Point();

		// Act.
		resultPoint = resultPoint.GetNormalizationVectors(source, target);

		// Assert.
		resultPoint.Should().Be(new Point(-1, 1));
	}
}
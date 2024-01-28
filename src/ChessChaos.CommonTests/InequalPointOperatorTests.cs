using ChessChaos.Common;
using FluentAssertions;

namespace ChessChaos.CommonTests;

[TestClass]
public class InequalPointOperatorTests
{
	[TestMethod]
	public void WhenInequallingByNotEqualsOperator_AndPointsAreSame_ThenPointsShouldBeEqual()
	{
		// Arrange.
		var (firstPoint, secondPoint) = GetSamePoints();

		// Act.
		var isPointsAreNotEqual = firstPoint != secondPoint;

		// Assert.
		isPointsAreNotEqual.Should().BeFalse();
	}

	[TestMethod]
	public void WhenInequallingByNotEqualsOperator_AndPointsAreDifferent_ThenPointsShouldBeNotEqual()
	{
		// Arrange.
		var (firstPoint, secondPoint) = GetDifferentPoints();

		// Act.
		var isPointsAreNotEqual = firstPoint != secondPoint;

		// Assert.
		isPointsAreNotEqual.Should().BeTrue();
	}

	private static (Point firstPoint, Point secondPoint) GetSamePoints()
	{
		const int x = 45;
		const int y = 24;

		var firstPoint = new Point(x, y);
		var secondPoint = new Point(x, y);

		return (firstPoint, secondPoint);
	}

	private static (Point firstPoint, Point secondPoint) GetDifferentPoints()
	{
		var firstPoint = new Point(1, 3);
		var secondPoint = new Point(2, 7);

		return (firstPoint, secondPoint);
	}
}
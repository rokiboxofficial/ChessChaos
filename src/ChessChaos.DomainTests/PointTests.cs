using ChessChaos.Domain;
using FluentAssertions;

namespace ChessChaos.DomainTests;

[TestClass]
public class PointTests
{
	[TestMethod]
	public void WhenCallingDefaultConstructor_ThenXAndYShouldBe0()
	{
		// Act.
		var point = new Point();

		// Assert.
		point.X.Should().Be(0);
		point.Y.Should().Be(0);
	}

	[TestMethod]
	public void WhenCallingConstructorWithNonZeroNumbers_ThenXAndYShouldBeCorrect()
	{
		// Act.
		var point = new Point(4, 6);

		// Assert.
		point.X.Should().Be(4);
		point.Y.Should().Be(6);
	}

	[TestMethod]
	public void WhenGettingHashCodes_AndPointsAreSame_ThenHashCodesShouldBeSame()
	{
		// Arrange.
		var (firstPoint, secondPoint) = GetSamePoints();

		// Act.
		var firstPointHashCode = firstPoint.GetHashCode();
		var secondPointHashCode = secondPoint.GetHashCode();

		// Assert.
		firstPointHashCode.Should().Be(secondPointHashCode);
	}

	[TestMethod]
	public void WhenEquallingByEqualsMethod_AndPointsAreSame_ThenPointsShouldBeEqual()
	{
		// Arrange.
		var (firstPoint, secondPoint) = GetSamePoints();

		// Act.
		var isPointsAreEqual = firstPoint.Equals(secondPoint);

		// Assert.
		isPointsAreEqual.Should().BeTrue();
	}

	[TestMethod]
	public void WhenEquallingByEqualsMethod_AndPointsAreDifferent_ThenPointsShouldBeNotEqual()
	{
		// Arrange.
		var (firstPoint, secondPoint) = GetDifferentPoints();

		// Act.
		var isPointsAreEqual = firstPoint.Equals(secondPoint);

		// Assert.
		isPointsAreEqual.Should().BeFalse();
	}

	[TestMethod]
	public void WhenEquallingByEqualsMethod_AndSecondPointIsNull_ThenPointsShouldBeNotEqual()
	{
		// Arrange.
		var firstPoint = new Point(1, 2);

		// Act.
		var isPointsAreEqual = firstPoint.Equals(null);

		// Assert.
		isPointsAreEqual.Should().BeFalse();
	}

	[TestMethod]
	public void WhenEquallingByEqualsMethod_AndSecondPointIsInteger_ThenPointsShouldBeNotEqual()
	{
		// Arrange.
		var firstPoint = new Point(1, 2);

		// Act.
		var isPointsAreEqual = firstPoint.Equals(9);

		// Assert.
		isPointsAreEqual.Should().BeFalse();
	}

	[TestMethod]
	public void WhenEquallingByEqualsOperator_AndPointsAreSame_ThenPointsShouldBeEqual()
	{
		// Arrange.
		var (firstPoint, secondPoint) = GetSamePoints();

		// Act.
		var isPointsAreEqual = firstPoint == secondPoint;

		// Assert.
		isPointsAreEqual.Should().BeTrue();
	}

	[TestMethod]
	public void WhenEquallingByEqualsOperator_AndPointsAreDifferent_ThenPointsShouldBeNotEqual()
	{
		// Arrange.
		var (firstPoint, secondPoint) = GetDifferentPoints();

		// Act.
		var isPointsAreEqual = firstPoint == secondPoint;

		// Assert.
		isPointsAreEqual.Should().BeFalse();
	}

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

	[TestMethod]
	public void WhenOffsetTwoPoints_AndOffsetCountPointsCorrect_ThenPointsPositionWillBeChanged()
	{
		// Arrange.
		const int x = 2;
		const int y = 3;
		const int offsetX = 6;
		const int offsetY = 9;

		var point = new Point(x, y);

		var offset = point.Offset(offsetX, offsetY);

		// Act.
		var isPointsWillBeOffset = offset != point;

		// Assert.
		isPointsWillBeOffset.Should().BeTrue();
	}

	[TestMethod]
	public void WhenOffsetTwoPoints_AndOffsetPointsAre0_ThenPositionsDontChanged()
	{
		// Arrange.
		const int x = 15;
		const int y = 12;
		const int offsetX = 0;
		const int offsetY = 0;

		var point = new Point(x, y);

		var offset = point.Offset(offsetX, offsetY);

		// Act.
		var isPointsWillBeOffset = point.X == offset.X && point.Y == point.Y;

		// Assert.
		isPointsWillBeOffset.Should().BeTrue();
	}

	[TestMethod]
	public void WhenOffsetTwoPoints_AndOffsetArePointNotZero_ThenPositionWillBeChanged()
	{
		// Arrange.
		const int x = 6;
		const int y = 2;
		const int offsetX = 2;
		const int offsetY = 5;

		var point = new Point(x, y);

		var offset = point.Offset(offsetX, offsetY);

		// Act.
		var isPointsWillBeOffset = point.X == offset.X && point.Y == point.Y;

		// Assert.
		isPointsWillBeOffset.Should().BeFalse();
	}

	[TestMethod]
	public void WhenOffsetPoint_AndPointCordsNotZero_ThenPositionNotChanged()
	{
		// Arrange.
		const int x = 5;
		const int y = 7;
		const int offsetX = 0;
		const int offsetY = 0;

		var point = new Point(x, y);

		var offset = point.Offset(new Point(offsetX,offsetY));

		// Act.
		var isPointsWillBeOffset = point.X == offset.X && point.Y == point.Y;

		// Assert.
		isPointsWillBeOffset.Should().BeTrue();
	}

	[TestMethod]
	public void WhenOffsetPoint_AndPointCordsNotZero_ThenPositionWillBeChanged()
	{
		// Arrange.
		const int x = 5;
		const int y = 7;
		const int offsetX = 2;
		const int offsetY = 4;

		var point = new Point(x, y);

		var offset = point.Offset(new Point(offsetX, offsetY));

		// Act.
		var isPointsWillBeOffset = point.X == offset.X && point.Y == point.Y;

		// Assert.
		isPointsWillBeOffset.Should().BeFalse();
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
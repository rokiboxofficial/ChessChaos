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
	public void WhenOffsetingTwoPoints_AndOffsetPointCorrect_ThenPointsPointShouldBeChanged()
	{
		// Arrange.
		var point = new Point(2, 3);

		var offset = point.Offset(6, 9);

		// Act.
		var isPointChanged = offset != point && (offset.X == 8 && offset.Y == 12);

		// Assert.
		isPointChanged.Should().BeTrue();
	}

	[TestMethod]
	public void WhenOffsetingTwoPoints_AndOffsetEquals0_ThenPointShouldBeNotChanged()
	{
		// Arrange.
		var point = new Point(15, 12);

		var offset = point.Offset(0, 0);

		// Act.
		var isPointChanged = point.X.Equals(offset.X) && point.Y.Equals(offset.Y);

		// Assert.
		isPointChanged.Should().BeTrue();
	}

	[TestMethod]
	public void WhenOffsetingTwoPoints_AndOffsetPointsIsNotZero_ThenPointShouldBeChanged()
	{
		// Arrange.
		var point = new Point(6, 2);

		var offset = point.Offset(2, 5);

		// Act.
		var isPointChanged = point.X.Equals(offset.X) && point.Y.Equals(offset.Y);

		// Assert.
		isPointChanged.Should().BeFalse();
	}

	[TestMethod]
	public void WhenOffsetingPoint_AndPointCordsIsNotZero_ThenPointShouldBeNotChanged()
	{
		// Arrange.
		var point = new Point(5, 7);

		var offset = point.Offset(0, 0);

		var offsetedPoint = point.Offset(offset);

		// Act.
		var isPointChanged = point.X.Equals(offsetedPoint.X) && point.Y.Equals(offsetedPoint.Y);

		// Assert.
		isPointChanged.Should().BeFalse();
	}

	[TestMethod]
	public void WhenOffsetingPoint_AndPointsXAndYNotZero_ThenPointShouldBeChanged()
	{
		// Arrange.
		var point = new Point(5, 7);

		var offset = new Point(2, 4);

		var offsetPoint = point.Offset(offset);

		// Act.
		var isPointsWillBeOffset = point.X.Equals(offsetPoint.X) && point.Y.Equals(offsetPoint.Y);

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
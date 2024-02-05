using ChessChaos.Common;
using FluentAssertions;

namespace ChessChaos.CommonTests;

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

	[TestMethod]
	public void WhenSubtracting_AndPontsAreTheSame_ThenPointXShouldBe0AndPointYShouldBe0()
	{
		// Arrange.
		var (firstPoint, secondPoint) = GetSamePoints();

		// Act.
		var result = firstPoint - secondPoint;

		// Assert.s
		result.Should().Be(new Point(0, 0));
	}

	[TestMethod]
	public void WhenSubtracting_AndFirstPointXIsPositiveAndYIsNegativeAndSecondPointXNegativeAndYPositive_ThenPointShouldBeCorrect()
	{
		// Arrange.
		var firstPoint = new Point(7, -9);
		var secondPoint = new Point(-5, 4);

		// Act.
		var result = firstPoint - secondPoint;

		// Assert.
		result.Should().Be(new Point(12, -13));
	}

	[TestMethod]
	public void WhenSubtracting_AndPontsAre0_ThenPointXShouldBe0AndPointYShouldBe0()
	{
		// Arrange.
		var firstPoint = new Point(0, 0);
		var secondPoint = new Point(0, 0);

		// Act.
		var result = firstPoint - secondPoint;

		// Assert.
		result.Should().Be(new Point(0, 0));
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
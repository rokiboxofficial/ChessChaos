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
		var secondPoint = new Point(-5, 2);

		// Act.
		var result = firstPoint - secondPoint;

		// Assert.
		result.Should().Be(new Point(12, -11));
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

	[TestMethod]
	public void WhenDirectingPoint_AndPointsSourceAndTargetAre0_ThenPointDirectionShouldBeNotChanged()
	{
		// Arange.
		var source = new Point(0, 0);
		var target = new Point(0, 0);
		var resultPoint = new Point();

		// Act.
		resultPoint = resultPoint.GetDirection(source, target);

		// Assert.
		resultPoint.Should().Be(new Point(0, 0));
	}

	[TestMethod]
	public void WhenDirectingPoints_AndSourcePointBiggerThanTargetPoint_ThenDirectionShouldBePositive()
	{
		// Arange.
		var source = new Point(3, 9);
		var target = new Point(2, 7);
		var resultPoint = new Point();

		// Act.
		resultPoint = resultPoint.GetDirection(source, target);

		// Assert.
		resultPoint.Should().Be(new Point(1, 1));
	}

	[TestMethod]
	public void WhenDirectingPoints_AndTargetPointBiggerThanSourcePoint_ThenDirectionCoordinatesShouldBeNegativeX1AndNegativeY1()
	{
		// Arange.
		var source = new Point(5, 7);
		var target = new Point(8, 12);
		var resultPoint = new Point();

		// Act.
		resultPoint = resultPoint.GetDirection(source, target);

		// Assert.
		resultPoint.Should().Be(new Point(-1, -1));
	}

	[TestMethod]
	public void WhenDirectingPoints_AndSourcePointEqualsTargetPoint_ThenDirectionShouldBe0()
	{
		// Arange.
		var source = new Point(3, 9);
		var target = new Point(3, 9);
		var resultPoint = new Point();

		// Act.
		resultPoint = resultPoint.GetDirection(source, target);

		// Assert.
		resultPoint.Should().Be(new Point(0, 0));
	}

	[TestMethod]
	public void WhenDirectingPoints_AndSourcePointY5AndTargetPointX4_ThenDirectionCoordinatesEqualsNegative1AndPositive1()
	{
		// Arange.
		var source = new Point(0, 5);
		var target = new Point(4, 0);
		var resultPoint = new Point();

		// Act.
		resultPoint = resultPoint.GetDirection(source, target);

		// Assert.
		resultPoint.Should().Be(new Point(-1, 1));
	}

	[TestMethod]
	public void WhenDirectingPoints_AndSourcePointYNegative4AndTargetPointXNegative5_ThenDirectionCoordinatesXPositive1AndNegative1()
	{
		// Arange.
		var source = new Point(3, -4);
		var target = new Point(-5, 0);
		var resultPoint = new Point();

		// Act.
		resultPoint = resultPoint.GetDirection(source, target);

		// Assert.
		resultPoint.Should().Be(new Point(1, -1));
	}

	[TestMethod]
	public void WhenDirectingPoints_AndSourcePointYPositive4AndTargetPointXNegative5_ThenDirectionCoordinatesXPositive1AndYPositive1()
	{
		// Arange.
		var source = new Point(0, 4);
		var target = new Point(-5, 0);
		var resultPoint = new Point();

		// Act.
		resultPoint = resultPoint.GetDirection(source, target);

		// Assert.
		resultPoint.Should().Be(new Point(1, 1));
	}

	[TestMethod]
	public void WhenDirectingPoints_AndSourcePointXPositive8AndTargetPointYPositive6_ThenDirectionCoordinatesXPositive1AndYNegative1()
	{
		// Arange.
		var source = new Point(8, 0);
		var target = new Point(0, 6);
		var resultPoint = new Point();

		// Act.
		resultPoint = resultPoint.GetDirection(source, target);

		// Assert.
		resultPoint.Should().Be(new Point(1, -1));
	}

	[TestMethod]
	public void WhenDirectingPoints_AndSourcePointXNegative8AndTargetPointYNegative6_ThenDirectionCoordinatesXNegative1AndYPositive1()
	{
		// Arange.
		var source = new Point(-8, 0);
		var target = new Point(0, -6);
		var resultPoint = new Point();

		// Act.
		resultPoint = resultPoint.GetDirection(source, target);

		// Assert.
		resultPoint.Should().Be(new Point(-1, 1));
	}

	[TestMethod]
	public void WhenDirectingPoints_AndSourcePointXPositive8AndTargetPointYNegative6_ThenDirectionCoordinatesXPositive1AndYPointPositive1()
	{
		// Arange.
		var source = new Point(8, 0);
		var target = new Point(0, 6);
		var resultPoint = new Point();

		// Act.
		resultPoint = resultPoint.GetDirection(source, target);

		// Assert.
		resultPoint.Should().Be(new Point(1, -1));
	}

	[TestMethod]
	public void WhenDirectingPoints_AndSourcePointXNegative6AndTargetPointYPositive8_ThenDirectionCoordinatesXNegative1AndYPointNegative1()
	{
		// Arange.
		var source = new Point(-6, 0);
		var target = new Point(0, 8);
		var resultPoint = new Point();

		// Act.
		resultPoint = resultPoint.GetDirection(source, target);

		// Assert.
		resultPoint.Should().Be(new Point(-1, -1));
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

using ChessChaos.Common;
using FluentAssertions;

namespace ChessChaos.CommonTests;

[TestClass]
public class DirectionExceptionTests
{
	[TestMethod]
	public void WhenDirectingPoints_AndPointsSourceAndTargetAre0_ThenThrowArgumentException()
	{
		// Arange.
		var source = new Point(0, 0);
		var target = new Point(0, 0);
		var resultPoint = new Point(0, 0);

		// Act.
		Action act = () => resultPoint.GetNormalizationVectors(source, target);

		// Assert.
		act.Should().Throw<ArgumentException>();
	}

	[TestMethod]
	public void WhenDirectingPoints_AndPointsSourceAndTargetAreTheSame_ThenThrowArgumentException()
	{
		// Arange.
		var source = new Point(4, 4);
		var target = new Point(4, 4);
		var resultPoint = new Point();

		// Act.
		Action act = () => resultPoint.GetNormalizationVectors(source, target);

		// Assert.
		act.Should().Throw<ArgumentException>();
	}

	[TestMethod]
	public void WhenDirectingPoints_AndPointsTargetXEqualsSourceX_ThenThrowException()
	{
		// Arange.
		var source = new Point(4, 0);
		var target = new Point(4, 0);
		var resultPoint = new Point();

		// Act.
		Action act = () => resultPoint.GetNormalizationVectors(source, target);

		// Assert.
		act.Should().Throw<DivideByZeroException>();
	}

	[TestMethod]
	public void WhenDirectingPoints_AndPointsTargetYEqualsSourceY_ThenThrowException()
	{
		// Arange.
		var source = new Point(0, 7);
		var target = new Point(0, 7);
		var resultPoint = new Point();

		// Act.
		Action act = () => resultPoint.GetNormalizationVectors(source, target);

		// Assert.
		act.Should().Throw<DivideByZeroException>();
	}

	[TestMethod]
	public void WhenDirectingPoints_AndPointsTargetXEqualsSourceXAndTargetYNotEqualsSourceY_ThenThrowException()
	{
		// Arange.
		var source = new Point(8, 7);
		var target = new Point(8, 9);
		var resultPoint = new Point();

		// Act.
		Action act = () => resultPoint.GetNormalizationVectors(source, target);

		// Assert.
		act.Should().Throw<DivideByZeroException>();
	}

	[TestMethod]
	public void WhenDirectingPoints_AndPointsTargetYEqualsSourceXAndTargetXNotEqualsSourceX_ThenThrowException()
	{
		// Arange.
		var source = new Point(8, 3);
		var target = new Point(4, 3);
		var resultPoint = new Point();

		// Act.
		Action act = () => resultPoint.GetNormalizationVectors(source, target);

		// Assert.
		act.Should().Throw<DivideByZeroException>();
	}
}
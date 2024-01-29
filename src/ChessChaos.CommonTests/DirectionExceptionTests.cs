
using ChessChaos.Common;
using FluentAssertions;

namespace ChessChaos.CommonTests;

[TestClass]
public class DirectionExceptionTests
{
	[TestMethod]
	[DataRow(4, 0, 4, 0)]
	[DataRow(7, 0, 7, 0)]
	[DataRow(8, 7, 8, 9)]
	[DataRow(8, 3, 4, 3)]
	public void WhenDirectingPoints_AndPointTargetXEqualsSourceXOrPointTargetYEqualsSourceY_ThenThrowException(
		int firstPointX, int firstPointY, int secondPointX, int secondPointY)
	{
		// Arange.
		var source = new Point(firstPointX, firstPointY);
		var target = new Point(secondPointX, secondPointY);
		var resultPoint = new Point();

		// Act.
		Action act = () => resultPoint.GetDirection(source, target);

		// Assert.
		act.Should().Throw<DivideByZeroException>();
	}

	[TestMethod]
	public void WhenDirectingPoints_AndPointsSourceAndTargetAre0_ThenThrowArgumentException()
	{
		// Arange.
		var source = new Point(0, 0);
		var target = new Point(0, 0);
		var resultPoint = new Point(0, 0);

		// Act.
		Action act = () => resultPoint.GetDirection(source, target);

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
		Action act = () => resultPoint.GetDirection(source, target);

		// Assert.
		act.Should().Throw<ArgumentException>();
	}
}
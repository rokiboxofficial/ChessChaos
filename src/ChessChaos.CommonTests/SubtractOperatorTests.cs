
using ChessChaos.Common;
using FluentAssertions;

namespace ChessChaos.CommonTests;

[TestClass]
internal class SubtractOperatorTests
{
	[TestMethod]
	public void WhenSubtracting_AndPontsAreTheSame_ThenPointXShouldBe0AndPointYShouldBe0()
	{
		// Arrange.
		var firstPoint = new Point(8, 3);
		var secondPoint = new Point(8, 3);

		// Act.
		var result = firstPoint - secondPoint;

		// Assert.
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
}

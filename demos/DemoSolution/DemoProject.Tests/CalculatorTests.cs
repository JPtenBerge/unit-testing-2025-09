using AwesomeAssertions;

namespace DemoProject.Tests;

[TestClass]
public class CalculatorTests
{
	Calculator _sut = null!; // system under test
	
	[TestInitialize]
	public void Init() => _sut = new();

	[DataTestMethod]
	[DataRow(4, 8, 12)]
	[DataRow(-4, 8, 4)]
	[DataRow(-4, -8, -12)]
	public void Add_Numbers_Works(int n1, int n2, int expected)
	{
		// Act
		_sut.Add(n1);
		_sut.Add(n2);
		
		// Assert
		_sut.Result.Should().Be(expected);
	}
	
	[TestMethod]
	public void Add_PositiveIntegers_Positive12()
	{
		// Act
		_sut.Add(4);
		_sut.Add(8);

		// Assert
		// Assert.AreEqual(sut.Result, 12);
		_sut.Result.Should().Be(12);
	}

	[TestMethod]
	public void Add_NegativeIntegers_Negative12()
	{
		// Act
		_sut.Add(-4);
		_sut.Add(-8);

		// Assert
		_sut.Result.Should().Be(-12);
	}

	[TestMethod]
	public void AwesomeAssertionShizzle()
	{
		var obj = new { Name = "JP" };
		var expected = new { Name = "JP", Age = 12 };
		obj.Should().BeEquivalentTo(expected, options => options.Excluding(x => x.Age));
	}
}
namespace TestProject1;

public class UnitTest1
{
	[Fact]
	public void Test1()
	{
		Assert.Equal("hoi", "doei");
	}

	[Theory]
	[InlineData(4, 8, 12)]
	public void DataDriven(int n1, int n2, int expected)
	{
		
	}
}
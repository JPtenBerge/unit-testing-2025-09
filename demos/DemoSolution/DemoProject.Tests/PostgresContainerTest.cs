using AwesomeAssertions;
using Testcontainers.PostgreSql;

namespace DemoProject.Tests;

[TestClass]
public class PostgresContainerTest
{
	[TestMethod]
	public async Task Doe()
	{
		var container = new PostgreSqlBuilder().WithImage("postgres").WithPortBinding(8118,8118).Build();
		// container.GetConnectionString()
		
		await container.StartAsync();

		var result = await container.ExecScriptAsync("SELECT 481;");
		result.Stdout.Should().Be("whaaat");
		result.ExitCode.Should().Be(0);
	}
}
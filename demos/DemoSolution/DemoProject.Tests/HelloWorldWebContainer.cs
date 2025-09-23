using AwesomeAssertions;
using DotNet.Testcontainers.Builders;

namespace DemoProject.Tests;

[TestClass]
public class HelloWorldWebContainer
{
	[TestMethod]
	public async Task HelloWorldTest()
	{
		var container = new ContainerBuilder()
			.WithImage("testcontainers/helloworld")
			.WithPortBinding(8080, 8080)
			// .WithWaitStrategy(Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(() => ))
			.Build();

		await container.StartAsync();

		var http = new HttpClient();
		var uri = new UriBuilder("http", container.Hostname, container.GetMappedPublicPort(), "uuid");

		var response = await http.GetStringAsync(uri.ToString());
		response.Should().Be("soemething");


	}
}
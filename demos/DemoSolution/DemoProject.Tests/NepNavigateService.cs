namespace DemoProject.Tests;

public class NepNavigateService : INavigateService
{
	public bool HasNextBeenCalled { get; set; }
	
	public int Next<T>(IEnumerable<T>? data, int? activeIndex)
	{
		HasNextBeenCalled = true;
		return 42;
	}
}
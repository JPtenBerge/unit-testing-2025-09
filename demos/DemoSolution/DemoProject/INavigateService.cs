namespace DemoProject;

public interface INavigateService
{
	int Next<T>(IEnumerable<T>? data, int? activeIndex);
}
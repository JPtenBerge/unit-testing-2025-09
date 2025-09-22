namespace DemoProject;

public class Autocompleter<T> where T : class
{
	public required string Query { get; set; }
	public required List<T> Data { get; set; }
	public List<T>? Suggestions { get; set; }
	public int? ActiveSuggestionIndex { get; set; }

	// virtual
	private INavigateService _navigateService;

	// dependency injection: pattern om dat mooie high cohesion, low coupling. ook voor mocken eeeeerg prettig. 
	
	public Autocompleter(INavigateService navigateService)
	{
		_navigateService = navigateService;
	}
	
	public void Autocomplete()
	{
		Suggestions = [];
		
		foreach (var item in Data)
		{
			// reflection is noodzakelijk kwaad
			var props = item.GetType().GetProperties().Where(x => x.PropertyType == typeof(string));
			foreach (var prop in props)
			{
				var value = (prop.GetValue(item) as string)!;
				if (value.Contains(Query, StringComparison.OrdinalIgnoreCase))
				{
					Suggestions.Add(item);
					break;
				}
			}
		}
	}

	public void Next()
	{
		ActiveSuggestionIndex = _navigateService.Next(Suggestions, ActiveSuggestionIndex);
	}
}
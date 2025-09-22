namespace DemoProject;

public class Autocompleter<T> where T : class
{
	public required string Query { get; set; }
	public required List<T> Data { get; set; }
	public List<T>? Suggestions { get; set; }

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
}
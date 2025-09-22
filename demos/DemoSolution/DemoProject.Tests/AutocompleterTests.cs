using AwesomeAssertions;

namespace DemoProject.Tests;

class Car
{
	public string Make { get; set; }
	public string Model { get; set; }
}

[TestClass]
public class AutocompleterTests
{
	private List<Car> _data = null!;

	[TestInitialize]
	public void Init()
	{
		_data =
		[
			new() { Make = "Opel", Model = "Astra" },
			new() { Make = "Volkswagen", Model = "Polo" },
			new() { Make = "Peugeot", Model = "E208" },
			new() { Make = "Opel", Model = "Corsa" },
			new() { Make = "Volkswagen", Model = "Scirocco" },
			new() { Make = "Seat", Model = "Busje" },
			new() { Make = "Volkswagen", Model = "Kever" },
			new() { Make = "Fiat", Model = "Multiplo" },
			new() { Make = "Mazda", Model = "MX-5" },
			new() { Make = "Tesla", Model = "Model 3" },
		];
	}

	[TestMethod]
	public void Autocomplete_HappyFlow_GivesSuggestions()
	{
		var sut = new Autocompleter<Car>
		{
			Query = "olk",
			Data = _data
		};

		sut.Autocomplete();

		List<Car> expected =
		[
			new() { Make = "Volkswagen", Model = "Polo" },
			new() { Make = "Volkswagen", Model = "Scirocco" },
			new() { Make = "Volkswagen", Model = "Kever" }
		];
		sut.Suggestions.Should().BeEquivalentTo(expected);
	}

	[TestMethod]
	public void Autocomplete_QueryWithNoMatches_EmptySuggestions()
	{
		var sut = new Autocompleter<Car>
		{
			Query = "wertyuiopuytrrftgh",
			Data = _data
		};

		sut.Autocomplete();

		sut.Suggestions.Should().BeEmpty();
	}

	[TestMethod]
	public void Autocomplete_EmptyQuery_EmptySuggestions()
	{
		var sut = new Autocompleter<Car>
		{
			Query = "wertyuiopuytrrftgh",
			Data = _data
		};

		sut.Autocomplete();

		sut.Suggestions.Should().BeEmpty();
	}
	
	[TestMethod]
	public void Autocomplete_QueryMatchesMultipleProperties_AddsSuggestionsUniquely()
	{
		var sut = new Autocompleter<Car>
		{
			Query = "o",
			Data = _data
		};

		sut.Autocomplete();

		sut.Suggestions.Should().OnlyHaveUniqueItems();
	}
	
	[TestMethod]
	public void Autocomplete_CapitalizedQuery_FiltersCaseInsensitively()
	{
		var sut = ArrangeSut("A");
	
		sut.Autocomplete();

		List<Car> expected =
		[
			new() { Make = "Opel", Model = "Astra" },
			new() { Make = "Volkswagen", Model = "Polo" },
			new() { Make = "Opel", Model = "Corsa" },
			new() { Make = "Volkswagen", Model = "Scirocco" },
			new() { Make = "Seat", Model = "Busje" },
			new() { Make = "Volkswagen", Model = "Kever" },
			new() { Make = "Fiat", Model = "Multiplo" },
			new() { Make = "Mazda", Model = "MX-5" },
			new() { Make = "Tesla", Model = "Model 3" },
		];
		sut.Suggestions.Should().BeEquivalentTo(expected);
	}

	private Autocompleter<Car> ArrangeSut(string query)
	{
		return new Autocompleter<Car>
		{
			Query = query,
			Data = _data
		};
	}
}
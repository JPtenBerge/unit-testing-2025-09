using AwesomeAssertions;
using Moq;

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
	private Mock<INavigateService> _navigateServiceMock = null!;
	
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

	// [TestCleanup]
	// public void CleanUp()
	// {
	// 	_navigateServiceMock.VerifyAll();
	// }

	[TestMethod]
	public void Autocomplete_HappyFlow_GivesSuggestions()
	{
		var sut = ArrangeSut("olk");

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
		var sut = ArrangeSut("wertyuiopuytrrftgh");

		sut.Autocomplete();

		sut.Suggestions.Should().BeEmpty();
	}

	[TestMethod]
	public void Autocomplete_EmptyQuery_EmptySuggestions()
	{
		var sut = ArrangeSut(string.Empty);

		sut.Autocomplete();

		// sut.Suggestions.Should().BeEmpty();
	}
	
	[TestMethod]
	public void Autocomplete_QueryMatchesMultipleProperties_AddsSuggestionsUniquely()
	{
		var sut = ArrangeSut("o");

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

	[TestMethod]
	public void Next_BasicSuggestions_NavigateServiceUsedAndReturnValueStored()
	{
		var sut = ArrangeSut("e");
		sut.Autocomplete();
		
		sut.Next();

		sut.ActiveSuggestionIndex.Should().Be(42);
		// _navigateServiceMock.HasNextBeenCalled.Should().BeTrue();
		// _navigateServiceMock.Verify(x => x.Next(It.IsAny<IEnumerable<Car>>(), It.IsAny<int?>()));
	}

	[TestMethod]
	// [ExpectedException(typeof(Exception))]
	public void Next_NavigateServiceThrows_Rethrows()
	{
		var sut = ArrangeSut("e");
		_navigateServiceMock.Setup(x => x.Next(It.IsAny<IEnumerable<Car>>(), It.IsAny<int?>())).Throws<NotSupportedException>();

		// sut.Next();

		var act = () => sut.Next();
		act.Should().Throw<Exception>();
		
		
		// Assert.ThrowsException<Exception>(() => sut.Next());
		// Assert.ThrowsException<NotSupportedException>(sut.Next);

		// try
		// {
		// 	sut.Next();
		// 	// Assert.Fail();
		// }
		// catch(NotSupportedException)
		// {
		// }
	}
	

	private Autocompleter<Car> ArrangeSut(string query)
	{
		// _navigateServiceMock = new NepNavigateService();

		// var bla = Mock.Of<INavigateService>(); // voornamelijk properties
		// bla.Prop1 = "iets";
		
		// _navigateServiceMock = new(MockBehavior.Strict);
		// _navigateServiceMock.SetupProperty(x => x.Prop1).Returns
		
		_navigateServiceMock = new();
		_navigateServiceMock.Setup(x => x.Next(It.IsAny<IEnumerable<Car>>(), It.IsAny<int?>())).Returns(42);
		// _navigateServiceMock.Setup(x => x.Next(It.IsAny<IEnumerable<Car>>(), It.IsAny<int>())).Throws<Exception>()
		
		return new Autocompleter<Car>(_navigateServiceMock.Object)
		{
			Query = query,
			Data = _data
		};
	}
}
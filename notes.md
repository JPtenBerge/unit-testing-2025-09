# Notes

## Conventies

[Hele lijst van Microsoft](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices).

Testmethodenaam:

- `Method_Input_Output()`
- `Method_State_ExpectedBehavior()`
- `Add_4_And_8_Should_Return_12()`

## Testframeworks

- xUnit <== deze had JP's voorkeur
  - `[Fact]` `[Theory]` met  `[InlineData(2, 4)]` `[InlineData(-2, -4)]` `[InlineData(2, -4)]`
- MSTest  <== Microsoft
  - `[TestMethod]`  `[TestClass]`  `[DataRow()]`
  - [data-driven tests waren vroeger een draak](https://learn.microsoft.com/en-us/visualstudio/test/how-to-create-a-data-driven-unit-test?view=vs-2022)
    ```cs
    [DataSource(@"Provider=Microsoft.SqlServerCe.Client.4.0;Data Source=C:\Data\MathsData.sdf", "AddIntegersData")]
    [TestMethod]
    public void TestStuff() { }
    ```
- NUnit
  - `[Test]`

verschillen zijn vooral merkbaar als je nog de Assert-class handmatig aanspreekt. Maaaaar... als je een assertion library inzet, dan maakt het NOG MINDER uit.



## Assertions libraries

- FluentAssertions
  - Sinds v8 geen FOSS meer, [licentie vereist](https://xceed.com/products/unit-testing/fluent-assertions/)
- [AwesomeAssertions](https://awesomeassertions.org/)
  - Fork van FluentAssertions v7 🤘
  - Heeft dit wat Shouldly niet heeft:
    ```cs
    dto.Should().BeEquivalentTo(expected, opts => opts.Excluding(x => x.Stuff));
    ```
- [Shouldly](https://docs.shouldly.org/)
  - Gesponsord door Info Support


## Test-driven development

Een werkwijze!

1. Schrijf een test
   - bug
   - nieuwe feature - heul wat tests (elke story/edge case)
2. Draai de test en zie dat hij faalt (rood)
3. Schrijf code   / bare minimum
4. Draai de test/alle tests en zie dat hij slaagt (groen)
5. Refactor

Repeat.

waarom?

- deadlines.
  - technical debt.
- forceert nadenken.
- werkt beter design in de hand

wanneer niet?

- geen idee hoe je project eruit gaat zien / architectuur
  - zoek het uit en pak dan TDD op

PoC proof of concept

>"niets is zo permanent als een tijdelijke oplossing"

## Mocking

Nabootsen van een dependency om gedrag consistent te hebben

Test double:
- Stubs: geen implementatie. interacties registreren
- Fakes: altijd nepdata terug
- Mocks: gedrag nabootsen
- Dummies: als je 6 parameters mee moet geven, maar je bent enkel geinteresseerd in wat er met 
  de eerste 3 gebeurt, dan moet je 3 nutteloze parameters mee gooien

IRL hebben mensen het altijd over mocking/mocks. Er zijn genoeg mockframeworks en geen 1 "test double framework".

## Coole links

- Svelte die integratietesten "unittesten" noemt: https://www.sveltesociety.dev/recipes/testing-and-debugging/unit-testing-svelte-component
  - React en Vue doen op dezelfde manier ook HTML renderen als unittesten wegzetten
- [Bowling Kata](https://codingdojo.org/kata/Bowling/) voor TDD


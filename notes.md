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

## Coole links

- Svelte die integratietesten "unittesten" noemt: https://www.sveltesociety.dev/recipes/testing-and-debugging/unit-testing-svelte-component
  - React en Vue doen op dezelfde manier ook HTML renderen als unittesten wegzetten



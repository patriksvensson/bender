using Bender.Tests.Data;
using Bender.Tests.Fakes;
using Shouldly;

namespace Bender.Tests;

public class AspireModelTests
{
    [Fact]
    public void Should_Match_On_Existing_Property()
    {
        // Given
        var model = AspireModel.Parse(Manifests.Simple);

        var ctx = model.CreateContext();
        ctx.InputGenerator = new FakeInputGenerator();
        ctx.AddInput("sqlserver.bindings.tcp.host", "example.com");
        ctx.AddInput("sqlserver.bindings.tcp.port", 8080);

        // When
        var result = ctx.Match("TestDatabase.connectionString");

        // Then
        result
            .ShouldBeOfType<string>()
            .ShouldBe("Server=example.com,8080;User ID=sa;" +
                      "Password=GeNeRaTeDiNpUt;TrustServerCertificate=true;" +
                      "Database=TestDatabase");
    }

    [Fact]
    public void Should_Match_On_Existing_Property_2()
    {
        // Given
        var model = AspireModel.Parse(Manifests.Simple);
        var ctx = model.CreateContext();

        // When
        var result = ctx.Match("sqlserver-password.inputs.value.default.generate.minLength");

        // Then
        result
            .ShouldBeOfType<int>()
            .ShouldBe(22);
    }
}
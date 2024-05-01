namespace Bender.Tests.Fakes;

public sealed class FakeInputGenerator : IInputGenerator
{
    public string Generate(AspireInput input)
    {
        return "GeNeRaTeDiNpUt";
    }
}
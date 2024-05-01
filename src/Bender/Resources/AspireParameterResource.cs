namespace Bender;

[DebuggerDisplay("Parameter: {Name,nq}")]
public sealed class AspireParameterResource : AspireResource,
    IAspireParsable<(string Name, AspireJsonModel.Resource Resource), AspireParameterResource>
{
    public required AspireString Value { get; init; }
    public required AspireInputs Inputs { get; init; }

    public static AspireParameterResource Parse((string Name, AspireJsonModel.Resource Resource) parsable)
    {
        return new AspireParameterResource
        {
            Name = parsable.Name,
            Type = parsable.Resource.Type,
            Value = AspireString.Parse(parsable.Resource.Value),
            Inputs = AspireInputs.Parse(parsable.Resource.Inputs),
        };
    }

    protected override object? Match(string path)
    {
        return path switch
        {
            "value" => Value,
            "inputs" => Inputs,
            _ => null,
        };
    }
}
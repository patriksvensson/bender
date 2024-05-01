namespace Bender;

public sealed class AspireInputs : List<AspireInput>,
    IAspireParsable<Dictionary<string, AspireJsonModel.Input>, AspireInputs>,
    IAspireMatchable
{
    private AspireInputs()
    {
    }

    private AspireInputs(IEnumerable<AspireInput> bindings)
        : base(bindings)
    {
    }

    public static AspireInputs Parse(Dictionary<string, AspireJsonModel.Input> parsable)
    {
        if (parsable == null)
        {
            return [];
        }

        return new AspireInputs(
            parsable.Select(item =>
                AspireInput.Parse((item.Key, item.Value))));
    }

    object? IAspireMatchable.Match(string path)
    {
        return this.Find(input => input.Name == path);
    }
}

[DebuggerDisplay("{Name,nq}")]
public sealed class AspireInput :
    IAspireParsable<(string Name, AspireJsonModel.Input Input), AspireInput>,
    IAspireMatchable
{
    public required string Name { get; init; }
    public required AspireInputType Type { get; init; }
    public required bool? Secret { get; init; }
    public required AspireInputDefaults Defaults { get; init; }

    public static AspireInput Parse((string Name, AspireJsonModel.Input Input) parsable)
    {
        return new AspireInput
        {
            Name = parsable.Name,
            Type = parsable.Input.Type switch
            {
                AspireJsonModel.TypeEnum.String => AspireInputType.String,
                _ => throw new InvalidOperationException("Unknown input type"),
            },
            Secret = parsable.Input.Secret,
            Defaults = AspireInputDefaults.Parse(parsable.Input.Default),
        };
    }

    object? IAspireMatchable.Match(string path)
    {
        return path switch
        {
            "name" => Name,
            "type" => Type,
            "secret" => Secret,
            "default" => Defaults,
            _ => null,
        };
    }
}
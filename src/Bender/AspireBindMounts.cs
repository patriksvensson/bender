namespace Bender;

public sealed class AspireBindMounts : List<AspireBindMount>,
    IAspireParsable<List<AspireJsonModel.BindMount>, AspireBindMounts>
{
    private AspireBindMounts()
    {
    }

    private AspireBindMounts(IEnumerable<AspireBindMount> bindings)
        : base(bindings)
    {
    }

    public static AspireBindMounts Parse(List<AspireJsonModel.BindMount> parsable)
    {
        if (parsable == null)
        {
            return [];
        }

        return new AspireBindMounts(
            parsable.Select(
                AspireBindMount.Parse));
    }
}

public sealed class AspireBindMount :
    IAspireParsable<AspireJsonModel.BindMount, AspireBindMount>
{
    public required AspireString Source { get; init; }
    public required AspireString Target { get; init; }
    public required bool ReadOnly { get; init; }

    public static AspireBindMount Parse(AspireJsonModel.BindMount parsable)
    {
        return new AspireBindMount
        {
            Source = AspireString.Parse(parsable.Source),
            Target = AspireString.Parse(parsable.Target),
            ReadOnly = parsable.ReadOnly,
        };
    }
}
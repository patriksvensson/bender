namespace Bender;

public sealed class AspireVolumes : List<AspireVolume>,
    IAspireParsable<List<AspireJsonModel.Volume>, AspireVolumes>,
    IAspireMatchable
{
    private AspireVolumes()
    {
    }

    private AspireVolumes(IEnumerable<AspireVolume> bindings)
        : base(bindings)
    {
    }

    public static AspireVolumes Parse(List<AspireJsonModel.Volume> parsable)
    {
        if (parsable == null)
        {
            return [];
        }

        return new AspireVolumes(
            parsable.Select(AspireVolume.Parse));
    }

    object? IAspireMatchable.Match(string path)
    {
        foreach (var volume in this)
        {
            if (volume.Name == path)
            {
                return volume;
            }
        }

        return null;
    }
}

[DebuggerDisplay("{Name,nq}")]
public sealed class AspireVolume :
    IAspireParsable<AspireJsonModel.Volume, AspireVolume>,
    IAspireMatchable
{
    public required string Name { get; init; }
    public required AspireString Target { get; init; }
    public required bool ReadOnly { get; init; }

    public static AspireVolume Parse(AspireJsonModel.Volume parsable)
    {
        return new AspireVolume
        {
            Name = parsable.Name,
            Target = AspireString.Parse(parsable.Target),
            ReadOnly = parsable.ReadOnly,
        };
    }

    object? IAspireMatchable.Match(string path)
    {
        return path switch
        {
            "name" => Name,
            "target" => Target,
            "readOnly" => ReadOnly,
            _ => null,
        };
    }
}
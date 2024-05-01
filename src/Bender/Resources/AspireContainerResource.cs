namespace Bender;

[DebuggerDisplay("Container: {Image,nq}")]
public sealed class AspireContainerResource : AspireResource,
    IAspireParsable<(string Name, AspireJsonModel.Resource Resource), AspireContainerResource>
{
    public required AspireString Image { get; init; }
    public required AspireString EntryPoint { get; init; }
    public required AspireString ConnectionString { get; init; }
    public required Dictionary<string, AspireString> EnvironmentVariables { get; init; }
    public required List<AspireString> Args { get; init; }
    public required AspireBindings Bindings { get; init; }
    public required AspireBindMounts BindMounts { get; init; }
    public required AspireVolumes Volumes { get; init; }

    public static AspireContainerResource Parse((string Name, AspireJsonModel.Resource Resource) parsable)
    {
        return new AspireContainerResource
        {
            Name = parsable.Name,
            Type = parsable.Resource.Type,
            Image = AspireString.Parse(parsable.Resource.Image),
            EntryPoint = AspireString.Parse(parsable.Resource.Entrypoint),
            ConnectionString = AspireString.Parse(parsable.Resource.ConnectionString),
            EnvironmentVariables = parsable.Resource.Env.ToAspireStringDictionary(),
            Args = parsable.Resource.Args.ToAspireStringList(),
            Bindings = AspireBindings.Parse(parsable.Resource.Bindings),
            BindMounts = AspireBindMounts.Parse(parsable.Resource.BindMounts),
            Volumes = AspireVolumes.Parse(parsable.Resource.Volumes),
        };
    }

    protected override object? Match(string path)
    {
        return path switch
        {
            "name" => Name,
            "image" => Image,
            "entryPoint" => EntryPoint,
            "connectionString" => ConnectionString,
            "bindings" => Bindings,
            "bindMounts" => BindMounts,
            "volumes" => Volumes,
            _ => null,
        };
    }
}
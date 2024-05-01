namespace Bender;

public sealed class AspireModel : IAspireMatchable
{
    public IReadOnlyList<AspireResource> Resources { get; }

    public AspireResource this[string name]
    {
        get
        {
            var resource = Resources.FirstOrDefault(x => x.Name == name);
            if (resource == null)
            {
                throw new InvalidOperationException($"Unknown resources '{name}'");
            }

            return resource;
        }
    }

    public AspireModel(IEnumerable<AspireResource> resources)
    {
        Resources = resources.ToArray();
    }

    public static AspireModel Parse(string json)
    {
        return AspireModelParser.Parse(json);
    }

    public AspireContext CreateContext()
    {
        return new AspireContext(this);
    }

    object? IAspireMatchable.Match(string path)
    {
        return Resources.FirstOrDefault(
            resource => resource.Name == path);
    }
}
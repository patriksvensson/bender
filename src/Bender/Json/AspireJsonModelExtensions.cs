namespace Bender.Json;

internal static class AspireJsonModelExtensions
{
    public static (string Name, string Version) GetTypeAndVersion(this AspireJsonModel.Resource resource)
    {
        var type = resource.Type.Split(".", StringSplitOptions.RemoveEmptyEntries);
        return (type[0], type[1]);
    }
}
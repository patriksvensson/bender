using Newtonsoft.Json;

namespace Bender;

internal static class AspireModelParser
{
    public static AspireModel Parse(string json)
    {
        var model = JsonConvert.DeserializeObject<AspireJsonModel>(json);
        if (model == null)
        {
            return new AspireModel([]);
        }

        var result = new List<AspireResource>();
        foreach (var (name, resource) in model.Resources)
        {
            var item = Parse(name, resource);
            if (item != null)
            {
                result.Add(item);
            }
        }

        return new AspireModel(result);
    }

    private static AspireResource? Parse(string name, AspireJsonModel.Resource resource)
    {
        return resource.GetTypeAndVersion() switch
        {
            ("container", "v0") => AspireContainerResource.Parse((name, resource)),
            ("project", "v0") => AspireProjectResource.Parse((name, resource)),
            ("value", "v0") => AspireValueResource.Parse((name, resource)),
            ("parameter", "v0") => AspireParameterResource.Parse((name, resource)),
            _ => null,
        };
    }
}
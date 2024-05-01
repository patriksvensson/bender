namespace Bender;

public static class DictionaryExtensions
{
    public static Dictionary<string, AspireString> ToAspireStringDictionary(this Dictionary<string, string> source)
    {
        if (source == null)
        {
            return [];
        }

        return source.ToDictionary(
            pair => pair.Key,
            pair => AspireString.Parse(pair.Value),
            StringComparer.OrdinalIgnoreCase);
    }
}
namespace Bender;

public static class ListExtensions
{
    public static List<AspireString> ToAspireStringList(this List<string> source)
    {
        return source == null
            ? []
            : source.ConvertAll(AspireString.Parse);
    }
}
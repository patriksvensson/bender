namespace Bender;

public sealed class AspireArguments : List<AspireString>
{
    public AspireArguments(IEnumerable<AspireString> arguments)
        : base(arguments)
    {
    }
}
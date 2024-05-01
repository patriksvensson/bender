namespace Bender;

[DebuggerDisplay("{ToDebugString(),nq}")]
public sealed class AspireString
{
    private readonly string? _raw;
    private readonly List<AspireStringToken> _tokens;

    public bool IsNull => _raw == null;

    internal AspireString(string? raw, List<AspireStringToken> tokens)
    {
        _raw = raw;
        _tokens = tokens;
    }

    public static AspireString Parse(string text)
    {
        return AspireStringParser.Parse(text);
    }

    public string Evaluate(AspireContext context)
    {
        var builder = new StringBuilder();
        foreach (var token in _tokens)
        {
            token.Switch(
                path =>
                {
                    var value = context.Match(path);
                    if (value == null)
                    {
                        throw new InvalidOperationException($"Could not resolve path '{path}'");
                    }

                    builder.Append(value);
                },
                text => builder.Append(text));
        }

        return builder.ToString();
    }

    private string ToDebugString()
    {
        return _raw ?? "<null>";
    }
}
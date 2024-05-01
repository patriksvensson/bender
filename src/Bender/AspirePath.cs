using System.Diagnostics.CodeAnalysis;

namespace Bender;

public sealed class AspirePath
{
    private readonly string[] _segments;

    [MemberNotNullWhen(true, nameof(Current))]
    public bool IsValid => _segments.Length > 0;
    [MemberNotNullWhen(false, nameof(Current))]
    public bool IsEmpty => _segments.Length == 0;
    [MemberNotNullWhen(true, nameof(Current))]
    public bool IsLeaf => _segments.Length == 1;

    public string? Current => IsValid ? _segments[0] : null;

    public AspirePath(string path)
    {
        _segments = path.Split('.', StringSplitOptions.RemoveEmptyEntries);
    }

    private AspirePath(IEnumerable<string> segments)
    {
        _segments = segments.ToArray();
    }

    public AspirePath Pop()
    {
        if (IsValid)
        {
            var parts = _segments[1..];
            return new AspirePath(parts);
        }

        return new AspirePath(Array.Empty<string>());
    }

    public override string ToString()
    {
        return string.Join(".", _segments);
    }
}
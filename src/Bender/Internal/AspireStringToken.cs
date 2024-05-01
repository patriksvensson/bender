namespace Bender;

internal sealed class AspireStringToken : OneOfBase<AspirePath, string>
{
    public AspireStringToken(OneOf<AspirePath, string> arg)
        : base(arg)
    {
    }

    public static implicit operator AspireStringToken(AspirePath path) => new(path);
    public static implicit operator AspireStringToken(string arg) => new(arg);
}
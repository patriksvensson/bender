namespace Bender;

public interface IAspireParsable<in TContext, out TSelf>
{
    static abstract TSelf Parse(TContext parsable);
}
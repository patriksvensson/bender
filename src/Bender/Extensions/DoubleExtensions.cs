namespace Bender;

public static class DoubleExtensions
{
    public static int? ToNullableInt(this double? value)
    {
        if (value == null)
        {
            return null;
        }

        return (int)value.Value;
    }
}
namespace Bender;

public sealed class AspireInputDefaults :
    IAspireParsable<AspireJsonModel.Default, AspireInputDefaults>,
    IAspireMatchable
{
    public required bool? Lower { get; set; }
    public required int MinLength { get; set; }
    public required int? MinLower { get; set; }
    public required int? MinNumeric { get; set; }
    public required int? MinSpecial { get; set; }
    public required int? MinUpper { get; set; }
    public required bool? Numeric { get; set; }
    public required bool? Special { get; set; }
    public required bool? Upper { get; set; }

    public static AspireInputDefaults Parse(AspireJsonModel.Default parsable)
    {
        return new AspireInputDefaults
        {
            Lower = parsable.Generate.Lower,
            MinLength = (int)parsable.Generate.MinLength,
            MinLower = parsable.Generate.MinLower.ToNullableInt(),
            MinNumeric = parsable.Generate.MinNumeric.ToNullableInt(),
            MinSpecial = parsable.Generate.MinSpecial.ToNullableInt(),
            MinUpper = parsable.Generate.MinUpper.ToNullableInt(),
            Numeric = parsable.Generate.Numeric,
            Special = parsable.Generate.Special,
            Upper = parsable.Generate.Upper,
        };
    }

    object? IAspireMatchable.Match(string path)
    {
        return path switch
        {
            "generate" => this,
            "minLength" => MinLength,
            "minLower" => MinLower,
            "minNumeric" => MinNumeric,
            "minSpecial" => MinSpecial,
            "minUpper" => MinUpper,
            "numeric" => Numeric,
            "special" => Special,
            "upper" => Upper,
            _ => null,
        };
    }
}
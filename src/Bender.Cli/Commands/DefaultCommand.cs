namespace Bender.Commands;

public sealed class DefaultCommand : Command<DefaultCommand.Settings>
{
    private readonly IAnsiConsole _console;

    public sealed class Settings : CommandSettings
    {
        [CommandArgument(0, "<NAME>")]
        public string? Name { get; set; }

        public Settings(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }
    }

    public DefaultCommand(IAnsiConsole console)
    {
        _console = console ?? throw new ArgumentNullException(nameof(console));
    }

    public override int Execute([NotNull] CommandContext context, [NotNull] Settings settings)
    {
        _console.MarkupLine($"Hello [yellow]{settings.Name.EscapeMarkup()}[/]!");
        return 0;
    }
}

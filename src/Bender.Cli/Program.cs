namespace Bender;

public static class Program
{
    public static int Main(string[] args)
    {
        var container = BuildContainer();

        var app = new CommandApp<DefaultCommand>(container);
        app.Configure(config =>
        {
            config.SetApplicationName("bender");
            config.UseStrictParsing();
        });

        return app.Run(args);
    }

    private static TypeRegistrar BuildContainer()
    {
        var services = new ServiceCollection();

        /////////////////////////////////////////
        // Register dependencies here
        /////////////////////////////////////////

        return new TypeRegistrar(services);
    }
}

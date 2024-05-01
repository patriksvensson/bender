namespace Bender;

public sealed class AspireBindings : List<AspireBinding>,
    IAspireParsable<Dictionary<string, AspireJsonModel.Binding>, AspireBindings>,
    IAspireMatchable
{
    private AspireBindings()
    {
    }

    private AspireBindings(IEnumerable<AspireBinding> bindings)
        : base(bindings)
    {
    }

    public static AspireBindings Parse(Dictionary<string, AspireJsonModel.Binding> parsable)
    {
        if (parsable == null)
        {
            return [];
        }

        return new AspireBindings(
            parsable.Select(item =>
                AspireBinding.Parse((item.Key, item.Value))));
    }

    object? IAspireMatchable.Match(string path)
    {
        return this.Find(binding => binding.Name == path);
    }
}

[DebuggerDisplay("{Name,nq}")]
public sealed class AspireBinding :
    IAspireParsable<(string Name, AspireJsonModel.Binding Binding), AspireBinding>,
    IAspireMatchable
{
    public required string Name { get; init; }
    public required AspireScheme Scheme { get; init; }
    public required AspireProtocol Protocol { get; init; }
    public required AspireTransport Transport { get; init; }
    public required bool? External { get; init; }
    public required int? TargetPort { get; init; }
    public required int? Port { get; init; }

    public static AspireBinding Parse((string Name, AspireJsonModel.Binding Binding) parsable)
    {
        return new AspireBinding
        {
            Name = parsable.Name,
            External = parsable.Binding.External,
            TargetPort = parsable.Binding.TargetPort.ToNullableInt(),
            Port = parsable.Binding.Port.ToNullableInt(),
            Scheme = parsable.Binding.Scheme switch
            {
                AspireJsonModel.Scheme.Http => AspireScheme.Http,
                AspireJsonModel.Scheme.Https => AspireScheme.Https,
                AspireJsonModel.Scheme.Tcp => AspireScheme.Tcp,
                AspireJsonModel.Scheme.Udp => AspireScheme.Udp,
                _ => throw new InvalidOperationException("Unknown schema"),
            },
            Protocol = parsable.Binding.Protocol switch
            {
                AspireJsonModel.Protocol.Tcp => AspireProtocol.Tcp,
                AspireJsonModel.Protocol.Udp => AspireProtocol.Udp,
                _ => throw new InvalidOperationException("Unknown protocol"),
            },
            Transport = parsable.Binding.Transport switch
            {
                AspireJsonModel.Transport.Http => AspireTransport.Http,
                AspireJsonModel.Transport.Http2 => AspireTransport.Http2,
                AspireJsonModel.Transport.Tcp => AspireTransport.Tcp,
                _ => throw new InvalidOperationException("Unknown transport"),
            },
        };
    }

    object? IAspireMatchable.Match(string path)
    {
        return path switch
        {
            "name" => Name,
            "external" => External,
            "targetPort" => TargetPort,
            "port" => Port,
            "scheme" => Scheme,
            "protocol" => Protocol,
            "transport" => Transport,
            _ => null,
        };
    }
}
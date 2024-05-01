namespace Bender;

public interface IInputGenerator
{
    string Generate(AspireInput input);
}

public sealed class AspireContext
{
    private readonly AspireModel _model;
    private readonly Dictionary<string, object?> _inputs;

    public IInputGenerator? InputGenerator { get; set; }

    internal AspireContext(AspireModel model)
    {
        _model = model ?? throw new ArgumentNullException(nameof(model));
        _inputs = new Dictionary<string, object?>(StringComparer.OrdinalIgnoreCase);
    }

    public void AddInput(string path, object? value)
    {
        _inputs[path] = value;
    }

    public object Match(string path)
    {
        var result = Match(new AspirePath(path));
        if (result == null)
        {
            throw new InvalidOperationException($"The path '{path}' could not be evaluated");
        }

        return result;
    }

    internal object? Match(AspirePath path)
    {
        if (_inputs.TryGetValue(path.ToString(), out var value))
        {
            return Format(path, value);
        }

        var originalPath = path;
        var current = (IAspireMatchable?)_model;

        while (current != null)
        {
            if (path.Current == null)
            {
                return Format(originalPath, current);
            }

            var result = current.Match(path.Current);
            if (path.IsLeaf)
            {
                return Format(originalPath, result);
            }

            if (result is IAspireMatchable matchable)
            {
                path = path.Pop();
                current = matchable;
            }
            else
            {
                if (path.IsLeaf)
                {
                    return Format(originalPath, current);
                }

                return null;
            }
        }

        return null;
    }

    private object? Format(AspirePath path, object? value)
    {
        // Interpolated string?
        if (value is AspireString)
        {
            while (value is AspireString str)
            {
                value = str.Evaluate(this);
            }
        }

        // Input?
        if (value is AspireInput input)
        {
            if (InputGenerator == null)
            {
                return null;
            }

            // If we got here, nothing was registered for the input path.
            // This means that we should generate the value.
            // Cache the generated value so it always return the same.
            var password = InputGenerator.Generate(input);
            AddInput(path.ToString(), password);
            return password;
        }

        return value;
    }
}
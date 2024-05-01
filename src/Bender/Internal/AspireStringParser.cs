namespace Bender;

internal static class AspireStringParser
{
    public static AspireString Parse(string? text)
    {
        var result = new List<AspireStringToken>();
        if (text == null)
        {
            return new AspireString(text, result);
        }

        var reader = new StringReader(text);
        int start = 0, index = 0;

        while (true)
        {
            var current = reader.Read();
            if (current == -1)
            {
                break;
            }

            if (current == '{')
            {
                var prev = text[start..index];
                result.Add(new AspireStringToken(prev));
                start = index + 1;
            }
            else if (current == '}')
            {
                var prev = text[start..index];
                var path = new AspirePath(prev);
                result.Add(new AspireStringToken(path));
                start = index + 1;
            }

            index++;
        }

        if (start != index)
        {
            var prev = text[start..index];
            result.Add(new AspireStringToken(prev));
        }

        return new AspireString(text, result);
    }
}
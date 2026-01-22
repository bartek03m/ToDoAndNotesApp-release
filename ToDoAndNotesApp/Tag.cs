// Represents a tag using Multiton pattern to ensure memory efficiency by sharing instances.
public class Tag
{
    private static readonly Dictionary<string, Tag> _instances = new();

    public string Name { get; init; }
    public string Color { get; init; }

    private Tag(string name, string color)
    {
        Name = name;
        Color = color;
    }

    // Ensures unique tag instances to prevent duplication in memory.
    public static Tag GetInstance(string name, string color)
    {
        // Guard clauses to prevent invalid state early.
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Tag name can't be empty", nameof(name));
        }

        if (string.IsNullOrWhiteSpace(color))
        {
            throw new ArgumentException("Tag color can't be empty", nameof(color));
        }

        if (_instances.ContainsKey(name))
        {
            return _instances[name];
        }

        var newTag = new Tag(name, color);
        _instances.Add(name, newTag);
        return newTag;
    }
}

namespace WorkoutTracker.Domain.ValueObjects;

public sealed class Rating
{
    public int Value { get; }

    private Rating(int value)
    {
        Value = value;
    }

    public static Rating Create(int value)
    {
        if (value < 1 || value > 10)
            throw new ArgumentOutOfRangeException(nameof(value), "Rating must be between 1 and 10.");

        return new Rating(value);
    }

    public override bool Equals(object? obj) =>
        obj is Rating other && Value == other.Value;

    public override int GetHashCode() => Value.GetHashCode();

    public static implicit operator int(Rating rating) => rating.Value;
}

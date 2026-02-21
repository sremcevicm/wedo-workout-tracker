using WorkoutTracker.Domain.Enums;

namespace WorkoutTracker.Domain.Entities;

public class User
{
    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Gender Gender { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public ICollection<Workout> Workouts { get; private set; } = new List<Workout>();

    private User()
    {
        Email = null!;
        PasswordHash = null!;
        FirstName = null!;
        LastName = null!;
    }

    public static User Create(string email, string passwordHash, string firstName, string lastName, Gender gender)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            PasswordHash = passwordHash,
            FirstName = firstName,
            LastName = lastName,
            Gender = gender,
            CreatedAt = DateTime.UtcNow
        };
    }
}

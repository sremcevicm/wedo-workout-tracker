namespace WorkoutTracker.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(string email, string firstName);
}
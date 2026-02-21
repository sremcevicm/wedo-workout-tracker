namespace WorkoutTracker.Application.Interfaces;

public interface IUnitOfWork
{
    IUserRepository UserRepository { get; }
    IWorkoutRepository WorkoutRepository { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

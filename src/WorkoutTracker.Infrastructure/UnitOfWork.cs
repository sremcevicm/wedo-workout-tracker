using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Infrastructure.Data;

namespace WorkoutTracker.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IUserRepository UserRepository { get; }
    public IWorkoutRepository WorkoutRepository { get; }

    public UnitOfWork(AppDbContext context, IUserRepository userRepository, IWorkoutRepository workoutRepository)
    {
        _context = context;
        UserRepository = userRepository;
        WorkoutRepository = workoutRepository;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}

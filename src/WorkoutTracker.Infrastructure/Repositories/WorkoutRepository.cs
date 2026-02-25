using Microsoft.EntityFrameworkCore;
using WorkoutTracker.Application.Interfaces;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Infrastructure.Data;

namespace WorkoutTracker.Infrastructure.Repositories;

public class WorkoutRepository : IWorkoutRepository
{
    private readonly AppDbContext _context;

    public WorkoutRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Workout workout, CancellationToken cancellationToken = default)
    {
        await _context.Workouts.AddAsync(workout, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Workout workout, CancellationToken cancellationToken = default)
    {
        _context.Workouts.Remove(workout);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<Workout?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Workouts.FindAsync([id], cancellationToken);
    }

    public async Task<IEnumerable<Workout>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.Workouts
            .Where(w => w.UserId == userId)
            .OrderByDescending(w => w.WorkoutDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Workout>> GetByUserIdAndMonthAsync(Guid userId, int year, int month, CancellationToken cancellationToken = default)
    {
        return await _context.Workouts
            .Where(w => w.UserId == userId && w.WorkoutDate.Year == year && w.WorkoutDate.Month == month)
            .OrderBy(w => w.WorkoutDate)
            .ToListAsync(cancellationToken);
    }

    public async Task UpdateAsync(Workout workout, CancellationToken cancellationToken = default)
    {
        _context.Workouts.Update(workout);
        await _context.SaveChangesAsync(cancellationToken);
    }

}


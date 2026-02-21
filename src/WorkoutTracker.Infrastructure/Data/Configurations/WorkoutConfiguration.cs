using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorkoutTracker.Domain.Entities;
using WorkoutTracker.Domain.ValueObjects;

namespace WorkoutTracker.Infrastructure.Data.Configurations;

public class WorkoutConfiguration : IEntityTypeConfiguration<Workout>
{
    public void Configure(EntityTypeBuilder<Workout> builder)
    {
        builder.HasKey(w => w.Id);

        builder.Property(w => w.ExerciseType)
            .IsRequired();

        builder.Property(w => w.Duration)
            .IsRequired();

        builder.Property(w => w.CaloriesBurned)
            .IsRequired();

        builder.Property(w => w.Difficulty)
            .IsRequired()
            .HasConversion(
                r => r.Value,
                v => Rating.Create(v));

        builder.Property(w => w.Fatigue)
            .IsRequired()
            .HasConversion(
                r => r.Value,
                v => Rating.Create(v));

        builder.Property(w => w.Notes)
            .HasMaxLength(1000);

        builder.Property(w => w.WorkoutDate)
            .IsRequired();

        builder.Property(w => w.CreatedAt)
            .IsRequired();
    }
}

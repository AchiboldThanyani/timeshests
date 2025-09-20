using Microsoft.EntityFrameworkCore;
using timesheets.Domain.Entities;

namespace timesheets.Infrastructure.Data;

public class TimesheetDbContext : DbContext
{
    public TimesheetDbContext(DbContextOptions<TimesheetDbContext> options) : base(options)
    {
    }

    public DbSet<Timesheet> Timesheets { get; set; }
    public DbSet<Project> Projects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure Timesheet entity
        modelBuilder.Entity<Timesheet>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.EmployeeName).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.HoursWorked).HasColumnType("decimal(5,2)");
            
            // Configure relationship with Project
            entity.HasOne(e => e.Project)
                  .WithMany(p => p.Timesheets)
                  .HasForeignKey(e => e.ProjectId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // Configure Project entity
        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Client).HasMaxLength(100);
        });
    }
}

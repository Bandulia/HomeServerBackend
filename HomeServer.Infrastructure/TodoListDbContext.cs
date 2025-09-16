using Microsoft.EntityFrameworkCore;

namespace HomeServer.Infrastructure
{
    public class TodoListDbContext(DbContextOptions<TodoListDbContext> options) : DbContext(options)
    {
        public DbSet<TodoEntity> Todos => Set<TodoEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<TodoEntity>();

            entity.ToTable("Todos");
            entity.HasKey(t => t.Id);

            entity.Property(t => t.Title).IsRequired().HasMaxLength(200);
            entity.Property(t => t.Description).HasMaxLength(2000);

            entity.Property(t => t.Priority).HasConversion<int>();

            entity.Property(t => t.CreatedAt).IsRequired();
            entity.Property(t => t.UpdatedAt);
            entity.Property(t => t.StartedAt);
            entity.Property(t => t.DueDate);
            entity.Property(t => t.CompletedAt);

            entity.Property(t => t.Notes).HasColumnType("jsonb");
            entity.Property(t => t.SubTasks).HasColumnType("jsonb");
        }
    }
}

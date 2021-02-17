using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TaskManager.Model;

namespace TaskManager.Data {
    public class TaskManagerContext : DbContext {

        public DbSet<User> Users { get; set; }

        // Constructor
        public TaskManagerContext(DbContextOptions<TaskManagerContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            ConfigureModelBuilderForUser(modelBuilder);
        }

        void ConfigureModelBuilderForUser(ModelBuilder modelBuilder) {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<User>()
                .Property(user => user.Username)
                .HasMaxLength(60)
                .IsRequired();

            modelBuilder.Entity<User>()
                .Property(user => user.Email)
                .HasMaxLength(60)
                .IsRequired();
        }

    }
}

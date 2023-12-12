using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UserApi.Entities
{
    public partial class UserDbContext : DbContext
    {
        public UserDbContext()
        {
        }

#pragma warning disable CS0436 // Type conflicts with imported type
        public UserDbContext(DbContextOptions<UserDbContext> options)
#pragma warning restore CS0436 // Type conflicts with imported type
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Name=ConnectionStrings:UserDb");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("Users_pkey");

                entity.HasIndex(e => e.Email, "Users_Email_key").IsUnique();

                entity.HasIndex(e => e.Username, "Users_Username_key").IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("uuid_generate_v4()");
                entity.Property(e => e.DateCreated)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnType("timestamp without time zone");
                entity.Property(e => e.DateModified)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .HasColumnType("timestamp without time zone");
                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsRequired(); // Added Required attribute assuming email should not be null
                entity.Property(e => e.FailedLoginAttempts).HasDefaultValueSql("0");
                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsRequired(); // Added Required attribute assuming first name should not be null
                entity.Property(e => e.IsActive).HasDefaultValueSql("true");
                entity.Property(e => e.LastLogin).HasColumnType("timestamp without time zone");
                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsRequired(); // Added Required attribute assuming last name should not be null
                entity.Property(e => e.Role).HasMaxLength(255);
                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsRequired(); // Added Required attribute assuming username should not be null
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

    public interface IPasswordHasher
    {
        (byte[] Hash, byte[] Salt) HashPassword(string password);
        (byte[] hash, byte[] salt) HashPassword(object password);
    }

    public class YourPasswordHasher : IPasswordHasher
    {
        private object salt;

        public (byte[] Hash, byte[] Salt) HashPassword(string password)
{
    byte[] hash = // Compute the hash from the password
    byte[] salt = // Generate a random salt

#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
    return (hash, salt);
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.
        }


        public (byte[] hash, byte[] salt) HashPassword(object password)
        {
            throw new NotImplementedException();
        }
    }
}

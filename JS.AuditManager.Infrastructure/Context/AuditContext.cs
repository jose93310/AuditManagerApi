using JS.AuditManager.Domain.ModelEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JS.AuditManager.Infrastructure.Context
{
    public class AuditContext : DbContext
    {
        public AuditContext(DbContextOptions<AuditContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración de la tabla User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User", "dbo");

                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId)
                    .IsRequired();

                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(255)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.StatusId)
                    .IsRequired();

                entity.Property(e => e.Phone)
                    .HasMaxLength(15)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime2")
                    .IsRequired();

                entity.Property(e => e.CreatedBy)
                    .IsRequired();

                entity.Property(e => e.ModifiedAt)
                    .HasColumnType("datetime");

                entity.Property(e => e.ModifiedBy);
            });

            // Configuración de Token
            modelBuilder.Entity<Token>(entity =>
            {
               
                entity.ToTable("Token", "dbo");

                entity.HasKey(t => t.TokenId);

                entity.HasOne(t => t.User)
                      .WithMany(u => u.Tokens)
                      .HasForeignKey(t => t.UserId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.Property(e => e.TokenId)
                    .IsRequired();

                entity.Property(e => e.UserId)
                    .IsRequired();

                entity.Property(e => e.AccessToken)
                    .HasMaxLength(2000)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime2")
                    .IsRequired();

                entity.Property(e => e.ExpiresAt)
                    .HasColumnType("datetime2")
                    .IsRequired();

                entity.Property(e => e.IsRevoked)
                    .IsRequired();

                entity.Property(e => e.RefreshToken)
                    .HasMaxLength(2000)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.RefreshExpiresAt)
                    .HasColumnType("datetime2")
                    .IsRequired();

            });
        }
    }
}

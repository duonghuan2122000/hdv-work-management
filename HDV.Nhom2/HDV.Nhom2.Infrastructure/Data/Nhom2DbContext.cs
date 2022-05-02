using HDV.Nhom2.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDV.Nhom2.Infrastructure
{
    public class Nhom2DbContext : DbContext
    {
        public Nhom2DbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<Company> Company { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Project> Project { get; set; }
        public virtual DbSet<Workitem> Workitem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            modelBuilder.Entity<Company>(entity =>
            {
                entity.ToTable("company");

                entity.HasComment("Bảng lưu thông tin công ty");

                entity.Property(e => e.Id).HasDefaultValueSql("''");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(1000);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.HasComment("thông tin nhân viên");

                entity.Property(e => e.Id).HasDefaultValueSql("''");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FirstName).HasMaxLength(20);

                entity.Property(e => e.Gender).HasColumnType("int(4)");

                entity.Property(e => e.LastName).HasMaxLength(20);

                entity.Property(e => e.PasswordHash).HasMaxLength(100);

                entity.Property(e => e.PasswordSalt).HasMaxLength(100);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

                entity.Property(e => e.Username).HasMaxLength(100);
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.ToTable("project");

                entity.HasComment("Thông tin dự án");

                entity.Property(e => e.Id).HasDefaultValueSql("''");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(1000);

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Workitem>(entity =>
            {
                entity.ToTable("workitem");

                entity.HasComment("thông tin công việc");

                entity.Property(e => e.Id).HasDefaultValueSql("''");

                entity.Property(e => e.Code).HasMaxLength(20);

                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.CreatedDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(1000);

                entity.Property(e => e.Status).HasColumnType("int(11)");

                entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            });
        }

    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EXE.DataAccess;

public partial class ExeContext : DbContext
{
    public ExeContext()
    {
    }

    public ExeContext(DbContextOptions<ExeContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Paper> Papers { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    public virtual DbSet<Spring> Springs { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserSession> UserSessions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
.SetBasePath(Directory.GetCurrentDirectory())
.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyStockDB"));

    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Paper>(entity =>
        {
            entity.ToTable("paper");

            entity.Property(e => e.PaperId).HasColumnName("paper_id");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Image)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Money)
                .HasColumnType("money")
                .HasColumnName("money");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.Property(e => e.PaymentId).HasColumnName("payment_id");
            entity.Property(e => e.Amount)
                .HasColumnType("money")
                .HasColumnName("amount");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.PaymentDate)
                .HasColumnType("datetime")
                .HasColumnName("payment_date");
            entity.Property(e => e.PaymentMethod).HasColumnName("payment_method");
            entity.Property(e => e.Status).HasColumnName("status");
            entity.Property(e => e.TransactionId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("transaction_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Payments_Users");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.Property(e => e.ProjectId).HasColumnName("project_id");
            entity.Property(e => e.Describe)
                .HasColumnType("text")
                .HasColumnName("describe");
            entity.Property(e => e.Image)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.PaperId).HasColumnName("paper_id");
            entity.Property(e => e.SizeId).HasColumnName("Size_id");
            entity.Property(e => e.SpringId).HasColumnName("spring_id");
            entity.Property(e => e.Total)
                .HasColumnType("money")
                .HasColumnName("total");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Paper).WithMany(p => p.Projects)
                .HasForeignKey(d => d.PaperId)
                .HasConstraintName("FK_Projects_paper");

            entity.HasOne(d => d.Size).WithMany(p => p.Projects)
                .HasForeignKey(d => d.SizeId)
                .HasConstraintName("FK_Projects_Size");

            entity.HasOne(d => d.Spring).WithMany(p => p.Projects)
                .HasForeignKey(d => d.SpringId)
                .HasConstraintName("FK_Projects_spring");

            entity.HasOne(d => d.User).WithMany(p => p.Projects)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Projects_Users");
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.ToTable("Size");

            entity.Property(e => e.SizeId).HasColumnName("size_id");
            entity.Property(e => e.Money)
                .HasColumnType("money")
                .HasColumnName("money");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Ratio)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("ratio");
        });

        modelBuilder.Entity<Spring>(entity =>
        {
            entity.ToTable("spring");

            entity.Property(e => e.SpringId).HasColumnName("Spring_id");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("color");
            entity.Property(e => e.Money)
                .HasColumnType("money")
                .HasColumnName("money");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Avatar)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("avatar");
            entity.Property(e => e.FacebookId)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("facebook_id");
            entity.Property(e => e.GoogleId)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("google_id");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Role).HasColumnName("role");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserSession>(entity =>
        {
            entity.HasKey(e => e.SessionId);

            entity.ToTable("User_Sessions");

            entity.Property(e => e.SessionId).HasColumnName("session_id");
            entity.Property(e => e.AccessToken)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("access_token");
            entity.Property(e => e.ExpireTime)
                .HasColumnType("datetime")
                .HasColumnName("expire_time");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserSessions)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_User_Sessions_Users");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

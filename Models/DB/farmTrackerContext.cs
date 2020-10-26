using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FarmTracker_services.Models.DB
{
    public partial class farmTrackerContext : DbContext
    {
        public farmTrackerContext()
        {
        }

        public farmTrackerContext(DbContextOptions<farmTrackerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CodeType> CodeType { get; set; }
        public virtual DbSet<GeneratedUcodes> GeneratedUcodes { get; set; }
        public virtual DbSet<MemberType> MemberType { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Sessions> Sessions { get; set; }
        public virtual DbSet<SignInLogs> SignInLogs { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CodeType>(entity =>
            {
                entity.HasKey(e => e.Ctuid);

                entity.ToTable("codeType");

                entity.Property(e => e.Ctuid).HasColumnName("CTUID");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<GeneratedUcodes>(entity =>
            {
                entity.HasKey(e => e.Guc);

                entity.ToTable("generatedUCodes");

                entity.Property(e => e.Guc)
                    .HasColumnName("GUC")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Ctuid).HasColumnName("CTUID");

                entity.Property(e => e.ExpirationDate)
                    .HasColumnName("expirationDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.ForIp)
                    .HasColumnName("forIP")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.ForUuid).HasColumnName("forUUID");

                entity.Property(e => e.IsValid)
                    .HasColumnName("isValid")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Ctu)
                    .WithMany(p => p.GeneratedUcodes)
                    .HasForeignKey(d => d.Ctuid)
                    .HasConstraintName("FK_generatedUCodes_codeType");

                entity.HasOne(d => d.ForUu)
                    .WithMany(p => p.GeneratedUcodes)
                    .HasForeignKey(d => d.ForUuid)
                    .HasConstraintName("FK_generatedUCodes_users");
            });

            modelBuilder.Entity<MemberType>(entity =>
            {
                entity.HasKey(e => e.Mtuid);

                entity.ToTable("memberType");

                entity.Property(e => e.Mtuid).HasColumnName("MTUID");

                entity.Property(e => e.CollaboratorLimit).HasColumnName("collaboratorLimit");

                entity.Property(e => e.FarmLimit).HasColumnName("farmLimit");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("money");

                entity.Property(e => e.PropertyLimit).HasColumnName("propertyLimit");

                entity.Property(e => e.SupportFlag).HasColumnName("supportFlag");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.Ruid);

                entity.ToTable("roles");

                entity.Property(e => e.Ruid).HasColumnName("RUID");

                entity.Property(e => e.CanEnterAdminDashboard).HasColumnName("can_enter_admin_dashboard");

                entity.Property(e => e.CreatedByUuid)
                    .HasColumnName("createdByUUID")
                    .HasMaxLength(36)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(32)
                    .IsFixedLength();

                entity.Property(e => e.SysRoleFlag).HasColumnName("sysRoleFlag");
            });

            modelBuilder.Entity<Sessions>(entity =>
            {
                entity.HasKey(e => e.Suid);

                entity.ToTable("sessions");

                entity.Property(e => e.Suid)
                    .HasColumnName("SUID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.IsValid)
                    .HasColumnName("isValid")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Uuid).HasColumnName("UUID");

                entity.HasOne(d => d.Uu)
                    .WithMany(p => p.Sessions)
                    .HasForeignKey(d => d.Uuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_sessions_users");
            });

            modelBuilder.Entity<SignInLogs>(entity =>
            {
                entity.HasKey(e => e.Luid);

                entity.ToTable("signIn_logs");

                entity.Property(e => e.Luid)
                    .HasColumnName("LUID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.AttemptedPassword)
                    .IsRequired()
                    .HasColumnName("attemptedPassword")
                    .HasMaxLength(255);

                entity.Property(e => e.AttemptedResult).HasColumnName("attemptedResult");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(50);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(50);

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Hostname)
                    .HasColumnName("hostname")
                    .HasMaxLength(255);

                entity.Property(e => e.IpAdd)
                    .HasColumnName("ipAdd")
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasColumnName("location")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Uuid).HasColumnName("UUID");

                entity.HasOne(d => d.Uu)
                    .WithMany(p => p.SignInLogs)
                    .HasForeignKey(d => d.Uuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_signIn_logs_users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Uuid);

                entity.ToTable("users");

                entity.HasIndex(e => e.Uuid)
                    .HasName("IX_users")
                    .IsUnique();

                entity.Property(e => e.Uuid)
                    .HasColumnName("UUID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DeletedByUuid).HasColumnName("deletedByUUID");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deletedDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedFlag).HasColumnName("deletedFlag");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(100);

                entity.Property(e => e.EmailActivated).HasColumnName("emailActivated");

                entity.Property(e => e.Mtuid)
                    .HasColumnName("MTUID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(24)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.PhoneNumber)
                    .HasColumnName("phoneNumber")
                    .HasMaxLength(16)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumberActivated).HasColumnName("phoneNumberActivated");

                entity.Property(e => e.ProfilPic)
                    .HasColumnName("profilPic")
                    .HasMaxLength(50);

                entity.Property(e => e.Ruid)
                    .HasColumnName("RUID")
                    .HasDefaultValueSql("((2))");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(50);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.DeletedByUu)
                    .WithMany(p => p.InverseDeletedByUu)
                    .HasForeignKey(d => d.DeletedByUuid)
                    .HasConstraintName("FK_UUID_deletedByUUID");

                entity.HasOne(d => d.Mtu)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Mtuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_users_memberType");

                entity.HasOne(d => d.Ru)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Ruid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_users_roles");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

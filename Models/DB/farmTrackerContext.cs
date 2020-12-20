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

        public virtual DbSet<Adds> Adds { get; set; }
        public virtual DbSet<CRoles> CRoles { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<CategoryOfProperties> CategoryOfProperties { get; set; }
        public virtual DbSet<CodeType> CodeType { get; set; }
        public virtual DbSet<Collaborators> Collaborators { get; set; }
        public virtual DbSet<Coptypes> Coptypes { get; set; }
        public virtual DbSet<Copvalues> Copvalues { get; set; }
        public virtual DbSet<EntityCopvalues> EntityCopvalues { get; set; }
        public virtual DbSet<EntityDetails> EntityDetails { get; set; }
        public virtual DbSet<EntityOfFp> EntityOfFp { get; set; }
        public virtual DbSet<FarmEntities> FarmEntities { get; set; }
        public virtual DbSet<FarmProperties> FarmProperties { get; set; }
        public virtual DbSet<Farms> Farms { get; set; }
        public virtual DbSet<GeneratedUcodes> GeneratedUcodes { get; set; }
        public virtual DbSet<IncomeAndExpeneses> IncomeAndExpeneses { get; set; }
        public virtual DbSet<MemberType> MemberType { get; set; }
        public virtual DbSet<Pictures> Pictures { get; set; }
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
            modelBuilder.Entity<Adds>(entity =>
            {
                entity.HasKey(e => e.Auid);

                entity.Property(e => e.Auid)
                    .HasColumnName("AUID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.ConfirmedByUuid).HasColumnName("confirmedByUUID");

                entity.Property(e => e.ConfirmedFlag).HasColumnName("confirmedFlag");

                entity.Property(e => e.CreatedByUuid).HasColumnName("createdByUUID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Cuid).HasColumnName("CUID");

                entity.Property(e => e.DeletedByUuid).HasColumnName("deletedByUUID");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deletedDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedFlag).HasColumnName("deletedFlag");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("money");

                entity.Property(e => e.PublishedDate)
                    .HasColumnName("publishedDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.PublishedFlag).HasColumnName("publishedFlag");

                entity.HasOne(d => d.ConfirmedByUu)
                    .WithMany(p => p.AddsConfirmedByUu)
                    .HasForeignKey(d => d.ConfirmedByUuid)
                    .HasConstraintName("FK_Adds_users1");

                entity.HasOne(d => d.CreatedByUu)
                    .WithMany(p => p.AddsCreatedByUu)
                    .HasForeignKey(d => d.CreatedByUuid)
                    .HasConstraintName("FK_Adds_users");

                entity.HasOne(d => d.Cu)
                    .WithMany(p => p.Adds)
                    .HasForeignKey(d => d.Cuid)
                    .HasConstraintName("FK_Adds_categories");

                entity.HasOne(d => d.DeletedByUu)
                    .WithMany(p => p.AddsDeletedByUu)
                    .HasForeignKey(d => d.DeletedByUuid)
                    .HasConstraintName("FK_Adds_users2");
            });

            modelBuilder.Entity<CRoles>(entity =>
            {
                entity.HasKey(e => e.Ruid);

                entity.ToTable("cRoles");

                entity.Property(e => e.Ruid)
                    .HasColumnName("RUID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.BasicRoleFlag)
                    .HasColumnName("basicRoleFlag")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CanCreateProperty)
                    .HasColumnName("can_create_property")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedByUuid).HasColumnName("createdByUUID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DeletedByUuid).HasColumnName("deletedByUUID");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deletedDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedFlag).HasColumnName("deletedFlag");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.HasOne(d => d.CreatedByUu)
                    .WithMany(p => p.CRolesCreatedByUu)
                    .HasForeignKey(d => d.CreatedByUuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_cRoles_users");

                entity.HasOne(d => d.DeletedByUu)
                    .WithMany(p => p.CRolesDeletedByUu)
                    .HasForeignKey(d => d.DeletedByUuid)
                    .HasConstraintName("FK_cRoles_users1");
            });

            modelBuilder.Entity<Categories>(entity =>
            {
                entity.HasKey(e => e.Cuid)
                    .HasName("PK_Categories");

                entity.ToTable("categories");

                entity.Property(e => e.Cuid).HasColumnName("CUID");

                entity.Property(e => e.EndPointFlag)
                    .HasColumnName("endPointFlag")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.SubCategoryOfCuid).HasColumnName("subCategoryOfCUID");
            });

            modelBuilder.Entity<CategoryOfProperties>(entity =>
            {
                entity.HasKey(e => e.Puid)
                    .HasName("PK_CategoryOfProperties");

                entity.ToTable("categoryOfProperties");

                entity.Property(e => e.Puid).HasColumnName("PUID");

                entity.Property(e => e.Cuid).HasColumnName("CUID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Tuid).HasColumnName("TUID");

                entity.HasOne(d => d.Cu)
                    .WithMany(p => p.CategoryOfProperties)
                    .HasForeignKey(d => d.Cuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryOfProperties_Categories");

                entity.HasOne(d => d.Tu)
                    .WithMany(p => p.CategoryOfProperties)
                    .HasForeignKey(d => d.Tuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CategoryOfProperties_COPTypes");
            });

            modelBuilder.Entity<CodeType>(entity =>
            {
                entity.HasKey(e => e.Ctuid);

                entity.ToTable("codeType");

                entity.HasIndex(e => e.Type)
                    .HasName("IX_codeType")
                    .IsUnique();

                entity.Property(e => e.Ctuid).HasColumnName("CTUID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(16)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Collaborators>(entity =>
            {
                entity.HasKey(e => new { e.Fuid, e.Uuid });

                entity.ToTable("collaborators");

                entity.Property(e => e.Fuid).HasColumnName("FUID");

                entity.Property(e => e.Uuid).HasColumnName("UUID");

                entity.Property(e => e.Ruid).HasColumnName("RUID");

                entity.HasOne(d => d.Fu)
                    .WithMany(p => p.Collaborators)
                    .HasForeignKey(d => d.Fuid)
                    .HasConstraintName("FK_collaborators_farms");

                entity.HasOne(d => d.Ru)
                    .WithMany(p => p.Collaborators)
                    .HasForeignKey(d => d.Ruid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_collaborators_cRoles");

                entity.HasOne(d => d.Uu)
                    .WithMany(p => p.Collaborators)
                    .HasForeignKey(d => d.Uuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_collaborators_users");
            });

            modelBuilder.Entity<Coptypes>(entity =>
            {
                entity.HasKey(e => e.Tuid);

                entity.ToTable("COPTypes");

                entity.HasIndex(e => e.Name)
                    .HasName("IX_COPTypes")
                    .IsUnique();

                entity.Property(e => e.Tuid).HasColumnName("TUID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Copvalues>(entity =>
            {
                entity.HasKey(e => new { e.Value, e.Puid });

                entity.ToTable("COPValues");

                entity.Property(e => e.Value)
                    .HasColumnName("value")
                    .HasMaxLength(50);

                entity.Property(e => e.Puid).HasColumnName("PUID");

                entity.HasOne(d => d.Pu)
                    .WithMany(p => p.Copvalues)
                    .HasForeignKey(d => d.Puid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_COPValues_COPValues");
            });

            modelBuilder.Entity<EntityCopvalues>(entity =>
            {
                entity.HasKey(e => new { e.Euid, e.Puid })
                    .HasName("PK_EntityCOPValues");

                entity.ToTable("entityCOPValues");

                entity.Property(e => e.Euid)
                    .HasColumnName("EUID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Puid).HasColumnName("PUID");

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnName("value");

                entity.HasOne(d => d.Eu)
                    .WithMany(p => p.EntityCopvalues)
                    .HasForeignKey(d => d.Euid)
                    .HasConstraintName("FK_entityCOPValues_entityOfFP1");

                entity.HasOne(d => d.Pu)
                    .WithMany(p => p.EntityCopvalues)
                    .HasForeignKey(d => d.Puid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EntityCOPValues_CategoryOfProperties1");
            });

            modelBuilder.Entity<EntityDetails>(entity =>
            {
                entity.HasKey(e => e.Duid)
                    .HasName("PK_entityDetail");

                entity.ToTable("entityDetails");

                entity.Property(e => e.Duid)
                    .HasColumnName("DUID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Cost)
                    .HasColumnName("cost")
                    .HasColumnType("money");

                entity.Property(e => e.CreatedByUuid).HasColumnName("createdByUUID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DeletedByUuid).HasColumnName("deletedByUUID");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deletedDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedFlag).HasColumnName("deletedFlag");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Euid).HasColumnName("EUID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.RemainderCompletedByUuid).HasColumnName("remainderCompletedByUUID");

                entity.Property(e => e.RemainderCompletedDate)
                    .HasColumnName("remainderCompletedDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.RemainderCompletedFlag).HasColumnName("remainderCompletedFlag");

                entity.Property(e => e.RemainderDate)
                    .HasColumnName("remainderDate")
                    .HasColumnType("datetime");

                entity.HasOne(d => d.CreatedByUu)
                    .WithMany(p => p.EntityDetailsCreatedByUu)
                    .HasForeignKey(d => d.CreatedByUuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_entityDetail_users1");

                entity.HasOne(d => d.DeletedByUu)
                    .WithMany(p => p.EntityDetailsDeletedByUu)
                    .HasForeignKey(d => d.DeletedByUuid)
                    .HasConstraintName("FK_entityDetail_users");

                entity.HasOne(d => d.Eu)
                    .WithMany(p => p.EntityDetails)
                    .HasForeignKey(d => d.Euid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_entityDetail_EntityOfFP");
            });

            modelBuilder.Entity<EntityOfFp>(entity =>
            {
                entity.HasKey(e => e.Euid)
                    .HasName("PK_EntityOfFP");

                entity.ToTable("entityOfFP");

                entity.Property(e => e.Euid)
                    .HasColumnName("EUID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Cost)
                    .HasColumnName("cost")
                    .HasColumnType("money");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.CreatedByUuid).HasColumnName("createdByUUID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Cuid).HasColumnName("CUID");

                entity.Property(e => e.DeletedByUuid).HasColumnName("deletedByUUID");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deletedDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedFlag).HasColumnName("deletedFlag");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .HasMaxLength(50);

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnName("lastModifiedDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.Puid).HasColumnName("PUID");

                entity.Property(e => e.PurchasedDate)
                    .HasColumnName("purchasedDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.SoldByUuid).HasColumnName("soldByUUID");

                entity.Property(e => e.SoldDate)
                    .HasColumnName("soldDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.SoldDetail).HasColumnName("soldDetail");

                entity.Property(e => e.SoldFlag).HasColumnName("soldFlag");

                entity.Property(e => e.SoldPrice)
                    .HasColumnName("soldPrice")
                    .HasColumnType("money");

                entity.HasOne(d => d.CreatedByUu)
                    .WithMany(p => p.EntityOfFpCreatedByUu)
                    .HasForeignKey(d => d.CreatedByUuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EntityOfFP_users1");

                entity.HasOne(d => d.Cu)
                    .WithMany(p => p.EntityOfFp)
                    .HasForeignKey(d => d.Cuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EntityOfFP_Categories");

                entity.HasOne(d => d.DeletedByUu)
                    .WithMany(p => p.EntityOfFpDeletedByUu)
                    .HasForeignKey(d => d.DeletedByUuid)
                    .HasConstraintName("FK_EntityOfFP_users");

                entity.HasOne(d => d.Pu)
                    .WithMany(p => p.EntityOfFp)
                    .HasForeignKey(d => d.Puid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_EntityOfFP_farmProperties");

                entity.HasOne(d => d.SoldByUu)
                    .WithMany(p => p.EntityOfFpSoldByUu)
                    .HasForeignKey(d => d.SoldByUuid)
                    .HasConstraintName("FK_EntityOfFP_users2");
            });

            modelBuilder.Entity<FarmEntities>(entity =>
            {
                entity.HasKey(e => e.Euid);

                entity.ToTable("farmEntities");

                entity.Property(e => e.Euid)
                    .HasColumnName("EUID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.CreatedByUuid).HasColumnName("createdByUUID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Cuid).HasColumnName("CUID");

                entity.Property(e => e.DeletedByUuid).HasColumnName("deletedByUUID");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deletedDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedFlag).HasColumnName("deletedFlag");

                entity.Property(e => e.Fuid).HasColumnName("FUID");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.HasOne(d => d.Cu)
                    .WithMany(p => p.FarmEntities)
                    .HasForeignKey(d => d.Cuid)
                    .HasConstraintName("FK_farmEntities_categories");

                entity.HasOne(d => d.DeletedByUu)
                    .WithMany(p => p.FarmEntities)
                    .HasForeignKey(d => d.DeletedByUuid)
                    .HasConstraintName("FK_farmEntities_users");

                entity.HasOne(d => d.Fu)
                    .WithMany(p => p.FarmEntities)
                    .HasForeignKey(d => d.Fuid)
                    .HasConstraintName("FK_farmEntities_farms");
            });

            modelBuilder.Entity<FarmProperties>(entity =>
            {
                entity.HasKey(e => e.Puid);

                entity.ToTable("farmProperties");

                entity.Property(e => e.Puid)
                    .HasColumnName("PUID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedByUuid).HasColumnName("createdByUUID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Cuid).HasColumnName("CUID");

                entity.Property(e => e.DeletedByUuid).HasColumnName("deletedByUUID");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deletedDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedFlag).HasColumnName("deletedFlag");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Fuid).HasColumnName("FUID");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnName("lastModifiedDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.HasOne(d => d.CreatedByUu)
                    .WithMany(p => p.FarmPropertiesCreatedByUu)
                    .HasForeignKey(d => d.CreatedByUuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_farmProperties_users1");

                entity.HasOne(d => d.Cu)
                    .WithMany(p => p.FarmProperties)
                    .HasForeignKey(d => d.Cuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_farmProperties_categories");

                entity.HasOne(d => d.DeletedByUu)
                    .WithMany(p => p.FarmPropertiesDeletedByUu)
                    .HasForeignKey(d => d.DeletedByUuid)
                    .HasConstraintName("FK_farmProperties_users");

                entity.HasOne(d => d.Fu)
                    .WithMany(p => p.FarmProperties)
                    .HasForeignKey(d => d.Fuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_farmProperties_farms");
            });

            modelBuilder.Entity<Farms>(entity =>
            {
                entity.HasKey(e => e.Fuid);

                entity.ToTable("farms");

                entity.Property(e => e.Fuid)
                    .HasColumnName("FUID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.CreatedByUuid).HasColumnName("createdByUUID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.DeletedByUuid).HasColumnName("deletedByUUID");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deletedDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedFlag).HasColumnName("deletedFlag");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnName("lastModifiedDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Location)
                    .HasColumnName("location")
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.HasOne(d => d.CreatedByUu)
                    .WithMany(p => p.FarmsCreatedByUu)
                    .HasForeignKey(d => d.CreatedByUuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_farms_users");

                entity.HasOne(d => d.DeletedByUu)
                    .WithMany(p => p.FarmsDeletedByUu)
                    .HasForeignKey(d => d.DeletedByUuid)
                    .HasConstraintName("FK_farms_users1");
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
                    .IsRequired()
                    .HasColumnName("isValid")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Ctu)
                    .WithMany(p => p.GeneratedUcodes)
                    .HasForeignKey(d => d.Ctuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_generatedUCodes_codeType");

                entity.HasOne(d => d.ForUu)
                    .WithMany(p => p.GeneratedUcodes)
                    .HasForeignKey(d => d.ForUuid)
                    .HasConstraintName("FK_generatedUCodes_users");
            });

            modelBuilder.Entity<IncomeAndExpeneses>(entity =>
            {
                entity.HasKey(e => e.Ieuid);

                entity.ToTable("incomeAndExpeneses");

                entity.Property(e => e.Ieuid)
                    .HasColumnName("IEUID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Cost)
                    .HasColumnName("cost")
                    .HasColumnType("money");

                entity.Property(e => e.CreatedByUuid).HasColumnName("createdByUUID");

                entity.Property(e => e.CreatedDate)
                    .HasColumnName("createdDate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedByUuid).HasColumnName("deletedByUUID");

                entity.Property(e => e.DeletedDate)
                    .HasColumnName("deletedDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.DeletedFlag).HasColumnName("deletedFlag");

                entity.Property(e => e.Description).HasColumnName("description");

                entity.Property(e => e.Fuid).HasColumnName("FUID");

                entity.Property(e => e.Head)
                    .IsRequired()
                    .HasColumnName("head")
                    .HasMaxLength(50);

                entity.Property(e => e.IncomeFlag).HasColumnName("incomeFlag");

                entity.HasOne(d => d.DeletedByUu)
                    .WithMany(p => p.IncomeAndExpeneses)
                    .HasForeignKey(d => d.DeletedByUuid)
                    .HasConstraintName("FK_incomeAndExpeneses_users");

                entity.HasOne(d => d.Fu)
                    .WithMany(p => p.IncomeAndExpeneses)
                    .HasForeignKey(d => d.Fuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_incomeAndExpeneses_farms");
            });

            modelBuilder.Entity<MemberType>(entity =>
            {
                entity.HasKey(e => e.Mtuid);

                entity.ToTable("memberType");

                entity.Property(e => e.Mtuid).HasColumnName("MTUID");

                entity.Property(e => e.CollaboratorLimit).HasColumnName("collaboratorLimit");

                entity.Property(e => e.EntityLimit).HasColumnName("entityLimit");

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

            modelBuilder.Entity<Pictures>(entity =>
            {
                entity.HasKey(e => e.Puid);

                entity.Property(e => e.Puid)
                    .HasColumnName("PUID")
                    .HasDefaultValueSql("(newsequentialid())");

                entity.Property(e => e.Address).HasColumnName("address");

                entity.Property(e => e.Auid).HasColumnName("AUID");

                entity.HasOne(d => d.Au)
                    .WithMany(p => p.Pictures)
                    .HasForeignKey(d => d.Auid)
                    .HasConstraintName("FK_Pictures_Adds");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.Ruid);

                entity.ToTable("roles");

                entity.Property(e => e.Ruid).HasColumnName("RUID");

                entity.Property(e => e.CanEnterAdminDashboard).HasColumnName("can_enter_admin_dashboard");

                entity.Property(e => e.CreatedByUuid).HasColumnName("createdByUUID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SysRoleFlag).HasColumnName("sysRoleFlag");

                entity.HasOne(d => d.CreatedByUu)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.CreatedByUuid)
                    .HasConstraintName("FK_roles_users");
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
                    .IsRequired()
                    .HasColumnName("isValid")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LastUsedDate)
                    .HasColumnName("lastUsedDate")
                    .HasColumnType("datetime");

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
                    .HasMaxLength(24)
                    .IsUnicode(false);

                entity.Property(e => e.AttemptedResult).HasColumnName("attemptedResult");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasMaxLength(50);

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasMaxLength(50);

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

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

                entity.Property(e => e.EmailActivated)
                    .HasColumnName("emailActivated")
                    .HasDefaultValueSql("((0))");

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

                entity.Property(e => e.PhoneNumberActivated)
                    .HasColumnName("phoneNumberActivated")
                    .HasDefaultValueSql("((0))");

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

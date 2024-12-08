using Common.Commons;
using Microsoft.EntityFrameworkCore;
using Repository.EF;
using Repository.Model;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.BCC01_EF
{
    public partial class BCC01_DbContextSql : DbContext
    {
        public BCC01_DbContextSql()
        {
        }
        public BCC01_DbContextSql(string connectString)
        {
        }
        public BCC01_DbContextSql(DbContextOptions<BCC01_DbContextSql> options)
            : base(options)
        {
        }


        public virtual DbSet<BCC01_MapProfileUser> BCC01_MapProfileUser { get; set; }
        public virtual DbSet<BCC01_Module> BCC01_Module { get; set; }
        public virtual DbSet<BCC01_Permission> BCC01_Permission { get; set; }
        public virtual DbSet<BCC01_PermissionObject> BCC01_PermissionObject { get; set; }
        public virtual DbSet<BCC01_Profile> BCC01_Profile { get; set; }
        public virtual DbSet<BCC01_RoleHierarchy> BCC01_RoleHierarchy { get; set; }
        public virtual DbSet<BCC01_RegisterInformation> BCC01_RegisterInformation { get; set; }
        public virtual DbSet<BCC01_DefaultModule> BCC01_DefaultModule { get; set; }
        public virtual DbSet<BCC01_Tenants> BCC01_Tenants { get; set; }
        public virtual DbSet<BCC01_User> BCC01_User { get; set; }
        public virtual DbSet<BCC01_CallFlowComponent> BCC01_CallFlowComponent { get; set; }
        public virtual DbSet<BCC01_Queue> BCC01_Queue { get; set; }
        public virtual DbSet<BCC01_MapAgentGroup> BCC01_MapAgentGroup { get; set; }
        public virtual DbSet<BCC01_DefaultPermissionObject> BCC01_DefaultPermissionObject { get; set; }
        public virtual DbSet<BCC01_DefaultCommonSetting> BCC01_DefaultCommonSetting { get; set; }
        public virtual DbSet<BCC01_CommonSetting> BCC01_CommonSetting { get; set; }
        public virtual DbSet<BCC01_MapAgentSkill> BCC01_MapAgentSkill { get; set; }
        public virtual DbSet<BCC01_ForgotPassword> BCC01_ForgotPassword { get; set; }
        public virtual DbSet<BCC01_TenantExtension> BCC01_TenantExtension { get; set; }
        public virtual DbSet<BCC01_Teams> BCC01_Teams { get; set; }
        public virtual DbSet<BCC01_MapTeamUser> BCC01_MapTeamUser { get; set; }
        public virtual DbSet<BCC01_MapUserReportTo> BCC01_MapUserReportTo { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(ConfigHelper.Get("ConnectionStrings", "BCC01_Connection"));
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BCC01_MapProfileUser>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.create_by)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.description).HasMaxLength(150);

                entity.Property(e => e.modify_by)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.username)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.BCC01_Profile)
                    .WithMany(p => p.BCC01_MapProfileUser)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BCC01_Map__profi__4BAC3F29");

                entity.HasOne(d => d.BCC01_User)
                    .WithMany(p => p.BCC01_MapProfileUser)
                    .HasForeignKey(d => d.username)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BCC01_Map__usern__4CA06362");
            });

            modelBuilder.Entity<BCC01_Module>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.create_by)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.description).HasMaxLength(250);

                entity.Property(e => e.display_name).HasMaxLength(150);

                entity.Property(e => e.modify_by)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.module_name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<BCC01_Permission>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.create_by)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.description).HasMaxLength(150);

                entity.Property(e => e.modify_by)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.object_name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.HasOne(d => d.BCC01_PermissionObject)
                    .WithMany(p => p.BCC01_Permission)
                    .HasForeignKey(d => d.permissionobject_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BCC01_Per__permi__534D60F1");

                entity.HasOne(d => d.BCC01_Profile)
                    .WithMany(p => p.BCC01_Permission)
                    .HasForeignKey(d => d.profile_id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BCC01_Per__profi__5441852A");
            });

            modelBuilder.Entity<BCC01_PermissionObject>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.create_by)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.description).HasMaxLength(250);

                entity.Property(e => e.modify_by)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.object_name)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            modelBuilder.Entity<BCC01_Profile>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.create_by)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.description).HasMaxLength(150);

                entity.Property(e => e.modify_by)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.profile_name)
                    .IsRequired()
                    .HasMaxLength(150);
            });
            modelBuilder.Entity<BCC01_RegisterInformation>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.address)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.business_type_id)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_by)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.customer_type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.email)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.expire_time).HasColumnType("datetime");

                entity.Property(e => e.modify_by)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.password)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.phone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.province_id)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.register_key)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.tenant_name)
                    .IsRequired()
                    .HasMaxLength(250);
            });
            modelBuilder.Entity<BCC01_RoleHierarchy>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.create_by)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.description).HasMaxLength(150);

                entity.Property(e => e.modify_by)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.role_name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<BCC01_Tenants>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.address)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.business_type_id)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.create_by)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.customer_type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.email)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.expire_time).HasColumnType("datetime");

                entity.Property(e => e.modify_by)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.phone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.province_id)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.tenant_name)
                    .IsRequired()
                    .HasMaxLength(250)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<BCC01_User>(entity =>
            {
                entity.HasKey(e => e.username)
                    .HasName("PK__BCC01_Us__F3DBC573801BAA9C");

                entity.Property(e => e.username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.avatar).IsUnicode(false);

                entity.Property(e => e.block_time).HasColumnType("datetime");

                entity.Property(e => e.create_by)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.description).HasMaxLength(150);

                entity.Property(e => e.email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.extension_number)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.fullname)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.language)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_by)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.password)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.phone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.reason_deactive).IsUnicode(false);

                entity.Property(e => e.report_to)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

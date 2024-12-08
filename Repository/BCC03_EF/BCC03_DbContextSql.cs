using Common.Commons;
using Microsoft.EntityFrameworkCore;
using Repository.EF;
using Repository.Model;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.BCC03_EF
{
    public partial class BCC03_DbContextSql : DbContext
    {
        public BCC03_DbContextSql()
        {
        }
        public BCC03_DbContextSql(string connectString)
        {
        }
        public BCC03_DbContextSql(DbContextOptions<BCC03_DbContextSql> options)
            : base(options)
        {
        }


        public virtual DbSet<BCC03_RecordingSplitFile> BCC03_RecordingSplitFile { get; set; }
        public virtual DbSet<BCC03_RecordingFile> BCC03_RecordingFile { get; set; }
        public virtual DbSet<BCC03_CallLog> BCC03_CallLog { get; set; }
        public virtual DbSet<BCC03_CallTransferLog> BCC03_CallTransferLog { get; set; }
        public virtual DbSet<BCC03_EventCall> BCC03_EventCall { get; set; }
       
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        {
            if (!optionsBuilder.IsConfigured)
            {
                #warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(ConfigHelper.Get("ConnectionStrings", "BCC03_Connection"));
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

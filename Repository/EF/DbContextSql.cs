using System;
using Common.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Repository.EF;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Repository.Model
{
    public partial class DbContextSql : DbContext
    {
        public DbContextSql()
        {
        }

        public DbContextSql(DbContextOptions<DbContextSql> options)
            : base(options)
        {
        }

        public virtual DbSet<BCC01_CallBackConfig> BCC01_CallBackConfig { get; set; }
        public virtual DbSet<BCC02_AgentStateLog> BCC02_AgentStateLog { get; set; }
        public virtual DbSet<BCC02_CallBackList> BCC02_CallBackList { get; set; }
        public virtual DbSet<BCC02_CallControlLog> BCC02_CallControlLog { get; set; }
        public virtual DbSet<BCC02_CallEventLog> BCC02_CallEventLog { get; set; }
        public virtual DbSet<BCC02_CallLog> BCC02_CallLog { get; set; }
        public virtual DbSet<BCC02_CallTagLog> BCC02_CallTagLog { get; set; }
        public virtual DbSet<BCC02_Customer> BCC02_Customer { get; set; }
        public virtual DbSet<BCC02_CustomerExtension> BCC02_CustomerExtension { get; set; }
        public virtual DbSet<BCC02_CustomerNote> BCC02_CustomerNote { get; set; }
        public virtual DbSet<BCC02_MapQueueAgent> BCC02_MapQueueAgent { get; set; }
        public virtual DbSet<BCC02_Organization> BCC02_Organization { get; set; }
        public virtual DbSet<BCC02_Queue> BCC02_Queue { get; set; }
        public virtual DbSet<BCC02_QueueHandleByAgent> BCC02_QueueHandleByAgent { get; set; }
        public virtual DbSet<BCC02_RecordingLog> BCC02_RecordingLog { get; set; }
        public virtual DbSet<BCC02_Ticket> BCC02_Ticket { get; set; }
        public virtual DbSet<BCC02_TicketExtension> BCC02_TicketExtension { get; set; }
        public virtual DbSet<BCC02_TicketNote> BCC02_TicketNote { get; set; }
        public virtual DbSet<BCC02_VoucherNoSetting> BCC02_VoucherNoSetting { get; set; }
        public virtual DbSet<ZCheckCallLog> ZCheckCallLog { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(ConfigHelper.Get("ConnectionStrings", "DefaultConnection"), providerOptions => providerOptions.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BCC01_CallBackConfig>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.condition_endtime).HasColumnType("datetime");

                entity.Property(e => e.condition_starttime).HasColumnType("datetime");

                entity.Property(e => e.condition_time_frame)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.description).HasMaxLength(250);

                entity.Property(e => e.distribution_type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");
            });

            modelBuilder.Entity<BCC02_AgentStateLog>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.extension_number)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.set_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.state_key)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.state_name).HasMaxLength(100);
            });

            modelBuilder.Entity<BCC02_CallBackList>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.assign_to)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.contact_name).HasMaxLength(250);

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.last_calltime).HasColumnType("datetime");

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.note).HasMaxLength(250);

                entity.Property(e => e.phone_number)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BCC02_CallControlLog>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.control_type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.ext_reference)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.unique_id)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BCC02_CallEventLog>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.call_status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.context)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.data).HasMaxLength(1000);

                entity.Property(e => e.event_name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.linked_id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.unique_id)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BCC02_CallLog>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.bridge_unique_id)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.call_direct)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.call_status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.channel)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                //entity.Property(e => e.channel_webrtc)
                //    .HasMaxLength(100)
                //    .IsUnicode(false);

                entity.Property(e => e.connect_time).HasColumnType("datetime");

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.end_time).HasColumnType("datetime");

                entity.Property(e => e.extension_number)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.joinqueue_time).HasColumnType("datetime");

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.phone_number)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.recording_url_file)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ringing_time).HasColumnType("datetime");

                entity.Property(e => e.sip_call_id)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.start_time).HasColumnType("datetime");

                //entity.Property(e => e.step_update).HasMaxLength(100);

                entity.Property(e => e.terminate_code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.transfer_unique_id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.unique_id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.unique_id_origin).HasMaxLength(50);
                entity.Property(e => e.unique_id_asterisk).HasMaxLength(20).IsUnicode(false);

                entity.HasOne(d => d.customer_)
                    .WithMany(p => p.BCC02_CallLog)
                    .HasForeignKey(d => d.customer_id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_BCC02_CallHistoryLog_BCC02_CustomerInformation");
            });

            modelBuilder.Entity<BCC02_CallTagLog>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.unique_id)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BCC02_Customer>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.address).HasMaxLength(350);

                entity.Property(e => e.avatar).IsUnicode(false);

                entity.Property(e => e.birth_date).HasColumnType("datetime");

                entity.Property(e => e.block_from).HasColumnType("datetime");

                entity.Property(e => e.block_to).HasColumnType("datetime");

                entity.Property(e => e.channel_type)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.citizen_id)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.customer_name).HasMaxLength(150);

                entity.Property(e => e.email)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.phone)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BCC02_CustomerExtension>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.field_id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.value).HasMaxLength(350);

                entity.HasOne(d => d.customer_)
                    .WithMany(p => p.BCC02_CustomerExtension)
                    .HasForeignKey(d => d.customer_id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_BCC02_CustomerInformationExtension_BCC02_CustomerInformation");
            });

            modelBuilder.Entity<BCC02_CustomerNote>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.note_content).HasMaxLength(1000);

                entity.Property(e => e.note_source)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.note_type)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.customer_)
                    .WithMany(p => p.BCC02_CustomerNote)
                    .HasForeignKey(d => d.customer_id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_BCC02_CustomerInformationNote_BCC02_CustomerInformation");
            });

            modelBuilder.Entity<BCC02_MapQueueAgent>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.extension_number)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.state)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BCC02_Organization>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.address).HasMaxLength(350);

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.description).HasMaxLength(350);

                entity.Property(e => e.email)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.organization_name).HasMaxLength(350);

                entity.Property(e => e.phone)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BCC02_Queue>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.agent_order)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_by)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.description).HasMaxLength(250);

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.queue_name)
                    .IsRequired()
                    .HasMaxLength(250);
            });

            modelBuilder.Entity<BCC02_QueueHandleByAgent>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.username)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

           

            modelBuilder.Entity<BCC02_Ticket>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.assign_to)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.channel_type)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.priority)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.status)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.ticket_no)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.title).HasMaxLength(150);

                entity.Property(e => e.unique_id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.customer_)
                    .WithMany(p => p.BCC02_Ticket)
                    .HasForeignKey(d => d.customer_id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_BCC02_Ticket_BCC02_CustomerInformation");
            });

            modelBuilder.Entity<BCC02_TicketExtension>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.field_id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.value).HasMaxLength(350);

                entity.HasOne(d => d.ticket_)
                    .WithMany(p => p.BCC02_TicketExtension)
                    .HasForeignKey(d => d.ticket_id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_BCC02_TicketExtension_BCC02_Ticket");
            });

            modelBuilder.Entity<BCC02_TicketNote>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.location_lat)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.location_long)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.note_content).HasMaxLength(1000);

                entity.Property(e => e.note_source)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.note_type)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.HasOne(d => d.ticket_)
                    .WithMany(p => p.BCC02_TicketNote)
                    .HasForeignKey(d => d.ticket_id)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_BCC02_TicketNote_BCC02_Ticket");
            });

            modelBuilder.Entity<BCC02_VoucherNoSetting>(entity =>
            {
                entity.Property(e => e.id).ValueGeneratedNever();

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.entity_type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.field_name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.prefix)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ZCheckCallLog>(entity =>
            {
                entity.HasNoKey();

                entity.Property(e => e.bridge_unique_id)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.call_direct)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.call_status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.channel)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.connect_time).HasColumnType("datetime");

                entity.Property(e => e.create_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.create_time).HasColumnType("datetime");

                entity.Property(e => e.end_time).HasColumnType("datetime");

                entity.Property(e => e.extension_number)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.joinqueue_time).HasColumnType("datetime");

                entity.Property(e => e.modify_by)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.modify_time).HasColumnType("datetime");

                entity.Property(e => e.phone_number)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.recording_url_file)
                    .HasMaxLength(500)
                    .IsUnicode(false);

                entity.Property(e => e.ringing_time).HasColumnType("datetime");

                entity.Property(e => e.sip_call_id)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.start_time).HasColumnType("datetime");

                entity.Property(e => e.step_update).HasMaxLength(100);

                entity.Property(e => e.terminate_code)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.transfer_unique_id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.unique_id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.unique_id_origin).HasMaxLength(50);

                entity.Property(e => e.write_time)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace zaloclone_test.Models
{
    public partial class Zalo_CloneContext : DbContext
    {
        public Zalo_CloneContext()
        {
        }

        public Zalo_CloneContext(DbContextOptions<Zalo_CloneContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Friend> Friends { get; set; } = null!;
        public virtual DbSet<GroupMember> GroupMembers { get; set; } = null!;
        public virtual DbSet<Groupchat> Groupchats { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<MessageReaction> MessageReactions { get; set; } = null!;
        public virtual DbSet<MessageStatus> MessageStatuses { get; set; } = null!;
        public virtual DbSet<SearchHistory> SearchHistories { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=hoancute;database=Zalo_Clone; TrustServerCertificate=true;Integrated Security = true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Friend>(entity =>
            {
                entity.HasKey(e => e.Friend1)
                    .HasName("PK__Friends__DEE0643E2A69B3A0");

                entity.Property(e => e.Friend1)
                    .HasMaxLength(36)
                    .HasColumnName("Friend");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUser).HasMaxLength(36);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.Property(e => e.UpdateUser).HasMaxLength(36);

                entity.Property(e => e.UserId1)
                    .HasMaxLength(36)
                    .HasColumnName("UserID_1");

                entity.Property(e => e.UserId2)
                    .HasMaxLength(36)
                    .HasColumnName("UserID_2");

                entity.HasOne(d => d.UserId1Navigation)
                    .WithMany(p => p.FriendUserId1Navigations)
                    .HasForeignKey(d => d.UserId1)
                    .HasConstraintName("FK__Friends__UserID___36B12243");

                entity.HasOne(d => d.UserId2Navigation)
                    .WithMany(p => p.FriendUserId2Navigations)
                    .HasForeignKey(d => d.UserId2)
                    .HasConstraintName("FK__Friends__UserID___37A5467C");
            });

            modelBuilder.Entity<GroupMember>(entity =>
            {
                entity.ToTable("GroupMember");

                entity.Property(e => e.GroupMemberId)
                    .HasMaxLength(36)
                    .HasColumnName("GroupMemberID");

                entity.Property(e => e.GroupId)
                    .HasMaxLength(36)
                    .HasColumnName("GroupID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupMembers)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__GroupMemb__Group__3D5E1FD2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.GroupMembers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__GroupMemb__UserI__3E52440B");
            });

            modelBuilder.Entity<Groupchat>(entity =>
            {
                entity.HasKey(e => e.GroupId)
                    .HasName("PK__Groupcha__149AF30AD541D9E1");

                entity.ToTable("Groupchat");

                entity.Property(e => e.GroupId)
                    .HasMaxLength(36)
                    .HasColumnName("GroupID");

                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUser).HasMaxLength(36);

                entity.Property(e => e.GroupName).HasMaxLength(50);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.Property(e => e.UpdateUser).HasMaxLength(36);
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.MessageId)
                    .HasMaxLength(36)
                    .HasColumnName("MessageID");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RecievedId)
                    .HasMaxLength(36)
                    .HasColumnName("RecievedID");

                entity.Property(e => e.SenderId)
                    .HasMaxLength(36)
                    .HasColumnName("SenderID");

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.Recieved)
                    .WithMany(p => p.MessageRecieveds)
                    .HasForeignKey(d => d.RecievedId)
                    .HasConstraintName("FK__Message__Recieve__2A4B4B5E");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.MessageSenders)
                    .HasForeignKey(d => d.SenderId)
                    .HasConstraintName("FK__Message__SenderI__29572725");
            });

            modelBuilder.Entity<MessageReaction>(entity =>
            {
                entity.HasKey(e => new { e.MessageId, e.UserId })
                    .HasName("PK__MessageR__19048FB6A7957CA4");

                entity.ToTable("MessageReaction");

                entity.Property(e => e.MessageId)
                    .HasMaxLength(36)
                    .HasColumnName("MessageID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .HasColumnName("UserID");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Reaction).HasMaxLength(255);

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.MessageReactions)
                    .HasForeignKey(d => d.MessageId)
                    .HasConstraintName("FK__MessageRe__Messa__2E1BDC42");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MessageReactions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MessageRe__UserI__2F10007B");
            });

            modelBuilder.Entity<MessageStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK__MessageS__C8EE204381E06A29");

                entity.ToTable("MessageStatus");

                entity.Property(e => e.StatusId)
                    .HasMaxLength(36)
                    .HasColumnName("StatusID");

                entity.Property(e => e.MessageId)
                    .HasMaxLength(36)
                    .HasColumnName("MessageID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.MessageStatuses)
                    .HasForeignKey(d => d.MessageId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__MessageSt__Messa__31EC6D26");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MessageStatuses)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__MessageSt__UserI__32E0915F");
            });

            modelBuilder.Entity<SearchHistory>(entity =>
            {
                entity.HasKey(e => e.SearchId)
                    .HasName("PK__SearchHi__21C53514246E21A2");

                entity.ToTable("SearchHistory");

                entity.Property(e => e.SearchId)
                    .HasMaxLength(36)
                    .HasColumnName("SearchID");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.SearchedUserId)
                    .HasMaxLength(36)
                    .HasColumnName("SearchedUserID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.SearchedUser)
                    .WithMany(p => p.SearchHistorySearchedUsers)
                    .HasForeignKey(d => d.SearchedUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SearchHis__Searc__4316F928");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SearchHistoryUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__SearchHis__UserI__4222D4EF");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .HasColumnName("UserID");

                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.Bio).HasMaxLength(40);

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUser).HasMaxLength(36);

                entity.Property(e => e.Dob).HasColumnType("datetime");

                entity.Property(e => e.Email).HasMaxLength(255);

                entity.Property(e => e.IsVerified).HasDefaultValueSql("((0))");

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.Phone).HasMaxLength(10);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.UpdateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UpdateUser).HasMaxLength(36);

                entity.Property(e => e.Username).HasMaxLength(255);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

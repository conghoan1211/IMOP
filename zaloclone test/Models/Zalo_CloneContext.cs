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

        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<CommentImage> CommentImages { get; set; } = null!;
        public virtual DbSet<CommentLike> CommentLikes { get; set; } = null!;
        public virtual DbSet<Conversation> Conversations { get; set; } = null!;
        public virtual DbSet<ConversationParticipant> ConversationParticipants { get; set; } = null!;
        public virtual DbSet<Friend> Friends { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<MessageBlock> MessageBlocks { get; set; } = null!;
        public virtual DbSet<MessageReaction> MessageReactions { get; set; } = null!;
        public virtual DbSet<MessageStatus> MessageStatuses { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<PostImage> PostImages { get; set; } = null!;
        public virtual DbSet<PostLike> PostLikes { get; set; } = null!;
        public virtual DbSet<Reaction> Reactions { get; set; } = null!;
        public virtual DbSet<SearchHistory> SearchHistories { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=ANEMOS\\SQLEXPRESS;database=Zalo_Clone;uid=sa;pwd=sa;Trusted_Connection=true");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.Property(e => e.CommentId)
                    .HasMaxLength(36)
                    .HasColumnName("CommentID");

                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsHide).HasDefaultValueSql("((0))");

                entity.Property(e => e.IsPinTop).HasDefaultValueSql("((0))");

                entity.Property(e => e.Likes).HasDefaultValueSql("((0))");

                entity.Property(e => e.PostId)
                    .HasMaxLength(36)
                    .HasColumnName("PostID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.CommentsNavigation)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comments__PostID__06CD04F7");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Comments__UserID__07C12930");
            });

            modelBuilder.Entity<CommentImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK__CommentI__7516F4ECEE816050");

                entity.Property(e => e.ImageId)
                    .HasMaxLength(36)
                    .HasColumnName("ImageID");

                entity.Property(e => e.CommentId)
                    .HasMaxLength(36)
                    .HasColumnName("CommentID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.CommentImages)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK__CommentIm__Comme__0B91BA14");
            });

            modelBuilder.Entity<CommentLike>(entity =>
            {
                entity.HasKey(e => e.LikeId)
                    .HasName("PK__CommentL__A2922CF4B3C17E5D");

                entity.Property(e => e.LikeId)
                    .HasMaxLength(36)
                    .HasColumnName("LikeID");

                entity.Property(e => e.CommentId)
                    .HasMaxLength(36)
                    .HasColumnName("CommentID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.CommentLikes)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CommentLi__Comme__151B244E");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.CommentLikes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CommentLi__UserI__14270015");
            });

            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.ToTable("Conversation");

                entity.Property(e => e.ConversationId)
                    .HasMaxLength(36)
                    .HasColumnName("ConversationID");

                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.ConversationName).HasMaxLength(50);

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreateUser).HasMaxLength(36);

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.Property(e => e.UpdateUser).HasMaxLength(36);
            });

            modelBuilder.Entity<ConversationParticipant>(entity =>
            {
                entity.HasKey(e => new { e.ConversationId, e.UserId })
                    .HasName("PK__Conversa__1128545D4156472A");

                entity.ToTable("ConversationParticipant");

                entity.Property(e => e.ConversationId)
                    .HasMaxLength(36)
                    .HasColumnName("ConversationID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .HasColumnName("UserID");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Conversation)
                    .WithMany(p => p.ConversationParticipants)
                    .HasForeignKey(d => d.ConversationId)
                    .HasConstraintName("FK__Conversat__Conve__5629CD9C");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ConversationParticipants)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Conversat__UserI__571DF1D5");
            });

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.HasKey(e => e.Friend1)
                    .HasName("PK__Friends__DEE0643E2C9692FE");

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
                    .HasConstraintName("FK__Friends__UserID___4F7CD00D");

                entity.HasOne(d => d.UserId2Navigation)
                    .WithMany(p => p.FriendUserId2Navigations)
                    .HasForeignKey(d => d.UserId2)
                    .HasConstraintName("FK__Friends__UserID___5070F446");
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

                entity.Property(e => e.MessageBlockId)
                    .HasMaxLength(36)
                    .HasColumnName("MessageBlockID");

                entity.Property(e => e.SenderId)
                    .HasMaxLength(36)
                    .HasColumnName("SenderID");

                entity.Property(e => e.UpdateAt).HasColumnType("datetime");

                entity.HasOne(d => d.MessageBlock)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.MessageBlockId)
                    .HasConstraintName("FK__Message__Message__5FB337D6");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Message__SenderI__5EBF139D");
            });

            modelBuilder.Entity<MessageBlock>(entity =>
            {
                entity.ToTable("MessageBlock");

                entity.Property(e => e.MessageBlockId)
                    .HasMaxLength(36)
                    .HasColumnName("MessageBlockID");

                entity.Property(e => e.ConversationId)
                    .HasMaxLength(36)
                    .HasColumnName("ConversationID");

                entity.Property(e => e.FirstSendDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Conversation)
                    .WithMany(p => p.MessageBlocks)
                    .HasForeignKey(d => d.ConversationId)
                    .HasConstraintName("FK__MessageBl__Conve__5AEE82B9");
            });

            modelBuilder.Entity<MessageReaction>(entity =>
            {
                entity.HasKey(e => new { e.MessageId, e.UserId })
                    .HasName("PK__MessageR__19048FB6B8BBEA69");

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

                entity.Property(e => e.ReactionId).HasColumnName("ReactionID");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.MessageReactions)
                    .HasForeignKey(d => d.MessageId)
                    .HasConstraintName("FK__MessageRe__Messa__693CA210");

                entity.HasOne(d => d.Reaction)
                    .WithMany(p => p.MessageReactions)
                    .HasForeignKey(d => d.ReactionId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__MessageRe__React__6B24EA82");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MessageReactions)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__MessageRe__UserI__6A30C649");
            });

            modelBuilder.Entity<MessageStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId)
                    .HasName("PK__MessageS__C8EE204351EFCB9A");

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
                    .HasConstraintName("FK__MessageSt__Messa__628FA481");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.MessageStatuses)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__MessageSt__UserI__6383C8BA");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.PostId)
                    .HasMaxLength(36)
                    .HasColumnName("PostID");

                entity.Property(e => e.Comments).HasDefaultValueSql("((0))");

                entity.Property(e => e.Content).HasColumnType("text");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IsComment).HasDefaultValueSql("((0))");

                entity.Property(e => e.Likes).HasDefaultValueSql("((0))");

                entity.Property(e => e.PinTop).HasDefaultValueSql("((0))");

                entity.Property(e => e.Privacy).HasDefaultValueSql("((1))");

                entity.Property(e => e.Shares).HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .HasColumnName("UserID");

                entity.Property(e => e.VideoUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Views).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Posts__UserID__7C4F7684");
            });

            modelBuilder.Entity<PostImage>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                    .HasName("PK__PostImag__7516F4ECE9B6D0AE");

                entity.Property(e => e.ImageId)
                    .HasMaxLength(36)
                    .HasColumnName("ImageID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ImageUrl)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PostId)
                    .HasMaxLength(36)
                    .HasColumnName("PostID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostImages)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK__PostImage__PostI__00200768");
            });

            modelBuilder.Entity<PostLike>(entity =>
            {
                entity.HasKey(e => e.LikeId)
                    .HasName("PK__PostLike__A2922CF488D1B306");

                entity.Property(e => e.LikeId)
                    .HasMaxLength(36)
                    .HasColumnName("LikeID");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PostId)
                    .HasMaxLength(36)
                    .HasColumnName("PostID");

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostLikes)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostLikes__PostI__10566F31");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.PostLikes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PostLikes__UserI__0F624AF8");
            });

            modelBuilder.Entity<Reaction>(entity =>
            {
                entity.ToTable("Reaction");

                entity.Property(e => e.ReactionId)
                    .ValueGeneratedNever()
                    .HasColumnName("ReactionID");

                entity.Property(e => e.ReactionName).HasMaxLength(36);
            });

            modelBuilder.Entity<SearchHistory>(entity =>
            {
                entity.HasKey(e => e.SearchId)
                    .HasName("PK__SearchHi__21C53514622D5138");

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
                    .HasConstraintName("FK__SearchHis__Searc__70DDC3D8");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SearchHistoryUsers)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__SearchHis__UserI__6FE99F9F");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId)
                    .HasMaxLength(36)
                    .HasColumnName("UserID");

                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.Bio).HasMaxLength(150);

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

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Backend.Models
{
    public partial class NorvoDBContext : DbContext
    {
        public NorvoDBContext()
        {
        }

        public NorvoDBContext(DbContextOptions<NorvoDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<Lyric> Lyrics { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }
        public virtual DbSet<Song> Songs { get; set; }
        public virtual DbSet<UsersTable> UsersTables { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>(entity =>
            {
                entity.Property(e => e.ArtistId).HasColumnName("ArtistID");

                entity.Property(e => e.ArtistName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ArtistSongs).HasMaxLength(50);
            });

            modelBuilder.Entity<Lyric>(entity =>
            {
                entity.HasKey(e => e.LyricsId)
                    .HasName("PK__Lyrics__F71D1C06A8D92072");

                entity.Property(e => e.LyricsId).HasColumnName("LyricsID");

                entity.Property(e => e.Lyrics).IsUnicode(false);

                entity.HasOne(d => d.SongNavigation)
                    .WithMany(p => p.Lyrics)
                    .HasForeignKey(d => d.Song)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Lyrics__Song__1332DBDC");
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasKey(e => e.TokenId)
                    .HasName("PK__RefreshT__AC16DAA7BCC2345A");

                entity.ToTable("RefreshToken");

                entity.Property(e => e.TokenId).HasColumnName("tokenID");

                entity.Property(e => e.ExpiryDate)
                    .HasColumnName("expiryDate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Token)
                    .IsRequired()
                    .HasColumnName("token")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.UserId).HasColumnName("userId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.RefreshTokens)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__RefreshTo__userI__160F4887");
            });

            modelBuilder.Entity<Song>(entity =>
            {
                entity.Property(e => e.SongId).HasColumnName("SongID");

                entity.Property(e => e.SongName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.AuthorNavigation)
                    .WithMany(p => p.Songs)
                    .HasForeignKey(d => d.Author)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Songs__Author__0D7A0286");
            });

            modelBuilder.Entity<UsersTable>(entity =>
            {
                entity.ToTable("UsersTable");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

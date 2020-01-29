using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FutbolAPI.Business.Models
{
    public partial class FutbolAPIContext : DbContext
    {
        public FutbolAPIContext()
        {
        }

        public FutbolAPIContext(DbContextOptions<FutbolAPIContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Manager> Manager { get; set; }
        public virtual DbSet<Match> Match { get; set; }
        public virtual DbSet<MatchPlayerAway> MatchPlayerAway { get; set; }
        public virtual DbSet<MatchPlayerHome> MatchPlayerHome { get; set; }
        public virtual DbSet<Player> Player { get; set; }
        public virtual DbSet<Referee> Referee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.\\SQLEXPRESS;Initial Catalog=FutbolAPI;User Id=sa;Password=mypassword;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Manager>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.RedCards).HasColumnName("redCards");

                entity.Property(e => e.TeamName)
                    .IsRequired()
                    .HasColumnName("teamName")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.YellowCards).HasColumnName("yellowCards");
            });

            modelBuilder.Entity<Match>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdAwayManager).HasColumnName("idAwayManager");

                entity.Property(e => e.IdHomeManager).HasColumnName("idHomeManager");

                entity.Property(e => e.Idreferee).HasColumnName("idreferee");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdAwayManagerNavigation)
                    .WithMany(p => p.MatchIdAwayManagerNavigation)
                    .HasForeignKey(d => d.IdAwayManager)
                    .HasConstraintName("FK_Match_Manager");

                entity.HasOne(d => d.IdHomeManagerNavigation)
                    .WithMany(p => p.MatchIdHomeManagerNavigation)
                    .HasForeignKey(d => d.IdHomeManager)
                    .HasConstraintName("FK_Match_Manager1");

                entity.HasOne(d => d.IdrefereeNavigation)
                    .WithMany(p => p.Match)
                    .HasForeignKey(d => d.Idreferee)
                    .HasConstraintName("FK_Match_Referee");
            });

            modelBuilder.Entity<MatchPlayerAway>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Idmatch).HasColumnName("idmatch");

                entity.Property(e => e.Idplayer).HasColumnName("idplayer");

                entity.HasOne(d => d.IdmatchNavigation)
                    .WithMany(p => p.MatchPlayerAway)
                    .HasForeignKey(d => d.Idmatch)
                    .HasConstraintName("FK_MatchPlayerAway_Match");

                entity.HasOne(d => d.IdplayerNavigation)
                    .WithMany(p => p.MatchPlayerAway)
                    .HasForeignKey(d => d.Idplayer)
                    .HasConstraintName("FK_MatchPlayerAway_Player");
            });

            modelBuilder.Entity<MatchPlayerHome>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Idmatch).HasColumnName("idmatch");

                entity.Property(e => e.Idplayer).HasColumnName("idplayer");

                entity.HasOne(d => d.IdmatchNavigation)
                    .WithMany(p => p.MatchPlayerHome)
                    .HasForeignKey(d => d.Idmatch)
                    .HasConstraintName("FK_MatchPlayerHome_Match");

                entity.HasOne(d => d.IdplayerNavigation)
                    .WithMany(p => p.MatchPlayerHome)
                    .HasForeignKey(d => d.Idplayer)
                    .HasConstraintName("FK_MatchPlayerHome_Player");
            });

            modelBuilder.Entity<Player>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MinutesPlayed).HasColumnName("minutesPlayed");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Number).HasColumnName("number");

                entity.Property(e => e.RedCards).HasColumnName("redCards");

                entity.Property(e => e.TeamName)
                    .IsRequired()
                    .HasColumnName("teamName")
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.YellowCards).HasColumnName("yellowCards");
            });

            modelBuilder.Entity<Referee>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.MinutesPlayed).HasColumnName("minutesPlayed");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WNADotNetCore.SnakeLadderGame.Database.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Board> Boards { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GamePlay> GamePlays { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=SnakeLadderGame;User Id=sa;Password=sasa@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Board>(entity =>
        {
            entity.HasKey(e => e.CellNo).HasName("PK__Board__EA439533A10E6F86");

            entity.ToTable("Board");

            entity.Property(e => e.Type).HasMaxLength(20);
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.GameId).HasName("PK__Game__2AB897FDACA18659");

            entity.ToTable("Game");

            entity.HasIndex(e => e.GameCode, "UQ__Game__18C8460C71AA217B").IsUnique();

            entity.Property(e => e.GameCode).HasMaxLength(50);
        });

        modelBuilder.Entity<GamePlay>(entity =>
        {
            entity.HasKey(e => e.MovementId).HasName("PK__GamePlay__D18224462BA1669B");

            entity.ToTable("GamePlay");

            entity.HasOne(d => d.Player).WithMany(p => p.GamePlays)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GamePlay__Player__403A8C7D");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PK__Players__4A4E74C8971DEB64");

            entity.Property(e => e.GameCode).HasMaxLength(50);
            entity.Property(e => e.PlayerName).HasMaxLength(50);

            entity.HasOne(d => d.GameCodeNavigation).WithMany(p => p.Players)
                .HasPrincipalKey(p => p.GameCode)
                .HasForeignKey(d => d.GameCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Players__GameCod__3A81B327");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

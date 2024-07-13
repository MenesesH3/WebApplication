using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Database;

public partial class ChallengedbContext : DbContext
{
    public ChallengedbContext()
    {
    }

    public ChallengedbContext(DbContextOptions<ChallengedbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ocorrenciaview> Ocorrenciaviews { get; set; }

    public virtual DbSet<Ocorrencia> Ocorrencia { get; set; }

    public virtual DbSet<Tipo> Tipos { get; set; }

    public virtual DbSet<Transportador> Transportadors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost\\SQLEXPRESS; Initial catalog=challengedb; Trusted_Connection=True; Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ocorrenciaview>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("OCORRENCIAVIEW");

            entity.Property(e => e.CnpjTrasnportador)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("CNPJ_TRASNPORTADOR");
            entity.Property(e => e.CodigoTipo)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("CODIGO_TIPO");
            entity.Property(e => e.DataSolucao)
                .HasColumnType("datetime")
                .HasColumnName("DATA_SOLUCAO");
            entity.Property(e => e.DataoCorrencia)
                .HasColumnType("datetime")
                .HasColumnName("DATAO_CORRENCIA");
            entity.Property(e => e.DescricaoTransportador)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("DESCRICAO_TRANSPORTADOR");
            entity.Property(e => e.Descricaotipo)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("DESCRICAOTIPO");
            entity.Property(e => e.OcorreuEm)
                .HasColumnType("datetime")
                .HasColumnName("OCORREU_EM");
            entity.Property(e => e.SolucaoEm)
                .HasColumnType("datetime")
                .HasColumnName("SOLUCAO_EM");
        });

        modelBuilder.Entity<Ocorrencia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OCORRENC__3214EC27D6089BCD");

            entity.ToTable("OCORRENCIA");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdTipo).HasColumnName("ID_TIPO");
            entity.Property(e => e.IdTransportador).HasColumnName("ID_TRANSPORTADOR");
            entity.Property(e => e.OcorreuEm)
                .HasColumnType("datetime")
                .HasColumnName("OCORREU_EM");
            entity.Property(e => e.SolucaoEm)
                .HasColumnType("datetime")
                .HasColumnName("SOLUCAO_EM");
        });

        modelBuilder.Entity<Tipo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TIPO__3214EC27BE3F086A");

            entity.ToTable("TIPO");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Codigo)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("CODIGO");
            entity.Property(e => e.Descricao)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("DESCRICAO");
        });

        modelBuilder.Entity<Transportador>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__TRANSPOR__3214EC27656C25B4");

            entity.ToTable("TRANSPORTADOR");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cnpj)
                .HasMaxLength(14)
                .IsUnicode(false)
                .HasColumnName("CNPJ");
            entity.Property(e => e.Descricao)
                .HasMaxLength(60)
                .IsUnicode(false)
                .HasColumnName("DESCRICAO");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

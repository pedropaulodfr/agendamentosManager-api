using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace agendamentosmanager_api.Models;

public partial class AgendamentobotContext : DbContext
{
    public AgendamentobotContext(DbContextOptions<AgendamentobotContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Agendamento> Agendamentos { get; set; }

    public virtual DbSet<Funcionamento> Funcionamentos { get; set; }

    public virtual DbSet<Horario> Horarios { get; set; }

    public virtual DbSet<Servico> Servicos { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Agendamento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Agendamentos_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Data).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Executado).HasDefaultValueSql("false");
            entity.Property(e => e.Nome).HasMaxLength(100);
            entity.Property(e => e.Servico).HasMaxLength(50);
            entity.Property(e => e.Telefone).HasMaxLength(20);
            entity.Property(e => e.UsuarioId).HasColumnName("Usuario_Id");
        });

        modelBuilder.Entity<Funcionamento>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Funcionamento_pkey");

            entity.ToTable("Funcionamento");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Abertura).HasColumnType("time with time zone");
            entity.Property(e => e.Almoco).HasColumnType("time with time zone");
            entity.Property(e => e.Dias).HasMaxLength(50);
            entity.Property(e => e.Fechamento).HasColumnType("time with time zone");
        });

        modelBuilder.Entity<Horario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Horarios_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
        });

        modelBuilder.Entity<Servico>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Servicos_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Descricao).HasMaxLength(150);
            entity.Property(e => e.Identificacao).HasMaxLength(50);
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Usuarios_pkey");

            entity.Property(e => e.Id).UseIdentityAlwaysColumn();
            entity.Property(e => e.Ativo).HasDefaultValueSql("true");
            entity.Property(e => e.Cpfcnpj)
                .HasMaxLength(18)
                .HasColumnName("CPFCNPJ");
            entity.Property(e => e.Deletado).HasDefaultValueSql("false");
            entity.Property(e => e.Master).HasDefaultValueSql("false");
            entity.Property(e => e.Nome).HasMaxLength(150);
            entity.Property(e => e.Perfil).HasMaxLength(15);
            entity.Property(e => e.Senha).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

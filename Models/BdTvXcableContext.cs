using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmpresaTVxCable.Models;

public partial class BdTvXcableContext : DbContext
{
    public BdTvXcableContext()
    {
    }

    public BdTvXcableContext(DbContextOptions<BdTvXcableContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Contrato> Contratos { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Servicio> Servicios { get; set; }

    public virtual DbSet<ZonaGeografica> ZonaGeograficas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

        }
    }
//warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("server=ana\\SQLEXPRESS; database=bd_tvXcable; integrated security=true; TrustServerCertificate=Yes;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente);

            entity.ToTable("cliente");

            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.Activo).HasColumnName("activo");
            entity.Property(e => e.Dni).HasColumnName("dni");
            entity.Property(e => e.IdZona).HasColumnName("id_zona");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdZonaNavigation).WithMany(p => p.Clientes)
                .HasForeignKey(d => d.IdZona)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_cliente_zona_geografica");
        });

        modelBuilder.Entity<Contrato>(entity =>
        {
            entity.HasKey(e => e.IdContrato);

            entity.ToTable("contrato");

            entity.Property(e => e.IdContrato).HasColumnName("id_contrato");
            entity.Property(e => e.IdCliente).HasColumnName("id_cliente");
            entity.Property(e => e.IdServicio).HasColumnName("id_servicio");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Contratos)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_contrato_cliente");

            entity.HasOne(d => d.IdServicioNavigation).WithMany(p => p.Contratos)
                .HasForeignKey(d => d.IdServicio)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_contrato_servicio");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.IdRegion);

            entity.ToTable("region");

            entity.Property(e => e.IdRegion).HasColumnName("id_region");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Servicio>(entity =>
        {
            entity.HasKey(e => e.IdServicio);

            entity.ToTable("servicio");

            entity.Property(e => e.IdServicio).HasColumnName("id_servicio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
        });

        modelBuilder.Entity<ZonaGeografica>(entity =>
        {
            entity.HasKey(e => e.IdZona);

            entity.ToTable("zona_geografica");

            entity.Property(e => e.IdZona).HasColumnName("id_zona");
            entity.Property(e => e.IdRegion).HasColumnName("id_region");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasOne(d => d.IdRegionNavigation).WithMany(p => p.ZonaGeograficas)
                .HasForeignKey(d => d.IdRegion)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_zona_geografica_region");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

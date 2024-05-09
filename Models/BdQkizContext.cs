using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace QKiz.Models;

public partial class BdQkizContext : DbContext
{
    public BdQkizContext()
    {
    }

    public BdQkizContext(DbContextOptions<BdQkizContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Direccion> Direccions { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Sexo> Sexos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            //        => optionsBuilder.UseMySql("server=localhost;port=3306;database=bd_qkiz;uid=root;password=Kaliman66", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.37-mysql"));
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Direccion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("direccion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Calle)
                .HasMaxLength(100)
                .HasColumnName("calle");
            entity.Property(e => e.Ciudad)
                .HasMaxLength(45)
                .HasColumnName("ciudad");
            entity.Property(e => e.Colonia)
                .HasMaxLength(45)
                .HasColumnName("colonia");
            entity.Property(e => e.Cp)
                .HasMaxLength(5)
                .HasColumnName("cp");
            entity.Property(e => e.Estado)
                .HasMaxLength(45)
                .HasColumnName("estado");
            entity.Property(e => e.Numext)
                .HasMaxLength(10)
                .HasColumnName("numext");
            entity.Property(e => e.Numint)
                .HasMaxLength(10)
                .HasColumnName("numint");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("empleado");

            entity.HasIndex(e => e.DireccionId, "direccion_id");

            entity.HasIndex(e => e.SexoId, "sexo_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Apellidom)
                .HasMaxLength(20)
                .HasColumnName("apellidom");
            entity.Property(e => e.Apellidop)
                .HasMaxLength(20)
                .HasColumnName("apellidop");
            entity.Property(e => e.DireccionId).HasColumnName("direccion_id");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .HasColumnName("email");
            entity.Property(e => e.Nombre)
                .HasMaxLength(45)
                .HasColumnName("nombre");
            entity.Property(e => e.Nss)
                .HasMaxLength(11)
                .IsFixedLength()
                .HasColumnName("nss");
            entity.Property(e => e.Rfc)
                .HasMaxLength(13)
                .IsFixedLength()
                .HasColumnName("rfc");
            entity.Property(e => e.SexoId).HasColumnName("sexo_id");
            entity.Property(e => e.Telefono)
                .HasMaxLength(10)
                .HasColumnName("telefono");

            entity.HasOne(d => d.Direccion).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.DireccionId)
                .HasConstraintName("empleado_ibfk_2");

            entity.HasOne(d => d.Sexo).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.SexoId)
                .HasConstraintName("empleado_ibfk_1");
        });

        modelBuilder.Entity<Sexo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("sexo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Genero)
                .HasMaxLength(45)
                .HasColumnName("genero");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

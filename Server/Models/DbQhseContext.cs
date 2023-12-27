using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QHSE.Server.Models;

public partial class DbqhseContext : DbContext
{
    public DbqhseContext()
    {
    }

    public DbqhseContext(DbContextOptions<DbqhseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Categorium> Categoria { get; set; }

    public virtual DbSet<Creacion> Creacions { get; set; }

    public virtual DbSet<Empresa> Empresas { get; set; }

    public virtual DbSet<Inspeccion> Inspeccions { get; set; }

    public virtual DbSet<InspeccionDet> InspeccionDets { get; set; }

    public virtual DbSet<Plantilla> Plantillas { get; set; }

    public virtual DbSet<PlantillaDet> PlantillaDets { get; set; }

    public virtual DbSet<SubCategorium> SubCategoria { get; set; }

    public virtual DbSet<TpoInspeccion> TpoInspeccions { get; set; }

    public virtual DbSet<TpoUsuario> TpoUsuarios { get; set; }

    public virtual DbSet<Trabajador> Trabajadors { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.IdArea);

            entity.ToTable("Area");

            entity.Property(e => e.IdArea).HasComment("Id Area");
            entity.Property(e => e.DescArea)
                .HasMaxLength(50)
                .HasComment("Descripción");
            entity.Property(e => e.IdCreate).HasComment("Id Creación");

            entity.HasOne(d => d.IdCreateNavigation).WithMany(p => p.Areas)
                .HasForeignKey(d => d.IdCreate)
                .HasConstraintName("FK_Area_Creacion");
        });

        modelBuilder.Entity<Categorium>(entity =>
        {
            entity.HasKey(e => e.IdCtg);

            entity.Property(e => e.IdCtg).HasComment("Id Categoria");
            entity.Property(e => e.DescCtg)
                .HasMaxLength(100)
                .HasComment("Desccripcion");
            entity.Property(e => e.IdCreate).HasComment("Id Creacion");

            entity.HasOne(d => d.IdCreateNavigation).WithMany(p => p.Categoria)
                .HasForeignKey(d => d.IdCreate)
                .HasConstraintName("FK_Categoria_Creacion");
        });

        modelBuilder.Entity<Creacion>(entity =>
        {
            entity.HasKey(e => e.IdCreate);

            entity.ToTable("Creacion");

            entity.Property(e => e.IdCreate).HasComment("Id Creación");
            entity.Property(e => e.Activo).HasComment("1=Activo, 0=Inactivo");
            entity.Property(e => e.FechaAnula)
                .HasComment("Fecha Anula")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaCrea)
                .HasComment("Fecha Crea")
                .HasColumnType("datetime");
            entity.Property(e => e.FechaModi)
                .HasComment("Fecha Modifica")
                .HasColumnType("datetime");
            entity.Property(e => e.PcAnula)
                .HasMaxLength(15)
                .HasComment("PC Anula");
            entity.Property(e => e.PcCrea)
                .HasMaxLength(15)
                .HasComment("PC Crea");
            entity.Property(e => e.PcModi)
                .HasMaxLength(15)
                .HasComment("PC Modifica");
            entity.Property(e => e.UsuaAnula)
                .HasMaxLength(15)
                .HasComment("Usuario Anula");
            entity.Property(e => e.UsuaCrea)
                .HasMaxLength(15)
                .HasComment("Usuario Crea");
            entity.Property(e => e.UsuaModi)
                .HasMaxLength(15)
                .HasComment("Usuario Modifica");
        });

        modelBuilder.Entity<Empresa>(entity =>
        {
            entity.HasKey(e => e.IdEmp);

            entity.ToTable("Empresa");

            entity.Property(e => e.IdEmp).HasComment("Id Empresa");
            entity.Property(e => e.CorreoEmp)
                .HasMaxLength(100)
                .HasComment("Correo Empresa");
            entity.Property(e => e.DirecEmp)
                .HasMaxLength(70)
                .HasComment("Dirección Empresa");
            entity.Property(e => e.DistEmp)
                .HasMaxLength(30)
                .HasComment("Distrito Empresa");
            entity.Property(e => e.DptEmp)
                .HasMaxLength(30)
                .HasComment("Departamento Emp");
            entity.Property(e => e.NroTelefono)
                .HasMaxLength(20)
                .HasComment("N° Telefono");
            entity.Property(e => e.ProvEmp)
                .HasMaxLength(30)
                .HasComment("Provincia Empresa");
            entity.Property(e => e.RazEmp)
                .HasMaxLength(50)
                .HasComment("Razon Social Emp");
            entity.Property(e => e.RucEmp)
                .HasMaxLength(11)
                .HasComment("Ruc Empresa");
        });

        modelBuilder.Entity<Inspeccion>(entity =>
        {
            entity.HasKey(e => e.IdInsp);

            entity.ToTable("Inspeccion");

            entity.Property(e => e.IdInsp).HasComment("Id Inspección");
            entity.Property(e => e.Fecha)
                .HasComment("Fecha Inspección")
                .HasColumnType("date");
            entity.Property(e => e.IdArea).HasComment("Id Area");
            entity.Property(e => e.IdCreate).HasComment("Id Creación");
            entity.Property(e => e.IdEmp).HasComment("Id Empresa");
            entity.Property(e => e.IdSuper1).HasComment("Id Trabajador 1");
            entity.Property(e => e.IdSuper2).HasComment("Id Trabajador 2");
            entity.Property(e => e.IdTpoInsp).HasComment("Id Tipo Inspección");
            entity.Property(e => e.NomJefeArea).HasMaxLength(50);
            entity.Property(e => e.NomSuper2)
                .HasMaxLength(50)
                .HasComment("Nombre trabajador 2");

            entity.HasOne(d => d.IdAreaNavigation).WithMany(p => p.Inspeccions)
                .HasForeignKey(d => d.IdArea)
                .HasConstraintName("FK_Inspeccion_Area");

            entity.HasOne(d => d.IdCreateNavigation).WithMany(p => p.Inspeccions)
                .HasForeignKey(d => d.IdCreate)
                .HasConstraintName("FK_Inspeccion_Creacion");

            entity.HasOne(d => d.IdEmpNavigation).WithMany(p => p.Inspeccions)
                .HasForeignKey(d => d.IdEmp)
                .HasConstraintName("FK_Inspeccion_Empresa");

            entity.HasOne(d => d.IdSuper1Navigation).WithMany(p => p.Inspeccions)
                .HasForeignKey(d => d.IdSuper1)
                .HasConstraintName("FK_Inspeccion_Trabajador");

            entity.HasOne(d => d.IdTpoInspNavigation).WithMany(p => p.Inspeccions)
                .HasForeignKey(d => d.IdTpoInsp)
                .HasConstraintName("FK_Inspeccion_TpoInspeccion");
        });

        modelBuilder.Entity<InspeccionDet>(entity =>
        {
            entity.HasKey(e => e.IdInspDet);

            entity.ToTable("InspeccionDet");

            entity.Property(e => e.IdInspDet).HasComment("Id detalle");
            entity.Property(e => e.Activo).HasComment("1=Activo, 0=Inactivo");
            entity.Property(e => e.FechaLevanta)
                .HasComment("Fecha levantamiento")
                .HasColumnType("date");
            entity.Property(e => e.Foto1).HasComment("1° Foto");
            entity.Property(e => e.Foto2).HasComment("2° Foto");
            entity.Property(e => e.IdInsp).HasComment("Id Inspección");
            entity.Property(e => e.IdSubCtg).HasComment("Id Categoria");
            entity.Property(e => e.NroPctg)
                .HasComment("% cumplimiento")
                .HasColumnType("decimal(2, 2)");
            entity.Property(e => e.Obs1)
                .HasMaxLength(300)
                .HasComment("1° Observación");
            entity.Property(e => e.Obs2)
                .HasMaxLength(300)
                .HasComment("2° Observación");
            entity.Property(e => e.OpcSelect1)
                .HasMaxLength(1)
                .HasComment("1° Inspeccion S,N,NA");
            entity.Property(e => e.OpcSelect2)
                .HasMaxLength(1)
                .HasComment("2° Inspeccion S,N,NA");

            entity.HasOne(d => d.IdInspNavigation).WithMany(p => p.InspeccionDets)
                .HasForeignKey(d => d.IdInsp)
                .HasConstraintName("FK_InspeccionDet_Inspeccion");

            entity.HasOne(d => d.IdSubCtgNavigation).WithMany(p => p.InspeccionDets)
                .HasForeignKey(d => d.IdSubCtg)
                .HasConstraintName("FK_InspeccionDet_SubCategoria");
        });

        modelBuilder.Entity<Plantilla>(entity =>
        {
            entity.HasKey(e => e.IdPlantilla);

            entity.ToTable("Plantilla");

            entity.Property(e => e.IdPlantilla).HasComment("Id Plantilla");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasComment("Descripción");
            entity.Property(e => e.IdArea).HasComment("Id Area");
            entity.Property(e => e.IdCreate).HasComment("Id Creación");

            entity.HasOne(d => d.IdAreaNavigation).WithMany(p => p.Plantillas)
                .HasForeignKey(d => d.IdArea)
                .HasConstraintName("FK_Plantilla_Area");

            entity.HasOne(d => d.IdCreateNavigation).WithMany(p => p.Plantillas)
                .HasForeignKey(d => d.IdCreate)
                .HasConstraintName("FK_Plantilla_Creacion");
        });

        modelBuilder.Entity<PlantillaDet>(entity =>
        {
            entity.HasKey(e => e.IdPlantillaDet);

            entity.ToTable("PlantillaDet");

            entity.Property(e => e.IdPlantillaDet).HasComment("Id Detalle Plantilla");
            entity.Property(e => e.Activo).HasComment("1=Activo, 0=Inactivo");
            entity.Property(e => e.IdPlantilla).HasComment("Id Plantilla");
            entity.Property(e => e.IdSubCtg).HasComment("Id Sub Categoria");
            entity.Property(e => e.NroOrden).HasMaxLength(5);

            entity.HasOne(d => d.IdPlantillaNavigation).WithMany(p => p.PlantillaDets)
                .HasForeignKey(d => d.IdPlantilla)
                .HasConstraintName("FK_PlantillaDet_Plantilla");

            entity.HasOne(d => d.IdSubCtgNavigation).WithMany(p => p.PlantillaDets)
                .HasForeignKey(d => d.IdSubCtg)
                .HasConstraintName("FK_PlantillaDet_SubCategoria");
        });

        modelBuilder.Entity<SubCategorium>(entity =>
        {
            entity.HasKey(e => e.IdSubCtg);

            entity.Property(e => e.IdSubCtg).HasComment("Id Sub Categoria");
            entity.Property(e => e.DescSubCtg)
                .HasMaxLength(500)
                .HasComment("Desc Sub Categoria");
            entity.Property(e => e.IdCreate).HasComment("Id Creción");
            entity.Property(e => e.IdCtg).HasComment("Id Categoria");

            entity.HasOne(d => d.IdCreateNavigation).WithMany(p => p.SubCategoria)
                .HasForeignKey(d => d.IdCreate)
                .HasConstraintName("FK_SubCategoria_Creacion");

            entity.HasOne(d => d.IdCtgNavigation).WithMany(p => p.SubCategoria)
                .HasForeignKey(d => d.IdCtg)
                .HasConstraintName("FK_SubCategoria_Categoria");
        });

        modelBuilder.Entity<TpoInspeccion>(entity =>
        {
            entity.HasKey(e => e.IdTpoInsp);

            entity.ToTable("TpoInspeccion");

            entity.Property(e => e.IdTpoInsp).HasComment("Id Tipo Inspección");
            entity.Property(e => e.Activo).HasComment("1=Activo, 0=Inactivo");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(50)
                .HasComment("Descripcion Tipo");
        });

        modelBuilder.Entity<TpoUsuario>(entity =>
        {
            entity.HasKey(e => e.IdTpoUsua);

            entity.ToTable("TpoUsuario");

            entity.Property(e => e.IdTpoUsua).HasComment("Id Tipo Usuario");
            entity.Property(e => e.Activo).HasComment("1=Activo, 0=Inactivo");
            entity.Property(e => e.DescTpo)
                .HasMaxLength(50)
                .HasComment("Descripción Tipo");
        });

        modelBuilder.Entity<Trabajador>(entity =>
        {
            entity.HasKey(e => e.IdTraba);

            entity.ToTable("Trabajador");

            entity.Property(e => e.IdTraba).HasComment("Id Trabajador");
            entity.Property(e => e.ApeTraba)
                .HasMaxLength(50)
                .HasComment("Apellido Trabajador");
            entity.Property(e => e.CorreoTraba)
                .HasMaxLength(100)
                .HasComment("Correo Trabajador");
            entity.Property(e => e.IdCreate).HasComment("Id Creación");
            entity.Property(e => e.NomTraba)
                .HasMaxLength(50)
                .HasComment("Nombre Trabajador");
            entity.Property(e => e.NroDoc)
                .HasMaxLength(11)
                .HasComment("N° Documento");
            entity.Property(e => e.NroTelefono)
                .HasMaxLength(15)
                .HasComment("N° Telefono");

            entity.HasOne(d => d.IdCreateNavigation).WithMany(p => p.Trabajadors)
                .HasForeignKey(d => d.IdCreate)
                .HasConstraintName("FK_Trabajador_Creacion");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsua);

            entity.ToTable("Usuario");

            entity.Property(e => e.IdUsua).HasComment("Id Usuario");
            entity.Property(e => e.ClaveUsua)
                .HasMaxLength(15)
                .HasComment("Clave Usuario");
            entity.Property(e => e.IdCreate).HasComment("Id Creación");
            entity.Property(e => e.IdTpoUsua).HasComment("Id Tipo Usuario");
            entity.Property(e => e.IdTraba).HasComment("Id Trabajador");
            entity.Property(e => e.NomUsua)
                .HasMaxLength(30)
                .HasComment("Nombre Usuario");

            entity.HasOne(d => d.IdCreateNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdCreate)
                .HasConstraintName("FK_Usuario_Creacion");

            entity.HasOne(d => d.IdTpoUsuaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdTpoUsua)
                .HasConstraintName("FK_Usuario_TpoUsuario");

            entity.HasOne(d => d.IdTrabaNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdTraba)
                .HasConstraintName("FK_Usuario_Trabajador");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

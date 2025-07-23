using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TrackademiApi.Models.Entities;

public partial class TrackademiContext : DbContext
{
    public TrackademiContext()
    {
    }

    public TrackademiContext(DbContextOptions<TrackademiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Calificaciones> Calificaciones { get; set; }

    public virtual DbSet<ControlAsistencia> ControlAsistencia { get; set; }

    public virtual DbSet<Estudiantes> Estudiantes { get; set; }

    public virtual DbSet<Log> Log { get; set; }

    public virtual DbSet<Materia> Materia { get; set; }

    public virtual DbSet<MateriaInscrita> MateriaInscrita { get; set; }

    public virtual DbSet<Perfiles> Perfiles { get; set; }

    public virtual DbSet<Periodos> Periodos { get; set; }

    public virtual DbSet<Usuarios> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=Trackademi;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Calificaciones>(entity =>
        {
            entity.HasKey(e => e.IdCalificacion);

            entity.Property(e => e.IdCalificacion).HasColumnName("idCalificacion");
            entity.Property(e => e.IdEstudiante).HasColumnName("idEstudiante");
            entity.Property(e => e.IdMateria).HasColumnName("idMateria");
            entity.Property(e => e.Observaciones).IsUnicode(false);
        });

        modelBuilder.Entity<ControlAsistencia>(entity =>
        {
            entity.HasKey(e => e.IdAsistencia).HasName("PK_Asistencias");

            entity.Property(e => e.IdAsistencia).HasColumnName("idAsistencia");
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdEstudiante).HasColumnName("idEstudiante");
            entity.Property(e => e.IdMateria).HasColumnName("idMateria");
        });

        modelBuilder.Entity<Estudiantes>(entity =>
        {
            entity.HasKey(e => e.IdEstudiante);

            entity.Property(e => e.IdEstudiante).HasColumnName("idEstudiante");
            entity.Property(e => e.Apellidos)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Genero)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Matricula)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Nombres)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Telefono)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.HasKey(e => e.IdLog).HasName("PK_LogActividades");

            entity.Property(e => e.IdLog).HasColumnName("idLog");
            entity.Property(e => e.Data).IsUnicode(false);
            entity.Property(e => e.Fecha)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Metodo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Url)
                .HasMaxLength(500)
                .IsUnicode(false)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<Materia>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Materias");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Detalle)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.IdPeriodo).HasColumnName("idPeriodo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MateriaInscrita>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_EstudianteAsignatura");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdEstudiante).HasColumnName("idEstudiante");
            entity.Property(e => e.IdMateria).HasColumnName("idMateria");
        });

        modelBuilder.Entity<Perfiles>(entity =>
        {
            entity.HasKey(e => e.IdPerfil);

            entity.Property(e => e.IdPerfil).HasColumnName("idPerfil");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(350)
                .IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Periodos>(entity =>
        {
            entity.HasKey(e => e.IdPeriodo);

            entity.Property(e => e.IdPeriodo).HasColumnName("idPeriodo");
            entity.Property(e => e.Codigo)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
        });

        modelBuilder.Entity<Usuarios>(entity =>
        {
            entity.HasKey(e => e.IdUsuario);

            entity.Property(e => e.IdUsuario).HasColumnName("idUsuario");
            entity.Property(e => e.Activo).HasDefaultValue(true);
            entity.Property(e => e.Apellidos)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Clave)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.Correo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FechaCreacion)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IdPerfil).HasColumnName("idPerfil");
            entity.Property(e => e.Nombres)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Usuario)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

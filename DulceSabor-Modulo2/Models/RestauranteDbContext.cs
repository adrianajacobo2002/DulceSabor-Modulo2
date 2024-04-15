using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DulceSabor_Modulo2.Models;

public partial class RestauranteDbContext : DbContext
{
    public RestauranteDbContext()
    {
    }

    public RestauranteDbContext(DbContextOptions<RestauranteDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cargo> Cargos { get; set; }

    public virtual DbSet<CategoriasPlato> CategoriasPlatos { get; set; }

    public virtual DbSet<Combo> Combos { get; set; }

    public virtual DbSet<Cuenta> Cuentas { get; set; }

    public virtual DbSet<DetalleCuenta> DetalleCuentas { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Estado> Estados { get; set; }

    public virtual DbSet<Mesa> Mesas { get; set; }

    public virtual DbSet<Plato> Platos { get; set; }

    public virtual DbSet<Promocione> Promociones { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.HasKey(e => e.IdCargo).HasName("PK__cargo__D3C09EC5057879E1");

            entity.ToTable("cargo");

            entity.Property(e => e.IdCargo).HasColumnName("id_cargo");
            entity.Property(e => e.TipoCargo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_cargo");
        });

        modelBuilder.Entity<CategoriasPlato>(entity =>
        {
            entity.HasKey(e => e.CategoriaId).HasName("PK__categori__DB875A4F737FC7E1");

            entity.ToTable("categorias_platos");

            entity.HasIndex(e => e.Nombre, "UQ__categori__72AFBCC6B649A8EF").IsUnique();

            entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Combo>(entity =>
        {
            entity.HasKey(e => e.ComboId).HasName("PK__combos__18F74AA3B49096BF");

            entity.ToTable("combos");

            entity.Property(e => e.ComboId).HasColumnName("combo_id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Disponible).HasColumnName("disponible");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");

            entity.HasMany(d => d.Platos).WithMany(p => p.Combos)
                .UsingEntity<Dictionary<string, object>>(
                    "CombosPlato",
                    r => r.HasOne<Plato>().WithMany()
                        .HasForeignKey("PlatoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__combos_pl__plato__4AB81AF0"),
                    l => l.HasOne<Combo>().WithMany()
                        .HasForeignKey("ComboId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__combos_pl__combo__49C3F6B7"),
                    j =>
                    {
                        j.HasKey("ComboId", "PlatoId").HasName("PK__combos_p__FA064B59922AEA95");
                        j.ToTable("combos_platos");
                        j.IndexerProperty<int>("ComboId").HasColumnName("combo_id");
                        j.IndexerProperty<int>("PlatoId").HasColumnName("plato_id");
                    });
        });

        modelBuilder.Entity<Cuenta>(entity =>
        {
            entity.HasKey(e => e.IdCuenta).HasName("PK__Cuentas__C7E2868597B7CE66");

            entity.Property(e => e.IdCuenta).HasColumnName("id_cuenta");
            entity.Property(e => e.Apellido)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Estado)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.FechaApertura)
                .HasColumnType("datetime")
                .HasColumnName("fecha_apertura");
            entity.Property(e => e.IdMesa).HasColumnName("id_mesa");
            entity.Property(e => e.Nombre)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<DetalleCuenta>(entity =>
        {
            entity.HasKey(e => e.IdDetalleCuenta).HasName("PK__DetalleC__CAFA94D6FC98E873");

            entity.Property(e => e.IdDetalleCuenta).HasColumnName("id_detalle_cuenta");
            entity.Property(e => e.Comentario)
                .HasColumnType("text")
                .HasColumnName("comentario");
            entity.Property(e => e.Descuento)
                .HasDefaultValue(0m)
                .HasColumnType("decimal(3, 2)")
                .HasColumnName("descuento");
            entity.Property(e => e.Estado)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("estado");
            entity.Property(e => e.IdCombo).HasColumnName("id_combo");
            entity.Property(e => e.IdCuenta).HasColumnName("id_cuenta");
            entity.Property(e => e.IdPlato).HasColumnName("id_plato");
            entity.Property(e => e.IdPromocion).HasColumnName("id_promocion");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");

            entity.HasOne(d => d.IdComboNavigation).WithMany(p => p.DetalleCuenta)
                .HasForeignKey(d => d.IdCombo)
                .HasConstraintName("FK__DetalleCu__id_co__59FA5E80");

            entity.HasOne(d => d.IdCuentaNavigation).WithMany(p => p.DetalleCuenta)
                .HasForeignKey(d => d.IdCuenta)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetalleCu__id_cu__5812160E");

            entity.HasOne(d => d.IdPlatoNavigation).WithMany(p => p.DetalleCuenta)
                .HasForeignKey(d => d.IdPlato)
                .HasConstraintName("FK__DetalleCu__id_pl__59063A47");

            entity.HasOne(d => d.IdPromocionNavigation).WithMany(p => p.DetalleCuenta)
                .HasForeignKey(d => d.IdPromocion)
                .HasConstraintName("FK__DetalleCu__id_pr__5AEE82B9");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Empleado__3213E83F8CF29E91");

            entity.ToTable("Empleado");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CargoId).HasColumnName("cargo_id");
            entity.Property(e => e.Direccion)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Dui)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("dui");
            entity.Property(e => e.EstadoId).HasColumnName("estado_id");
            entity.Property(e => e.FechaContratacion).HasColumnName("fecha_contratacion");
            entity.Property(e => e.FechaNacimiento).HasColumnName("fecha_nacimiento");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Salario)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("salario");
            entity.Property(e => e.Telefono)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono");
            entity.Property(e => e.TelefonoReferencia)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("telefono_referencia");

            entity.HasOne(d => d.Cargo).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.CargoId)
                .HasConstraintName("FK__Empleado__cargo___4BAC3F29");

            entity.HasOne(d => d.Estado).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.EstadoId)
                .HasConstraintName("FK__Empleado__estado__4CA06362");
        });

        modelBuilder.Entity<Estado>(entity =>
        {
            entity.HasKey(e => e.IdEstado).HasName("PK__estado__86989FB24A6CC0D4");

            entity.ToTable("estado");

            entity.Property(e => e.IdEstado).HasColumnName("id_estado");
            entity.Property(e => e.TipoEstado)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("tipo_estado");
        });

        modelBuilder.Entity<Mesa>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Mesa__3213E83F23CA9CF6");

            entity.ToTable("Mesa");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.NumeroAsientos).HasColumnName("numero_asientos");
            entity.Property(e => e.NumeroMesa).HasColumnName("numero_mesa");
        });

        modelBuilder.Entity<Plato>(entity =>
        {
            entity.HasKey(e => e.PlatoId).HasName("PK__platos__2F101FAFF7D521ED");

            entity.ToTable("platos");

            entity.Property(e => e.PlatoId).HasColumnName("plato_id");
            entity.Property(e => e.Categoria)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("categoria");
            entity.Property(e => e.CategoriaId).HasColumnName("categoria_id");
            entity.Property(e => e.Descripcion)
                .HasColumnType("text")
                .HasColumnName("descripcion");
            entity.Property(e => e.Disponible).HasColumnName("disponible");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");

            entity.HasOne(d => d.CategoriaNavigation).WithMany(p => p.Platos)
                .HasForeignKey(d => d.CategoriaId)
                .HasConstraintName("FK__platos__categori__4D94879B");
        });

        modelBuilder.Entity<Promocione>(entity =>
        {
            entity.HasKey(e => e.PromocionId).HasName("PK__promocio__EFD43212E76FB95A");

            entity.ToTable("promociones");

            entity.Property(e => e.PromocionId).HasColumnName("promocion_id");
            entity.Property(e => e.Descripcion)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasColumnName("descripcion");
            entity.Property(e => e.Descuento)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("descuento");
            entity.Property(e => e.FechaFin).HasColumnName("fecha_fin");
            entity.Property(e => e.FechaInicio).HasColumnName("fecha_inicio");
            entity.Property(e => e.Nombre)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("nombre");

            entity.HasMany(d => d.Platos).WithMany(p => p.Promocions)
                .UsingEntity<Dictionary<string, object>>(
                    "PromocionesPlato",
                    r => r.HasOne<Plato>().WithMany()
                        .HasForeignKey("PlatoId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__promocion__plato__4E88ABD4"),
                    l => l.HasOne<Promocione>().WithMany()
                        .HasForeignKey("PromocionId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK__promocion__promo__4F7CD00D"),
                    j =>
                    {
                        j.HasKey("PromocionId", "PlatoId").HasName("PK__promocio__0D2533E8ABA4F75B");
                        j.ToTable("promociones_platos");
                        j.IndexerProperty<int>("PromocionId").HasColumnName("promocion_id");
                        j.IndexerProperty<int>("PlatoId").HasColumnName("plato_id");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

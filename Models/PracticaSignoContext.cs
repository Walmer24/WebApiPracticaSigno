using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PracticaSigno.Models;

public partial class PracticaSignoContext : DbContext
{
    public PracticaSignoContext()
    {
    }

    public PracticaSignoContext(DbContextOptions<PracticaSignoContext> options)
        : base(options)
    {
    }

    public DbSet<Producto> Productos { get; set; }
    public DbSet<Venta> Ventas { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<DetalleVenta> DetalleVentas { get; set; }

    /* protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
 #warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
         => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB; Database=PracticaSigno; Trusted_connection=true; Encrypt=false;");
 */
 /*   protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);

        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasOne(p => p.IdCategoriasNavigator)
            .WithMany(c => c.Productos)
            .HasForeignKey(p => p.IdCategoria);
        });

        modelBuilder.Entity<DetalleVenta>(entity =>
        {
            entity.HasMany(d => d.Productos)
            .WithOne(p => p.IdDetalleVentaNavigator)
            .HasForeignKey(d => d.IdProducto);

            entity.HasMany(d => d.Ventas)
            .WithOne(v => v.IdDetalleVentaNavigator)
            .HasForeignKey(d => d.IdVenta);
        });
    }*/

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

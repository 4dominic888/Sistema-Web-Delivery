using Delivery.Domain;
using Delivery.Domain.Food;
using Delivery.Domain.Order;
using Delivery.Domain.User;
using Microsoft.EntityFrameworkCore;

namespace Delivery.Persistence.Data
{
    public class DeliveryDBContext : DbContext
    {
        public DeliveryDBContext(DbContextOptions<DeliveryDBContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Cliente>(c =>
            {
                c.HasMany(e => e.Pedidos)
                .WithOne(e => e.Cliente)
                .HasForeignKey(e => e.IdCliente);

                c.Property(e => e.DateBirth).HasColumnType("Date");
            });

            modelBuilder.Entity<Repartidor>(r =>
            {
                r.HasOne(e => e.PedidoEnCurso)
                .WithOne(e => e.Repartidor)
                .HasForeignKey<Pedido>(e => e.IdRepartidor);
                r.Property(e => e.DateBirth).HasColumnType("Date");
            });

            modelBuilder.Entity<Administrador>(a =>
            {
                a.Property(e => e.DateBirth).HasColumnType("Date");
            });

            modelBuilder.Entity<Comida>(c =>
            {
                c.Property(e => e.Descripcion).HasColumnType("text");
                c.Property(e => e.Precio).HasColumnType("money");
            });

            modelBuilder.Entity<CaracteristicaComida>(ca =>
            {
                ca.Property(e => e.Detalle).HasColumnType("text");
                ca.Property(e => e.Precio).HasColumnType("money");
            });

            modelBuilder.Entity<Direccion>(d =>
            {
                d.Property(e => e.Detalle).HasColumnType("text");
            });

            modelBuilder.Entity<MetodoPago>(m =>
            {
                m.Property(e => e.fechaExpiracion).HasColumnType("Date");
                m.Property(e => e.CVV).HasColumnType("char(3)");
            });

            modelBuilder.Entity<Pedido>(p =>
            {
                p.Property(e => e.Detalle).HasColumnType("text");
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<PreferenciasPagina> PreferenciasPaginas { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<MetodoPago> MetodoPagos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Repartidor> Repartidores { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Comida> Comidas { get; set; }
        public DbSet<CaracteristicaComida> CaracteristicaComidas { get; set; }

    }
}

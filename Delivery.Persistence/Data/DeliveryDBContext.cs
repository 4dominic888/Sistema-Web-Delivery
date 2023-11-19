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

            modelBuilder.Entity<Usuario>(a =>
            {
                a.Property(e => e.DateBirth).HasColumnType("Date");
                a.Property(e => e.Phone).HasMaxLength(9);
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

            modelBuilder.Entity<Pedido>(p =>
            {
                p.Property(e => e.Detalle).HasColumnType("text");
                p.Property(e => e.Cliente).HasColumnType("nvarchar(200)");
                p.Property(e => e.Repartidor).HasColumnType("nvarchar(200)");
                p.Property(e => e.Telefono).HasColumnType("nvarchar(9)");
            });

            modelBuilder.Entity<MetodoPago>(mp =>
            {
                mp.Property(x => x.CVV).HasColumnType("char(3)");
            });

            modelBuilder.Entity<Comida_CaracteristicaMenu>().HasKey(am => new
            {
                am.IdComida,
                am.IdCaracteristicaComida
            });

            modelBuilder.Entity<Comida_CaracteristicaMenu>().HasOne(c => c.Comida).
                WithMany(am => am.comida_CaracteristicasMenu).HasForeignKey(c => c.IdComida);

            modelBuilder.Entity<Comida_CaracteristicaMenu>().HasOne(c => c.CaracteristicaComida).
                WithMany(am => am.comida_CaracteristicasMenu).HasForeignKey(c => c.IdCaracteristicaComida);

            modelBuilder.Entity<Comida_CaracteristicaPedido>(cc =>
            {
                cc.Property(x => x.Contenido).HasColumnType("text");
            });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<MetodoPago> MetodoPagos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Comida> Comidas { get; set; }
        public DbSet<CaracteristicaComida> CaracteristicaComidas { get; set; }
        public DbSet<Comida_CaracteristicaMenu> Comida_CaracteristicasMenu { get; set; }
        public DbSet<Comida_CaracteristicaPedido> Comida_CaracteristicasPedido { get; set; }

    }
}

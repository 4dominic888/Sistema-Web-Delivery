﻿// <auto-generated />
using System;
using Delivery.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Delivery.Persistence.Migrations
{
    [DbContext(typeof(DeliveryDBContext))]
    partial class DeliveryDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Delivery.Domain.Food.CaracteristicaComida", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Detalle")
                        .HasColumnType("text");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("money");

                    b.HasKey("Id");

                    b.ToTable("CaracteristicaComidas");
                });

            modelBuilder.Entity("Delivery.Domain.Food.Comida", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Categoria")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Imagen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("MenuDelDia")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal>("Precio")
                        .HasColumnType("money");

                    b.Property<int>("Stock")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("Comidas");
                });

            modelBuilder.Entity("Delivery.Domain.Food.Comida_CaracteristicaMenu", b =>
                {
                    b.Property<int>("IdComida")
                        .HasColumnType("int");

                    b.Property<int>("IdCaracteristicaComida")
                        .HasColumnType("int");

                    b.HasKey("IdComida", "IdCaracteristicaComida");

                    b.HasIndex("IdCaracteristicaComida");

                    b.ToTable("Comida_CaracteristicasMenu");
                });

            modelBuilder.Entity("Delivery.Domain.Food.Comida_CaracteristicaPedido", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int?>("IdCaracteristicaComida")
                        .HasColumnType("int");

                    b.Property<int>("IdCliente")
                        .HasColumnType("int");

                    b.Property<int>("IdComida")
                        .HasColumnType("int");

                    b.Property<int?>("IdPedido")
                        .HasColumnType("int");

                    b.Property<int>("agrupamiento")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IdCaracteristicaComida");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdComida");

                    b.HasIndex("IdPedido");

                    b.ToTable("Comida_CaracteristicasPedido");
                });

            modelBuilder.Entity("Delivery.Domain.Order.Direccion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Detalle")
                        .HasColumnType("text");

                    b.Property<string>("Nombre_Calle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Direcciones");
                });

            modelBuilder.Entity("Delivery.Domain.Order.MetodoPago", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CVV")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreTarjeta")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Numero")
                        .HasMaxLength(18)
                        .HasColumnType("nvarchar(18)");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.Property<DateTime?>("fechaExpiracion")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("MetodoPagos");
                });

            modelBuilder.Entity("Delivery.Domain.Order.Pedido", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Codigo"));

                    b.Property<string>("Detalle")
                        .HasColumnType("text");

                    b.Property<int>("Estado")
                        .HasColumnType("int");

                    b.Property<DateTime>("Fecha_Fin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Fecha_Inicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdCliente")
                        .HasColumnType("int");

                    b.Property<int>("IdDireccion")
                        .HasColumnType("int");

                    b.Property<int?>("IdMetodoPago")
                        .HasColumnType("int");

                    b.Property<int?>("IdRepartidor")
                        .HasColumnType("int");

                    b.Property<float?>("Total")
                        .HasColumnType("real");

                    b.HasKey("Codigo");

                    b.HasIndex("IdCliente");

                    b.HasIndex("IdDireccion")
                        .IsUnique();

                    b.HasIndex("IdMetodoPago");

                    b.HasIndex("IdRepartidor")
                        .IsUnique()
                        .HasFilter("[IdRepartidor] IS NOT NULL");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("Delivery.Domain.User.Administrador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("ContenidoDestacado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DateBirth")
                        .HasColumnType("Date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<bool>("Recomendaciones")
                        .HasColumnType("bit");

                    b.Property<string>("Rol")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Sexo")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Administradores");
                });

            modelBuilder.Entity("Delivery.Domain.User.Chef", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("ContenidoDestacado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DateBirth")
                        .HasColumnType("Date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<bool>("Recomendaciones")
                        .HasColumnType("bit");

                    b.Property<string>("Rol")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Sexo")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Chefs");
                });

            modelBuilder.Entity("Delivery.Domain.User.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("ContenidoDestacado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DateBirth")
                        .HasColumnType("Date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<int>("PreferenciaCategoria")
                        .HasColumnType("int");

                    b.Property<bool>("Recomendaciones")
                        .HasColumnType("bit");

                    b.Property<string>("Rol")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Sexo")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Delivery.Domain.User.Repartidor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("ContenidoDestacado")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DateBirth")
                        .HasColumnType("Date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(9)
                        .HasColumnType("nvarchar(9)");

                    b.Property<bool>("Recomendaciones")
                        .HasColumnType("bit");

                    b.Property<string>("Rol")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Sexo")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Repartidores");
                });

            modelBuilder.Entity("Delivery.Domain.Food.Comida_CaracteristicaMenu", b =>
                {
                    b.HasOne("Delivery.Domain.Food.CaracteristicaComida", "CaracteristicaComida")
                        .WithMany("comida_CaracteristicasMenu")
                        .HasForeignKey("IdCaracteristicaComida")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Delivery.Domain.Food.Comida", "Comida")
                        .WithMany("comida_CaracteristicasMenu")
                        .HasForeignKey("IdComida")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CaracteristicaComida");

                    b.Navigation("Comida");
                });

            modelBuilder.Entity("Delivery.Domain.Food.Comida_CaracteristicaPedido", b =>
                {
                    b.HasOne("Delivery.Domain.Food.CaracteristicaComida", "caracteristicaComida")
                        .WithMany("Comida_CaracteristicasPedido")
                        .HasForeignKey("IdCaracteristicaComida");

                    b.HasOne("Delivery.Domain.User.Cliente", "Cliente")
                        .WithMany("comida_CaracteristicaPedidos")
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Delivery.Domain.Food.Comida", "Comida")
                        .WithMany("Comida_CaracteristicasPedido")
                        .HasForeignKey("IdComida")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Delivery.Domain.Order.Pedido", "Pedido")
                        .WithMany("Comida_CaracteristicasPedido")
                        .HasForeignKey("IdPedido");

                    b.Navigation("Cliente");

                    b.Navigation("Comida");

                    b.Navigation("Pedido");

                    b.Navigation("caracteristicaComida");
                });

            modelBuilder.Entity("Delivery.Domain.Order.Pedido", b =>
                {
                    b.HasOne("Delivery.Domain.User.Cliente", "Cliente")
                        .WithMany("Pedidos")
                        .HasForeignKey("IdCliente")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Delivery.Domain.Order.Direccion", "Direccion")
                        .WithOne("Pedido")
                        .HasForeignKey("Delivery.Domain.Order.Pedido", "IdDireccion")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Delivery.Domain.Order.MetodoPago", "MetodoPago")
                        .WithMany()
                        .HasForeignKey("IdMetodoPago");

                    b.HasOne("Delivery.Domain.User.Repartidor", "Repartidor")
                        .WithOne("PedidoEnCurso")
                        .HasForeignKey("Delivery.Domain.Order.Pedido", "IdRepartidor");

                    b.Navigation("Cliente");

                    b.Navigation("Direccion");

                    b.Navigation("MetodoPago");

                    b.Navigation("Repartidor");
                });

            modelBuilder.Entity("Delivery.Domain.Food.CaracteristicaComida", b =>
                {
                    b.Navigation("Comida_CaracteristicasPedido");

                    b.Navigation("comida_CaracteristicasMenu");
                });

            modelBuilder.Entity("Delivery.Domain.Food.Comida", b =>
                {
                    b.Navigation("Comida_CaracteristicasPedido");

                    b.Navigation("comida_CaracteristicasMenu");
                });

            modelBuilder.Entity("Delivery.Domain.Order.Direccion", b =>
                {
                    b.Navigation("Pedido");
                });

            modelBuilder.Entity("Delivery.Domain.Order.Pedido", b =>
                {
                    b.Navigation("Comida_CaracteristicasPedido");
                });

            modelBuilder.Entity("Delivery.Domain.User.Cliente", b =>
                {
                    b.Navigation("Pedidos");

                    b.Navigation("comida_CaracteristicaPedidos");
                });

            modelBuilder.Entity("Delivery.Domain.User.Repartidor", b =>
                {
                    b.Navigation("PedidoEnCurso");
                });
#pragma warning restore 612, 618
        }
    }
}

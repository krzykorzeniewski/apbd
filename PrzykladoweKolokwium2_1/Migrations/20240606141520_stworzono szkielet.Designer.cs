﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrzykladoweKolokwium2_1.Context;

#nullable disable

namespace PrzykladoweKolokwium2_1.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20240606141520_stworzono szkielet")]
    partial class stworzonoszkielet
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.4.24267.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PrzykladoweKolokwium2_1.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("FirstName");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("LastName");

                    b.HasKey("Id");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("PrzykladoweKolokwium2_1.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ClientId")
                        .HasColumnType("int")
                        .HasColumnName("ClientID");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedAt");

                    b.Property<DateTime?>("FulfilledAt")
                        .HasColumnType("datetime2")
                        .HasColumnName("FulfilledAt");

                    b.Property<int>("StatusId")
                        .HasColumnType("int")
                        .HasColumnName("StatusID");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("StatusId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("PrzykladoweKolokwium2_1.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Name");

                    b.Property<decimal>("Price")
                        .HasPrecision(10, 2)
                        .HasColumnType("decimal(10,2)")
                        .HasColumnName("Price");

                    b.HasKey("Id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("PrzykladoweKolokwium2_1.Models.ProductOrder", b =>
                {
                    b.Property<int>("ProductId")
                        .HasColumnType("int")
                        .HasColumnName("ProductID");

                    b.Property<int>("OrderId")
                        .HasColumnType("int")
                        .HasColumnName("OrderID");

                    b.Property<int>("Amount")
                        .HasColumnType("int")
                        .HasColumnName("Amount");

                    b.HasKey("ProductId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("Product_Order");
                });

            modelBuilder.Entity("PrzykladoweKolokwium2_1.Models.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Name");

                    b.HasKey("Id");

                    b.ToTable("Status");
                });

            modelBuilder.Entity("PrzykladoweKolokwium2_1.Models.Order", b =>
                {
                    b.HasOne("PrzykladoweKolokwium2_1.Models.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrzykladoweKolokwium2_1.Models.Status", "Status")
                        .WithMany("Orders")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("PrzykladoweKolokwium2_1.Models.ProductOrder", b =>
                {
                    b.HasOne("PrzykladoweKolokwium2_1.Models.Order", "Order")
                        .WithMany("ProductOrders")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PrzykladoweKolokwium2_1.Models.Product", "Product")
                        .WithMany("ProductOrders")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("PrzykladoweKolokwium2_1.Models.Client", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("PrzykladoweKolokwium2_1.Models.Order", b =>
                {
                    b.Navigation("ProductOrders");
                });

            modelBuilder.Entity("PrzykladoweKolokwium2_1.Models.Product", b =>
                {
                    b.Navigation("ProductOrders");
                });

            modelBuilder.Entity("PrzykladoweKolokwium2_1.Models.Status", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}

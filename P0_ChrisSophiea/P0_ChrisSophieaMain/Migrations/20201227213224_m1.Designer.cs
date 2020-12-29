﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using P0_ChrisSophiea;

namespace Project0.Migrations
{
    [DbContext(typeof(DAOUtility))]
    [Migration("20201227213224_m1")]
    partial class m1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("ItemPurchase", b =>
                {
                    b.Property<int>("Item1ItemId")
                        .HasColumnType("int");

                    b.Property<int>("PurchasesPurchaseId")
                        .HasColumnType("int");

                    b.HasKey("Item1ItemId", "PurchasesPurchaseId");

                    b.HasIndex("PurchasesPurchaseId");

                    b.ToTable("ItemPurchase");
                });

            modelBuilder.Entity("P0_ChrisSophiea.Customer", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Fname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Lname")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CustomerId");

                    b.ToTable("customers");
                });

            modelBuilder.Entity("P0_ChrisSophiea.Inventory", b =>
                {
                    b.Property<int>("InventoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("InventoryAmount")
                        .HasColumnType("int");

                    b.Property<int>("Item1Id")
                        .HasColumnType("int");

                    b.Property<int>("Store1Id")
                        .HasColumnType("int");

                    b.HasKey("InventoryId");

                    b.HasIndex("Item1Id");

                    b.HasIndex("Store1Id");

                    b.ToTable("inventories");
                });

            modelBuilder.Entity("P0_ChrisSophiea.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ItemDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ItemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ItemPrice")
                        .HasColumnType("float");

                    b.Property<string>("ItemType")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ItemId");

                    b.ToTable("items");
                });

            modelBuilder.Entity("P0_ChrisSophiea.Purchase", b =>
                {
                    b.Property<int>("PurchaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Customer1Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("PurchaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Store1Id")
                        .HasColumnType("int");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("PurchaseId");

                    b.HasIndex("Customer1Id");

                    b.HasIndex("Store1Id");

                    b.ToTable("purchases");
                });

            modelBuilder.Entity("P0_ChrisSophiea.Store", b =>
                {
                    b.Property<int>("StoreId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StoreAddress")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StoreId");

                    b.ToTable("stores");
                });

            modelBuilder.Entity("ItemPurchase", b =>
                {
                    b.HasOne("P0_ChrisSophiea.Item", null)
                        .WithMany()
                        .HasForeignKey("Item1ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("P0_ChrisSophiea.Purchase", null)
                        .WithMany()
                        .HasForeignKey("PurchasesPurchaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("P0_ChrisSophiea.Inventory", b =>
                {
                    b.HasOne("P0_ChrisSophiea.Item", "Item1")
                        .WithMany("Inventories")
                        .HasForeignKey("Item1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("P0_ChrisSophiea.Store", "Store1")
                        .WithMany("Inventories")
                        .HasForeignKey("Store1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item1");

                    b.Navigation("Store1");
                });

            modelBuilder.Entity("P0_ChrisSophiea.Purchase", b =>
                {
                    b.HasOne("P0_ChrisSophiea.Customer", "Customer1")
                        .WithMany("Purchases")
                        .HasForeignKey("Customer1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("P0_ChrisSophiea.Store", "Store1")
                        .WithMany("Purchases")
                        .HasForeignKey("Store1Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer1");

                    b.Navigation("Store1");
                });

            modelBuilder.Entity("P0_ChrisSophiea.Customer", b =>
                {
                    b.Navigation("Purchases");
                });

            modelBuilder.Entity("P0_ChrisSophiea.Item", b =>
                {
                    b.Navigation("Inventories");
                });

            modelBuilder.Entity("P0_ChrisSophiea.Store", b =>
                {
                    b.Navigation("Inventories");

                    b.Navigation("Purchases");
                });
#pragma warning restore 612, 618
        }
    }
}

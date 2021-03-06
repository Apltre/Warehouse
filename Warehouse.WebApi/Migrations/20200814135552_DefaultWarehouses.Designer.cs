﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Warehouse.Data;

namespace Warehouse.WebApi.Migrations
{
    [DbContext(typeof(WarehouseContext))]
    [Migration("20200814135552_DefaultWarehouses")]
    partial class DefaultWarehouses
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Warehouse.Data.Entities.Terminal", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnName("id")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_on")
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool>("IsActive")
                        .HasColumnName("is_active")
                        .HasColumnType("boolean");

                    b.Property<string>("WarehouseId")
                        .IsRequired()
                        .HasColumnName("warehouse_id")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_terminals");

                    b.HasIndex("WarehouseId")
                        .HasName("ix_terminals_warehouse_id");

                    b.ToTable("terminals");
                });

            modelBuilder.Entity("Warehouse.Data.Entities.Warehous", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnName("id")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("created_on")
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("text");

                    b.HasKey("Id")
                        .HasName("pk_warehouses");

                    b.ToTable("warehouses");
                });

            modelBuilder.Entity("Warehouse.Data.Entities.Terminal", b =>
                {
                    b.HasOne("Warehouse.Data.Entities.Warehous", "Warehouse")
                        .WithMany("Terminals")
                        .HasForeignKey("WarehouseId")
                        .HasConstraintName("fk_terminals_warehouses_warehouse_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

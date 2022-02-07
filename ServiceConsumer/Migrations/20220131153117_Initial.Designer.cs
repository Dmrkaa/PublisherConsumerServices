﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServiceConsumer.Data;

namespace ServiceConsumer.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20220131153117_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ServiceConsumer.Models.Organization", b =>
                {
                    b.Property<Guid>("OrganizationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("OrganizationID");

                    b.ToTable("Organizations");

                    b.HasData(
                        new
                        {
                            OrganizationID = new Guid("ee6afa50-35fc-4e89-82c1-1ad0f953aae7"),
                            Name = "Marvel"
                        },
                        new
                        {
                            OrganizationID = new Guid("a2033d2d-8cf8-42e5-be9b-11049700fdc1"),
                            Name = "Universal"
                        });
                });

            modelBuilder.Entity("ServiceConsumer.Models.User", b =>
                {
                    b.Property<Guid>("UserID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("MiddleName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid?>("OrganizationID")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("OrganizationInfoKey")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserID");

                    b.HasIndex("OrganizationInfoKey");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserID = new Guid("f3af8321-c917-4d3b-911e-547e28102c90"),
                            Email = "abc@gmail.kz",
                            LastName = "Hohland",
                            MiddleName = "Ivanovich",
                            Name = "Tom"
                        },
                        new
                        {
                            UserID = new Guid("e9aefbfd-45e9-4fb1-bd0d-ebfe36ec6a4f"),
                            Email = "WolfAlice@outllok.com",
                            LastName = "Wolf",
                            Name = "Alice"
                        });
                });

            modelBuilder.Entity("ServiceConsumer.Models.User", b =>
                {
                    b.HasOne("ServiceConsumer.Models.Organization", "Organization")
                        .WithMany("Users")
                        .HasForeignKey("OrganizationInfoKey");

                    b.Navigation("Organization");
                });

            modelBuilder.Entity("ServiceConsumer.Models.Organization", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}

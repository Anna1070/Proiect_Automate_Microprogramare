﻿// <auto-generated />
using System;
using Colectare_pubela.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Colectare_pubela.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250321155120_DumpsterAndModelUpdate")]
    partial class DumpsterAndModelUpdate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.3");

            modelBuilder.Entity("Colectare_pubela.Models.Cetateni", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CNP")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Cetateni");
                });

            modelBuilder.Entity("Colectare_pubela.Models.Colectari", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT")
                        .HasDefaultValueSql("NEWID()");

                    b.Property<DateTime>("CollectionTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("TagId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Colectari");
                });

            modelBuilder.Entity("Colectare_pubela.Models.Pubela", b =>
                {
                    b.Property<string>("TagId")
                        .HasColumnType("TEXT");

                    b.HasKey("TagId");

                    b.ToTable("Pubela");
                });

            modelBuilder.Entity("Colectare_pubela.Models.PubeleCetateni", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<Guid>("IdCetatean")
                        .HasColumnType("TEXT");

                    b.Property<string>("TagId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("IdCetatean");

                    b.HasIndex("TagId");

                    b.ToTable("PubeleCetateni");
                });

            modelBuilder.Entity("Colectare_pubela.Models.PubeleCetateni", b =>
                {
                    b.HasOne("Colectare_pubela.Models.Cetateni", "Cetatean")
                        .WithMany()
                        .HasForeignKey("IdCetatean")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Colectare_pubela.Models.Pubela", "Pubela")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cetatean");

                    b.Navigation("Pubela");
                });
#pragma warning restore 612, 618
        }
    }
}

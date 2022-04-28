﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PrimerParcialSanMartin.Models;

namespace PrimerParcialSanMartin.Migrations
{
    [DbContext(typeof(AcademiaDbContext))]
    [Migration("20211107181147_Cursos")]
    partial class Cursos
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PrimerParcialSanMartin.Models.Curso", b =>
                {
                    b.Property<int?>("CodigoCurso")
                        .HasColumnType("int");

                    b.Property<int?>("Duracion")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("FotoRuta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("ProfesorACargo")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("TipoCursadaCodigo")
                        .HasColumnType("int");

                    b.HasKey("CodigoCurso");

                    b.HasIndex("TipoCursadaCodigo");

                    b.ToTable("Cursos");
                });

            modelBuilder.Entity("PrimerParcialSanMartin.Models.TipoCursada", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Codigo");

                    b.ToTable("TipoCursadas");
                });

            modelBuilder.Entity("PrimerParcialSanMartin.Models.Curso", b =>
                {
                    b.HasOne("PrimerParcialSanMartin.Models.TipoCursada", "TipoCursada")
                        .WithMany()
                        .HasForeignKey("TipoCursadaCodigo");

                    b.Navigation("TipoCursada");
                });
#pragma warning restore 612, 618
        }
    }
}
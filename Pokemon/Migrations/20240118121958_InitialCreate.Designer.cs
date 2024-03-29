﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Pokemon.Data;

#nullable disable

namespace Pokemon.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240118121958_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Pokemon.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("Pokemon.Models.CategoriaPokemon", b =>
                {
                    b.Property<int>("IdPokemon")
                        .HasColumnType("int");

                    b.Property<int>("IdCategoria")
                        .HasColumnType("int");

                    b.HasKey("IdPokemon", "IdCategoria");

                    b.HasIndex("IdCategoria");

                    b.ToTable("CategoriaPokemon");
                });

            modelBuilder.Entity("Pokemon.Models.Critico", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimerNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Critico");
                });

            modelBuilder.Entity("Pokemon.Models.Entrenador", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Apellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gimnasio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PaisId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PaisId");

                    b.ToTable("Entrenador");
                });

            modelBuilder.Entity("Pokemon.Models.EntrenadorPokemon", b =>
                {
                    b.Property<int>("IdPokemon")
                        .HasColumnType("int");

                    b.Property<int>("IdEntrenador")
                        .HasColumnType("int");

                    b.HasKey("IdPokemon", "IdEntrenador");

                    b.HasIndex("IdEntrenador");

                    b.ToTable("EntrenadorPokemon");
                });

            modelBuilder.Entity("Pokemon.Models.Pais", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pais");
                });

            modelBuilder.Entity("Pokemon.Models.PokemoN", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PokemoN");
                });

            modelBuilder.Entity("Pokemon.Models.Reseña", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("CriticoId")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PokemoNId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CriticoId");

                    b.HasIndex("PokemoNId");

                    b.ToTable("Reseñas");
                });

            modelBuilder.Entity("Pokemon.Models.CategoriaPokemon", b =>
                {
                    b.HasOne("Pokemon.Models.Categoria", "Categoria")
                        .WithMany("CategoriaPokemon")
                        .HasForeignKey("IdCategoria")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pokemon.Models.PokemoN", "PokemoN")
                        .WithMany("CategoriaPokemon")
                        .HasForeignKey("IdPokemon")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categoria");

                    b.Navigation("PokemoN");
                });

            modelBuilder.Entity("Pokemon.Models.Entrenador", b =>
                {
                    b.HasOne("Pokemon.Models.Pais", "Pais")
                        .WithMany("Entrenadores")
                        .HasForeignKey("PaisId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pais");
                });

            modelBuilder.Entity("Pokemon.Models.EntrenadorPokemon", b =>
                {
                    b.HasOne("Pokemon.Models.Entrenador", "Entrenador")
                        .WithMany("EntrenadorPokemon")
                        .HasForeignKey("IdEntrenador")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pokemon.Models.PokemoN", "PokemoN")
                        .WithMany("EntrenadorPokemon")
                        .HasForeignKey("IdPokemon")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entrenador");

                    b.Navigation("PokemoN");
                });

            modelBuilder.Entity("Pokemon.Models.Reseña", b =>
                {
                    b.HasOne("Pokemon.Models.Critico", "Critico")
                        .WithMany("Reseñas")
                        .HasForeignKey("CriticoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Pokemon.Models.PokemoN", "PokemoN")
                        .WithMany("Reseñas")
                        .HasForeignKey("PokemoNId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Critico");

                    b.Navigation("PokemoN");
                });

            modelBuilder.Entity("Pokemon.Models.Categoria", b =>
                {
                    b.Navigation("CategoriaPokemon");
                });

            modelBuilder.Entity("Pokemon.Models.Critico", b =>
                {
                    b.Navigation("Reseñas");
                });

            modelBuilder.Entity("Pokemon.Models.Entrenador", b =>
                {
                    b.Navigation("EntrenadorPokemon");
                });

            modelBuilder.Entity("Pokemon.Models.Pais", b =>
                {
                    b.Navigation("Entrenadores");
                });

            modelBuilder.Entity("Pokemon.Models.PokemoN", b =>
                {
                    b.Navigation("CategoriaPokemon");

                    b.Navigation("EntrenadorPokemon");

                    b.Navigation("Reseñas");
                });
#pragma warning restore 612, 618
        }
    }
}

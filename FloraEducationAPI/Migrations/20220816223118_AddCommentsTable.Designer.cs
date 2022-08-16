﻿// <auto-generated />
using System;
using FloraEducationAPI.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FloraEducationAPI.Migrations
{
    [DbContext(typeof(FloraEducationDbContext))]
    [Migration("20220816223118_AddCommentsTable")]
    partial class AddCommentsTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.28")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("FloraEducationAPI.Domain.Models.Authentication.User", b =>
                {
                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.HasKey("Username");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FloraEducationAPI.Domain.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AuthorUsername")
                        .HasColumnType("text");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<Guid?>("PlantId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AuthorUsername");

                    b.HasIndex("PlantId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("FloraEducationAPI.Domain.Models.Plant", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Maintenance")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Predispositions")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Plants");
                });

            modelBuilder.Entity("FloraEducationAPI.Domain.Models.Comment", b =>
                {
                    b.HasOne("FloraEducationAPI.Domain.Models.Authentication.User", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorUsername");

                    b.HasOne("FloraEducationAPI.Domain.Models.Plant", "Plant")
                        .WithMany()
                        .HasForeignKey("PlantId");
                });
#pragma warning restore 612, 618
        }
    }
}

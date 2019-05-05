﻿// <auto-generated />
using System;
using Fitter.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fitter.DAL.Migrations
{
    [DbContext(typeof(FitterDbContext))]
    [Migration("20190429191450_tag_attachment_removed")]
    partial class tag_attachment_removed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Fitter.DAL.Entity.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Created");

                    b.Property<Guid?>("CurrentAuthorId");

                    b.Property<Guid>("CurrentPostId");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CurrentAuthorId");

                    b.HasIndex("CurrentPostId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("Fitter.DAL.Entity.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Created");

                    b.Property<Guid?>("CurrentAuthorId");

                    b.Property<Guid>("CurrentTeamId");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CurrentAuthorId");

                    b.HasIndex("CurrentTeamId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("Fitter.DAL.Entity.Team", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("AdminId");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("AdminId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Fitter.DAL.Entity.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<string>("Password");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Fitter.DAL.Entity.UsersInTeam", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("TeamId");

                    b.Property<Guid>("Id");

                    b.HasKey("UserId", "TeamId");

                    b.HasIndex("TeamId");

                    b.ToTable("UsersInTeams");
                });

            modelBuilder.Entity("Fitter.DAL.Entity.Comment", b =>
                {
                    b.HasOne("Fitter.DAL.Entity.User", "Author")
                        .WithMany("Comments")
                        .HasForeignKey("CurrentAuthorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Fitter.DAL.Entity.Post", "Post")
                        .WithMany("Comments")
                        .HasForeignKey("CurrentPostId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fitter.DAL.Entity.Post", b =>
                {
                    b.HasOne("Fitter.DAL.Entity.User", "Author")
                        .WithMany("Posts")
                        .HasForeignKey("CurrentAuthorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("Fitter.DAL.Entity.Team", "Team")
                        .WithMany("Posts")
                        .HasForeignKey("CurrentTeamId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fitter.DAL.Entity.Team", b =>
                {
                    b.HasOne("Fitter.DAL.Entity.User", "Admin")
                        .WithMany()
                        .HasForeignKey("AdminId");
                });

            modelBuilder.Entity("Fitter.DAL.Entity.UsersInTeam", b =>
                {
                    b.HasOne("Fitter.DAL.Entity.Team", "Team")
                        .WithMany("UsersInTeams")
                        .HasForeignKey("TeamId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Fitter.DAL.Entity.User", "User")
                        .WithMany("UsersInTeams")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
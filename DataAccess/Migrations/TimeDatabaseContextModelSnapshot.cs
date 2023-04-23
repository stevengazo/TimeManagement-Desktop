﻿// <auto-generated />
using System;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccess.Migrations
{
    [DbContext(typeof(TimeDatabaseContext))]
    partial class TimeDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Models.CategoryItem", b =>
                {
                    b.Property<int>("CategoryItemId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryItemId");

                    b.ToTable("CategoryItems");
                });

            modelBuilder.Entity("Models.PriorityItem", b =>
                {
                    b.Property<int>("PriorityItemId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PriorityItemId");

                    b.ToTable("PriorityItems");
                });

            modelBuilder.Entity("Models.StatusItem", b =>
                {
                    b.Property<int>("StatusItemId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StatusItemId");

                    b.ToTable("StatusItems");
                });

            modelBuilder.Entity("Models.TaskItem", b =>
                {
                    b.Property<int>("TaskItemId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryItemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsEnable")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastEditionDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PriorityItemId")
                        .HasColumnType("int");

                    b.Property<int>("StatusItemId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TaskItemId");

                    b.HasIndex("CategoryItemId");

                    b.HasIndex("PriorityItemId");

                    b.HasIndex("StatusItemId");

                    b.HasIndex("UserId");

                    b.ToTable("TaskItems");
                });

            modelBuilder.Entity("Models.TimeItem", b =>
                {
                    b.Property<int>("TimeItemId")
                        .HasColumnType("int");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TaskItemId")
                        .HasColumnType("int");

                    b.HasKey("TimeItemId");

                    b.HasIndex("TaskItemId");

                    b.ToTable("TimeItems");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsArchive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEnable")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Models.TaskItem", b =>
                {
                    b.HasOne("Models.CategoryItem", "CategoryItem")
                        .WithMany("TaskItems")
                        .HasForeignKey("CategoryItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.PriorityItem", null)
                        .WithMany("TaskItems")
                        .HasForeignKey("PriorityItemId");

                    b.HasOne("Models.StatusItem", "StatusItem")
                        .WithMany("TaskItems")
                        .HasForeignKey("StatusItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.User", "User")
                        .WithMany("TaskItems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CategoryItem");

                    b.Navigation("StatusItem");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Models.TimeItem", b =>
                {
                    b.HasOne("Models.TaskItem", "TaskItem")
                        .WithMany("TimeItems")
                        .HasForeignKey("TaskItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TaskItem");
                });

            modelBuilder.Entity("Models.CategoryItem", b =>
                {
                    b.Navigation("TaskItems");
                });

            modelBuilder.Entity("Models.PriorityItem", b =>
                {
                    b.Navigation("TaskItems");
                });

            modelBuilder.Entity("Models.StatusItem", b =>
                {
                    b.Navigation("TaskItems");
                });

            modelBuilder.Entity("Models.TaskItem", b =>
                {
                    b.Navigation("TimeItems");
                });

            modelBuilder.Entity("Models.User", b =>
                {
                    b.Navigation("TaskItems");
                });
#pragma warning restore 612, 618
        }
    }
}

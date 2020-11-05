﻿// <auto-generated />
using System;
using ApplicationFitness;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApplicationFitness.Migrations
{
    [DbContext(typeof(FitnessAppContext))]
    [Migration("20201104160036_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApplicationFitness.Domain.Models.Dish", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("ApplicationFitness.Domain.Models.DishDay", b =>
                {
                    b.Property<int>("DishId")
                        .HasColumnType("int");

                    b.Property<int>("ProgramDayId")
                        .HasColumnType("int");

                    b.HasKey("DishId", "ProgramDayId");

                    b.HasIndex("ProgramDayId");

                    b.ToTable("DishDays");
                });

            modelBuilder.Entity("ApplicationFitness.Domain.Models.ProgramDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ScheduleId")
                        .HasColumnType("int");

                    b.Property<string>("TrainingLink")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleId");

                    b.ToTable("ProgramDays");
                });

            modelBuilder.Entity("ApplicationFitness.Domain.Models.ProgramSchedule", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FitnessProgramDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FitnessProgramName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NutritionProgramDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NutritionProgramName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProgramTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProgramTypeId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("ApplicationFitness.Domain.Models.ProgramType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Types");
                });

            modelBuilder.Entity("ApplicationFitness.Domain.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("ApplicationFitness.Domain.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("ApplicationFitness.Domain.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Height")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.Property<int>("YearOfBirth")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ApplicationFitness.Domain.Models.UserSchedule", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ProgramScheduleId")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ProgramScheduleId");

                    b.HasIndex("ProgramScheduleId");

                    b.ToTable("UsersPrograms");
                });

            modelBuilder.Entity("ApplicationFitness.Domain.Models.DishDay", b =>
                {
                    b.HasOne("ApplicationFitness.Domain.Models.ProgramDay", "ProgramDay")
                        .WithMany("DishDays")
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationFitness.Domain.Models.Dish", "Dish")
                        .WithMany("DishDays")
                        .HasForeignKey("ProgramDayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicationFitness.Domain.Models.ProgramDay", b =>
                {
                    b.HasOne("ApplicationFitness.Domain.Models.ProgramSchedule", "Schedule")
                        .WithMany("ProgramWeek")
                        .HasForeignKey("ScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicationFitness.Domain.Models.ProgramSchedule", b =>
                {
                    b.HasOne("ApplicationFitness.Domain.Models.ProgramType", "ProgramType")
                        .WithMany("ProgramSchedules")
                        .HasForeignKey("ProgramTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicationFitness.Domain.Models.Review", b =>
                {
                    b.HasOne("ApplicationFitness.Domain.Models.User", "User")
                        .WithMany("Reviews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicationFitness.Domain.Models.User", b =>
                {
                    b.HasOne("ApplicationFitness.Domain.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ApplicationFitness.Domain.Models.UserSchedule", b =>
                {
                    b.HasOne("ApplicationFitness.Domain.Models.User", "User")
                        .WithMany("UserSchedules")
                        .HasForeignKey("ProgramScheduleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ApplicationFitness.Domain.Models.ProgramSchedule", "ProgramSchedule")
                        .WithMany("UserSchedules")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

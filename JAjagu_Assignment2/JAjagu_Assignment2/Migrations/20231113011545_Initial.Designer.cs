﻿// <auto-generated />
using System;
using JAjagu_Assignment2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JAjagu_Assignment2.Migrations
{
    [DbContext(typeof(CourseRegistrationDbContext))]
    [Migration("20231113011545_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JAjagu_Assignment2.Entities.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CourseId"));

                    b.Property<string>("CourseInstructor")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumberOfStudent")
                        .HasColumnType("int");

                    b.Property<string>("RoomNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("StartDate")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");

                    b.HasData(
                        new
                        {
                            CourseId = 100,
                            CourseInstructor = "Jasveen Kaur",
                            CourseName = "Programing Concepts 1",
                            NumberOfStudent = 2,
                            RoomNumber = "1C09",
                            StartDate = new DateTime(2023, 10, 18, 20, 15, 45, 342, DateTimeKind.Local).AddTicks(9113)
                        },
                        new
                        {
                            CourseId = 101,
                            CourseInstructor = "Yash Shah",
                            CourseName = "System Analysis",
                            NumberOfStudent = 2,
                            RoomNumber = "4G25",
                            StartDate = new DateTime(2023, 11, 2, 20, 15, 45, 342, DateTimeKind.Local).AddTicks(9187)
                        },
                        new
                        {
                            CourseId = 102,
                            CourseInstructor = "Rick Guzik",
                            CourseName = "UX/UI Experience",
                            NumberOfStudent = 2,
                            RoomNumber = "2B25",
                            StartDate = new DateTime(2023, 10, 13, 20, 15, 45, 342, DateTimeKind.Local).AddTicks(9191)
                        });
                });

            modelBuilder.Entity("JAjagu_Assignment2.Entities.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StudentId"));

                    b.Property<int>("CourseId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("StudentEmail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("StudentId");

                    b.HasIndex("CourseId");

                    b.ToTable("Students");

                    b.HasData(
                        new
                        {
                            StudentId = 1,
                            CourseId = 100,
                            Status = "ConfirmationMessageNotSent",
                            StudentEmail = "barts@gmail.com",
                            StudentName = "Bart Simpson"
                        },
                        new
                        {
                            StudentId = 2,
                            CourseId = 100,
                            Status = "ConfirmationMessageNotSent",
                            StudentEmail = "lbart@yahoo.com",
                            StudentName = "Lisa Bart"
                        },
                        new
                        {
                            StudentId = 3,
                            CourseId = 101,
                            Status = "ConfirmationMessageNotSent",
                            StudentEmail = "culjon@yahoo.com",
                            StudentName = "Ajagu"
                        },
                        new
                        {
                            StudentId = 4,
                            CourseId = 101,
                            Status = "ConfirmationMessageNotSent",
                            StudentEmail = "culjon@yahoo.com",
                            StudentName = "Amos Mars"
                        },
                        new
                        {
                            StudentId = 5,
                            CourseId = 102,
                            Status = "ConfirmationMessageNotSent",
                            StudentEmail = "culjon@yahoo.com",
                            StudentName = "James Cordon"
                        },
                        new
                        {
                            StudentId = 6,
                            CourseId = 102,
                            Status = "ConfirmationMessageNotSent",
                            StudentEmail = "culjon@yahoo.com",
                            StudentName = "Jordan Holmes"
                        });
                });

            modelBuilder.Entity("JAjagu_Assignment2.Entities.Student", b =>
                {
                    b.HasOne("JAjagu_Assignment2.Entities.Course", "Course")
                        .WithMany("Students")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");
                });

            modelBuilder.Entity("JAjagu_Assignment2.Entities.Course", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}

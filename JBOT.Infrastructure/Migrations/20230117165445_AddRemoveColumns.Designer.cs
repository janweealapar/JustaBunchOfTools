﻿// <auto-generated />
using System;
using JBOT.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JBOT.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20230117165445_AddRemoveColumns")]
    partial class AddRemoveColumns
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JBOT.Domain.Entities.Operator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Operators");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Equal"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Not Equal"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Greater Than"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Greater Than Or Equal To"
                        },
                        new
                        {
                            Id = 5,
                            Name = "LessThan"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Less Than Or Equal To"
                        });
                });

            modelBuilder.Entity("JBOT.Domain.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Failed"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Success"
                        });
                });

            modelBuilder.Entity("JBOT.Domain.Entities.UnitTest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DatabaseId")
                        .HasColumnType("int");

                    b.Property<string>("DatabaseName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateRecorded")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ObjectId")
                        .HasColumnType("int");

                    b.Property<string>("ObjectName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ObjectType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RecordUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.Property<string>("TestName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("StatusId");

                    b.ToTable("UnitTests");
                });

            modelBuilder.Entity("JBOT.Domain.Entities.UnitTestAssertation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ActualValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateRecorded")
                        .HasColumnType("datetime2");

                    b.Property<string>("ExpectedValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("OperatorId")
                        .HasColumnType("int");

                    b.Property<string>("RecordUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("StatusId")
                        .HasColumnType("int");

                    b.Property<int?>("UnitTestId")
                        .HasColumnType("int");

                    b.Property<int?>("UnitTestParameterId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OperatorId");

                    b.HasIndex("StatusId");

                    b.HasIndex("UnitTestId");

                    b.HasIndex("UnitTestParameterId");

                    b.ToTable("UnitTestAssertation");
                });

            modelBuilder.Entity("JBOT.Domain.Entities.UnitTestParameter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DateModified")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateRecorded")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsOutput")
                        .HasColumnType("bit");

                    b.Property<int>("MaxLength")
                        .HasColumnType("int");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ParameterId")
                        .HasColumnType("int");

                    b.Property<string>("ParameterName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ParameterType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Precision")
                        .HasColumnType("int");

                    b.Property<string>("RecordUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Scale")
                        .HasColumnType("int");

                    b.Property<int?>("UnitTestId")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("UnitTestId");

                    b.ToTable("UnitTestParameter");
                });

            modelBuilder.Entity("JBOT.Domain.Entities.UnitTest", b =>
                {
                    b.HasOne("JBOT.Domain.Entities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("JBOT.Domain.Entities.UnitTestAssertation", b =>
                {
                    b.HasOne("JBOT.Domain.Entities.Operator", "Operator")
                        .WithMany()
                        .HasForeignKey("OperatorId");

                    b.HasOne("JBOT.Domain.Entities.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.HasOne("JBOT.Domain.Entities.UnitTest", "UnitTest")
                        .WithMany("Assertations")
                        .HasForeignKey("UnitTestId");

                    b.HasOne("JBOT.Domain.Entities.UnitTestParameter", "UnitTestParameter")
                        .WithMany()
                        .HasForeignKey("UnitTestParameterId");

                    b.Navigation("Operator");

                    b.Navigation("Status");

                    b.Navigation("UnitTest");

                    b.Navigation("UnitTestParameter");
                });

            modelBuilder.Entity("JBOT.Domain.Entities.UnitTestParameter", b =>
                {
                    b.HasOne("JBOT.Domain.Entities.UnitTest", "UnitTest")
                        .WithMany("Parameters")
                        .HasForeignKey("UnitTestId");

                    b.Navigation("UnitTest");
                });

            modelBuilder.Entity("JBOT.Domain.Entities.UnitTest", b =>
                {
                    b.Navigation("Assertations");

                    b.Navigation("Parameters");
                });
#pragma warning restore 612, 618
        }
    }
}

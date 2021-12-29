﻿// <auto-generated />
using System;
using MCC61_API_Project.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MCC61_API_Project.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MCC61_API_Project.Models.Account", b =>
                {
                    b.Property<string>("NIK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("ExpiredDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OTP")
                        .HasColumnType("int");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("isUsed")
                        .HasColumnType("bit");

                    b.HasKey("NIK");

                    b.ToTable("tb_m_account");
                });

            modelBuilder.Entity("MCC61_API_Project.Models.AccountRole", b =>
                {
                    b.Property<int>("AccountRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NIK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("AccountRoleId");

                    b.HasIndex("NIK");

                    b.HasIndex("RoleId");

                    b.ToTable("tb_tr_accountRole");
                });

            modelBuilder.Entity("MCC61_API_Project.Models.Education", b =>
                {
                    b.Property<int>("EducationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Degree")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("GPA")
                        .HasColumnType("real");

                    b.Property<int>("UniversityID")
                        .HasColumnType("int");

                    b.HasKey("EducationID");

                    b.HasIndex("UniversityID");

                    b.ToTable("tb_m_education");
                });

            modelBuilder.Entity("MCC61_API_Project.Models.Employee", b =>
                {
                    b.Property<string>("NIK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Salary")
                        .HasColumnType("int");

                    b.HasKey("NIK");

                    b.ToTable("tb_m_employee");
                });

            modelBuilder.Entity("MCC61_API_Project.Models.Profiling", b =>
                {
                    b.Property<string>("NIK")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("EducationId")
                        .HasColumnType("int");

                    b.HasKey("NIK");

                    b.HasIndex("EducationId");

                    b.ToTable("tb_tr_profiling");
                });

            modelBuilder.Entity("MCC61_API_Project.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("tb_m_Role");
                });

            modelBuilder.Entity("MCC61_API_Project.Models.University", b =>
                {
                    b.Property<int>("UniversityID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UniversityID");

                    b.ToTable("tb_m_university");
                });

            modelBuilder.Entity("MCC61_API_Project.Models.Account", b =>
                {
                    b.HasOne("MCC61_API_Project.Models.Employee", "Employee")
                        .WithOne("Account")
                        .HasForeignKey("MCC61_API_Project.Models.Account", "NIK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("MCC61_API_Project.Models.AccountRole", b =>
                {
                    b.HasOne("MCC61_API_Project.Models.Account", "Account")
                        .WithMany("AccountRoles")
                        .HasForeignKey("NIK");

                    b.HasOne("MCC61_API_Project.Models.Role", "Role")
                        .WithMany("AccountRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("MCC61_API_Project.Models.Education", b =>
                {
                    b.HasOne("MCC61_API_Project.Models.University", "University")
                        .WithMany("Educations")
                        .HasForeignKey("UniversityID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("University");
                });

            modelBuilder.Entity("MCC61_API_Project.Models.Profiling", b =>
                {
                    b.HasOne("MCC61_API_Project.Models.Education", "Education")
                        .WithMany("Profilings")
                        .HasForeignKey("EducationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MCC61_API_Project.Models.Account", "Account")
                        .WithOne("Profiling")
                        .HasForeignKey("MCC61_API_Project.Models.Profiling", "NIK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");

                    b.Navigation("Education");
                });

            modelBuilder.Entity("MCC61_API_Project.Models.Account", b =>
                {
                    b.Navigation("AccountRoles");

                    b.Navigation("Profiling");
                });

            modelBuilder.Entity("MCC61_API_Project.Models.Education", b =>
                {
                    b.Navigation("Profilings");
                });

            modelBuilder.Entity("MCC61_API_Project.Models.Employee", b =>
                {
                    b.Navigation("Account");
                });

            modelBuilder.Entity("MCC61_API_Project.Models.Role", b =>
                {
                    b.Navigation("AccountRoles");
                });

            modelBuilder.Entity("MCC61_API_Project.Models.University", b =>
                {
                    b.Navigation("Educations");
                });
#pragma warning restore 612, 618
        }
    }
}

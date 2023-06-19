﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Qurrah.Data;

#nullable disable

namespace Qurrah.Data.Migrations
{
    [DbContext(typeof(QurrahDbContext))]
    [Migration("20230618103830_AddCenterUser")]
    partial class AddCenterUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "443b9ab1-91f4-4e43-b6a9-d878e16f9bb3",
                            Name = "Parent",
                            NormalizedName = "Parent"
                        },
                        new
                        {
                            Id = "49fab27d-476e-4b81-b2c7-27282340dace",
                            Name = "Center",
                            NormalizedName = "Center"
                        },
                        new
                        {
                            Id = "113ca503-db17-41a0-b74f-704c85acc2e5",
                            Name = "Administrator",
                            NormalizedName = "Administrator"
                        },
                        new
                        {
                            Id = "3006c5e5-d5f6-438d-92e6-7035502278cc",
                            Name = "CenterApprover",
                            NormalizedName = "CenterApprover"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "a92dd7e3-f9cb-4c4d-ac58-a1cd75e69a43",
                            RoleId = "113ca503-db17-41a0-b74f-704c85acc2e5"
                        },
                        new
                        {
                            UserId = "d95fe55c-2d73-4282-882d-284212ecd035",
                            RoleId = "3006c5e5-d5f6-438d-92e6-7035502278cc"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Qurrah.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FKCreatedByUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte>("FKUserTypeId")
                        .HasColumnType("tinyint");

                    b.Property<DateTime?>("LastModifiedOn")
                        .HasColumnType("datetime2");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("FKCreatedByUserId");

                    b.HasIndex("FKUserTypeId");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "a92dd7e3-f9cb-4c4d-ac58-a1cd75e69a43",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "20b53309-5688-4886-a279-da65086ec66f",
                            CreatedOn = new DateTime(2023, 6, 18, 13, 38, 30, 28, DateTimeKind.Local).AddTicks(6202),
                            Email = "Admin@Qurrah.com",
                            EmailConfirmed = false,
                            FKUserTypeId = (byte)1,
                            LockoutEnabled = false,
                            NormalizedEmail = "Admin@Qurrah.com",
                            NormalizedUserName = "Admin",
                            PasswordHash = "AQAAAAIAAYagAAAAEMUnWcBw7CfKM8pPHBA6xUW0ngG3V6/eqJA4lyC6RM+PMLY3LEWXmPb4z6lbKUir/g==",
                            PhoneNumber = "0543700744",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "2cf9982e-3193-47ac-9f27-6ea3475c309f",
                            TwoFactorEnabled = false,
                            UserName = "Admin"
                        },
                        new
                        {
                            Id = "d95fe55c-2d73-4282-882d-284212ecd035",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "2c78fa7b-0843-43d3-885f-433f0589a7f3",
                            CreatedOn = new DateTime(2023, 6, 18, 13, 38, 30, 89, DateTimeKind.Local).AddTicks(9430),
                            Email = "CenterReviewer@Qurrah.com",
                            EmailConfirmed = false,
                            FKUserTypeId = (byte)2,
                            LockoutEnabled = false,
                            NormalizedEmail = "CenterReviewer@Qurrah.com",
                            NormalizedUserName = "CenterReviewer",
                            PasswordHash = "AQAAAAIAAYagAAAAEPOK88qguTwUOyh79ogwfeGBiNBGaAWhhY/rAU2zNYIVnSgUsEMbqHAA1kBZrVcmAQ==",
                            PhoneNumber = "0543700745",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "1897374d-76f8-4d4a-a7dc-58711cbd4dc1",
                            TwoFactorEnabled = false,
                            UserName = "CenterReviewer"
                        });
                });

            modelBuilder.Entity("Qurrah.Entities.Center", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Center");
                });

            modelBuilder.Entity("Qurrah.Entities.CenterOwnerUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int>("FKCenterId")
                        .HasColumnType("int");

                    b.Property<byte>("FKGenderId")
                        .HasColumnType("tinyint");

                    b.Property<string>("FKUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("FourthName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("IdNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("SecondName")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ThirdName")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("FKCenterId");

                    b.HasIndex("FKGenderId");

                    b.HasIndex("FKUserId")
                        .IsUnique();

                    b.ToTable("CenterOwnerUser");
                });

            modelBuilder.Entity("Qurrah.Entities.FAQ", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AnswerAr")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AnswerEn")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<int>("FKTypeId")
                        .HasColumnType("int");

                    b.Property<string>("QuestionAr")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("QuestionEn")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.HasIndex("FKTypeId");

                    b.ToTable("FAQ");
                });

            modelBuilder.Entity("Qurrah.Entities.FAQType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DescriptionAr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescriptionEn")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DisplayOrder")
                        .HasColumnType("int");

                    b.Property<string>("NameAr")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("NameEn")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.HasKey("Id");

                    b.ToTable("FAQType");
                });

            modelBuilder.Entity("Qurrah.Entities.Gender", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Gender");

                    b.HasData(
                        new
                        {
                            Id = (byte)1,
                            Name = "Male"
                        },
                        new
                        {
                            Id = (byte)2,
                            Name = "Female"
                        });
                });

            modelBuilder.Entity("Qurrah.Entities.ParentUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<byte>("FKGenderId")
                        .HasColumnType("tinyint");

                    b.Property<string>("FKUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("FourthName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("IdNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("SecondName")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("ThirdName")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("Id");

                    b.HasIndex("FKGenderId");

                    b.HasIndex("FKUserId")
                        .IsUnique();

                    b.ToTable("ParentUser");
                });

            modelBuilder.Entity("Qurrah.Entities.UserType", b =>
                {
                    b.Property<byte>("Id")
                        .HasColumnType("tinyint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("UserType");

                    b.HasData(
                        new
                        {
                            Id = (byte)1,
                            Name = "Administrator"
                        },
                        new
                        {
                            Id = (byte)2,
                            Name = "CenterApprover"
                        },
                        new
                        {
                            Id = (byte)3,
                            Name = "Parent"
                        },
                        new
                        {
                            Id = (byte)4,
                            Name = "Center"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Qurrah.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Qurrah.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Qurrah.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Qurrah.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Qurrah.Entities.ApplicationUser", b =>
                {
                    b.HasOne("Qurrah.Entities.ApplicationUser", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("FKCreatedByUserId");

                    b.HasOne("Qurrah.Entities.UserType", "UserType")
                        .WithMany("ApplicationUsers")
                        .HasForeignKey("FKUserTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("CreatedBy");

                    b.Navigation("UserType");
                });

            modelBuilder.Entity("Qurrah.Entities.CenterOwnerUser", b =>
                {
                    b.HasOne("Qurrah.Entities.Center", "Center")
                        .WithMany()
                        .HasForeignKey("FKCenterId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Qurrah.Entities.Gender", "Gender")
                        .WithMany()
                        .HasForeignKey("FKGenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Qurrah.Entities.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("FKUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Center");

                    b.Navigation("Gender");
                });

            modelBuilder.Entity("Qurrah.Entities.FAQ", b =>
                {
                    b.HasOne("Qurrah.Entities.FAQType", "FAQType")
                        .WithMany("FAQs")
                        .HasForeignKey("FKTypeId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("FAQType");
                });

            modelBuilder.Entity("Qurrah.Entities.ParentUser", b =>
                {
                    b.HasOne("Qurrah.Entities.Gender", "Gender")
                        .WithMany("ParentUsers")
                        .HasForeignKey("FKGenderId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Qurrah.Entities.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("FKUserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Gender");
                });

            modelBuilder.Entity("Qurrah.Entities.FAQType", b =>
                {
                    b.Navigation("FAQs");
                });

            modelBuilder.Entity("Qurrah.Entities.Gender", b =>
                {
                    b.Navigation("ParentUsers");
                });

            modelBuilder.Entity("Qurrah.Entities.UserType", b =>
                {
                    b.Navigation("ApplicationUsers");
                });
#pragma warning restore 612, 618
        }
    }
}
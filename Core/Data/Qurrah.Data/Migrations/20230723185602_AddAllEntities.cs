using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Qurrah.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAllEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CenterLicenseStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterLicenseStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CenterStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CenterType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FAQType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Language",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LanguageCulture = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    RTL = table.Column<bool>(type: "bit", nullable: false),
                    Published = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Language", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FAQ",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Question = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Answer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FKTypeId = table.Column<int>(type: "int", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FAQ", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FAQ_FAQType_FKTypeId",
                        column: x => x.FKTypeId,
                        principalTable: "FAQType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FileDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExtension = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FileData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FKFileTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FileDetails_FileType_FKFileTypeId",
                        column: x => x.FKFileTypeId,
                        principalTable: "FileType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CenterLicenseStatusDescription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKStatusId = table.Column<int>(type: "int", nullable: false),
                    FKLanguageId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterLicenseStatusDescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterLicenseStatusDescription_CenterLicenseStatus_FKStatusId",
                        column: x => x.FKStatusId,
                        principalTable: "CenterLicenseStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CenterLicenseStatusDescription_Language_FKLanguageId",
                        column: x => x.FKLanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CenterStatusDescription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKCenterStatusId = table.Column<int>(type: "int", nullable: false),
                    FKLanguageId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CenterStatusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterStatusDescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterStatusDescription_CenterStatus_CenterStatusId",
                        column: x => x.CenterStatusId,
                        principalTable: "CenterStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CenterStatusDescription_Language_FKLanguageId",
                        column: x => x.FKLanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CenterTypeDescription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKCenterTypeId = table.Column<int>(type: "int", nullable: false),
                    FKLanguageId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterTypeDescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterTypeDescription_CenterType_FKCenterTypeId",
                        column: x => x.FKCenterTypeId,
                        principalTable: "CenterType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CenterTypeDescription_Language_FKLanguageId",
                        column: x => x.FKLanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GenderDescription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKGenderId = table.Column<int>(type: "int", nullable: false),
                    FKLanguageId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderDescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GenderDescription_Gender_FKGenderId",
                        column: x => x.FKGenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GenderDescription_Language_FKLanguageId",
                        column: x => x.FKLanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LanguageDescription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKLanguageId = table.Column<int>(type: "int", nullable: false),
                    FKInLanguageId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageDescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LanguageDescription_Language_FKInLanguageId",
                        column: x => x.FKInLanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LanguageDescription_Language_FKLanguageId",
                        column: x => x.FKLanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocalizedProperty",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocaleKeyGroup = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LocaleKey = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    LocaleValue = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    FKLanguageId = table.Column<int>(type: "int", nullable: false),
                    EntityId = table.Column<long>(type: "bigint", nullable: false),
                    IsMultiLine = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizedProperty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalizedProperty_Language_FKLanguageId",
                        column: x => x.FKLanguageId,
                        principalTable: "Language",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ThirdName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    FourthName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    IdNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FKGenderId = table.Column<int>(type: "int", nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FKUserTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FKCreatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_FKCreatedByUserId",
                        column: x => x.FKCreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Gender_FKGenderId",
                        column: x => x.FKGenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_UserType_FKUserTypeId",
                        column: x => x.FKUserTypeId,
                        principalTable: "UserType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Center",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IBAN = table.Column<string>(type: "nvarchar(24)", maxLength: 24, nullable: true),
                    FKCenterTypeId = table.Column<int>(type: "int", nullable: true),
                    FKIBANFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FKCenterStatusId = table.Column<int>(type: "int", nullable: false),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FKStatusUpdatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Center", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Center_AspNetUsers_FKStatusUpdatedByUserId",
                        column: x => x.FKStatusUpdatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Center_CenterStatus_FKCenterStatusId",
                        column: x => x.FKCenterStatusId,
                        principalTable: "CenterStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Center_CenterType_FKCenterTypeId",
                        column: x => x.FKCenterTypeId,
                        principalTable: "CenterType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Center_FileDetails_FKIBANFileId",
                        column: x => x.FKIBANFileId,
                        principalTable: "FileDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CenterLicense",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicenseNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FKFileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FKStatusId = table.Column<int>(type: "int", nullable: false),
                    RejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FKStatusUpdatedByUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterLicense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterLicense_AspNetUsers_FKStatusUpdatedByUserId",
                        column: x => x.FKStatusUpdatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CenterLicense_CenterLicenseStatus_FKStatusId",
                        column: x => x.FKStatusId,
                        principalTable: "CenterLicenseStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CenterLicense_FileDetails_FKFileId",
                        column: x => x.FKFileId,
                        principalTable: "FileDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2552fcee-c887-44e4-8c52-1e9536072db0", null, "Center", "Center" },
                    { "8e566438-3423-4633-a3a1-a186243f1e16", null, "Parent", "Parent" },
                    { "ec177499-cbda-4f46-8b1d-bf501d3d45ab", null, "Administrator", "Administrator" },
                    { "f9262273-caca-4345-a96e-3264d4d2021c", null, "CenterApprover", "CenterApprover" }
                });

            migrationBuilder.InsertData(
                table: "CenterLicenseStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "UnderConsideration" },
                    { 2, "Approved" },
                    { 3, "Rejected" }
                });

            migrationBuilder.InsertData(
                table: "CenterStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "UnderConsideration" },
                    { 2, "Approved" },
                    { 3, "Rejected" }
                });

            migrationBuilder.InsertData(
                table: "CenterType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Dependent" },
                    { 2, "InDependent" }
                });

            migrationBuilder.InsertData(
                table: "FileType",
                columns: new[] { "Id", "ContentType", "Name" },
                values: new object[,]
                {
                    { 1, "application/pdf", "PDF" },
                    { 2, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "DOCX" },
                    { 3, "application/msword", "DOC" },
                    { 4, "image/x-png", "PNG" },
                    { 5, "image/jpeg", "JPG" },
                    { 6, "image/jpeg", "JPEG" }
                });

            migrationBuilder.InsertData(
                table: "Gender",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Male" },
                    { 2, "Female" }
                });

            migrationBuilder.InsertData(
                table: "Language",
                columns: new[] { "Id", "DisplayOrder", "LanguageCulture", "Name", "Published", "RTL" },
                values: new object[,]
                {
                    { 1, 1, "ar-SA", "Arabic", true, true },
                    { 2, 2, "en-US", "English", true, false }
                });

            migrationBuilder.InsertData(
                table: "UserType",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Administrator" },
                    { 2, "CenterApprover" },
                    { 3, "Parent" },
                    { 4, "Center" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "FKCreatedByUserId", "FKGenderId", "FKUserTypeId", "FirstName", "FourthName", "IdNumber", "LastModifiedOn", "LockoutEnabled", "LockoutEnd", "MobileNumber", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecondName", "SecurityStamp", "ThirdName", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "7b9d6e61-c9d1-4882-81fa-9bb9ea23c575", 0, "013906d1-ef76-498f-b2cf-d5a84874b2e9", new DateTime(2023, 7, 23, 21, 56, 2, 457, DateTimeKind.Local).AddTicks(2089), "Admin@Qurrah.com", false, null, 1, 1, "Administrator", "", "", null, false, null, "", "Admin@Qurrah.com", "Admin", "AQAAAAIAAYagAAAAEAWR0PZsliWCAGs5uymDGfOg/yxA9ZkAM78gnnPI6y0SBKY0RFZE5i4r6EjNFI5Wtw==", "0543700744", false, null, "a9e5f96e-5f88-46a1-805e-c60641aa80da", null, false, "Admin" },
                    { "9b7a12a4-de6e-438b-a605-0310b45b5cd6", 0, "cda14ce3-fdc2-42fd-8a9c-d7ab92a6c7a1", new DateTime(2023, 7, 23, 21, 56, 2, 633, DateTimeKind.Local).AddTicks(4976), "CenterReviewer@Qurrah.com", false, null, 1, 2, "Reviewer", "", "", null, false, null, "", "CenterReviewer@Qurrah.com", "CenterReviewer", "AQAAAAIAAYagAAAAEEUVgqZF8rcistKQSG2aomduvOC7xGqyoeSfw2/RVzJDef/wzXXlp7m4KGGDv3uL4w==", "0543700745", false, null, "7fdb8fe6-59d5-4ff0-ba72-d766436ee845", null, false, "CenterReviewer" }
                });

            migrationBuilder.InsertData(
                table: "CenterLicenseStatusDescription",
                columns: new[] { "Id", "Description", "FKLanguageId", "FKStatusId" },
                values: new object[,]
                {
                    { 1, "Under Consideration", 2, 1 },
                    { 2, "قيد الدراسة", 1, 1 },
                    { 3, "Approved", 2, 2 },
                    { 4, "مقبول", 1, 2 },
                    { 5, "Rejected", 2, 3 },
                    { 6, "مرفوض", 1, 3 }
                });

            migrationBuilder.InsertData(
                table: "CenterStatusDescription",
                columns: new[] { "Id", "CenterStatusId", "Description", "FKCenterStatusId", "FKLanguageId" },
                values: new object[,]
                {
                    { 1, null, "Under Consideration", 1, 2 },
                    { 2, null, "قيد الدراسة", 1, 1 },
                    { 3, null, "Approved", 2, 2 },
                    { 4, null, "مقبول", 2, 1 },
                    { 5, null, "Rejected", 3, 2 },
                    { 6, null, "مرفوض", 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "CenterTypeDescription",
                columns: new[] { "Id", "Description", "FKCenterTypeId", "FKLanguageId" },
                values: new object[,]
                {
                    { 1, "InDependent", 2, 2 },
                    { 2, "غير مستقل", 2, 1 },
                    { 3, "Dependent", 1, 2 },
                    { 4, "مستقل", 1, 1 }
                });

            migrationBuilder.InsertData(
                table: "GenderDescription",
                columns: new[] { "Id", "Description", "FKGenderId", "FKLanguageId" },
                values: new object[,]
                {
                    { 1, "ذكر", 1, 1 },
                    { 2, "Male", 1, 2 },
                    { 3, "أنثى", 2, 1 },
                    { 4, "Female", 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "LanguageDescription",
                columns: new[] { "Id", "Description", "FKInLanguageId", "FKLanguageId" },
                values: new object[,]
                {
                    { 1, "العربية", 1, 1 },
                    { 2, "Arabic", 2, 1 },
                    { 3, "الإنجليزية", 1, 2 },
                    { 4, "English", 2, 2 }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "ec177499-cbda-4f46-8b1d-bf501d3d45ab", "7b9d6e61-c9d1-4882-81fa-9bb9ea23c575" },
                    { "f9262273-caca-4345-a96e-3264d4d2021c", "9b7a12a4-de6e-438b-a605-0310b45b5cd6" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FKCreatedByUserId",
                table: "AspNetUsers",
                column: "FKCreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FKGenderId",
                table: "AspNetUsers",
                column: "FKGenderId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FKUserTypeId",
                table: "AspNetUsers",
                column: "FKUserTypeId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Center_FKCenterStatusId",
                table: "Center",
                column: "FKCenterStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Center_FKCenterTypeId",
                table: "Center",
                column: "FKCenterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Center_FKIBANFileId",
                table: "Center",
                column: "FKIBANFileId");

            migrationBuilder.CreateIndex(
                name: "IX_Center_FKStatusUpdatedByUserId",
                table: "Center",
                column: "FKStatusUpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Center_IBAN",
                table: "Center",
                column: "IBAN",
                unique: true,
                filter: "[IBAN] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Center_Name",
                table: "Center",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CenterLicense_FKFileId",
                table: "CenterLicense",
                column: "FKFileId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterLicense_FKStatusId",
                table: "CenterLicense",
                column: "FKStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterLicense_FKStatusUpdatedByUserId",
                table: "CenterLicense",
                column: "FKStatusUpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterLicenseStatus_Name",
                table: "CenterLicenseStatus",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CenterLicenseStatusDescription_FKLanguageId_FKStatusId",
                table: "CenterLicenseStatusDescription",
                columns: new[] { "FKLanguageId", "FKStatusId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CenterLicenseStatusDescription_FKStatusId",
                table: "CenterLicenseStatusDescription",
                column: "FKStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterStatus_Name",
                table: "CenterStatus",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CenterStatusDescription_CenterStatusId",
                table: "CenterStatusDescription",
                column: "CenterStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterStatusDescription_FKLanguageId_FKCenterStatusId",
                table: "CenterStatusDescription",
                columns: new[] { "FKLanguageId", "FKCenterStatusId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CenterType_Name",
                table: "CenterType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CenterTypeDescription_FKCenterTypeId",
                table: "CenterTypeDescription",
                column: "FKCenterTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterTypeDescription_FKLanguageId_FKCenterTypeId",
                table: "CenterTypeDescription",
                columns: new[] { "FKLanguageId", "FKCenterTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FAQ_FKTypeId",
                table: "FAQ",
                column: "FKTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FileDetails_FKFileTypeId",
                table: "FileDetails",
                column: "FKFileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FileType_Name",
                table: "FileType",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gender_Name",
                table: "Gender",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GenderDescription_FKGenderId",
                table: "GenderDescription",
                column: "FKGenderId");

            migrationBuilder.CreateIndex(
                name: "IX_GenderDescription_FKLanguageId_FKGenderId",
                table: "GenderDescription",
                columns: new[] { "FKLanguageId", "FKGenderId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Language_LanguageCulture",
                table: "Language",
                column: "LanguageCulture",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LanguageDescription_FKInLanguageId",
                table: "LanguageDescription",
                column: "FKInLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LanguageDescription_FKLanguageId_FKInLanguageId",
                table: "LanguageDescription",
                columns: new[] { "FKLanguageId", "FKInLanguageId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedProperty_FKLanguageId",
                table: "LocalizedProperty",
                column: "FKLanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedProperty_LocaleKeyGroup",
                table: "LocalizedProperty",
                column: "LocaleKeyGroup");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizedProperty_LocaleKeyGroup_LocaleKey_FKLanguageId_EntityId",
                table: "LocalizedProperty",
                columns: new[] { "LocaleKeyGroup", "LocaleKey", "FKLanguageId", "EntityId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserType_Name",
                table: "UserType",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Center");

            migrationBuilder.DropTable(
                name: "CenterLicense");

            migrationBuilder.DropTable(
                name: "CenterLicenseStatusDescription");

            migrationBuilder.DropTable(
                name: "CenterStatusDescription");

            migrationBuilder.DropTable(
                name: "CenterTypeDescription");

            migrationBuilder.DropTable(
                name: "FAQ");

            migrationBuilder.DropTable(
                name: "GenderDescription");

            migrationBuilder.DropTable(
                name: "LanguageDescription");

            migrationBuilder.DropTable(
                name: "LocalizedProperty");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "FileDetails");

            migrationBuilder.DropTable(
                name: "CenterLicenseStatus");

            migrationBuilder.DropTable(
                name: "CenterStatus");

            migrationBuilder.DropTable(
                name: "CenterType");

            migrationBuilder.DropTable(
                name: "FAQType");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "UserType");

            migrationBuilder.DropTable(
                name: "FileType");
        }
    }
}

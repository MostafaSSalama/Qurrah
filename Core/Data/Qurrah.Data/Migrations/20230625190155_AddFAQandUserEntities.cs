using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Qurrah.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddFAQandUserEntities : Migration
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
                name: "Center",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Center", x => x.Id);
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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
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
                    LocaleValue = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FKLanguageId = table.Column<int>(type: "int", nullable: false),
                    EntityId = table.Column<long>(type: "bigint", nullable: false)
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
                        principalColumn: "Id");
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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CenterOwnerUser",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FKCenterId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ThirdName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    FourthName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FKGenderId = table.Column<int>(type: "int", nullable: false),
                    IdNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FKUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CenterOwnerUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CenterOwnerUser_AspNetUsers_FKUserId",
                        column: x => x.FKUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CenterOwnerUser_Center_FKCenterId",
                        column: x => x.FKCenterId,
                        principalTable: "Center",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CenterOwnerUser_Gender_FKGenderId",
                        column: x => x.FKGenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParentUser",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ThirdName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    FourthName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FKGenderId = table.Column<int>(type: "int", nullable: false),
                    IdNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    FKUserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParentUser_AspNetUsers_FKUserId",
                        column: x => x.FKUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ParentUser_Gender_FKGenderId",
                        column: x => x.FKGenderId,
                        principalTable: "Gender",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1c1fad40-cd3b-426f-b4c0-6205236df447", null, "Administrator", "Administrator" },
                    { "71773683-0f94-4e25-9d97-ffe70827fbb6", null, "Center", "Center" },
                    { "a4196a4c-6713-4214-b3a2-0ef0159ba7e3", null, "Parent", "Parent" },
                    { "abd66b75-dd65-4809-8db5-874c0f8ce275", null, "CenterApprover", "CenterApprover" }
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
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedOn", "Email", "EmailConfirmed", "FKCreatedByUserId", "FKUserTypeId", "LastModifiedOn", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "ad856c8c-eebb-4984-b0ec-5ce7f3efa22c", 0, "e643d9a2-d478-4e5c-8ccf-f9bf32142061", new DateTime(2023, 6, 25, 22, 1, 55, 342, DateTimeKind.Local).AddTicks(7471), "Admin@Qurrah.com", false, null, 1, null, false, null, "Admin@Qurrah.com", "Admin", "AQAAAAIAAYagAAAAEJTE5PrIrSrBthlhc7n16og3Xnir72Sc+ZRYWHO50eRqXjQOt2nbGZK+KpRey196KA==", "0543700744", false, "c2a4accb-0aa3-4cde-af0e-fcc63ca75a45", false, "Admin" },
                    { "b6578ba7-8b09-41c1-83b5-331186006e14", 0, "3a869b20-05ee-4d0b-a25f-b2caf6391cea", new DateTime(2023, 6, 25, 22, 1, 55, 474, DateTimeKind.Local).AddTicks(4718), "CenterReviewer@Qurrah.com", false, null, 2, null, false, null, "CenterReviewer@Qurrah.com", "CenterReviewer", "AQAAAAIAAYagAAAAEOrEm4/zLg7B487c0Wx+WZQH1SK+JiEmA9HgPsg9Z+PuE99fu+OuvMZDs8pv3of0Dw==", "0543700745", false, "544dead9-996c-481a-a720-e1b46bd93844", false, "CenterReviewer" }
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
                    { "1c1fad40-cd3b-426f-b4c0-6205236df447", "ad856c8c-eebb-4984-b0ec-5ce7f3efa22c" },
                    { "abd66b75-dd65-4809-8db5-874c0f8ce275", "b6578ba7-8b09-41c1-83b5-331186006e14" }
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
                name: "IX_Center_Name",
                table: "Center",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CenterOwnerUser_FKCenterId",
                table: "CenterOwnerUser",
                column: "FKCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterOwnerUser_FKGenderId",
                table: "CenterOwnerUser",
                column: "FKGenderId");

            migrationBuilder.CreateIndex(
                name: "IX_CenterOwnerUser_FKUserId",
                table: "CenterOwnerUser",
                column: "FKUserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FAQ_FKTypeId",
                table: "FAQ",
                column: "FKTypeId");

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
                name: "IX_LocalizedProperty_LocaleKeyGroup_LocaleKey_LocaleValue_FKLanguageId_EntityId",
                table: "LocalizedProperty",
                columns: new[] { "LocaleKeyGroup", "LocaleKey", "LocaleValue", "FKLanguageId", "EntityId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParentUser_FKGenderId",
                table: "ParentUser",
                column: "FKGenderId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentUser_FKUserId",
                table: "ParentUser",
                column: "FKUserId",
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
                name: "CenterOwnerUser");

            migrationBuilder.DropTable(
                name: "FAQ");

            migrationBuilder.DropTable(
                name: "GenderDescription");

            migrationBuilder.DropTable(
                name: "LanguageDescription");

            migrationBuilder.DropTable(
                name: "LocalizedProperty");

            migrationBuilder.DropTable(
                name: "ParentUser");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Center");

            migrationBuilder.DropTable(
                name: "FAQType");

            migrationBuilder.DropTable(
                name: "Language");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "UserType");
        }
    }
}

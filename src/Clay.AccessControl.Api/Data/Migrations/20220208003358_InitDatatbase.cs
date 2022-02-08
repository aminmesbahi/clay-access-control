using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clay.AccessControl.Api.Data.Migrations
{
    public partial class InitDatatbase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserName = table.Column<string>(type: "TEXT", maxLength: 35, nullable: false),
                    TagId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagToken = table.Column<Guid>(type: "TEXT", nullable: false),
                    LockId = table.Column<int>(type: "INTEGER", nullable: false),
                    LockDescription = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    LockToken = table.Column<Guid>(type: "TEXT", nullable: false),
                    AccessResult = table.Column<string>(type: "TEXT", nullable: false),
                    ClientIp = table.Column<string>(type: "TEXT", maxLength: 15, nullable: false),
                    ActionedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Description = table.Column<string>(type: "TEXT", maxLength: 256, nullable: false),
                    Token = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 35, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false),
                    Token = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LockTag",
                columns: table => new
                {
                    LockId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagId = table.Column<int>(type: "INTEGER", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LockTag", x => new { x.LockId, x.TagId });
                    table.ForeignKey(
                        name: "FK_LockTag_Locks_LockId",
                        column: x => x.LockId,
                        principalTable: "Locks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LockTag_Tags_TagId",
                        column: x => x.TagId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Audits",
                columns: new[] { "Id", "AccessResult", "ActionedAt", "ClientIp", "LockDescription", "LockId", "LockToken", "TagId", "TagToken", "UserId", "UserName" },
                values: new object[] { 1, "Opened", new DateTime(2022, 1, 10, 8, 10, 10, 0, DateTimeKind.Unspecified), "", "The Brick House First Door (Tunnel)", 1, new Guid("7025cdba-4810-47d9-acdc-99f48766c0aa"), 1, new Guid("6f5f6b36-ace9-401e-8e97-5dea550e2b3d"), 1, "Darjan" });

            migrationBuilder.InsertData(
                table: "Audits",
                columns: new[] { "Id", "AccessResult", "ActionedAt", "ClientIp", "LockDescription", "LockId", "LockToken", "TagId", "TagToken", "UserId", "UserName" },
                values: new object[] { 2, "Opened", new DateTime(2022, 1, 10, 8, 10, 20, 0, DateTimeKind.Unspecified), "", "The Brick House Second Door (Office)", 2, new Guid("d5bc20c6-4a33-4b18-aa89-589b1e3382df"), 1, new Guid("6f5f6b36-ace9-401e-8e97-5dea550e2b3d"), 1, "Darjan" });

            migrationBuilder.InsertData(
                table: "Audits",
                columns: new[] { "Id", "AccessResult", "ActionedAt", "ClientIp", "LockDescription", "LockId", "LockToken", "TagId", "TagToken", "UserId", "UserName" },
                values: new object[] { 3, "Opened", new DateTime(2022, 1, 10, 8, 15, 20, 0, DateTimeKind.Unspecified), "", "The Brick House First Door (Tunnel)", 1, new Guid("7025cdba-4810-47d9-acdc-99f48766c0aa"), 2, new Guid("935007b3-0c77-4ed4-be51-e51d11c944ee"), 2, "Medi" });

            migrationBuilder.InsertData(
                table: "Audits",
                columns: new[] { "Id", "AccessResult", "ActionedAt", "ClientIp", "LockDescription", "LockId", "LockToken", "TagId", "TagToken", "UserId", "UserName" },
                values: new object[] { 4, "Opened", new DateTime(2022, 1, 10, 8, 15, 35, 0, DateTimeKind.Unspecified), "", "The Brick House Second Door (Office)", 2, new Guid("d5bc20c6-4a33-4b18-aa89-589b1e3382df"), 2, new Guid("935007b3-0c77-4ed4-be51-e51d11c944ee"), 2, "Medi" });

            migrationBuilder.InsertData(
                table: "Audits",
                columns: new[] { "Id", "AccessResult", "ActionedAt", "ClientIp", "LockDescription", "LockId", "LockToken", "TagId", "TagToken", "UserId", "UserName" },
                values: new object[] { 5, "Opened", new DateTime(2022, 1, 10, 8, 30, 20, 0, DateTimeKind.Unspecified), "", "The Brick House First Door (Tunnel)", 1, new Guid("7025cdba-4810-47d9-acdc-99f48766c0aa"), 3, new Guid("a85b118a-95bf-4a31-8e07-d873c37434dd"), 3, "Daniel" });

            migrationBuilder.InsertData(
                table: "Audits",
                columns: new[] { "Id", "AccessResult", "ActionedAt", "ClientIp", "LockDescription", "LockId", "LockToken", "TagId", "TagToken", "UserId", "UserName" },
                values: new object[] { 6, "NOT AUTHORIZED :: Tag not authorized", new DateTime(2022, 1, 10, 8, 30, 30, 0, DateTimeKind.Unspecified), "", "The Brick House Second Door (Office)", 2, new Guid("d5bc20c6-4a33-4b18-aa89-589b1e3382df"), 3, new Guid("a85b118a-95bf-4a31-8e07-d873c37434dd"), 3, "Daniel" });

            migrationBuilder.InsertData(
                table: "Audits",
                columns: new[] { "Id", "AccessResult", "ActionedAt", "ClientIp", "LockDescription", "LockId", "LockToken", "TagId", "TagToken", "UserId", "UserName" },
                values: new object[] { 7, "Opened", new DateTime(2022, 1, 10, 8, 30, 33, 0, DateTimeKind.Unspecified), "", "The Brick House Second Door (Office)", 2, new Guid("d5bc20c6-4a33-4b18-aa89-589b1e3382df"), 4, new Guid("5f43e5a1-13fc-4d28-9d60-35cde5c0bc8e"), 3, "Daniel" });

            migrationBuilder.InsertData(
                table: "Audits",
                columns: new[] { "Id", "AccessResult", "ActionedAt", "ClientIp", "LockDescription", "LockId", "LockToken", "TagId", "TagToken", "UserId", "UserName" },
                values: new object[] { 8, "Opened", new DateTime(2022, 1, 10, 8, 45, 5, 0, DateTimeKind.Unspecified), "", "The Brick House First Door (Tunnel)", 1, new Guid("7025cdba-4810-47d9-acdc-99f48766c0aa"), 5, new Guid("207340c0-b073-4d16-a8c5-819a0117c9cb"), 4, "Peter" });

            migrationBuilder.InsertData(
                table: "Audits",
                columns: new[] { "Id", "AccessResult", "ActionedAt", "ClientIp", "LockDescription", "LockId", "LockToken", "TagId", "TagToken", "UserId", "UserName" },
                values: new object[] { 9, "NOT AUTHORIZED :: Tag not authorized", new DateTime(2022, 1, 10, 8, 45, 10, 0, DateTimeKind.Unspecified), "", "The Brick House Second Door (Office)", 2, new Guid("d5bc20c6-4a33-4b18-aa89-589b1e3382df"), 5, new Guid("207340c0-b073-4d16-a8c5-819a0117c9cb"), 4, "Peter" });

            migrationBuilder.InsertData(
                table: "Audits",
                columns: new[] { "Id", "AccessResult", "ActionedAt", "ClientIp", "LockDescription", "LockId", "LockToken", "TagId", "TagToken", "UserId", "UserName" },
                values: new object[] { 10, "NOT AUTHORIZED :: Tag is not active", new DateTime(2022, 1, 10, 8, 50, 10, 0, DateTimeKind.Unspecified), "", "The Brick House First Door (Tunnel)", 1, new Guid("7025cdba-4810-47d9-acdc-99f48766c0aa"), 6, new Guid("f330243e-3314-4f41-9bd3-8577b2faf823"), 5, "Amin" });

            migrationBuilder.InsertData(
                table: "Audits",
                columns: new[] { "Id", "AccessResult", "ActionedAt", "ClientIp", "LockDescription", "LockId", "LockToken", "TagId", "TagToken", "UserId", "UserName" },
                values: new object[] { 11, "NOT AUTHORIZED :: Tag is not active", new DateTime(2022, 1, 10, 8, 50, 12, 0, DateTimeKind.Unspecified), "", "The Brick House First Door (Tunnel)", 1, new Guid("7025cdba-4810-47d9-acdc-99f48766c0aa"), 6, new Guid("f330243e-3314-4f41-9bd3-8577b2faf823"), 5, "Amin" });

            migrationBuilder.InsertData(
                table: "Audits",
                columns: new[] { "Id", "AccessResult", "ActionedAt", "ClientIp", "LockDescription", "LockId", "LockToken", "TagId", "TagToken", "UserId", "UserName" },
                values: new object[] { 12, "NOT AUTHORIZED :: Tag is not active", new DateTime(2022, 1, 10, 8, 50, 14, 0, DateTimeKind.Unspecified), "", "The Brick House First Door (Tunnel)", 1, new Guid("7025cdba-4810-47d9-acdc-99f48766c0aa"), 6, new Guid("f330243e-3314-4f41-9bd3-8577b2faf823"), 5, "Amin" });

            migrationBuilder.InsertData(
                table: "Audits",
                columns: new[] { "Id", "AccessResult", "ActionedAt", "ClientIp", "LockDescription", "LockId", "LockToken", "TagId", "TagToken", "UserId", "UserName" },
                values: new object[] { 13, "Opened", new DateTime(2022, 1, 10, 8, 55, 40, 0, DateTimeKind.Unspecified), "", "The Brick House First Door (Tunnel)", 1, new Guid("7025cdba-4810-47d9-acdc-99f48766c0aa"), 4, new Guid("5f43e5a1-13fc-4d28-9d60-35cde5c0bc8e"), 3, "Daniel" });

            migrationBuilder.InsertData(
                table: "Audits",
                columns: new[] { "Id", "AccessResult", "ActionedAt", "ClientIp", "LockDescription", "LockId", "LockToken", "TagId", "TagToken", "UserId", "UserName" },
                values: new object[] { 14, "Opened", new DateTime(2022, 1, 10, 8, 55, 45, 0, DateTimeKind.Unspecified), "", "The Brick House Second Door (Office)", 2, new Guid("d5bc20c6-4a33-4b18-aa89-589b1e3382df"), 4, new Guid("5f43e5a1-13fc-4d28-9d60-35cde5c0bc8e"), 3, "Daniel" });

            migrationBuilder.InsertData(
                table: "Locks",
                columns: new[] { "Id", "Description", "Token" },
                values: new object[] { 1, "The Brick House First Door (Tunnel)", new Guid("7025cdba-4810-47d9-acdc-99f48766c0aa") });

            migrationBuilder.InsertData(
                table: "Locks",
                columns: new[] { "Id", "Description", "Token" },
                values: new object[] { 2, "The Brick House Second Door (Office)", new Guid("d5bc20c6-4a33-4b18-aa89-589b1e3382df") });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Darjan" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Medi" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Daniel" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name" },
                values: new object[] { 4, "Peter" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Name" },
                values: new object[] { 5, "Amin" });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "IsActive", "OwnerId", "Token" },
                values: new object[] { 1, true, 1, new Guid("6f5f6b36-ace9-401e-8e97-5dea550e2b3d") });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "IsActive", "OwnerId", "Token" },
                values: new object[] { 2, true, 2, new Guid("935007b3-0c77-4ed4-be51-e51d11c944ee") });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "IsActive", "OwnerId", "Token" },
                values: new object[] { 3, true, 3, new Guid("a85b118a-95bf-4a31-8e07-d873c37434dd") });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "IsActive", "OwnerId", "Token" },
                values: new object[] { 4, true, 3, new Guid("5f43e5a1-13fc-4d28-9d60-35cde5c0bc8e") });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "IsActive", "OwnerId", "Token" },
                values: new object[] { 5, true, 4, new Guid("207340c0-b073-4d16-a8c5-819a0117c9cb") });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "IsActive", "OwnerId", "Token" },
                values: new object[] { 6, false, 5, new Guid("f330243e-3314-4f41-9bd3-8577b2faf823") });

            migrationBuilder.InsertData(
                table: "LockTag",
                columns: new[] { "LockId", "TagId", "CreateDate" },
                values: new object[] { 1, 1, new DateTime(2022, 2, 8, 4, 3, 57, 789, DateTimeKind.Local).AddTicks(7575) });

            migrationBuilder.InsertData(
                table: "LockTag",
                columns: new[] { "LockId", "TagId", "CreateDate" },
                values: new object[] { 1, 2, new DateTime(2022, 2, 8, 4, 3, 57, 794, DateTimeKind.Local).AddTicks(3359) });

            migrationBuilder.InsertData(
                table: "LockTag",
                columns: new[] { "LockId", "TagId", "CreateDate" },
                values: new object[] { 1, 3, new DateTime(2022, 2, 8, 4, 3, 57, 794, DateTimeKind.Local).AddTicks(3365) });

            migrationBuilder.InsertData(
                table: "LockTag",
                columns: new[] { "LockId", "TagId", "CreateDate" },
                values: new object[] { 1, 4, new DateTime(2022, 2, 8, 4, 3, 57, 794, DateTimeKind.Local).AddTicks(3452) });

            migrationBuilder.InsertData(
                table: "LockTag",
                columns: new[] { "LockId", "TagId", "CreateDate" },
                values: new object[] { 1, 5, new DateTime(2022, 2, 8, 4, 3, 57, 794, DateTimeKind.Local).AddTicks(3457) });

            migrationBuilder.InsertData(
                table: "LockTag",
                columns: new[] { "LockId", "TagId", "CreateDate" },
                values: new object[] { 1, 6, new DateTime(2022, 2, 8, 4, 3, 57, 794, DateTimeKind.Local).AddTicks(3463) });

            migrationBuilder.InsertData(
                table: "LockTag",
                columns: new[] { "LockId", "TagId", "CreateDate" },
                values: new object[] { 2, 1, new DateTime(2022, 2, 8, 4, 3, 57, 794, DateTimeKind.Local).AddTicks(3326) });

            migrationBuilder.InsertData(
                table: "LockTag",
                columns: new[] { "LockId", "TagId", "CreateDate" },
                values: new object[] { 2, 2, new DateTime(2022, 2, 8, 4, 3, 57, 794, DateTimeKind.Local).AddTicks(3363) });

            migrationBuilder.InsertData(
                table: "LockTag",
                columns: new[] { "LockId", "TagId", "CreateDate" },
                values: new object[] { 2, 4, new DateTime(2022, 2, 8, 4, 3, 57, 794, DateTimeKind.Local).AddTicks(3455) });

            migrationBuilder.InsertData(
                table: "LockTag",
                columns: new[] { "LockId", "TagId", "CreateDate" },
                values: new object[] { 2, 6, new DateTime(2022, 2, 8, 4, 3, 57, 794, DateTimeKind.Local).AddTicks(3465) });

            migrationBuilder.CreateIndex(
                name: "IX_LockTag_TagId",
                table: "LockTag",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_OwnerId",
                table: "Tags",
                column: "OwnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Audits");

            migrationBuilder.DropTable(
                name: "LockTag");

            migrationBuilder.DropTable(
                name: "Locks");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

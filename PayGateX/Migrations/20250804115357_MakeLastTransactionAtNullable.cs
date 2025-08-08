using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PayGateX.Migrations
{
    /// <inheritdoc />
    public partial class MakeLastTransactionAtNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56394bd2-7986-4b79-857e-3e737143d084");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "655f186a-3efc-48a8-b629-03e42a20d120");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e43390a-8f2d-43e8-b65e-bd5826feb243");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fb4778c3-61c9-49cc-8976-2524e0ebe0c1");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastTransactionAt",
                table: "Cards",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "7afa680f-3e0c-4e78-a10c-6f466e0f5ba9", null, "Admin", "ADMIN" },
                    { "8ea5136a-b965-471c-82fc-3f2332f00051", null, "Manager", "MANAGER" },
                    { "97df79bd-2e48-4fe8-8505-8a482e57e44f", null, "CustomerSupport", "CUSTOMERSUPPORT" },
                    { "bbbac41d-211e-46d5-902a-edccda9c9087", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7afa680f-3e0c-4e78-a10c-6f466e0f5ba9");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8ea5136a-b965-471c-82fc-3f2332f00051");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "97df79bd-2e48-4fe8-8505-8a482e57e44f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bbbac41d-211e-46d5-902a-edccda9c9087");

            migrationBuilder.AlterColumn<DateTime>(
                name: "LastTransactionAt",
                table: "Cards",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "56394bd2-7986-4b79-857e-3e737143d084", null, "Manager", "MANAGER" },
                    { "655f186a-3efc-48a8-b629-03e42a20d120", null, "Admin", "ADMIN" },
                    { "9e43390a-8f2d-43e8-b65e-bd5826feb243", null, "User", "USER" },
                    { "fb4778c3-61c9-49cc-8976-2524e0ebe0c1", null, "CustomerSupport", "CUSTOMERSUPPORT" }
                });
        }
    }
}

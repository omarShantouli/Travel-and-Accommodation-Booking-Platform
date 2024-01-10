using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    public partial class seeding_AppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("5842ae96-2944-4dfd-aa3f-8876cda7967c"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("ce9e40a2-77fb-4e6f-8173-287a175367b6"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("ea65ddcb-b59e-47e7-9cb1-83c77e5d200d"));

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "Email", "PasswordHash", "Role" },
                values: new object[] { new Guid("0028afe9-14d5-4bfb-a0ad-4dc484d2d470"), "admin@example.com", "$2a$12$6FjKqlXYrjM/oHxRpmHGSuuSEoPqPTuLTKIVahnpMD3Yx4NWyT5/6", "Admin" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "Email", "PasswordHash", "Role" },
                values: new object[] { new Guid("1b4e08ba-4f3d-4dd2-8cf7-0ce19b267776"), "jane.smith@example.com", "$2a$12$6FjKqlXYrjM/oHxRpmHGSu1W14LN06bNNOoPKpLumG0Grz3IkJEui", "User" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "Email", "PasswordHash", "Role" },
                values: new object[] { new Guid("262ea98f-6f2e-4ddb-ae78-f65473540d2b"), "john.doe@example.com", "$2a$12$6FjKqlXYrjM/oHxRpmHGSuLJubmSkPYEUye2KxdpvEr5dKDSM2Op6", "User" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("0028afe9-14d5-4bfb-a0ad-4dc484d2d470"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("1b4e08ba-4f3d-4dd2-8cf7-0ce19b267776"));

            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "Id",
                keyValue: new Guid("262ea98f-6f2e-4ddb-ae78-f65473540d2b"));

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "Email", "PasswordHash", "Role" },
                values: new object[] { new Guid("5842ae96-2944-4dfd-aa3f-8876cda7967c"), "jane.smith@example.com", "$2a$10$TncV90XPOddLuoFX/7Scs.pOPeXVLP6yRraiWOto9iyZphar08Mhu", "User" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "Email", "PasswordHash", "Role" },
                values: new object[] { new Guid("ce9e40a2-77fb-4e6f-8173-287a175367b6"), "admin@example.com", "$2a$10$mlsJWyi621DCiO5LzT9S0.MEA9.eL7EmMbwNwhaQ9YuYVmy8Itmne", "Admin" });

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "Id", "Email", "PasswordHash", "Role" },
                values: new object[] { new Guid("ea65ddcb-b59e-47e7-9cb1-83c77e5d200d"), "john.doe@example.com", "$2a$10$WCT.EfPsA2FdH4721DQrD.Le2qYBGoLX5mVOf3U9kdRbmDst9TG6e", "User" });
        }
    }
}

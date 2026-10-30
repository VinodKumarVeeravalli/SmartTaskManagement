using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace STM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class LinkProjectsToIdentityUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "dbo",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETUTCDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "CreatedBy",
                schema: "dbo",
                table: "Projects",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_CreatedBy",
                schema: "dbo",
                table: "Projects",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_CreatedBy",
                schema: "dbo",
                table: "Projects",
                column: "CreatedBy",
                principalSchema: "dbo",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_CreatedBy",
                schema: "dbo",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Projects_CreatedBy",
                schema: "dbo",
                table: "Projects");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                schema: "dbo",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETUTCDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "CreatedBy",
                schema: "dbo",
                table: "Projects",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);
        }
    }
}

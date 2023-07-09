using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Onion.Template.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class TodoSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_TODO_TB_USER_ID_USEER",
                table: "TB_TODO");

            migrationBuilder.RenameColumn(
                name: "ID_USEER",
                table: "TB_TODO",
                newName: "ID_USER");

            migrationBuilder.RenameIndex(
                name: "IX_TB_TODO_ID_USEER",
                table: "TB_TODO",
                newName: "IX_TB_TODO_ID_USER");

            migrationBuilder.AddColumn<DateTime>(
                name: "DT_EXCLUDED",
                table: "TB_TODO",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DT_INCLUDED",
                table: "TB_TODO",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddColumn<DateTime>(
                name: "DT_LAST_MODIFIED",
                table: "TB_TODO",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_TODO_TB_USER_ID_USER",
                table: "TB_TODO",
                column: "ID_USER",
                principalTable: "TB_USER",
                principalColumn: "ID_USER",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TB_TODO_TB_USER_ID_USER",
                table: "TB_TODO");

            migrationBuilder.DropColumn(
                name: "DT_EXCLUDED",
                table: "TB_TODO");

            migrationBuilder.DropColumn(
                name: "DT_INCLUDED",
                table: "TB_TODO");

            migrationBuilder.DropColumn(
                name: "DT_LAST_MODIFIED",
                table: "TB_TODO");

            migrationBuilder.RenameColumn(
                name: "ID_USER",
                table: "TB_TODO",
                newName: "ID_USEER");

            migrationBuilder.RenameIndex(
                name: "IX_TB_TODO_ID_USER",
                table: "TB_TODO",
                newName: "IX_TB_TODO_ID_USEER");

            migrationBuilder.AddForeignKey(
                name: "FK_TB_TODO_TB_USER_ID_USEER",
                table: "TB_TODO",
                column: "ID_USEER",
                principalTable: "TB_USER",
                principalColumn: "ID_USER",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

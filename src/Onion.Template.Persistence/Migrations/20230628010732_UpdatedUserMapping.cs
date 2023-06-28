using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Onion.Template.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUserMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DS_PWD",
                table: "TB_USER",
                newName: "DS_USERNAME");

            migrationBuilder.AddColumn<string>(
                name: "DS_PWD_HASH",
                table: "TB_USER",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DS_PWD_SALT",
                table: "TB_USER",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DS_PWD_HASH",
                table: "TB_USER");

            migrationBuilder.DropColumn(
                name: "DS_PWD_SALT",
                table: "TB_USER");

            migrationBuilder.RenameColumn(
                name: "DS_USERNAME",
                table: "TB_USER",
                newName: "DS_PWD");
        }
    }
}

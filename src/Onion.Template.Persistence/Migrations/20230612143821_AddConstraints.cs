using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Onion.Template.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DS_EMAIL",
                table: "TB_USER",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_TB_USER_DS_EMAIL",
                table: "TB_USER",
                column: "DS_EMAIL",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TB_USER_DS_EMAIL",
                table: "TB_USER");

            migrationBuilder.AlterColumn<string>(
                name: "DS_EMAIL",
                table: "TB_USER",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}

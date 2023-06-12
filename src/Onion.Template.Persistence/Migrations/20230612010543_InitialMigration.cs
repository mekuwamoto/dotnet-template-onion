using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Onion.Template.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_USER",
                columns: table => new
                {
                    ID_USER = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DS_FIRST_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DS_LAST_NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DS_EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DS_PWD = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_USER", x => x.ID_USER);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_USER");
        }
    }
}

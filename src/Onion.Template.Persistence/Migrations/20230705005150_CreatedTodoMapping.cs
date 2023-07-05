using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Onion.Template.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreatedTodoMapping : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TB_TODO",
                columns: table => new
                {
                    ID_TODO = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ID_USEER = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DS_TITLE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FL_COMPLETED = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TB_TODO", x => x.ID_TODO);
                    table.ForeignKey(
                        name: "FK_TB_TODO_TB_USER_ID_USEER",
                        column: x => x.ID_USEER,
                        principalTable: "TB_USER",
                        principalColumn: "ID_USER",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TB_TODO_ID_USEER",
                table: "TB_TODO",
                column: "ID_USEER");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TB_TODO");
        }
    }
}

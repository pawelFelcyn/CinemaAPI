using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class AddedCreatedByIdColumnToMoviesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Movies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Movies_CreatedById",
                table: "Movies",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Movies_Users_CreatedById",
                table: "Movies",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movies_Users_CreatedById",
                table: "Movies");

            migrationBuilder.DropIndex(
                name: "IX_Movies_CreatedById",
                table: "Movies");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Movies");
        }
    }
}

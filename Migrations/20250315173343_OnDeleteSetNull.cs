using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PartyApi.Migrations
{
    /// <inheritdoc />
    public partial class OnDeleteSetNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parties_Users_UserId",
                table: "Parties");

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_Users_UserId",
                table: "Parties",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parties_Users_UserId",
                table: "Parties");

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_Users_UserId",
                table: "Parties",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}

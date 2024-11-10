using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PartyApi.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Parties_PartyId",
                table: "Members");

            migrationBuilder.AlterColumn<int>(
                name: "PartyId",
                table: "Members",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Parties",
                columns: new[] { "PartyId", "Budget", "PartyName" },
                values: new object[,]
                {
                    { 1, 1000m, "Birthday Party" },
                    { 2, 5000m, "Wedding Party" }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "MemberId", "FirstName", "LastName", "PartyId" },
                values: new object[,]
                {
                    { 1, "John", "Doe", 1 },
                    { 2, "Jane", "Doe", 1 },
                    { 3, "Alice", "Smith", 2 },
                    { 4, "Bob", "Smith", 2 }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Parties_PartyId",
                table: "Members",
                column: "PartyId",
                principalTable: "Parties",
                principalColumn: "PartyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Members_Parties_PartyId",
                table: "Members");

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Members",
                keyColumn: "MemberId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Parties",
                keyColumn: "PartyId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Parties",
                keyColumn: "PartyId",
                keyValue: 2);

            migrationBuilder.AlterColumn<int>(
                name: "PartyId",
                table: "Members",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Members_Parties_PartyId",
                table: "Members",
                column: "PartyId",
                principalTable: "Parties",
                principalColumn: "PartyId");
        }
    }
}

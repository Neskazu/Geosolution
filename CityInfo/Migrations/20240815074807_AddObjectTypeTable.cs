using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CityInfo.Migrations
{
    /// <inheritdoc />
    public partial class AddObjectTypeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObjectType",
                table: "CityObjects");

            migrationBuilder.AddColumn<int>(
                name: "ObjectTypeId",
                table: "CityObjects",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ObjectType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ObjectType", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityObjects_ObjectTypeId",
                table: "CityObjects",
                column: "ObjectTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CityObjects_ObjectType_ObjectTypeId",
                table: "CityObjects",
                column: "ObjectTypeId",
                principalTable: "ObjectType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CityObjects_ObjectType_ObjectTypeId",
                table: "CityObjects");

            migrationBuilder.DropTable(
                name: "ObjectType");

            migrationBuilder.DropIndex(
                name: "IX_CityObjects_ObjectTypeId",
                table: "CityObjects");

            migrationBuilder.DropColumn(
                name: "ObjectTypeId",
                table: "CityObjects");

            migrationBuilder.AddColumn<string>(
                name: "ObjectType",
                table: "CityObjects",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

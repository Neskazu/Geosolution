using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CityInfo.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCityObjectWithMultipleTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "CityObjectTypeId",
                table: "CityObjects",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CityObjectType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityObjectType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CityModelTypeMapping",
                columns: table => new
                {
                    CityObjectModelId = table.Column<int>(type: "integer", nullable: false),
                    CityObjectTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CityModelTypeMapping", x => new { x.CityObjectModelId, x.CityObjectTypeId });
                    table.ForeignKey(
                        name: "FK_CityModelTypeMapping_CityObjectType_CityObjectTypeId",
                        column: x => x.CityObjectTypeId,
                        principalTable: "CityObjectType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CityModelTypeMapping_CityObjects_CityObjectModelId",
                        column: x => x.CityObjectModelId,
                        principalTable: "CityObjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CityObjects_CityObjectTypeId",
                table: "CityObjects",
                column: "CityObjectTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CityModelTypeMapping_CityObjectTypeId",
                table: "CityModelTypeMapping",
                column: "CityObjectTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CityObjects_CityObjectType_CityObjectTypeId",
                table: "CityObjects",
                column: "CityObjectTypeId",
                principalTable: "CityObjectType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CityObjects_CityObjectType_CityObjectTypeId",
                table: "CityObjects");

            migrationBuilder.DropTable(
                name: "CityModelTypeMapping");

            migrationBuilder.DropTable(
                name: "CityObjectType");

            migrationBuilder.DropIndex(
                name: "IX_CityObjects_CityObjectTypeId",
                table: "CityObjects");

            migrationBuilder.DropColumn(
                name: "CityObjectTypeId",
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
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CityInfo.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCityObjectWithMultipleTypes2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CityModelTypeMapping_CityObjectType_CityObjectTypeId",
                table: "CityModelTypeMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_CityObjects_CityObjectType_CityObjectTypeId",
                table: "CityObjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CityObjectType",
                table: "CityObjectType");

            migrationBuilder.RenameTable(
                name: "CityObjectType",
                newName: "CityObjectTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CityObjectTypes",
                table: "CityObjectTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CityModelTypeMapping_CityObjectTypes_CityObjectTypeId",
                table: "CityModelTypeMapping",
                column: "CityObjectTypeId",
                principalTable: "CityObjectTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CityObjects_CityObjectTypes_CityObjectTypeId",
                table: "CityObjects",
                column: "CityObjectTypeId",
                principalTable: "CityObjectTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CityModelTypeMapping_CityObjectTypes_CityObjectTypeId",
                table: "CityModelTypeMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_CityObjects_CityObjectTypes_CityObjectTypeId",
                table: "CityObjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CityObjectTypes",
                table: "CityObjectTypes");

            migrationBuilder.RenameTable(
                name: "CityObjectTypes",
                newName: "CityObjectType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CityObjectType",
                table: "CityObjectType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CityModelTypeMapping_CityObjectType_CityObjectTypeId",
                table: "CityModelTypeMapping",
                column: "CityObjectTypeId",
                principalTable: "CityObjectType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CityObjects_CityObjectType_CityObjectTypeId",
                table: "CityObjects",
                column: "CityObjectTypeId",
                principalTable: "CityObjectType",
                principalColumn: "Id");
        }
    }
}

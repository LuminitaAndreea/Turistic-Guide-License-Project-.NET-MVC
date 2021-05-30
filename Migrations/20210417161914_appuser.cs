using Microsoft.EntityFrameworkCore.Migrations;

namespace LicenseProject.Migrations
{
    public partial class appuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Cart1Id",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Cart2Id",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FavoriteCart1FavoriteListRestaurantId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FavoriteCart2FavoriteListTuristicObjectId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FavoriteCart1FavoriteListRestaurantId",
                table: "AspNetUsers",
                column: "FavoriteCart1FavoriteListRestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FavoriteCart2FavoriteListTuristicObjectId",
                table: "AspNetUsers",
                column: "FavoriteCart2FavoriteListTuristicObjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FavoriteListRestaurants_FavoriteCart1FavoriteListRestaurantId",
                table: "AspNetUsers",
                column: "FavoriteCart1FavoriteListRestaurantId",
                principalTable: "FavoriteListRestaurants",
                principalColumn: "FavoriteListRestaurantId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_FavoriteListTuristicObjects_FavoriteCart2FavoriteListTuristicObjectId",
                table: "AspNetUsers",
                column: "FavoriteCart2FavoriteListTuristicObjectId",
                principalTable: "FavoriteListTuristicObjects",
                principalColumn: "FavoriteListTuristicObjectId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FavoriteListRestaurants_FavoriteCart1FavoriteListRestaurantId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_FavoriteListTuristicObjects_FavoriteCart2FavoriteListTuristicObjectId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FavoriteCart1FavoriteListRestaurantId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FavoriteCart2FavoriteListTuristicObjectId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Cart1Id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Cart2Id",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FavoriteCart1FavoriteListRestaurantId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FavoriteCart2FavoriteListTuristicObjectId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");
        }
    }
}

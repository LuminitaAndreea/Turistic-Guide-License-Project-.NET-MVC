using Microsoft.EntityFrameworkCore.Migrations;

namespace LicenseProject.Migrations
{
    public partial class favoritelists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavoriteListRestaurants",
                columns: table => new
                {
                    FavoriteListRestaurantId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteListRestaurants", x => x.FavoriteListRestaurantId);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteListTuristicObjects",
                columns: table => new
                {
                    FavoriteListTuristicObjectId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteListTuristicObjects", x => x.FavoriteListTuristicObjectId);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteRestaurants",
                columns: table => new
                {
                    FavoriteRestaurantId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RestaurantId = table.Column<int>(type: "int", nullable: true),
                    FavoriteListRestaurantId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteRestaurants", x => x.FavoriteRestaurantId);
                    table.ForeignKey(
                        name: "FK_FavoriteRestaurants_FavoriteListRestaurants_FavoriteListRestaurantId",
                        column: x => x.FavoriteListRestaurantId,
                        principalTable: "FavoriteListRestaurants",
                        principalColumn: "FavoriteListRestaurantId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavoriteRestaurants_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "RestaurantId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FavoriteTuristicObjects",
                columns: table => new
                {
                    FavoriteTuristicObjectId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TuristicObjectId = table.Column<int>(type: "int", nullable: true),
                    FavoriteListTuristicObjectId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteTuristicObjects", x => x.FavoriteTuristicObjectId);
                    table.ForeignKey(
                        name: "FK_FavoriteTuristicObjects_FavoriteListTuristicObjects_FavoriteListTuristicObjectId",
                        column: x => x.FavoriteListTuristicObjectId,
                        principalTable: "FavoriteListTuristicObjects",
                        principalColumn: "FavoriteListTuristicObjectId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FavoriteTuristicObjects_TuristicObjects_TuristicObjectId",
                        column: x => x.TuristicObjectId,
                        principalTable: "TuristicObjects",
                        principalColumn: "TuristicObjectId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteRestaurants_FavoriteListRestaurantId",
                table: "FavoriteRestaurants",
                column: "FavoriteListRestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteRestaurants_RestaurantId",
                table: "FavoriteRestaurants",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteTuristicObjects_FavoriteListTuristicObjectId",
                table: "FavoriteTuristicObjects",
                column: "FavoriteListTuristicObjectId");

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteTuristicObjects_TuristicObjectId",
                table: "FavoriteTuristicObjects",
                column: "TuristicObjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavoriteRestaurants");

            migrationBuilder.DropTable(
                name: "FavoriteTuristicObjects");

            migrationBuilder.DropTable(
                name: "FavoriteListRestaurants");

            migrationBuilder.DropTable(
                name: "FavoriteListTuristicObjects");
        }
    }
}

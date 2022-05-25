using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Unipply.Migrations
{
    public partial class AddFavouritesSpecialtyToUserProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpecialtyDataUserProfileData",
                columns: table => new
                {
                    FavouritesSpecialtiesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserProfileDatasId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialtyDataUserProfileData", x => new { x.FavouritesSpecialtiesId, x.UserProfileDatasId });
                    table.ForeignKey(
                        name: "FK_SpecialtyDataUserProfileData_SpecialtyData_FavouritesSpecia~",
                        column: x => x.FavouritesSpecialtiesId,
                        principalTable: "SpecialtyData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecialtyDataUserProfileData_UserProfileData_UserProfileDat~",
                        column: x => x.UserProfileDatasId,
                        principalTable: "UserProfileData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpecialtyDataUserProfileData_UserProfileDatasId",
                table: "SpecialtyDataUserProfileData",
                column: "UserProfileDatasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpecialtyDataUserProfileData");
        }
    }
}

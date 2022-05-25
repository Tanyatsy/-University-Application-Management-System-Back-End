using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Unipply.Migrations
{
    public partial class ChangeForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          /*  migrationBuilder.DropForeignKey(
                name: "FK_RecommendationFacultyData_UserProfileData_UserProfileDataId",
                table: "RecommendationFacultyData");*/

           /* migrationBuilder.DropIndex(
                name: "IX_RecommendationFacultyData_UserProfileDataId",
                table: "RecommendationFacultyData");*/

           /* migrationBuilder.DropColumn(
                name: "UserProfileDataId",
                table: "RecommendationFacultyData");
*/
            migrationBuilder.AddColumn<Guid>(
                name: "RecommendationFacultyDataId",
                table: "UserProfileData",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "FacultyDataUserProfileData",
                columns: table => new
                {
                    FavouritesFacultiesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserProfileDatasId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FacultyDataUserProfileData", x => new { x.FavouritesFacultiesId, x.UserProfileDatasId });
                    table.ForeignKey(
                        name: "FK_FacultyDataUserProfileData_FacultyData_FavouritesFacultiesId",
                        column: x => x.FavouritesFacultiesId,
                        principalTable: "FacultyData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FacultyDataUserProfileData_UserProfileData_UserProfileDatas~",
                        column: x => x.UserProfileDatasId,
                        principalTable: "UserProfileData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileData_RecommendationFacultyDataId",
                table: "UserProfileData",
                column: "RecommendationFacultyDataId");

            migrationBuilder.CreateIndex(
                name: "IX_FacultyDataUserProfileData_UserProfileDatasId",
                table: "FacultyDataUserProfileData",
                column: "UserProfileDatasId");


        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfileData_RecommendationFacultyData_RecommendationFac~",
                table: "UserProfileData");

            migrationBuilder.DropTable(
                name: "FacultyDataUserProfileData");

            migrationBuilder.DropIndex(
                name: "IX_UserProfileData_RecommendationFacultyDataId",
                table: "UserProfileData");

            migrationBuilder.DropColumn(
                name: "RecommendationFacultyDataId",
                table: "UserProfileData");

            migrationBuilder.AddColumn<Guid>(
                name: "UserProfileDataId",
                table: "RecommendationFacultyData",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationFacultyData_UserProfileDataId",
                table: "RecommendationFacultyData",
                column: "UserProfileDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_RecommendationFacultyData_UserProfileData_UserProfileDataId",
                table: "RecommendationFacultyData",
                column: "UserProfileDataId",
                principalTable: "UserProfileData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

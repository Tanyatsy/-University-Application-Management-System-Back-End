using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Unipply.Migrations
{
    public partial class AddUserProfileData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecommendationFacultyData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FacultyTitle = table.Column<string>(type: "text", nullable: true),
                    FacultyDataId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendationFacultyData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecommendationFacultyData_FacultyData_FacultyDataId",
                        column: x => x.FacultyDataId,
                        principalTable: "FacultyData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserProfileData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserDataId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfileData_UserData_UserDataId",
                        column: x => x.UserDataId,
                        principalTable: "UserData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecommendationFacultyDataUserProfileData",
                columns: table => new
                {
                    FavouritesFacultiesId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserProfileDatasId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendationFacultyDataUserProfileData", x => new { x.FavouritesFacultiesId, x.UserProfileDatasId });
                    table.ForeignKey(
                        name: "FK_RecommendationFacultyDataUserProfileData_RecommendationFacu~",
                        column: x => x.FavouritesFacultiesId,
                        principalTable: "RecommendationFacultyData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecommendationFacultyDataUserProfileData_UserProfileData_Us~",
                        column: x => x.UserProfileDatasId,
                        principalTable: "UserProfileData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpecialtyData_FacultyId",
                table: "SpecialtyData",
                column: "FacultyId");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationFacultyData_FacultyDataId",
                table: "RecommendationFacultyData",
                column: "FacultyDataId");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationFacultyDataUserProfileData_UserProfileDatasId",
                table: "RecommendationFacultyDataUserProfileData",
                column: "UserProfileDatasId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileData_UserDataId",
                table: "UserProfileData",
                column: "UserDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_SpecialtyData_FacultyData_FacultyId",
                table: "SpecialtyData",
                column: "FacultyId",
                principalTable: "FacultyData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SpecialtyData_FacultyData_FacultyId",
                table: "SpecialtyData");

            migrationBuilder.DropTable(
                name: "RecommendationFacultyDataUserProfileData");

            migrationBuilder.DropTable(
                name: "RecommendationFacultyData");

            migrationBuilder.DropTable(
                name: "UserProfileData");

            migrationBuilder.DropIndex(
                name: "IX_SpecialtyData_FacultyId",
                table: "SpecialtyData");

        }
    }
}

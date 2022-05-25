using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Unipply.Migrations
{
    public partial class DeleteRecommendationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /* migrationBuilder.DropForeignKey(
                 name: "FK_UserProfileData_RecommendationFacultyData_RecommendationFac~",
                 table: "UserProfileData");*/

            migrationBuilder.DropTable(
                name: "RecommendationFacultyDataUserProfileData");

            migrationBuilder.DropTable(
                name: "RecommendationFacultyData");

            migrationBuilder.DropIndex(
                name: "IX_UserProfileData_RecommendationFacultyDataId",
                table: "UserProfileData");

            migrationBuilder.DropColumn(
                name: "RecommendationFacultyDataId",
                table: "UserProfileData");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "RecommendationFacultyDataId",
                table: "UserProfileData",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "RecommendationFacultyData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FacultyDataId = table.Column<Guid>(type: "uuid", nullable: false),
                    FacultyTitle = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecommendationFacultyData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RecommendationFacultyData_FacultyData_FacultyDataId",
                        column: x => x.FacultyDataId,
                        principalTable: "FacultyData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileData_RecommendationFacultyDataId",
                table: "UserProfileData",
                column: "RecommendationFacultyDataId");

            migrationBuilder.CreateIndex(
                name: "IX_RecommendationFacultyData_FacultyDataId",
                table: "RecommendationFacultyData",
                column: "FacultyDataId");

            /*migrationBuilder.AddForeignKey(
                name: "FK_UserProfileData_RecommendationFacultyData_RecommendationFac~",
                table: "UserProfileData",
                column: "RecommendationFacultyDataId",
                principalTable: "RecommendationFacultyData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);*/
        }
    }
}

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Unipply.Models;
using Unipply.Models.Recommendation;

namespace Unipply.Migrations
{
    public partial class ExtendUserProfileData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutMe",
                table: "UserProfileData",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "UserProfileData",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Avatar",
                table: "UserProfileData",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "Documents",
                table: "UserProfileData",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "UserProfileData",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<List<HobbyModel>>(
                name: "Hobbies",
                table: "UserProfileData",
                type: "json",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "UserProfileData",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<List<RecommendationFacultiesModel>>(
                name: "Recommendations",
                table: "UserProfileData",
                type: "json",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AboutMe",
                table: "UserProfileData");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "UserProfileData");

            migrationBuilder.DropColumn(
                name: "Avatar",
                table: "UserProfileData");

            migrationBuilder.DropColumn(
                name: "Documents",
                table: "UserProfileData");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "UserProfileData");

            migrationBuilder.DropColumn(
                name: "Hobbies",
                table: "UserProfileData");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "UserProfileData");

            migrationBuilder.DropColumn(
                name: "Recommendations",
                table: "UserProfileData");
        }
    }
}

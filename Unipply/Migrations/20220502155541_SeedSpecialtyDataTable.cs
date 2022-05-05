using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Unipply.Migrations
{
    public partial class SeedSpecialtyDataTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpecialtyData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true),
                    FacultyId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialtyData", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SpecialtyData",
                columns: new[] { "Id", "Description", "FacultyId", "Title" },
                values: new object[,]
                {
                    { new Guid("901a4f48-967b-4963-aff6-f7477e12466a"), "Short Description", new Guid("f6aa4c0e-edc2-4bad-a22b-8516e3637546"), "Engineering and management in Energetics" },
                    { new Guid("e001ebdd-895a-4892-8c7b-947284d28258"), "Short Description", new Guid("0f64242d-fda3-4885-8a7f-4624731adfde"), "Law" },
                    { new Guid("cf6ce58a-d3ea-4901-b9aa-7c0be3a33a80"), "Short Description", new Guid("0f64242d-fda3-4885-8a7f-4624731adfde"), "Engineering and Management in constructions" },
                    { new Guid("2caecd30-a59a-4134-b5f4-98f32bbac557"), "Short Description", new Guid("0f64242d-fda3-4885-8a7f-4624731adfde"), "Wood processing technology" },
                    { new Guid("956be74d-9dcd-4cf5-ad79-7fd2a8e9df57"), "Short Description", new Guid("0f64242d-fda3-4885-8a7f-4624731adfde"), "Mining engineering" },
                    { new Guid("6ce77e0c-2518-4a2b-bba5-2d8be51a1f36"), "Short Description", new Guid("0f64242d-fda3-4885-8a7f-4624731adfde"), "Geodesy, topography and mapping" },
                    { new Guid("6002480d-37fd-4aea-9162-0fa004b92818"), "Short Description", new Guid("0f64242d-fda3-4885-8a7f-4624731adfde"), "Assessment of Real Estate" },
                    { new Guid("942aa9c8-3623-4edb-8440-799c5a496b2d"), "Short Description", new Guid("0f64242d-fda3-4885-8a7f-4624731adfde"), "Construction and civil engineering" },
                    { new Guid("9a7581f6-bbfb-4a2a-a54e-dbf9683223ef"), "Short Description", new Guid("0f64242d-fda3-4885-8a7f-4624731adfde"), "Fire Engineering and Civil Protection" },
                    { new Guid("f3478051-f593-452b-bd4c-f84539bcab83"), "Short Description", new Guid("f3664286-15e9-49e5-b952-c02d744e09db"), "Сomputers and Networks" },
                    { new Guid("b963a497-8cb4-43ff-9c88-be2c71c24755"), "Short Description", new Guid("f3664286-15e9-49e5-b952-c02d744e09db"), "Informational Management" },
                    { new Guid("7d652683-e8d9-476c-80c5-8f8f9e49c1a7"), "Short Description", new Guid("f3664286-15e9-49e5-b952-c02d744e09db"), "Information technology" },
                    { new Guid("31dba013-0ce5-4a25-8567-7d4d4a097efe"), "Short Description", new Guid("f3664286-15e9-49e5-b952-c02d744e09db"), "Informational security" },
                    { new Guid("7cdf0042-9c4f-4a84-8333-0555b87f98a2"), "Short Description", new Guid("5ec9f43e-b68c-48cc-a983-1463716f8b03"), "Business and Administration" },
                    { new Guid("79eefef3-3a02-42fb-a740-e92610598b60"), "Short Description", new Guid("f3664286-15e9-49e5-b952-c02d744e09db"), "Software Engineering" },
                    { new Guid("303e2855-9f0c-4411-99ff-931c05a0ac48"), "Short Description", new Guid("f3664286-15e9-49e5-b952-c02d744e09db"), "Microelectronics and nanotechnologies" },
                    { new Guid("bca078b6-faa4-4027-9819-8dfeb93fc545"), "Short Description", new Guid("f3664286-15e9-49e5-b952-c02d744e09db"), "Automation and Information Science" },
                    { new Guid("c45c9b75-532a-4e24-9c80-840bf451c386"), "Short Description", new Guid("f3664286-15e9-49e5-b952-c02d744e09db"), "Robotics and mechatronics" },
                    { new Guid("34ae72b1-d57d-4f11-ab90-c1aaa83b7b3e"), "Short Description", new Guid("f3664286-15e9-49e5-b952-c02d744e09db"), "Biomedical systems engineering" },
                    { new Guid("baa78b6c-2ceb-4347-96d8-ca7d8dff19bd"), "Short Description", new Guid("13f040e8-f953-4eb4-af06-a56501e10ef0"), "Engineering and Management in Telecommunications" },
                    { new Guid("8ab3849b-4ffa-430d-bab1-53ded21b923f"), "Short Description", new Guid("13f040e8-f953-4eb4-af06-a56501e10ef0"), "Telecommunications technologies and systems" },
                    { new Guid("bef9fd08-4740-4ba7-bdcc-e074232ec9eb"), "Short Description", new Guid("13f040e8-f953-4eb4-af06-a56501e10ef0"), "Telecommunications networks and software" },
                    { new Guid("166798c9-720f-4c92-b711-93be29b7089e"), "Short Description", new Guid("13f040e8-f953-4eb4-af06-a56501e10ef0"), "Radio and television communications" },
                    { new Guid("b8de5904-f002-4737-9e37-568faf92f801"), "Short Description", new Guid("13f040e8-f953-4eb4-af06-a56501e10ef0"), "Applied electronics" },
                    { new Guid("3f7c0a1c-6adf-4052-8cef-edc084acdffc"), "Short Description", new Guid("41ba4ac4-5991-4977-a134-7c7750095021"), "Technology and management of catering" },
                    { new Guid("152eb23f-b89a-487a-8b51-55fd57cc1775"), "Short Description", new Guid("41ba4ac4-5991-4977-a134-7c7750095021"), "Technology of Food Production" },
                    { new Guid("fc2c2dbe-785a-46c3-a58c-98ec292d55fe"), "Short Description", new Guid("41ba4ac4-5991-4977-a134-7c7750095021"), "Technology of Wine and Fermented Products" },
                    { new Guid("3d1f4d33-f29d-4f10-9a25-535788eb0438"), "Short Description", new Guid("f3664286-15e9-49e5-b952-c02d744e09db"), "Applied Informatics" },
                    { new Guid("856db952-8b97-4a10-874e-e376bad8f6cc"), "Short Description", new Guid("5ec9f43e-b68c-48cc-a983-1463716f8b03"), "Marketing and Logistics" },
                    { new Guid("a69041e7-c4e3-48a3-8ca2-567012077b89"), "Short Description", new Guid("5ec9f43e-b68c-48cc-a983-1463716f8b03"), "Accounting" },
                    { new Guid("667364de-c97a-4602-b879-5e5ccd575feb"), "Short Description", new Guid("0e2aed9e-99eb-41c4-a3ad-a06bf63459e2"), "Textile, clothes, footwear and leather processing" },
                    { new Guid("e64d5245-a443-441e-a970-a44d043639fd"), "Short Description", new Guid("f6aa4c0e-edc2-4bad-a22b-8516e3637546"), "Electroenergetics" },
                    { new Guid("a10387f7-d2d1-45f8-84a4-f33c25414140"), "Short Description", new Guid("f6aa4c0e-edc2-4bad-a22b-8516e3637546"), "Thermoenergetics" },
                    { new Guid("aea06a0f-15de-4bb7-a5d1-2338a4421ddb"), "Short Description", new Guid("f6aa4c0e-edc2-4bad-a22b-8516e3637546"), "Engineering and Quality Management" },
                    { new Guid("b2f39374-44a0-422d-8f67-4718aca12779"), "Short Description", new Guid("78050741-da48-451d-bdc7-e05031aa724e"), "Engineering and industrial technologies" },
                    { new Guid("21a081bd-84fb-48d2-93ac-ca9a4051fa08"), "Short Description", new Guid("78050741-da48-451d-bdc7-e05031aa724e"), "Transport Engineering and Management" },
                    { new Guid("86fd7637-a499-4fd9-8664-370b52809125"), "Short Description", new Guid("78050741-da48-451d-bdc7-e05031aa724e"), "Technology of car construction" },
                    { new Guid("d03eb219-6767-40b7-aa52-1cb4dcfe2eff"), "Short Description", new Guid("78050741-da48-451d-bdc7-e05031aa724e"), "Machinery and Production Systems" },
                    { new Guid("0edaee5b-9836-440b-9706-ccbe62e412a4"), "Short Description", new Guid("78050741-da48-451d-bdc7-e05031aa724e"), "Mechanical engineering" },
                    { new Guid("ea7b1962-dbc9-4c19-9029-c915e175bcf0"), "Short Description", new Guid("78050741-da48-451d-bdc7-e05031aa724e"), "Machinery and Refrigerating installations, air-conditioning systems" },
                    { new Guid("c6dd205b-dde3-4688-9441-caa671ae9a55"), "Short Description", new Guid("78050741-da48-451d-bdc7-e05031aa724e"), "Product design engineering" },
                    { new Guid("20366fc8-5355-4947-b629-4ce5dc30877d"), "Short Description", new Guid("78050741-da48-451d-bdc7-e05031aa724e"), "Industrial Design" },
                    { new Guid("f6a7db95-5a83-47fb-a49a-fb794912af80"), "Short Description", new Guid("78050741-da48-451d-bdc7-e05031aa724e"), "Motor Transport Engineering" },
                    { new Guid("8862bffd-e76b-476d-a052-1744911d2486"), "Short Description", new Guid("78050741-da48-451d-bdc7-e05031aa724e"), "Rail transport engineering" },
                    { new Guid("eae0c50c-2ea4-43d3-b3fb-a885e7c19f3e"), "Short Description", new Guid("78050741-da48-451d-bdc7-e05031aa724e"), "Transport and shipping services" },
                    { new Guid("f90c4804-2743-487c-aece-3aea8015779e"), "Short Description", new Guid("d543979a-8a39-4213-9197-84c29d4cf58a"), "Interior Design" },
                    { new Guid("4a48e042-5a06-4e58-a456-2a89334a147f"), "Short Description", new Guid("d543979a-8a39-4213-9197-84c29d4cf58a"), "Sculpture" },
                    { new Guid("3fb02bd3-a8a2-41e2-99ea-ca574de5108e"), "Short Description", new Guid("d543979a-8a39-4213-9197-84c29d4cf58a"), "Environmental engineering" },
                    { new Guid("21adc284-960b-408a-b66e-5d4d8e05cdb9"), "Short Description", new Guid("d543979a-8a39-4213-9197-84c29d4cf58a"), "Mechanical Engineering in Construction" },
                    { new Guid("deca6746-63a3-4da7-b9cd-4f6ba6089d1d"), "Short Description", new Guid("d543979a-8a39-4213-9197-84c29d4cf58a"), "Engineering of construction materials and fittings" },
                    { new Guid("a9987eca-dbf6-4cb7-ba5c-aa663e0728a4"), "Short Description", new Guid("d543979a-8a39-4213-9197-84c29d4cf58a"), "Architecture" },
                    { new Guid("edba02e7-d8af-408d-9dee-8da99af6c8a7"), "Short Description", new Guid("d543979a-8a39-4213-9197-84c29d4cf58a"), "Urban planning and landscape management" },
                    { new Guid("d1e70abd-b0f5-40a9-b1f2-894fbfb50fb3"), "Short Description", new Guid("d543979a-8a39-4213-9197-84c29d4cf58a"), "Landscape architecture and green spaces" },
                    { new Guid("5a9e55e7-555f-4786-aaf0-aba6c2640a92"), "Short Description", new Guid("d543979a-8a39-4213-9197-84c29d4cf58a"), "Railways. Roads and Bridges" },
                    { new Guid("82d5d9cc-4c1e-4f1c-9fb8-36dacf17e74a"), "Short Description", new Guid("d543979a-8a39-4213-9197-84c29d4cf58a"), "Water Protection and Engineering" },
                    { new Guid("aa3867be-3795-46da-8115-c362e885353f"), "Short Description", new Guid("d543979a-8a39-4213-9197-84c29d4cf58a"), "Engineering of gas supply and heating systems, ventilation" },
                    { new Guid("19841459-7c80-4f42-aa36-9939bef10716"), "Short Description", new Guid("0e2aed9e-99eb-41c4-a3ad-a06bf63459e2"), "Decorative Arts" },
                    { new Guid("b462bc0c-98c4-4671-9b32-a2a09f0e0eec"), "Short Description", new Guid("0e2aed9e-99eb-41c4-a3ad-a06bf63459e2"), "Engineering and Industrial Technologies" },
                    { new Guid("fc103339-1765-40f1-8912-91fd8d35d357"), "Short Description", new Guid("41ba4ac4-5991-4977-a134-7c7750095021"), "Public nutrition services" },
                    { new Guid("756af7f3-5208-4e15-9f12-dae6f8f17f4f"), "Short Description", new Guid("41ba4ac4-5991-4977-a134-7c7750095021"), "Industrial Biotechnology" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpecialtyData");
        }
    }
}

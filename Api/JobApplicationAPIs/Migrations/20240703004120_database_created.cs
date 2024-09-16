using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobApplicationAPIs.Migrations
{
    /// <inheritdoc />
    public partial class database_created : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BasicDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RelationshipStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EducationDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EducationLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BoardName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PassingYear = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: true),
                    Percentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BasicDetailsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EducationDetails_BasicDetails_BasicDetailsId",
                        column: x => x.BasicDetailsId,
                        principalTable: "BasicDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LanguageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LanguageLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasicDetailsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Languages_BasicDetails_BasicDetailsId",
                        column: x => x.BasicDetailsId,
                        principalTable: "BasicDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Preferences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notice = table.Column<int>(type: "int", nullable: false),
                    ExpectedCtc = table.Column<int>(type: "int", nullable: false),
                    CurrentCtc = table.Column<int>(type: "int", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasicDetailsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preferences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Preferences_BasicDetails_BasicDetailsId",
                        column: x => x.BasicDetailsId,
                        principalTable: "BasicDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Technologies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TechnologyName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TechnologyLevel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BasicDetailsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technologies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Technologies_BasicDetails_BasicDetailsId",
                        column: x => x.BasicDetailsId,
                        principalTable: "BasicDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkExperiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    BasicDetailsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkExperiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkExperiences_BasicDetails_BasicDetailsId",
                        column: x => x.BasicDetailsId,
                        principalTable: "BasicDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EducationDetails_BasicDetailsId",
                table: "EducationDetails",
                column: "BasicDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Languages_BasicDetailsId",
                table: "Languages",
                column: "BasicDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Preferences_BasicDetailsId",
                table: "Preferences",
                column: "BasicDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_Technologies_BasicDetailsId",
                table: "Technologies",
                column: "BasicDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkExperiences_BasicDetailsId",
                table: "WorkExperiences",
                column: "BasicDetailsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EducationDetails");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "Preferences");

            migrationBuilder.DropTable(
                name: "Technologies");

            migrationBuilder.DropTable(
                name: "WorkExperiences");

            migrationBuilder.DropTable(
                name: "BasicDetails");
        }
    }
}

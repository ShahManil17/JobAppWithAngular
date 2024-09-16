using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobApplicationAPIs.Migrations
{
    /// <inheritdoc />
    public partial class spgetAllDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[getAllDetails]
				AS
				BEGIN
					SELECT (
					SELECT basic.*,
					(
						SELECT
						edu.BoardName, edu.Percentage, edu.PassingYear,edu.EducationLevel
						FROM EducationDetails as edu
						WHERE edu.BasicDetailsId = basic.Id
						FOR JSON PATH
					) AS education,
					(
						SELECT 
						work.Id, work.Company, work.Designation, work.StartDate, work.EndDate
						FROM WorkExperiences AS work
						WHERE work.BasicDetailsId = basic.Id
						FOR JSON PATH
					) AS work,
					(
						SELECT 
						lang.LanguageName, lang.LanguageLevel
						FROM Languages as lang
						WHERE lang.BasicDetailsId = basic.Id
						FOR JSON PATH
					) AS languages,
					(
						SELECT 
						tech.TechnologyName, tech.TechnologyLevel
						FROM Technologies AS tech
						WHERE tech.BasicDetailsId = basic.Id
						FOR JSON PATH
					) AS technologies,
					(
						SELECT 
						pref.ExpectedCtc, pref.CurrentCtc, pref.Location, pref.Notice, pref.Department
						FROM Preferences AS pref
						WHERE pref.BasicDetailsId = basic.Id
						FOR JSON PATH
					) AS preferences
					FROM BasicDetails as basic
					FOR JSON PATH)
				END;";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}

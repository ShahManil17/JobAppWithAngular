using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobApplicationAPIs.Migrations
{
    /// <inheritdoc />
    public partial class spgetAllData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE OR ALTER PROCEDURE getAllData
                AS
                BEGIN
	                SELECT * FROM BasicDetails;
                END;";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP PROCEDURE getAllData";
            migrationBuilder.Sql(sp);
        }
    }
}

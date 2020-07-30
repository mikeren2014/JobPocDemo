using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore.Migrations;

namespace JobPocDemo.Data.Migrations
{
    [SuppressMessage("ReSharper", "InconsistentNaming", Justification = "Poor Adherence to naming conventions, Class is Migration")]
    public partial class addType : Migration
    {
        #region methods

        protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropColumn("Type", "Jobs");
        protected override void Up(MigrationBuilder migrationBuilder) => migrationBuilder.AddColumn<string>("Type", "Jobs", nullable: true);

        #endregion
    }
}

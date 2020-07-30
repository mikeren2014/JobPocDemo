using Microsoft.EntityFrameworkCore.Migrations;

namespace JobPocDemo.Data.Migrations
{
    public partial class ContentAndTypeShouldBeRequired : Migration
    {
        #region methods

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>("Type", "Jobs", "TEXT", nullable: true, oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>("Content", "Jobs", "TEXT", nullable: true, oldClrType: typeof(string));
        }

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Jobs] where [Content] is null or [Type] is null");

            migrationBuilder.AlterColumn<string>("Type", "Jobs", nullable: false, oldClrType: typeof(string), oldType: "TEXT", oldNullable: true);

            migrationBuilder.AlterColumn<string>("Content", "Jobs", nullable: false, oldClrType: typeof(string), oldType: "TEXT", oldNullable: true);
        }

        #endregion
    }
}

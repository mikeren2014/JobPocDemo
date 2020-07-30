using Microsoft.EntityFrameworkCore.Migrations;

namespace JobPocDemo.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        #region methods

        protected override void Down(MigrationBuilder migrationBuilder) => migrationBuilder.DropTable("Jobs");

        protected override void Up(MigrationBuilder migrationBuilder) =>
            migrationBuilder.CreateTable("Jobs",
                                         table => new
                                                  {
                                                      Id = table.Column<int>(nullable: false)
                                                                .Annotation("Sqlite:Autoincrement", true),
                                                      Content = table.Column<string>(nullable: true)
                                                  },
                                         constraints: table => { table.PrimaryKey("PK_Jobs", x => x.Id); });

        #endregion
    }
}

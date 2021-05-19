using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogApi.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlogPosts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 32, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 4096, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogPosts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BlogPosts",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[] { 1L, "Descrition 1", "Test 1" });

            migrationBuilder.InsertData(
                table: "BlogPosts",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[] { 2L, "Descrition 2", "Test 2" });

            migrationBuilder.InsertData(
                table: "BlogPosts",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[] { 3L, "Descrition 3", "Test 3" });

            migrationBuilder.InsertData(
                table: "BlogPosts",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[] { 4L, "Descrition 4", "Test 4" });

            migrationBuilder.InsertData(
                table: "BlogPosts",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[] { 5L, "Descrition 5", "Test 5" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogPosts");
        }
    }
}

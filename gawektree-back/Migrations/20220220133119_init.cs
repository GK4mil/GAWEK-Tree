using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace gawektree_back.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Records",
                columns: table => new
                {
                    guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    parentGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Records", x => x.guid);
                });

            migrationBuilder.InsertData(
                table: "Records",
                columns: new[] { "guid", "name", "parentGuid" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000000"), "root", new Guid("00000000-0000-0000-0000-000000000000") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Records");
        }
    }
}

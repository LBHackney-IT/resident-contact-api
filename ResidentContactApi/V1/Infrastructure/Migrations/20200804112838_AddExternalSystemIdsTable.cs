using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ResidentContactApi.V1.Infrastructure.Migrations
{
    public partial class AddExternalSystemIdsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "external_system_ids",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    resident_id = table.Column<int>(nullable: false),
                    external_system_lookup_id = table.Column<int>(nullable: false),
                    external_id_value = table.Column<string>(nullable: false),
                    external_id_name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_external_system_ids", x => x.id);
                    table.ForeignKey(
                        name: "FK_external_system_ids_external_system_lookup_external_system_~",
                        column: x => x.external_system_lookup_id,
                        principalTable: "external_system_lookup",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_external_system_ids_residents_resident_id",
                        column: x => x.resident_id,
                        principalTable: "residents",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_external_system_ids_external_system_lookup_id",
                table: "external_system_ids",
                column: "external_system_lookup_id");

            migrationBuilder.CreateIndex(
                name: "IX_external_system_ids_resident_id",
                table: "external_system_ids",
                column: "resident_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "external_system_ids");
        }
    }
}

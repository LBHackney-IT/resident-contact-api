using Microsoft.EntityFrameworkCore.Migrations;

namespace ResidentContactApi.V1.Infrastructure.Migrations
{
    public partial class RenameContactAndContactSubTypeLookupTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contact_details_contact-sub-type-lookup_contact_subtype_loo~",
                table: "contact_details");

            migrationBuilder.DropForeignKey(
                name: "FK_contact_details_contact-type-lookup_contact_type_lookup_id",
                table: "contact_details");

            migrationBuilder.DropPrimaryKey(
                name: "PK_contact-type-lookup",
                table: "contact-type-lookup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_contact-sub-type-lookup",
                table: "contact-sub-type-lookup");

            migrationBuilder.RenameTable(
                name: "contact-type-lookup",
                newName: "contact_type_lookup");

            migrationBuilder.RenameTable(
                name: "contact-sub-type-lookup",
                newName: "contact_sub_type_lookup");

            migrationBuilder.AddPrimaryKey(
                name: "PK_contact_type_lookup",
                table: "contact_type_lookup",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_contact_sub_type_lookup",
                table: "contact_sub_type_lookup",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_contact_details_contact_sub_type_lookup_contact_subtype_loo~",
                table: "contact_details",
                column: "contact_subtype_lookup_id",
                principalTable: "contact_sub_type_lookup",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_contact_details_contact_type_lookup_contact_type_lookup_id",
                table: "contact_details",
                column: "contact_type_lookup_id",
                principalTable: "contact_type_lookup",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contact_details_contact_sub_type_lookup_contact_subtype_loo~",
                table: "contact_details");

            migrationBuilder.DropForeignKey(
                name: "FK_contact_details_contact_type_lookup_contact_type_lookup_id",
                table: "contact_details");

            migrationBuilder.DropPrimaryKey(
                name: "PK_contact_type_lookup",
                table: "contact_type_lookup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_contact_sub_type_lookup",
                table: "contact_sub_type_lookup");

            migrationBuilder.RenameTable(
                name: "contact_type_lookup",
                newName: "contact-type-lookup");

            migrationBuilder.RenameTable(
                name: "contact_sub_type_lookup",
                newName: "contact-sub-type-lookup");

            migrationBuilder.AddPrimaryKey(
                name: "PK_contact-type-lookup",
                table: "contact-type-lookup",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_contact-sub-type-lookup",
                table: "contact-sub-type-lookup",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_contact_details_contact-sub-type-lookup_contact_subtype_loo~",
                table: "contact_details",
                column: "contact_subtype_lookup_id",
                principalTable: "contact-sub-type-lookup",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_contact_details_contact-type-lookup_contact_type_lookup_id",
                table: "contact_details",
                column: "contact_type_lookup_id",
                principalTable: "contact-type-lookup",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

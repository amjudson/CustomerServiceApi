using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CustomerServiceApi.Migrations
{
    /// <inheritdoc />
    public partial class PersonTypeClientOption : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ClientOption",
                table: "PersonTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("11270c83-4a0c-40f9-bbc5-6b6894d9d95d"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 14, 33, 5, 812, DateTimeKind.Utc).AddTicks(4553));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("4cc3d613-57ff-4fc8-8d7d-b908678769d2"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 14, 33, 5, 812, DateTimeKind.Utc).AddTicks(4546));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("50cf2794-76c4-4f35-8692-27bfbbfffee7"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 14, 33, 5, 812, DateTimeKind.Utc).AddTicks(4557));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("53f8d078-11a1-4089-9ab3-39dbf72425d9"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 14, 33, 5, 812, DateTimeKind.Utc).AddTicks(4550));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("55136886-37df-4188-b5ea-0e74958e627c"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 14, 33, 5, 812, DateTimeKind.Utc).AddTicks(4542));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("9bfbf657-4ede-4e6c-94fe-ed5c1764af28"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 14, 33, 5, 812, DateTimeKind.Utc).AddTicks(4561));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
                column: "CreatedDate",
                value: new DateTime(2024, 11, 14, 14, 33, 5, 812, DateTimeKind.Utc).AddTicks(4448));

            migrationBuilder.UpdateData(
                table: "PersonTypes",
                keyColumn: "PersonTypeId",
                keyValue: 1,
                column: "ClientOption",
                value: false);

            migrationBuilder.UpdateData(
                table: "PersonTypes",
                keyColumn: "PersonTypeId",
                keyValue: 2,
                column: "ClientOption",
                value: false);

            migrationBuilder.UpdateData(
                table: "PersonTypes",
                keyColumn: "PersonTypeId",
                keyValue: 3,
                column: "ClientOption",
                value: false);

            migrationBuilder.UpdateData(
                table: "PersonTypes",
                keyColumn: "PersonTypeId",
                keyValue: 4,
                column: "ClientOption",
                value: false);

            migrationBuilder.UpdateData(
                table: "PersonTypes",
                keyColumn: "PersonTypeId",
                keyValue: 5,
                column: "ClientOption",
                value: false);

            migrationBuilder.UpdateData(
                table: "PersonTypes",
                keyColumn: "PersonTypeId",
                keyValue: 6,
                column: "ClientOption",
                value: false);

            migrationBuilder.UpdateData(
                table: "PersonTypes",
                keyColumn: "PersonTypeId",
                keyValue: 7,
                column: "ClientOption",
                value: false);

            migrationBuilder.UpdateData(
                table: "PersonTypes",
                keyColumn: "PersonTypeId",
                keyValue: 8,
                column: "ClientOption",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientOption",
                table: "PersonTypes");

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("11270c83-4a0c-40f9-bbc5-6b6894d9d95d"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 26, 16, 30, 6, 742, DateTimeKind.Utc).AddTicks(596));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("4cc3d613-57ff-4fc8-8d7d-b908678769d2"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 26, 16, 30, 6, 742, DateTimeKind.Utc).AddTicks(587));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("50cf2794-76c4-4f35-8692-27bfbbfffee7"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 26, 16, 30, 6, 742, DateTimeKind.Utc).AddTicks(599));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("53f8d078-11a1-4089-9ab3-39dbf72425d9"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 26, 16, 30, 6, 742, DateTimeKind.Utc).AddTicks(591));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("55136886-37df-4188-b5ea-0e74958e627c"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 26, 16, 30, 6, 742, DateTimeKind.Utc).AddTicks(583));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("9bfbf657-4ede-4e6c-94fe-ed5c1764af28"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 26, 16, 30, 6, 742, DateTimeKind.Utc).AddTicks(604));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 26, 16, 30, 6, 742, DateTimeKind.Utc).AddTicks(482));
        }
    }
}

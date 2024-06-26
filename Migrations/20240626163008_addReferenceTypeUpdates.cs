using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomerServiceApi.Migrations
{
    /// <inheritdoc />
    public partial class addReferenceTypeUpdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ClientOption",
                table: "PhoneTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ClientOption",
                table: "EmailTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ClientOption",
                table: "AddressTypes",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AddressTypes",
                keyColumn: "AddressTypeId",
                keyValue: 1,
                columns: new[] { "ClientId", "ClientOption" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "AddressTypes",
                keyColumn: "AddressTypeId",
                keyValue: 2,
                columns: new[] { "ClientId", "ClientOption" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "AddressTypes",
                keyColumn: "AddressTypeId",
                keyValue: 3,
                columns: new[] { "ClientId", "ClientOption" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "AddressTypes",
                keyColumn: "AddressTypeId",
                keyValue: 4,
                columns: new[] { "ClientId", "ClientOption" },
                values: new object[] { null, false });

            migrationBuilder.InsertData(
                table: "AddressTypes",
                columns: new[] { "AddressTypeId", "ClientId", "ClientOption", "Description", "Name" },
                values: new object[,]
                {
                    { 5, null, true, "Main Office address", "Main Office" },
                    { 6, null, true, "Branch Office address", "Branch Office" }
                });

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

            migrationBuilder.UpdateData(
                table: "EmailTypes",
                keyColumn: "EmailTypeId",
                keyValue: 1,
                columns: new[] { "ClientId", "ClientOption" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "EmailTypes",
                keyColumn: "EmailTypeId",
                keyValue: 2,
                columns: new[] { "ClientId", "ClientOption" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "EmailTypes",
                keyColumn: "EmailTypeId",
                keyValue: 3,
                columns: new[] { "ClientId", "ClientOption" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "EmailTypes",
                keyColumn: "EmailTypeId",
                keyValue: 4,
                columns: new[] { "ClientId", "ClientOption" },
                values: new object[] { null, false });

            migrationBuilder.InsertData(
                table: "EmailTypes",
                columns: new[] { "EmailTypeId", "ClientId", "ClientOption", "Description", "Name" },
                values: new object[,]
                {
                    { 5, null, true, "Support Department", "Support" },
                    { 6, null, true, "Receiving Department", "Receiving" },
                    { 7, null, true, "Sales Department", "Sales" },
                    { 8, null, true, "Service Department", "Service" },
                    { 9, null, true, "Legal Department", "Legal" }
                });

            migrationBuilder.UpdateData(
                table: "PhoneTypes",
                keyColumn: "PhoneTypeId",
                keyValue: 1,
                columns: new[] { "ClientId", "ClientOption" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "PhoneTypes",
                keyColumn: "PhoneTypeId",
                keyValue: 2,
                columns: new[] { "ClientId", "ClientOption" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "PhoneTypes",
                keyColumn: "PhoneTypeId",
                keyValue: 3,
                columns: new[] { "ClientId", "ClientOption" },
                values: new object[] { null, false });

            migrationBuilder.UpdateData(
                table: "PhoneTypes",
                keyColumn: "PhoneTypeId",
                keyValue: 4,
                columns: new[] { "ClientId", "ClientOption" },
                values: new object[] { null, false });

            migrationBuilder.InsertData(
                table: "PhoneTypes",
                columns: new[] { "PhoneTypeId", "ClientId", "ClientOption", "Description", "Name" },
                values: new object[,]
                {
                    { 5, null, true, "Receiving Department", "Receiving" },
                    { 6, null, true, "Accounting Department", "Accounting" },
                    { 7, null, true, "Legal Department", "Legal" },
                    { 8, null, true, "Sales Department", "Sales" },
                    { 9, null, true, "Service Department", "Service" },
                    { 10, null, true, "Engineering Department", "Engineering" },
                    { 11, null, true, "Other - place holder until the true name is added", "Other ...To be added" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AddressTypes",
                keyColumn: "AddressTypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AddressTypes",
                keyColumn: "AddressTypeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EmailTypes",
                keyColumn: "EmailTypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "EmailTypes",
                keyColumn: "EmailTypeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "EmailTypes",
                keyColumn: "EmailTypeId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "EmailTypes",
                keyColumn: "EmailTypeId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "EmailTypes",
                keyColumn: "EmailTypeId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PhoneTypes",
                keyColumn: "PhoneTypeId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PhoneTypes",
                keyColumn: "PhoneTypeId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PhoneTypes",
                keyColumn: "PhoneTypeId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PhoneTypes",
                keyColumn: "PhoneTypeId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PhoneTypes",
                keyColumn: "PhoneTypeId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PhoneTypes",
                keyColumn: "PhoneTypeId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PhoneTypes",
                keyColumn: "PhoneTypeId",
                keyValue: 11);

            migrationBuilder.DropColumn(
                name: "ClientOption",
                table: "PhoneTypes");

            migrationBuilder.DropColumn(
                name: "ClientOption",
                table: "EmailTypes");

            migrationBuilder.DropColumn(
                name: "ClientOption",
                table: "AddressTypes");

            migrationBuilder.UpdateData(
                table: "AddressTypes",
                keyColumn: "AddressTypeId",
                keyValue: 1,
                column: "ClientId",
                value: new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"));

            migrationBuilder.UpdateData(
                table: "AddressTypes",
                keyColumn: "AddressTypeId",
                keyValue: 2,
                column: "ClientId",
                value: new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"));

            migrationBuilder.UpdateData(
                table: "AddressTypes",
                keyColumn: "AddressTypeId",
                keyValue: 3,
                column: "ClientId",
                value: new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"));

            migrationBuilder.UpdateData(
                table: "AddressTypes",
                keyColumn: "AddressTypeId",
                keyValue: 4,
                column: "ClientId",
                value: new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("11270c83-4a0c-40f9-bbc5-6b6894d9d95d"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 25, 14, 26, 37, 922, DateTimeKind.Utc).AddTicks(7591));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("4cc3d613-57ff-4fc8-8d7d-b908678769d2"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 25, 14, 26, 37, 922, DateTimeKind.Utc).AddTicks(7583));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("50cf2794-76c4-4f35-8692-27bfbbfffee7"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 25, 14, 26, 37, 922, DateTimeKind.Utc).AddTicks(7595));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("53f8d078-11a1-4089-9ab3-39dbf72425d9"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 25, 14, 26, 37, 922, DateTimeKind.Utc).AddTicks(7587));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("55136886-37df-4188-b5ea-0e74958e627c"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 25, 14, 26, 37, 922, DateTimeKind.Utc).AddTicks(7579));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("9bfbf657-4ede-4e6c-94fe-ed5c1764af28"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 25, 14, 26, 37, 922, DateTimeKind.Utc).AddTicks(7598));

            migrationBuilder.UpdateData(
                table: "Clients",
                keyColumn: "ClientId",
                keyValue: new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"),
                column: "CreatedDate",
                value: new DateTime(2024, 6, 25, 14, 26, 37, 922, DateTimeKind.Utc).AddTicks(7478));

            migrationBuilder.UpdateData(
                table: "EmailTypes",
                keyColumn: "EmailTypeId",
                keyValue: 1,
                column: "ClientId",
                value: new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"));

            migrationBuilder.UpdateData(
                table: "EmailTypes",
                keyColumn: "EmailTypeId",
                keyValue: 2,
                column: "ClientId",
                value: new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"));

            migrationBuilder.UpdateData(
                table: "EmailTypes",
                keyColumn: "EmailTypeId",
                keyValue: 3,
                column: "ClientId",
                value: new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"));

            migrationBuilder.UpdateData(
                table: "EmailTypes",
                keyColumn: "EmailTypeId",
                keyValue: 4,
                column: "ClientId",
                value: new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"));

            migrationBuilder.UpdateData(
                table: "PhoneTypes",
                keyColumn: "PhoneTypeId",
                keyValue: 1,
                column: "ClientId",
                value: new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"));

            migrationBuilder.UpdateData(
                table: "PhoneTypes",
                keyColumn: "PhoneTypeId",
                keyValue: 2,
                column: "ClientId",
                value: new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"));

            migrationBuilder.UpdateData(
                table: "PhoneTypes",
                keyColumn: "PhoneTypeId",
                keyValue: 3,
                column: "ClientId",
                value: new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"));

            migrationBuilder.UpdateData(
                table: "PhoneTypes",
                keyColumn: "PhoneTypeId",
                keyValue: 4,
                column: "ClientId",
                value: new Guid("e8094495-8504-422d-87b0-29c9baf4df7e"));
        }
    }
}

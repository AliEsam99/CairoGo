using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CairoGo.Migrations
{
    /// <inheritdoc />
    public partial class removeTicketPricePropInPlace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketPrice",
                table: "Places");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TicketPrice",
                table: "Places",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}

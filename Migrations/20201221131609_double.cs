using Microsoft.EntityFrameworkCore.Migrations;

namespace WaterApplication.Migrations
{
    public partial class @double : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Cost",
                table: "CostWater",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Cost",
                table: "CostWater",
                type: "real",
                nullable: false,
                oldClrType: typeof(double));
        }
    }
}

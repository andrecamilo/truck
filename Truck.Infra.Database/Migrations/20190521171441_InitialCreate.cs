using Microsoft.EntityFrameworkCore.Migrations;

namespace Truck.Infra.Database.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Red = table.Column<byte>(nullable: false),
                    Green = table.Column<byte>(nullable: false),
                    Blue = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trucks",
                columns: table => new
                {
                    Chassis = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    ManufactureYear = table.Column<int>(nullable: false),
                    ModelYear = table.Column<int>(nullable: false),
                    ColorId = table.Column<int>(nullable: false),
                    ModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trucks", x => x.Chassis);
                    table.ForeignKey(
                        name: "FK_Trucks_Colors_ColorId",
                        column: x => x.ColorId,
                        principalTable: "Colors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trucks_Models_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Models",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "Blue", "Green", "Name", "Red" },
                values: new object[] { 1, (byte)0, (byte)0, "Vermelho", (byte)255 });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "Blue", "Green", "Name", "Red" },
                values: new object[] { 2, (byte)255, (byte)0, "Azul", (byte)0 });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "Blue", "Green", "Name", "Red" },
                values: new object[] { 3, (byte)0, (byte)255, "Verde", (byte)0 });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "Blue", "Green", "Name", "Red" },
                values: new object[] { 4, (byte)6, (byte)255, "Amarelo", (byte)255 });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "Blue", "Green", "Name", "Red" },
                values: new object[] { 5, (byte)51, (byte)153, "Laranja", (byte)255 });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Id", "Blue", "Green", "Name", "Red" },
                values: new object[] { 6, (byte)169, (byte)169, "Cinza", (byte)169 });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "FH" });

            migrationBuilder.InsertData(
                table: "Models",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "FM" });

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_ColorId",
                table: "Trucks",
                column: "ColorId");

            migrationBuilder.CreateIndex(
                name: "IX_Trucks_ModelId",
                table: "Trucks",
                column: "ModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trucks");

            migrationBuilder.DropTable(
                name: "Colors");

            migrationBuilder.DropTable(
                name: "Models");
        }
    }
}

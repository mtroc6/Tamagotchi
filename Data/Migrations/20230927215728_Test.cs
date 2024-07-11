using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tamagotchi.Data.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CurrentTamagotchis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Id_User = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Health = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Age_State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fun = table.Column<int>(type: "int", nullable: false),
                    Hygiene = table.Column<int>(type: "int", nullable: false),
                    Energy = table.Column<int>(type: "int", nullable: false),
                    Hunger = table.Column<int>(type: "int", nullable: false),
                    Max_Health = table.Column<int>(type: "int", nullable: false),
                    Max_Hunger = table.Column<int>(type: "int", nullable: false),
                    Max_Energy = table.Column<int>(type: "int", nullable: false),
                    Max_Hygiene = table.Column<int>(type: "int", nullable: false),
                    Max_Fun = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentTamagotchis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Health = table.Column<int>(type: "int", nullable: false),
                    Hunger = table.Column<int>(type: "int", nullable: false),
                    Energy = table.Column<int>(type: "int", nullable: false),
                    Hygiene = table.Column<int>(type: "int", nullable: false),
                    Fun = table.Column<int>(type: "int", nullable: false),
                    Max_Health = table.Column<int>(type: "int", nullable: false),
                    Max_Hunger = table.Column<int>(type: "int", nullable: false),
                    Max_Energy = table.Column<int>(type: "int", nullable: false),
                    Max_Hygiene = table.Column<int>(type: "int", nullable: false),
                    Max_Fun = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Id_Stat = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_Statistics_Id_Stat",
                        column: x => x.Id_Stat,
                        principalTable: "Statistics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pets_Id_Stat",
                table: "Pets",
                column: "Id_Stat",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrentTamagotchis");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Statistics");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Feedo.Persistance.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SocialNetworks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialNetworks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SocialCommentId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    OriginalComment = table.Column<string>(type: "nvarchar(800)", maxLength: 800, nullable: true),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UserFullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Sentiment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SentimentRate = table.Column<double>(type: "float", nullable: false),
                    SocialNetworkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_SocialNetworks_SocialNetworkId",
                        column: x => x.SocialNetworkId,
                        principalTable: "SocialNetworks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_SocialNetworkId",
                table: "Comments",
                column: "SocialNetworkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "SocialNetworks");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectFY.Migrations
{
    public partial class JIEC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SKUNumber = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    ProductCategory = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    ProductSubCategory = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    ProductPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                    table.UniqueConstraint("AK_Product_SKUNumber", x => x.SKUNumber);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    UserEmail = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    UserPassword = table.Column<string>(type: "nvarchar(1000)", nullable: false),
                    UserRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserID);
                    table.UniqueConstraint("AK_User_UserEmail", x => x.UserEmail);
                });

            migrationBuilder.CreateTable(
                name: "ProductDetails",
                columns: table => new
                {
                    ProductDetailsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductID = table.Column<int>(type: "int", nullable: false),
                    NumberOfSales = table.Column<double>(type: "float", nullable: false),
                    MonthOfSale = table.Column<string>(type: "nvarchar(1000)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDetails", x => x.ProductDetailsID);
                    table.ForeignKey(
                        name: "FK_ProductDetails_Product_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Product",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductDetails_ProductID",
                table: "ProductDetails",
                column: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductDetails");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Product");
        }
    }
}

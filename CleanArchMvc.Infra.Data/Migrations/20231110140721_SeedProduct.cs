using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchMvc.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Products(Name,Description,Price,Stock,Image,CategoryId) " +
               "VALUES('Caderno espiral','caderno escolar espiral 150 folhas',7.65,50,'caderno1.jpg',1)");

            mb.Sql("INSERT INTO Products(Name,Description,Price,Stock,Image,CategoryId) " +
                "VALUES('Estojo escolar','Estojo escolar cinza',5.65,70,'estojo1.jpg',1)");

            mb.Sql("INSERT INTO Products(Name,Description,Price,Stock,Image,CategoryId) " +
               "VALUES('Caneta escolar','Caneta preta da big',2.65,70,'caneta1.jpg',1)");

            mb.Sql("INSERT INTO Products(Name,Description,Price,Stock,Image,CategoryId) " +
               "VALUES('Calculadora','calculadora 3x4',15.65,30,'calculadora1.jpg',1)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Products");
        }
    }
}

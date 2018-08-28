namespace Estamparia.MVC.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulandoTabelaTamanhos : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Tamanhos (TamanhoId, Nome) VALUES (1, 'P')");
            Sql("INSERT INTO Tamanhos (TamanhoId, Nome) VALUES (2, 'M')");
            Sql("INSERT INTO Tamanhos (TamanhoId, Nome) VALUES (3, 'G')");
            Sql("INSERT INTO Tamanhos (TamanhoId, Nome) VALUES (4, 'GG')");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM Tamanhos WHERE TamanhoId IN(1,2,3,4)");
        }
    }
}

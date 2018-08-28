namespace Estamparia.MVC.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class PopulandoTabelaGolas : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT INTO Golas (GolaId, Nome) VALUES (1, 'V')");
            Sql("INSERT INTO Golas (GolaId, Nome) VALUES (2, 'Redonda')");
        }

        public override void Down()
        {
            Sql("DELETE FROM Golas WHERE GolaId IN (1,2)");
        }
    }
}

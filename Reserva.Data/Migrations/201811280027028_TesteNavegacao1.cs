namespace Reserva.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class TesteNavegacao1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ReservaSalas", newName: "ReservaSala");
            DropPrimaryKey("dbo.ReservaSala");
            AddPrimaryKey("dbo.ReservaSala", "Codigo");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ReservaSala");
            AddPrimaryKey("dbo.ReservaSala", "ID");
            RenameTable(name: "dbo.ReservaSala", newName: "ReservaSalas");
        }
    }
}

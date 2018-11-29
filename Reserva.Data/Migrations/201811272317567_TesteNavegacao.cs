namespace Reserva.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class TesteNavegacao : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ReservaSalas", "Codigo", "dbo.Sala");
            DropForeignKey("dbo.ReservaSalas", "Codigo", "dbo.Usuario");
            DropIndex("dbo.ReservaSalas", new[] { "Codigo" });
            AddColumn("dbo.ReservaSalas", "UsuarioCodigo", c => c.Guid(nullable: false));
            AddColumn("dbo.ReservaSalas", "SalaCodigo", c => c.Guid(nullable: false));
            CreateIndex("dbo.ReservaSalas", "UsuarioCodigo");
            CreateIndex("dbo.ReservaSalas", "SalaCodigo");
            AddForeignKey("dbo.ReservaSalas", "SalaCodigo", "dbo.Sala", "Codigo", cascadeDelete: true);
            AddForeignKey("dbo.ReservaSalas", "UsuarioCodigo", "dbo.Usuario", "Codigo", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReservaSalas", "UsuarioCodigo", "dbo.Usuario");
            DropForeignKey("dbo.ReservaSalas", "SalaCodigo", "dbo.Sala");
            DropIndex("dbo.ReservaSalas", new[] { "SalaCodigo" });
            DropIndex("dbo.ReservaSalas", new[] { "UsuarioCodigo" });
            DropColumn("dbo.ReservaSalas", "SalaCodigo");
            DropColumn("dbo.ReservaSalas", "UsuarioCodigo");
            CreateIndex("dbo.ReservaSalas", "Codigo");
            AddForeignKey("dbo.ReservaSalas", "Codigo", "dbo.Usuario", "Codigo", cascadeDelete: true);
            AddForeignKey("dbo.ReservaSalas", "Codigo", "dbo.Sala", "Codigo", cascadeDelete: true);
        }
    }
}

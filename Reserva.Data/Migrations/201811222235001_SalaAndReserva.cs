namespace Reserva.Data.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class SalaAndReserva : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReservaSalas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Codigo = c.Guid(nullable: false),
                        DtInicio = c.DateTime(nullable: false),
                        DtFim = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Sala", t => t.Codigo, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.Codigo, cascadeDelete: true)
                .Index(t => t.Codigo);
            
            CreateTable(
                "dbo.Sala",
                c => new
                    {
                        Codigo = c.Guid(nullable: false),
                        ID = c.Int(nullable: false, identity: true),
                        Capacidade = c.Int(nullable: false),
                        Identificacao = c.String(),
                        Descricao = c.String(),
                    })
                .PrimaryKey(t => t.Codigo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReservaSalas", "Codigo", "dbo.Usuario");
            DropForeignKey("dbo.ReservaSalas", "Codigo", "dbo.Sala");
            DropIndex("dbo.ReservaSalas", new[] { "Codigo" });
            DropTable("dbo.Sala");
            DropTable("dbo.ReservaSalas");
        }
    }
}

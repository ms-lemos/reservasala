namespace Reserva.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Codigo = c.Guid(nullable: false),
                        ID = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Senha = c.String(),
                        Nome = c.String(),
                        Cargo = c.String(),
                    })
                .PrimaryKey(t => t.Codigo);
            
            CreateTable(
                "dbo.ReservaSala",
                c => new
                    {
                        Codigo = c.Guid(nullable: false),
                        ID = c.Int(nullable: false, identity: true),
                        DtInicio = c.DateTime(nullable: false),
                        DtFim = c.DateTime(nullable: false),
                        UsuarioCodigo = c.Guid(nullable: false),
                        SalaCodigo = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Codigo)
                .ForeignKey("dbo.Sala", t => t.SalaCodigo, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.UsuarioCodigo, cascadeDelete: true)
                .Index(t => t.UsuarioCodigo)
                .Index(t => t.SalaCodigo);
            
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
            DropForeignKey("dbo.ReservaSala", "UsuarioCodigo", "dbo.Usuario");
            DropForeignKey("dbo.ReservaSala", "SalaCodigo", "dbo.Sala");
            DropIndex("dbo.ReservaSala", new[] { "SalaCodigo" });
            DropIndex("dbo.ReservaSala", new[] { "UsuarioCodigo" });
            DropTable("dbo.Sala");
            DropTable("dbo.ReservaSala");
            DropTable("dbo.Usuario");
        }
    }
}

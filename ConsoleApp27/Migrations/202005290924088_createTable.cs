namespace ConsoleApp27.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Note",
                c => new
                    {
                        notlarId = c.Int(nullable: false, identity: true),
                        konu = c.String(),
                        baslik = c.String(),
                        User_usersId = c.Int(),
                    })
                .PrimaryKey(t => t.notlarId)
                .ForeignKey("dbo.User", t => t.User_usersId)
                .Index(t => t.User_usersId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        usersId = c.Int(nullable: false, identity: true),
                        kullaniciAdi = c.String(),
                        sifre = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.usersId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Note", "User_usersId", "dbo.User");
            DropIndex("dbo.Note", new[] { "User_usersId" });
            DropTable("dbo.User");
            DropTable("dbo.Note");
        }
    }
}

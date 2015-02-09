namespace ClockIn_ClockOut.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContextUpdateBIG : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.TimeEntries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TimeIn = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        TimeOut = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        timeMinutes = c.Time(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Role = c.Int(nullable: false),
                        Timed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.TimeEntries");
            DropTable("dbo.Roles");
        }
    }
}

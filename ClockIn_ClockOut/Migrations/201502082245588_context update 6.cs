namespace ClockIn_ClockOut.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contextupdate6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TimeEntries", "timeMinutes", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TimeEntries", "timeMinutes");
        }
    }
}

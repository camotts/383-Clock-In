namespace ClockIn_ClockOut.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContextUpdate5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TimeEntries", "TimeIn", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.TimeEntries", "TimeOut", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TimeEntries", "TimeOut", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TimeEntries", "TimeIn", c => c.DateTime(nullable: false));
        }
    }
}

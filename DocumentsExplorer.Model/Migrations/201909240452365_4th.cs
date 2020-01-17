namespace DocumentsExplorer.Model
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4th : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MeetingAttendance", "DepartmentId", c => c.Int());
            AddColumn("dbo.MeetingAttendance", "DepartmentName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MeetingAttendance", "DepartmentName");
            DropColumn("dbo.MeetingAttendance", "DepartmentId");
        }
    }
}

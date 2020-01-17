namespace DocumentsExplorer.Model
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class second : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MeetingAttendance", "CouncilMemberId", "dbo.CouncilMember");
            DropForeignKey("dbo.ReferenceItem", "DecisionId", "dbo.Decision");
            DropIndex("dbo.MeetingAttendance", new[] { "CouncilMemberId" });
            DropIndex("dbo.ReferenceItem", new[] { "Decision_Id" });
            DropColumn("dbo.ReferenceItem", "ReferenceDecisionId");
            RenameColumn(table: "dbo.ReferenceItem", name: "Decision_Id", newName: "ReferenceDecisionId");
            AlterColumn("dbo.MeetingAttendance", "CouncilMemberId", c => c.Int());
            CreateIndex("dbo.MeetingAttendance", "CouncilMemberId");
            AddForeignKey("dbo.MeetingAttendance", "CouncilMemberId", "dbo.CouncilMember", "Id");
            AddForeignKey("dbo.ReferenceItem", "DecisionId", "dbo.Decision", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReferenceItem", "DecisionId", "dbo.Decision");
            DropForeignKey("dbo.MeetingAttendance", "CouncilMemberId", "dbo.CouncilMember");
            DropIndex("dbo.MeetingAttendance", new[] { "CouncilMemberId" });
            AlterColumn("dbo.MeetingAttendance", "CouncilMemberId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.ReferenceItem", name: "ReferenceDecisionId", newName: "Decision_Id");
            AddColumn("dbo.ReferenceItem", "ReferenceDecisionId", c => c.Int());
            CreateIndex("dbo.ReferenceItem", "Decision_Id");
            CreateIndex("dbo.MeetingAttendance", "CouncilMemberId");
            AddForeignKey("dbo.ReferenceItem", "DecisionId", "dbo.Decision", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MeetingAttendance", "CouncilMemberId", "dbo.CouncilMember", "Id", cascadeDelete: true);
        }
    }
}

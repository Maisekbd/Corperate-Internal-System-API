namespace DocumentsExplorer.Model
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contributor", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Decision", "CompanyId", "dbo.Company");
            DropForeignKey("dbo.Decision", "ReferenceDecisionId", "dbo.Decision");
            DropForeignKey("dbo.CouncilMemberCouncilType", "CouncilMember_Id", "dbo.CouncilMember");
            DropForeignKey("dbo.CouncilMemberCouncilType", "CouncilType_Id", "dbo.CouncilType");
            DropForeignKey("dbo.MinutesOfMeeting", "CouncilTypeId", "dbo.CouncilType");
            DropForeignKey("dbo.MeetingAttendance", "MinutesOfMeetingId", "dbo.MinutesOfMeeting");
            DropForeignKey("dbo.MinutesOfMeeting", "RoundId", "dbo.Round");
            DropForeignKey("dbo.Attachment", "MinutesOfMeetingId", "dbo.MinutesOfMeeting");
            DropIndex("dbo.Contributor", new[] { "CompanyId" });
            DropIndex("dbo.Decision", new[] { "CompanyId" });
            DropIndex("dbo.Decision", new[] { "ReferenceDecisionId" });
            DropIndex("dbo.Attachment", new[] { "MinutesOfMeetingId" });
            DropIndex("dbo.MinutesOfMeeting", new[] { "RoundId" });
            DropIndex("dbo.MinutesOfMeeting", new[] { "CouncilTypeId" });
            DropIndex("dbo.MeetingAttendance", new[] { "MinutesOfMeetingId" });
            DropIndex("dbo.CouncilMemberCouncilType", new[] { "CouncilMember_Id" });
            DropIndex("dbo.CouncilMemberCouncilType", new[] { "CouncilType_Id" });
            CreateTable(
                "dbo.Action",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TreeNumber = c.String(),
                        Description = c.String(),
                        ResponsibleId = c.String(),
                        ResponsibleName = c.String(),
                        ActionType = c.Int(nullable: false),
                        AgendaItemId = c.Int(nullable: false),
                        CreationDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdateDate = c.DateTime(),
                        LastUpdateBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AgendaItem", t => t.AgendaItemId, cascadeDelete: true)
                .Index(t => t.AgendaItemId);
            
            CreateTable(
                "dbo.AgendaItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AgendaText = c.String(),
                        AgendaNumber = c.String(),
                        AgendaTitle = c.String(),
                        PresentedBy = c.String(),
                        Conclusion = c.String(),
                        MeetingId = c.Int(nullable: false),
                        CreationDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdateDate = c.DateTime(),
                        LastUpdateBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Meeting", t => t.MeetingId, cascadeDelete: true)
                .Index(t => t.MeetingId);
            
            CreateTable(
                "dbo.Meeting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MeetingNumber = c.Int(nullable: false),
                        MeetingDate = c.DateTime(nullable: false),
                        MeetingTime = c.Time(nullable: false, precision: 7),
                        MeetingSummary = c.String(),
                        Location = c.String(),
                        Objective = c.String(),
                        PreparedById = c.String(),
                        PreparedByName = c.String(),
                        MeetingIndexNumber = c.String(),
                        RoundId = c.Int(nullable: false),
                        CouncilTypeId = c.Int(nullable: false),
                        CreationDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdateDate = c.DateTime(),
                        LastUpdateBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CouncilType", t => t.CouncilTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Round", t => t.RoundId, cascadeDelete: true)
                .Index(t => t.RoundId)
                .Index(t => t.CouncilTypeId);
            
            CreateTable(
                "dbo.MeetingAttachment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Path = c.String(),
                        FileExtension = c.String(),
                        MeetingId = c.Int(nullable: false),
                        CreationDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdateDate = c.DateTime(),
                        LastUpdateBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Meeting", t => t.MeetingId, cascadeDelete: true)
                .Index(t => t.MeetingId);
            
            CreateTable(
                "dbo.ReferenceItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        Path = c.String(),
                        FileExtension = c.String(),
                        ReferenceTypeId = c.Int(nullable: false),
                        DecisionId = c.Int(nullable: false),
                        ReferenceDecisionId = c.Int(),
                        CreationDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdateDate = c.DateTime(),
                        LastUpdateBy = c.String(),
                        Decision_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Decision", t => t.DecisionId, cascadeDelete: true)
                .ForeignKey("dbo.Decision", t => t.ReferenceDecisionId)
                .ForeignKey("dbo.ReferenceType", t => t.ReferenceTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Decision", t => t.Decision_Id)
                .Index(t => t.ReferenceTypeId)
                .Index(t => t.DecisionId)
                .Index(t => t.ReferenceDecisionId)
                .Index(t => t.Decision_Id);
            
            CreateTable(
                "dbo.ReferenceType",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreationDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdateDate = c.DateTime(),
                        LastUpdateBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.DepartmentCoordinator",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(),
                        DepartmentId = c.Int(nullable: false),
                        DepartmentName = c.String(),
                        EmployeeId = c.Int(nullable: false),
                        EmployeeName = c.String(),
                        CreationDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdateDate = c.DateTime(),
                        LastUpdateBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Notification",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EmployeeId = c.Int(nullable: false),
                        EmployeeNo = c.String(),
                        EmployeeMail = c.String(),
                        Title = c.String(),
                        Body = c.String(),
                        status = c.Int(nullable: false),
                        DueDate = c.DateTime(nullable: false),
                        IsOpen = c.Boolean(nullable: false),
                        NotificationType = c.Int(nullable: false),
                        DecisionId = c.Int(),
                        CreationDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdateDate = c.DateTime(),
                        LastUpdateBy = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Decision", t => t.DecisionId)
                .Index(t => t.DecisionId);
            
            AddColumn("dbo.Decision", "AgendaItemId", c => c.Int());
            AddColumn("dbo.Decision", "ActionId", c => c.Int());
            AddColumn("dbo.DecisionExecution", "DepartmentName", c => c.String());
            AddColumn("dbo.DecisionExecution", "ActionType", c => c.Int(nullable: false));
            AddColumn("dbo.CouncilMember", "CouncilTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.CouncilMember", "CouncilType_Id", c => c.Int());
            AddColumn("dbo.Attachment", "DecisionExecutionId", c => c.Int(nullable: false));
            AddColumn("dbo.MeetingAttendance", "Name", c => c.String());
            AddColumn("dbo.MeetingAttendance", "Email", c => c.String());
            AddColumn("dbo.MeetingAttendance", "JobDescription", c => c.String());
            AddColumn("dbo.MeetingAttendance", "EmployeId", c => c.String());
            AddColumn("dbo.MeetingAttendance", "EmployeName", c => c.String());
            AddColumn("dbo.MeetingAttendance", "MemberType", c => c.Int(nullable: false));
            AddColumn("dbo.MeetingAttendance", "Role", c => c.Int(nullable: false));
            AddColumn("dbo.MeetingAttendance", "CauseOfAbsence", c => c.String());
            AddColumn("dbo.MeetingAttendance", "MeetingId", c => c.Int(nullable: false));
            AddColumn("dbo.Round", "CouncilTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.CouncilMember", "CouncilTypeId");
            CreateIndex("dbo.CouncilMember", "CouncilType_Id");
            CreateIndex("dbo.MeetingAttendance", "MeetingId");
            CreateIndex("dbo.Round", "CouncilTypeId");
            CreateIndex("dbo.Decision", "AgendaItemId");
            CreateIndex("dbo.Decision", "ActionId");
            CreateIndex("dbo.Attachment", "DecisionExecutionId");
            AddForeignKey("dbo.CouncilMember", "CouncilTypeId", "dbo.CouncilType", "Id");
            AddForeignKey("dbo.CouncilMember", "CouncilType_Id", "dbo.CouncilType", "Id");
            AddForeignKey("dbo.MeetingAttendance", "MeetingId", "dbo.Meeting", "Id");
            AddForeignKey("dbo.Round", "CouncilTypeId", "dbo.CouncilType", "Id");
            AddForeignKey("dbo.Decision", "ActionId", "dbo.Action", "Id");
            AddForeignKey("dbo.Decision", "AgendaItemId", "dbo.AgendaItem", "Id");
            AddForeignKey("dbo.Attachment", "DecisionExecutionId", "dbo.DecisionExecution", "Id", cascadeDelete: true);
            DropColumn("dbo.Decision", "CompanyId");
            DropColumn("dbo.Decision", "ContributorsProfitPercentage");
            DropColumn("dbo.Decision", "LegalReserve");
            DropColumn("dbo.Decision", "PalestineSupport");
            DropColumn("dbo.Decision", "MainReserve");
            DropColumn("dbo.Decision", "ProgramValue");
            DropColumn("dbo.Decision", "ProgramValueInDollar");
            DropColumn("dbo.Decision", "ReferenceDecisionId");
            DropColumn("dbo.SubCategory", "NeedCompanyField");
            DropColumn("dbo.SubCategory", "NeedBudgetFields");
            DropColumn("dbo.SubCategory", "NeedInvestmentProgramFields");
            DropColumn("dbo.SubCategory", "IsLoan");
            DropColumn("dbo.Attachment", "MinutesOfMeetingId");
            DropColumn("dbo.Attachment", "CreateDate");
            DropColumn("dbo.MeetingAttendance", "MinutesOfMeetingId");
            DropTable("dbo.Contributor");
            DropTable("dbo.MinutesOfMeeting");
            DropTable("dbo.CouncilMemberCouncilType");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.CouncilMemberCouncilType",
                c => new
                    {
                        CouncilMember_Id = c.Int(nullable: false),
                        CouncilType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CouncilMember_Id, t.CouncilType_Id });
            
            CreateTable(
                "dbo.MinutesOfMeeting",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoundId = c.Int(nullable: false),
                        CouncilTypeId = c.Int(nullable: false),
                        MeetingDate = c.DateTime(nullable: false),
                        Location = c.String(),
                        MeetingSummary = c.String(),
                        CreateDate = c.DateTime(),
                        CreationDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdateDate = c.DateTime(),
                        LastUpdateBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contributor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreateDate = c.DateTime(),
                        CompanyId = c.Int(nullable: false),
                        CreationDate = c.DateTime(),
                        CreatedBy = c.String(),
                        LastUpdateDate = c.DateTime(),
                        LastUpdateBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.MeetingAttendance", "MinutesOfMeetingId", c => c.Int(nullable: false));
            AddColumn("dbo.Attachment", "CreateDate", c => c.DateTime());
            AddColumn("dbo.Attachment", "MinutesOfMeetingId", c => c.Int(nullable: false));
            AddColumn("dbo.SubCategory", "IsLoan", c => c.Boolean(nullable: false));
            AddColumn("dbo.SubCategory", "NeedInvestmentProgramFields", c => c.Boolean(nullable: false));
            AddColumn("dbo.SubCategory", "NeedBudgetFields", c => c.Boolean(nullable: false));
            AddColumn("dbo.SubCategory", "NeedCompanyField", c => c.Boolean(nullable: false));
            AddColumn("dbo.Decision", "ReferenceDecisionId", c => c.Int());
            AddColumn("dbo.Decision", "ProgramValueInDollar", c => c.Single(nullable: false));
            AddColumn("dbo.Decision", "ProgramValue", c => c.Single(nullable: false));
            AddColumn("dbo.Decision", "MainReserve", c => c.Single(nullable: false));
            AddColumn("dbo.Decision", "PalestineSupport", c => c.Single(nullable: false));
            AddColumn("dbo.Decision", "LegalReserve", c => c.Single(nullable: false));
            AddColumn("dbo.Decision", "ContributorsProfitPercentage", c => c.Single(nullable: false));
            AddColumn("dbo.Decision", "CompanyId", c => c.Int());
            DropForeignKey("dbo.Notification", "DecisionId", "dbo.Decision");
            DropForeignKey("dbo.ReferenceItem", "Decision_Id", "dbo.Decision");
            DropForeignKey("dbo.ReferenceItem", "ReferenceTypeId", "dbo.ReferenceType");
            DropForeignKey("dbo.ReferenceItem", "ReferenceDecisionId", "dbo.Decision");
            DropForeignKey("dbo.ReferenceItem", "DecisionId", "dbo.Decision");
            DropForeignKey("dbo.Attachment", "DecisionExecutionId", "dbo.DecisionExecution");
            DropForeignKey("dbo.Decision", "AgendaItemId", "dbo.AgendaItem");
            DropForeignKey("dbo.Decision", "ActionId", "dbo.Action");
            DropForeignKey("dbo.Action", "AgendaItemId", "dbo.AgendaItem");
            DropForeignKey("dbo.AgendaItem", "MeetingId", "dbo.Meeting");
            DropForeignKey("dbo.Meeting", "RoundId", "dbo.Round");
            DropForeignKey("dbo.Round", "CouncilTypeId", "dbo.CouncilType");
            DropForeignKey("dbo.MeetingAttendance", "MeetingId", "dbo.Meeting");
            DropForeignKey("dbo.Meeting", "CouncilTypeId", "dbo.CouncilType");
            DropForeignKey("dbo.CouncilMember", "CouncilType_Id", "dbo.CouncilType");
            DropForeignKey("dbo.CouncilMember", "CouncilTypeId", "dbo.CouncilType");
            DropForeignKey("dbo.MeetingAttachment", "MeetingId", "dbo.Meeting");
            DropIndex("dbo.Notification", new[] { "DecisionId" });
            DropIndex("dbo.ReferenceItem", new[] { "Decision_Id" });
            DropIndex("dbo.ReferenceItem", new[] { "ReferenceDecisionId" });
            DropIndex("dbo.ReferenceItem", new[] { "DecisionId" });
            DropIndex("dbo.ReferenceItem", new[] { "ReferenceTypeId" });
            DropIndex("dbo.Attachment", new[] { "DecisionExecutionId" });
            DropIndex("dbo.Decision", new[] { "ActionId" });
            DropIndex("dbo.Decision", new[] { "AgendaItemId" });
            DropIndex("dbo.Round", new[] { "CouncilTypeId" });
            DropIndex("dbo.MeetingAttendance", new[] { "MeetingId" });
            DropIndex("dbo.CouncilMember", new[] { "CouncilType_Id" });
            DropIndex("dbo.CouncilMember", new[] { "CouncilTypeId" });
            DropIndex("dbo.MeetingAttachment", new[] { "MeetingId" });
            DropIndex("dbo.Meeting", new[] { "CouncilTypeId" });
            DropIndex("dbo.Meeting", new[] { "RoundId" });
            DropIndex("dbo.AgendaItem", new[] { "MeetingId" });
            DropIndex("dbo.Action", new[] { "AgendaItemId" });
            DropColumn("dbo.Round", "CouncilTypeId");
            DropColumn("dbo.MeetingAttendance", "MeetingId");
            DropColumn("dbo.MeetingAttendance", "CauseOfAbsence");
            DropColumn("dbo.MeetingAttendance", "Role");
            DropColumn("dbo.MeetingAttendance", "MemberType");
            DropColumn("dbo.MeetingAttendance", "EmployeName");
            DropColumn("dbo.MeetingAttendance", "EmployeId");
            DropColumn("dbo.MeetingAttendance", "JobDescription");
            DropColumn("dbo.MeetingAttendance", "Email");
            DropColumn("dbo.MeetingAttendance", "Name");
            DropColumn("dbo.Attachment", "DecisionExecutionId");
            DropColumn("dbo.CouncilMember", "CouncilType_Id");
            DropColumn("dbo.CouncilMember", "CouncilTypeId");
            DropColumn("dbo.DecisionExecution", "ActionType");
            DropColumn("dbo.DecisionExecution", "DepartmentName");
            DropColumn("dbo.Decision", "ActionId");
            DropColumn("dbo.Decision", "AgendaItemId");
            DropTable("dbo.Notification");
            DropTable("dbo.DepartmentCoordinator");
            DropTable("dbo.ReferenceType");
            DropTable("dbo.ReferenceItem");
            DropTable("dbo.MeetingAttachment");
            DropTable("dbo.Meeting");
            DropTable("dbo.AgendaItem");
            DropTable("dbo.Action");
            CreateIndex("dbo.CouncilMemberCouncilType", "CouncilType_Id");
            CreateIndex("dbo.CouncilMemberCouncilType", "CouncilMember_Id");
            CreateIndex("dbo.MeetingAttendance", "MinutesOfMeetingId");
            CreateIndex("dbo.MinutesOfMeeting", "CouncilTypeId");
            CreateIndex("dbo.MinutesOfMeeting", "RoundId");
            CreateIndex("dbo.Attachment", "MinutesOfMeetingId");
            CreateIndex("dbo.Decision", "ReferenceDecisionId");
            CreateIndex("dbo.Decision", "CompanyId");
            CreateIndex("dbo.Contributor", "CompanyId");
            AddForeignKey("dbo.Attachment", "MinutesOfMeetingId", "dbo.MinutesOfMeeting", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MinutesOfMeeting", "RoundId", "dbo.Round", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MeetingAttendance", "MinutesOfMeetingId", "dbo.MinutesOfMeeting", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MinutesOfMeeting", "CouncilTypeId", "dbo.CouncilType", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CouncilMemberCouncilType", "CouncilType_Id", "dbo.CouncilType", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CouncilMemberCouncilType", "CouncilMember_Id", "dbo.CouncilMember", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Decision", "ReferenceDecisionId", "dbo.Decision", "Id");
            AddForeignKey("dbo.Decision", "CompanyId", "dbo.Company", "Id");
            AddForeignKey("dbo.Contributor", "CompanyId", "dbo.Company", "Id", cascadeDelete: true);
        }
    }
}

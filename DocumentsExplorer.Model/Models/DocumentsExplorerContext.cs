using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocumentsExplorer.Model.Models
{
    public partial class DocumentsExplorerContext : DbContext
    {
        static DocumentsExplorerContext()
        {
            //Database.SetInitializer<DocumentsExplorerContext>(null);
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DocumentsExplorerContext, DocumentExplorerMigrationConfiguration>("DocumentsExplorerContext"));

        }

        public DocumentsExplorerContext()
            : base("Name=DocumentsExplorerContext")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<CouncilType> CouncilTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<Decision> Decisions { get; set; }
        public DbSet<CouncilMember> CouncilMembers { get; set; }
        public DbSet<Round> Rounds { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<MeetingAttendance> MeetingAttendances { get; set; }
        public DbSet<AgendaItem> AgendaItems { get; set; }
        public DbSet<AgendaDetail> Actions { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<ActivitySector> ActivitySectors { get; set; }
        public DbSet<DecisionType> DecisionTypes { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<ReferenceType> ReferenceTypes { get; set; }
        public DbSet<DepartmentResponsible> DepartmentResponsibles { get; set; }
        public DbSet<DepartmentCoordinator> DepartmentCoordinators { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<CouncilTypePermission> CouncilTypePermissions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            /// Company- ActivitySector Many_To_Many
            modelBuilder.Entity<Company>()
                .HasMany<ActivitySector>(s => s.ActivitySectors)
                .WithMany(c => c.Companies)
                .Map(cs =>
                {
                    cs.MapLeftKey("CompanyId");
                    cs.MapRightKey("ActivitySectorId");
                    cs.ToTable("CompanySectors");
                });

            /// Decision- ActivitySector Many_To_Many
            modelBuilder.Entity<Decision>()
              .HasMany<ActivitySector>(s => s.ActivitySectors)
              .WithMany(c => c.Decisions)
              .Map(cs =>
              {
                  cs.MapLeftKey("DecisionId");
                  cs.MapRightKey("ActivitySectorId");
                  cs.ToTable("DecisionSectors");
              });

            /// Decision- Company Many_To_Many
            modelBuilder.Entity<Decision>()
             .HasMany<Company>(s => s.Companies)
             .WithMany(c => c.Decisions)
             .Map(cs =>
             {
                 cs.MapLeftKey("DecisionId");
                 cs.MapRightKey("CompanyId");
                 cs.ToTable("DecisionCompanies");
             });

            modelBuilder.Entity<Decision>()
            .HasMany<Department>(s => s.Departments)
            .WithMany(c => c.Decisions)
            .Map(cs =>
            {
                cs.MapLeftKey("DecisionId");
                cs.MapRightKey("DepartmentId");
                cs.ToTable("DecisionDepartments");
            });

            modelBuilder.Entity<MeetingAttendance>()
                .HasRequired(c => c.Meeting)
                .WithMany(m => m.MeetingAttendances)
                .HasForeignKey(m => m.MeetingId)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<AgendaItem>()
               .HasRequired(c => c.Meeting)
               .WithMany(m => m.AgendaItems)
               .HasForeignKey(m => m.MeetingId)
               .WillCascadeOnDelete(true);


            modelBuilder.Entity<Round>()
                .HasRequired(l => l.CouncilType)
                .WithMany()
                .HasForeignKey(l => l.CouncilTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<CouncilMember>()
                .HasRequired(l => l.CouncilType)
                .WithMany()
                .HasForeignKey(l => l.CouncilTypeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ReferenceItem>()
                .HasRequired(l => l.Decision)
                .WithMany(c => c.ReferenceItems)
                .HasForeignKey(l => l.DecisionId)
                .WillCascadeOnDelete(false);


            modelBuilder.Entity<ReferenceItem>()
                .HasOptional(ccr => ccr.ReferenceDecision)
                .WithMany()
                .HasForeignKey(ccr => ccr.ReferenceDecisionId)
                .WillCascadeOnDelete(false);


            //modelBuilder.Entity<Attachment>()
            //.HasRequired<MinutesOfMeeting>(s => s.MinutesOfMeeting)
            //.WithMany(g => g.Attachments)
            //.HasForeignKey<int>(s => s.MinutesOfMeetingId);

        }
    }
}

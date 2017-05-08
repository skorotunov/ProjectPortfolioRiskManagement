using Microsoft.AspNet.Identity.EntityFramework;
using ProjectPortfolioRiskManager.Domain.Concrete;
using ProjectPortfolioRiskManager.Domain.Migrations;
using System.Data.Entity;

namespace ProjectPortfolioRiskManager.Domain.Infrastructure
{
    public class EFDbContext : IdentityDbContext<User>
    {
        public DbSet<Questionnaire> Questionnaires { get; set; }
        public DbSet<Template> Templates { get; set; }
        public DbSet<CompanySize> CompanySizes { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<LikertItem> LikertItems { get; set; }
        public DbSet<Templates_LikertItems> Templates_LikertItems { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        public EFDbContext()
            : base("EFDbContext")
        { }

        static EFDbContext()
        {
            Database.SetInitializer<EFDbContext>(new IdentityDbInit());
        }

        public static EFDbContext Create()
        {
            return new EFDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Questionnaire>()
                .HasRequired(x => x.Template)
                .WithMany(y => y.Questionnaires)
                .HasForeignKey(x => x.TemplateId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Questionnaire>()
                .HasRequired(x => x.CompanySize)
                .WithMany(y => y.Questionnaires)
                .HasForeignKey(x => x.CompanySizeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Questionnaire>()
                .HasRequired(x => x.Position)
                .WithMany(y => y.Questionnaires)
                .HasForeignKey(x => x.PositionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Questionnaire>()
                .HasRequired(x => x.User)
                .WithMany(y => y.Questionnaires)
                .HasForeignKey(x => x.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Template>()
                .HasMany(x => x.CompanySizes)
                .WithMany(y => y.Templates)
                .Map(xy =>
                {
                    xy.MapLeftKey("TemplateRefId");
                    xy.MapRightKey("CompanySizeRefId");
                    xy.ToTable("Templates_CompanySizes");
                });

            modelBuilder.Entity<Template>()
                .HasMany(x => x.Positions)
                .WithMany(y => y.Templates)
                .Map(xy =>
                {
                    xy.MapLeftKey("TemplateRefId");
                    xy.MapRightKey("PositionRefId");
                    xy.ToTable("Templates_Positions");
                });

            modelBuilder.Entity<Template>()
                .HasOptional(x => x.CreationUser)
                .WithMany(y => y.CreatedTemplates)
                .HasForeignKey(x => x.CreationUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Template>()
                .HasOptional(x => x.LastUpdateUser)
                .WithMany(y => y.UpdatedTemplates)
                .HasForeignKey(x => x.LastUpdateUserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Section>()
                .HasRequired(x => x.Template)
                .WithMany(y => y.Sections)
                .HasForeignKey(x => x.TemplateId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Question>()
                .HasRequired(x => x.Section)
                .WithMany(y => y.Questions)
                .HasForeignKey(x => x.SectionId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Answer>()
                .HasRequired(x => x.Questionnaire)
                .WithMany(y => y.Answers)
                .HasForeignKey(x => x.QuestionnaireId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Answer>()
                .HasRequired(x => x.LikertItem)
                .WithMany(y => y.Answers)
                .HasForeignKey(x => x.LikertItemId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Answer>()
                .HasRequired(x => x.Question)
                .WithMany(y => y.Answers)
                .HasForeignKey(x => x.QuestionId)
                .WillCascadeOnDelete(false);            
        }
    }

    public class IdentityDbInit : MigrateDatabaseToLatestVersion<EFDbContext, Configuration>
    { }
}

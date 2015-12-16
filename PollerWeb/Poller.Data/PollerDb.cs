namespace Poller.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Migrations;
    using System.Data.Entity;

    public class PollerDb : IdentityDbContext<ApplicationUser>
    {
        public PollerDb()
            : base("PollerDbConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<PollerDb, Configuration>());
        }

        public static PollerDb Create()
        {
            return new PollerDb();
        }

        public IDbSet<Poll> Polls { get; set; }

        public IDbSet<PollQuestion> PollQuestions { get; set; }

        public IDbSet<PollAnswer> PollAnswers { get; set; }

        public System.Data.Entity.DbSet<Poller.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}

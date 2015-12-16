namespace Poller.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Models;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<PollerDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(PollerDb context)
        {
            //var hasher = new PasswordHasher();
            //var pass = hasher.HashPassword("123123");
            //var user = new ApplicationUser { UserName = "slav@slav.pk", Email = "slav@slav.pk", PasswordHash = pass };
            //context.Users.Add(user);

            //context.SaveChanges();
        }
    }
}

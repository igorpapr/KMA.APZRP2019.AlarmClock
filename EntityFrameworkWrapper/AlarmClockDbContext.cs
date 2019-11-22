using System.Data.Entity;
using KMA.APZRP2019.AlarmClock.EntityFrameworkWrapper.Migrations;
using KMA.APZRP2019.AlarmClock.EntityFrameworkWrapper.ModelConfiguration;
using User = KMA.APZRP2019.AlarmClock.DBModels.User;
namespace KMA.APZRP2019.AlarmClock.EntityFrameworkWrapper
{
    public class AlarmClockDbContext : DbContext
    {
        public AlarmClockDbContext(): base("DB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<AlarmClockDbContext, Configuration>());
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new AlarmClockConfiguration());
        }
    }
}

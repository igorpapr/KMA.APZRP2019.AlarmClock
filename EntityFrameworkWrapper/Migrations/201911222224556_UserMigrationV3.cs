namespace KMA.APZRP2019.AlarmClock.EntityFrameworkWrapper.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UserMigrationV3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.User", "Login", c => c.String(nullable: false));
            AddColumn("dbo.User", "LastLoginTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.User", "LastLoginTime");
            DropColumn("dbo.User", "Login");
        }
    }
}

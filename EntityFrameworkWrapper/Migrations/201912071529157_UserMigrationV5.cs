namespace KMA.APZRP2019.AlarmClock.EntityFrameworkWrapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserMigrationV5 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.User", new[] { "Email", "Login" });
            CreateIndex("dbo.User", "Login", unique: true);
            CreateIndex("dbo.User", "Email", unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.User", new[] { "Email" });
            DropIndex("dbo.User", new[] { "Login" });
            CreateIndex("dbo.User", new[] { "Email", "Login" }, unique: true);
        }
    }
}

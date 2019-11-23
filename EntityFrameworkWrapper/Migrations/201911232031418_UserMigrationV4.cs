namespace KMA.APZRP2019.AlarmClock.EntityFrameworkWrapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserMigrationV4 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.User", new[] { "Email" });
            AlterColumn("dbo.User", "Login", c => c.String(nullable: false, maxLength: 256));
            CreateIndex("dbo.User", new[] { "Email", "Login" }, unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.User", new[] { "Email", "Login" });
            AlterColumn("dbo.User", "Login", c => c.String(nullable: false));
            CreateIndex("dbo.User", "Email", unique: true);
        }
    }
}

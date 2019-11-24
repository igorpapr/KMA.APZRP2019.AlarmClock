namespace KMA.APZRP2019.AlarmClock.EntityFrameworkWrapper.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UserMigrationV1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 256),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Guid)
                .Index(t => t.Email, unique: true);
            
            CreateTable(
                "dbo.AlarmClock",
                c => new
                    {
                        Guid = c.Guid(nullable: false),
                        NextAlarmTime = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        NextAlarmTime1 = c.DateTime(nullable: false),
                        OwnerGuid = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Guid)
                .ForeignKey("dbo.User", t => t.OwnerGuid, cascadeDelete: true)
                .Index(t => t.OwnerGuid);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlarmClock", "OwnerGuid", "dbo.User");
            DropIndex("dbo.AlarmClock", new[] { "OwnerGuid" });
            DropIndex("dbo.User", new[] { "Email" });
            DropTable("dbo.AlarmClock");
            DropTable("dbo.User");
        }
    }
}

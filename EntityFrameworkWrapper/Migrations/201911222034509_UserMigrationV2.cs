namespace KMA.APZRP2019.AlarmClock.EntityFrameworkWrapper.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserMigrationV2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AlarmClock", name: "NextAlarmTime", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.AlarmClock", name: "NextAlarmTime1", newName: "NextAlarmTime");
            RenameColumn(table: "dbo.AlarmClock", name: "__mig_tmp__0", newName: "LastAlarmTime");
            AlterColumn("dbo.AlarmClock", "NextAlarmTime", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AlarmClock", "NextAlarmTime", c => c.DateTime(nullable: false));
            RenameColumn(table: "dbo.AlarmClock", name: "LastAlarmTime", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.AlarmClock", name: "NextAlarmTime", newName: "NextAlarmTime1");
            RenameColumn(table: "dbo.AlarmClock", name: "__mig_tmp__0", newName: "NextAlarmTime");
        }
    }
}

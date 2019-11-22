using System.Data.Entity.ModelConfiguration;

namespace KMA.APZRP2019.AlarmClock.EntityFrameworkWrapper.ModelConfiguration
{
    public class AlarmClockConfiguration : EntityTypeConfiguration<DBModels.AlarmClock>
    {
        public AlarmClockConfiguration()
        {
            ToTable("AlarmClock");
            HasKey(d => d.Guid);
            Property(d => d.Guid).HasColumnName("Guid").IsRequired();
            Property(d => d.LastAlarmTime).HasColumnName("LastAlarmTime").HasColumnType("datetime2").IsRequired();
            Property(d => d.NextAlarmTime).HasColumnName("NextAlarmTime").HasColumnType("datetime2").IsRequired();

        }
    }
}

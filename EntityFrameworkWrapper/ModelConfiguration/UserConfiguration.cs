using KMA.APZRP2019.AlarmClock.DBModels;
using System.Data.Entity.ModelConfiguration;

namespace KMA.APZRP2019.AlarmClock.EntityFrameworkWrapper.ModelConfiguration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("User");
            HasKey(user => user.Guid);
            Property(user => user.Guid).HasColumnName("Guid").IsRequired();
            Property(user => user.FirstName).HasColumnName("FirstName").IsRequired();
            Property(user => user.LastName).HasColumnName("LastName").IsRequired();
            Property(user => user.Login).HasColumnName("Login").HasMaxLength(256).IsRequired();
            Property(user => user.Email).HasColumnName("Email").HasMaxLength(256).IsRequired();
            Property(user => user.Password).HasColumnName("Password").IsRequired();
            Property(user => user.LastLoginTime).HasColumnName("LastLoginTime").HasColumnType("datetime2").IsRequired();
            HasIndex(ind => new {ind.Email, ind.Login }).IsUnique(true);

        }
    }
}

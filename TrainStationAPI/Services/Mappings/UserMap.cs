using FluentNHibernate.Mapping;
using NHibernate.Id;
using TrainStationAPI.Model;

namespace TrainStationAPI.Services.Mappings
{
    public class UserMap : ClassMap<UserModel>
    {
        public UserMap()
        {
            Id(x => x.Id).GeneratedBy.UuidString();
            Map(x => x.UserName);
            Map(x => x.Email);
            Map(x => x.EmailConfirmed);
            Map(x => x.NormalizedEmail);
            Map(x => x.NormalizedUserName);
            Map(x => x.PasswordHash);
            Map(x => x.PhoneNumber);
            Map(x => x.PhoneNumberConfirmed);
            Map(x => x.SecurityStamp);
            Map(x => x.TwoFactorEnabled);
            Map(x => x.ConcurrencyStamp);
            Map(x => x.LockoutEnabled);
            Map(x => x.LockoutEnd);
            Map(x => x.LockoutEndUnixTimeSeconds);
            Map(x => x.AccessFailedCount);
        }
    }
}

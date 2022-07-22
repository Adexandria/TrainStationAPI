using FluentNHibernate.Mapping;
using NHibernate.Id;
using TrainStationAPI.Model;

namespace TrainStationAPI.Services.Mappings
{
    public class UserClaimMap : ClassMap<UserClaim>
    {
      public UserClaimMap()
        {
          //Table("AspNetUserClaims");
            Id(x => x.Id).Column("Id").GeneratedBy.Identity();
            Map(x => x.ClaimType).Column("ClaimType");
            Map(x => x.ClaimValue).Column("ClaimValue");
            References(x => x.UserId).Class<UserModel>().Column("UserId").Not.Nullable();
        }
    }
}

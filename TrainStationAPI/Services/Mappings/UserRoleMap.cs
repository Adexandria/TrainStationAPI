using FluentNHibernate.Mapping;
using NHibernate.Id;
using TrainStationAPI.Model;

namespace TrainStationAPI.Services.Mappings
{
    public class UserRoleMap //:ClassMap<UserRole>
    {
       /* public UserRoleMap()
        {
            CompositeId().KeyReference(x=>x.RoleId);
            CompositeId().KeyReference(x=>x.UserId);
        }*/
    }
}

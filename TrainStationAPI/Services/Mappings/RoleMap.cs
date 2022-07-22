using FluentNHibernate.Mapping;
using NHibernate.Id;
using TrainStationAPI.Model;

namespace TrainStationAPI.Services.Mappings
{
    public class RoleMap :ClassMap<Role>
    {
        public RoleMap()
        {
            Id(x => x.Id);
            Map(x => x.Name);           
            Map(x => x.ConcurrencyStamp);
            Map(x => x.NormalizedName);
        }
    }
}

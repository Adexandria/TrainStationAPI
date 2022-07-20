using FluentNHibernate.Mapping;
using TrainStationAPI.Model;

namespace TrainStationAPI.Services.Mappings
{
    public class TrainMap : ClassMap<Train>
    {
        public TrainMap()
        {
            Id(x => x.TrainId).GeneratedBy.Guid();
            Map(x => x.Name);
        }
    }
}

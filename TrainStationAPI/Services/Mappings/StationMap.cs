using FluentNHibernate.Mapping;
using TrainStationAPI.Model;

namespace TrainStationAPI.Services.Mappings
{
    public class StationMap :ClassMap<Station>
    {
        public StationMap()
        {
            Id(x => x.StationId).GeneratedBy.Guid();
            Map(x => x.Name);
            Map(x => x.Location);
            
        }
    }
}

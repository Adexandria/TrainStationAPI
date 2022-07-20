using FluentNHibernate.Mapping;
using TrainStationAPI.Model;

namespace TrainStationAPI.Services.Mappings
{
    public class TrainInfoMap :ClassMap<TrainInfo>
    {
        public TrainInfoMap()
        {
            Id(x => x.InfoId).GeneratedBy.Guid();
            Map(x => x.Capacity);
            Map(x => x.Description);
            References(x => x.Train).Unique();
        }
    }
}

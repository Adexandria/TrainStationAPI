using FluentNHibernate.Mapping;
using TrainStationAPI.Model;

namespace TrainStationAPI.Services.Mappings
{
    public class ConnectionMap : ClassMap<Connection>
    {
        public ConnectionMap()
        {
            Id(x => x.ConnectionId).GeneratedBy.Guid();
            Map(x => x.DepartureTime);
            Map(x => x.ArrivalTime);
            Map(x => x.Duration);
            References(x => x.Train).Unique();
            References(x => x.CurrentStation).Column("CurrentStationId").Unique();
            References(x => x.NextStation).Column("NextStationId").Unique();
        }
    }
}

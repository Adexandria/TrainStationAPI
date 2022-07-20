using TrainStationAPI.Model.DTO.Station;
using TrainStationAPI.Model.DTO.Train;

namespace TrainStationAPI.Model.DTO.Connection
{
    public class ConnectionDTO
    {
        public virtual Guid ConnectionId { get; set; }
        public virtual TrainDTO Train { get; set; }
        public virtual StationDTO Station { get; set; }
        public virtual DateTime DepartureTime { get; set; }
        public virtual DateTime ArrivalTime { get; set; }
        public virtual double Duration { get; set; }
    }
}

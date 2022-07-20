using TrainStationAPI.Model.DTO.Connection;

namespace TrainStationAPI.Model.DTO.Station
{
    public class StationDTO
    {
        public virtual Guid StationId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Location { get; set; }
    }
}
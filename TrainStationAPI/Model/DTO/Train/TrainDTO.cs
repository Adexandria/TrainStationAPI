using TrainStationAPI.Model.DTO.Connection;
using TrainStationAPI.Model.DTO.TrainInfo;

namespace TrainStationAPI.Model.DTO.Train
{
    public class TrainDTO
    {
        public virtual Guid TrainId { get; set; }
        public virtual string Name { get; set; }
        public TrainInfoDTO TrainInformation { get; set; }
    }
}

using TrainStationAPI.Model.DTO.Train;

namespace TrainStationAPI.Model.DTO.TrainInfo
{
    public class TrainInfoDTO
    {
        public virtual Guid InfoId { get; set; }
        public virtual int Capacity { get; set; }
        public virtual string Description { get; set; }
        public virtual TrainDTO Train { get; set; }
    }
}
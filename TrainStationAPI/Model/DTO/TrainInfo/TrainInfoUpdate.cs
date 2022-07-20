using System.ComponentModel.DataAnnotations;

namespace TrainStationAPI.Model.DTO.TrainInfo
{
    public class TrainInfoUpdate
    {
        public virtual int Capacity { get; set; }
        public virtual string Description { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace TrainStationAPI.Model.DTO.TrainInfo
{
    public class TrainInfoCreate
    {
        [Required(ErrorMessage = "Enter number of seats")]
        public virtual int Capacity { get; set; }
        
        [Required(ErrorMessage = "Enter Description")]
        public virtual string Description { get; set; }
    }
}

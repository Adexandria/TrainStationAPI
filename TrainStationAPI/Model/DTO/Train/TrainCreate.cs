using System.ComponentModel.DataAnnotations;

namespace TrainStationAPI.Model.DTO.Train
{
    public class TrainCreate
    {
        [Required(ErrorMessage = "Enter a train name")]
        public virtual string Name { get; set; }

    }
}

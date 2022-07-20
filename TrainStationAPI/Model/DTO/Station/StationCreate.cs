using System.ComponentModel.DataAnnotations;

namespace TrainStationAPI.Model.DTO.Station
{
    public class StationCreate 
    {
        [Required(ErrorMessage ="Enter Station Name")]
        public virtual string Name { get; set; }
        
        [Required(ErrorMessage = "Enter Station Location")]
        public virtual string Location { get; set; }
    }
}

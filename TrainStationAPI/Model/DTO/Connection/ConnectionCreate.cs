using System.ComponentModel.DataAnnotations;

namespace TrainStationAPI.Model.DTO.Connection
{
    public class ConnectionCreate
    {
        [Required(ErrorMessage="Enter Train Id")]
        public Guid TrainId { get; set; }
        [Required(ErrorMessage = "Enter Current Station Id")]
        public Guid CurrentStationId { get; set; }
        [Required(ErrorMessage = "Enter Next Station Id")]
        public Guid NextStationId { get; set; }

        [Required(ErrorMessage = "Enter Departure Time")]
        public DateTime DepartureTime { get; set; }
        [Required(ErrorMessage = "Enter Arrival Time")]
        public DateTime ArrivalTime { get; set; }
        [Required(ErrorMessage = "Enter Duration")]
        public double Duration { get; set; }
    }
}

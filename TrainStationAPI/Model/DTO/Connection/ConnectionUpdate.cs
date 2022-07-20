namespace TrainStationAPI.Model.DTO.Connection
{
    public class ConnectionUpdate
    {
        public virtual DateTime DepartureTime { get; set; }
        public virtual DateTime ArrivalTime { get; set; }
        public virtual double Duration { get; set; }
    }
}

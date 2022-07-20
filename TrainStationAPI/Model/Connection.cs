namespace TrainStationAPI.Model
{
    public class Connection
    {
        public virtual Guid ConnectionId { get; set; }
        public virtual Train Train { get; set; }
        public virtual Station CurrentStation { get; set; }
        public virtual Station NextStation { get; set; }
        public virtual DateTime DepartureTime { get; set; }
        public virtual DateTime ArrivalTime { get; set; }
        public virtual double Duration { get; set; }
    }
}

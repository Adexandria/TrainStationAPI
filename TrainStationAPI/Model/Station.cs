namespace TrainStationAPI.Model
{
    public class Station
    {
        public virtual Guid StationId { get; set; }
        public virtual string Name { get; set; }
        public virtual string Location { get; set; }
        
    }
}

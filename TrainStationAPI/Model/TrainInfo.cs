namespace TrainStationAPI.Model
{
    public class TrainInfo
    {
        public virtual Guid InfoId { get; set; }
        public virtual int Capacity { get; set; }
        public virtual string Description { get; set; }
        public virtual Train Train { get; set; }
    }
}
namespace TrainStationAPI.Services
{
    public interface ITrainStation<T>  
    {
        IEnumerable<T> GetAll();
        Task<T> Get(Guid id);
        Task Add(T item);
        Task Remove(Guid id);
        Task Update(T item);
        
    }
}

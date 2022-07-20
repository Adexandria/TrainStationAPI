using TrainStationAPI.Model;

namespace TrainStationAPI.Services
{
    public interface ITrainInfo
    {
        Task<bool> IsExist(Guid id);
        Task Add(TrainInfo item);
        Task Remove(Guid id);
        Task Update(TrainInfo item);
    }
}

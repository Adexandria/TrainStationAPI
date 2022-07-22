using TrainStationAPI.Model;

namespace TrainStationAPI.Services
{
    public interface ITrainInfo
    {
        bool IsExist(Guid id);
        TrainInfo GetTrainInfoByTrainId(Guid trainId);
        Task Add(TrainInfo item);
        Task Remove(Guid id);
        Task Update(TrainInfo item);
    }
}

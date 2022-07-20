using NHibernate;
using TrainStationAPI.Model;

namespace TrainStationAPI.Services
{
    public class TrainInfoRepository : ITrainInfo
    {
        private readonly FluentNhibernateHelper _db;

        public TrainInfoRepository(FluentNhibernateHelper db)
        {
            _db = db;
        }

        public async Task<bool> IsExist(Guid id)
        {
            TrainInfo trainInfo = await _db.session.GetAsync<TrainInfo>(id);
            if(trainInfo is null)
            {
                return false;
            }
            return true;
        }
        
        public async Task Add(TrainInfo item)
        {
            ITransaction transaction = OpenTransaction();
            await _db.session.SaveAsync(item);
            transaction.Commit();
        }

        public async Task Remove(Guid id)
        {
            ITransaction transaction = OpenTransaction();
            TrainInfo item = _db.session.Get<TrainInfo>(id);
            await _db.session.DeleteAsync(item);
            await transaction.CommitAsync();
        }

        public async Task Update(TrainInfo item)
        {
            ITransaction transaction = OpenTransaction();
            await _db.session.UpdateAsync(item);
            await transaction.CommitAsync();
        }
        
        public ITransaction OpenTransaction()
        {
            return _db.session.BeginTransaction();
        }

       
    }
}

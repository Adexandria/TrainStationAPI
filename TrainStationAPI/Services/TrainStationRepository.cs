using NHibernate;

namespace TrainStationAPI.Services
{
    public class TrainStationRepository<T> : ITrainStation<T> where T:class
    {
        private readonly FluentNhibernateHelper db;
        public TrainStationRepository(FluentNhibernateHelper db)
        {
            this.db = db;
        }
        public async Task Add(T item)
        {
            try
            {
                ITransaction transaction = OpenTransaction();
                await db.session.SaveAsync(item);
                await transaction.CommitAsync();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<T> Get(Guid id)
        {
            try
            {
                T item = await db.session.GetAsync<T>(id);
                return item;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<T> GetAll()
        {
            return db.session.Query<T>();
        }

        public async Task Remove(Guid id)
        {
            try
            {
                T item = await Get(id);
                if(item == null)
                {
                    throw new NullReferenceException(nameof(item));
                }
                ITransaction transaction = OpenTransaction();
                await db.session.DeleteAsync(item);
                await transaction.CommitAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task Update(T item)
        {
            try
            {
                ITransaction transaction = OpenTransaction();
                await db.session.UpdateAsync(item);
                await transaction.CommitAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
        
        private ITransaction OpenTransaction()
        {
            return db.session.BeginTransaction();
        }
    }
}

using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System.Reflection;
using TrainStationAPI.Model;

namespace TrainStationAPI.Services
{
    public class FluentNhibernateHelper
    {

       public FluentNhibernateHelper(string connectionString)
        {
            if (_sessionFactory is null)
            {
                session = CreateConfiguration(connectionString).BuildSessionFactory().OpenSession();
            }
        }
        
        public NHibernate.ISession session;
        
        private readonly ISessionFactory _sessionFactory;
        
        public static Configuration CreateConfiguration(string connectionString)  => Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString
                (connectionString))
            .Mappings(m =>
            {
                m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly());
            })
            .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true)).BuildConfiguration();
    }
    
}

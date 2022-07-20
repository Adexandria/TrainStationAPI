using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System.Reflection;


namespace TrainStationAPI.Services
{
    public class FluentNhibernateHelper
    {
        public FluentNhibernateHelper()
        {
           session =  CreateSessionFactory.OpenSession();
        }
        public NHibernate.ISession session;
        private readonly ISessionFactory _sessionFactory;

        public ISessionFactory CreateSessionFactory => _sessionFactory ??
            Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString
                (_connectionString))
            .Mappings(m => m.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
               .ExposeConfiguration(cfg => new SchemaUpdate(cfg)
                 .Execute(false, true))
            .BuildSessionFactory();



        private readonly string _connectionString = @"Data Source = (localdb)\MSSQLLocalDB; Database = TrainStationDb; Integrated Security = True";
    }
    
}

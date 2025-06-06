using NHibernate;
using NHibernate.Cfg;
using System.Configuration;
using System.Reflection;

namespace ClientesCRUD.App_Start
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                {
                    var cfg = new NHibernate.Cfg.Configuration();

                    cfg.DataBaseIntegration(x =>
                    {
                        x.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
                        x.Driver<NHibernate.Driver.SqlClientDriver>();
                        x.Dialect<NHibernate.Dialect.MsSql2012Dialect>();
                        x.LogFormattedSql = true;
                        x.LogSqlInConsole = true;
                    });

                    cfg.AddAssembly(typeof(ClientesCRUD.Models.Cliente).Assembly);

                    _sessionFactory = cfg.BuildSessionFactory();
                }

                return _sessionFactory;
            }
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Concrete;
using DAL.Contracts;
using Lamar;

namespace DAL.DI
{
    public class RepositoryRegistry : ServiceRegistry
    {
        public RepositoryRegistry()
        {
            IncludeRegistry<UnitOfWorkRegistry>();

            For<IUserRepository>().Use<UserRepository>();
            For<IShipmentRepository>().Use<ShipmentRepository>();
            For<IHistoryRepository>().Use<HistoryRepository>();
            For<IPostOfficeRepository>().Use<PostOfficeRepository>();
            For<IPackageRepository>().Use<PackageRepository>();
            For<ITransportRepository>().Use<TransportRepository>();
        }


    }
}

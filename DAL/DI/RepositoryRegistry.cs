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
            For<IUserTypeRepository>().Use<UserTypeRepository>();
            For<IDepartmantRepository>().Use<DepartmantRepository>();
            For<ICourseRepository>().Use<CourseRepository>();
            For<IAcademicYearRepository>().Use<AcademicYearRepository>();
            For<IAttendanceRepository>().Use<AttendanceRepository>();
            For<IStudentCardRepository>().Use<StudentCardRepository>();
            For<IGroupRepository>().Use<GroupRepository>();
            For<IGroupStudentRepository>().Use<GroupStudentRepository>();
            For<IScheduleRepository>().Use<ScheduleRepository>();
            //    For<IHistoryRepository>().Use<HistoryRepository>();
            //    For<IPostOfficeRepository>().Use<PostOfficeRepository>();
            //    For<IPackageRepository>().Use<PackageRepository>();
            //    For<ITransportRepository>().Use<TransportRepository>();
        }


    }
}

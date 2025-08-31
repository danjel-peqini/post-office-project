using DAL.DI;
using Domain.Concrete;
using Domain.Contracts;
using Lamar;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DI
{
    public class DomainRegistry : ServiceRegistry
    {
        public DomainRegistry()
        {
            IncludeRegistry<DomainUnitOfWorkRegistry>();

            For<IUserDomain>().Use<UserDomain>();
            For<IDepartmantDomain>().Use<DepartmantDomain>();
            For<IAcademicYearDomain>().Use<AcademicYearDomain>();
            For<IAttendanceDomain>().Use<AttendanceDomain>();
            For<IRoomDomain>().Use<RoomDomain>();
            For<ICourseDomain>().Use<CourseDomain>();
            For<IProgramDomain>().Use<ProgramDomain>();
            For<IStudentCardDomain>().Use<StudentCardDomain>();
            For<IGroupDomain>().Use<GroupDomain>();
            For<ITeacherDomain>().Use<TeacherDomain>();
            For<IScheduleDomain>().Use<ScheduleDomain>();
            For<ISessionDomain>().Use<SessionDomain>();

            AddRepositoryRegistries();
            AddHttpContextRegistries();
        }

        private void AddRepositoryRegistries()
        {
            IncludeRegistry<RepositoryRegistry>();
        }

        private void AddHttpContextRegistries()
        {
            For<IHttpContextAccessor>().Use<HttpContextAccessor>();
        }
    }
}

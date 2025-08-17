using DAL.Contracts;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete
{
    internal class CourseRepository: BaseRepository<TblCourse, Guid>, ICourseRepository
    {
        public CourseRepository(SchoolAdministrationContext dbContext) : base(dbContext)
        {
        }
    }
}

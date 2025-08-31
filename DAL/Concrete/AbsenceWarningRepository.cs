using System;
using System.Linq;
using DAL.Contracts;
using Entities.Models;
using Helpers;

namespace DAL.Concrete
{
    internal class AbsenceWarningRepository : BaseRepository<TblAbsenceWarning, Guid>, IAbsenceWarningRepository
    {
        public AbsenceWarningRepository(SchoolAdministrationContext dbContext) : base(dbContext)
        {
        }

        public TblAbsenceWarning AddOrUpdate(Guid studentId, Guid courseId, double percentage)
        {
            var entity = context.FirstOrDefault(x => x.StudentId == studentId && x.CourseId == courseId);
            if (entity == null)
            {
                entity = new TblAbsenceWarning
                {
                    Id = Guid.NewGuid(),
                    StudentId = studentId,
                    CourseId = courseId,
                    Percentage = percentage,
                    SentAt = DateTime.UtcNow,
                    Status = EntityStatus.Active
                };
                Add(entity);
            }
            else
            {
                entity.Percentage = percentage;
                entity.SentAt = DateTime.UtcNow;
                Update(entity);
            }
            return entity;
        }
    }
}

using DAL.Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DAL.Concrete
{
    internal class AttendanceRepository : BaseRepository<TblAttendance, Guid>, IAttendanceRepository
    {
        private readonly SchoolAdministrationContext _dbContext;

        public AttendanceRepository(SchoolAdministrationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public TblAttendance CheckIn(string studentCardCode, Guid sessionId)
        {
            var student = _dbContext.TblStudentCards.FirstOrDefault(x => x.StudentCardCode == studentCardCode);
            if (student == null) throw new Exception("Student card not found");
            var attendance = new TblAttendance
            {
                Id = Guid.NewGuid(),
                SessionId = sessionId,
                StudentId = student.Id,
                CheckInTime = DateTime.UtcNow,
                CheckedInBy = "student"
            };
            Add(attendance);
            _dbContext.SaveChanges();
            return attendance;
        }

        public IEnumerable<TblAttendance> GetByStudent(Guid studentCardId)
        {
            return _dbContext.TblAttendances.Where(a => a.StudentId == studentCardId).AsNoTracking().ToList();
        }
    }
}

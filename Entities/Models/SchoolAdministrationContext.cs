using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Entities.Models
{
    public partial class SchoolAdministrationContext : DbContext
    {
        public SchoolAdministrationContext()
        {
        }

        public SchoolAdministrationContext(DbContextOptions<SchoolAdministrationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TblAbsenceWarning> TblAbsenceWarnings { get; set; } = null!;
        public virtual DbSet<TblAcademicYear> TblAcademicYears { get; set; } = null!;
        public virtual DbSet<TblAttendance> TblAttendances { get; set; } = null!;
        public virtual DbSet<TblCourse> TblCourses { get; set; } = null!;
        public virtual DbSet<TblProgram> TblPrograms { get; set; } = null!;
        public virtual DbSet<TblDepartment> TblDepartments { get; set; } = null!;
        public virtual DbSet<TblGroup> TblGroups { get; set; } = null!;
        public virtual DbSet<TblGroupStudent> TblGroupStudents { get; set; } = null!;
        public virtual DbSet<TblRoom> TblRooms { get; set; } = null!;
        public virtual DbSet<TblSchedule> TblSchedules { get; set; } = null!;
        public virtual DbSet<TblSession> TblSessions { get; set; } = null!;
        public virtual DbSet<TblStudentCard> TblStudentCards { get; set; } = null!;
        public virtual DbSet<TblTeacher> TblTeachers { get; set; } = null!;
        public virtual DbSet<TblUser> TblUsers { get; set; } = null!;
        public virtual DbSet<TblUserType> TblUserTypes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=school-adm;MultipleActiveResultSets=True;Trusted_Connection=True;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var dateTimeOffsetConverter = new ValueConverter<DateTimeOffset, DateTime>(
                v => v.UtcDateTime,
                v => new DateTimeOffset(DateTime.SpecifyKind(v, DateTimeKind.Utc)));

            var nullableDateTimeOffsetConverter = new ValueConverter<DateTimeOffset?, DateTime?>(
                v => v.HasValue ? v.Value.UtcDateTime : null,
                v => v.HasValue ? new DateTimeOffset(DateTime.SpecifyKind(v.Value, DateTimeKind.Utc)) : (DateTimeOffset?)null);

            modelBuilder.Entity<TblAbsenceWarning>(entity =>
            {
                entity.ToTable("tblAbsenceWarnings");

                entity.HasIndex(e => new { e.StudentId, e.CourseId }, "UQ_Warning")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SentAt).HasColumnType("datetime");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TblAbsenceWarnings)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblAbsenc__Cours__1CBC4616");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.TblAbsenceWarnings)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblAbsenc__Stude__1BC821DD");
            });

            modelBuilder.Entity<TblAcademicYear>(entity =>
            {
                entity.ToTable("tblAcademicYears");

                entity.HasIndex(e => e.Year, "UQ__tblAcade__D4BD6054EF1F39F6")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.Year).HasMaxLength(20);
            });

            modelBuilder.Entity<TblAttendance>(entity =>
            {
                entity.ToTable("tblAttendances");

                entity.HasIndex(e => new { e.SessionId, e.StudentId }, "UQ_StudentSession")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CheckInTime)
                    .HasColumnType("datetimeoffset");

                entity.Property(e => e.CheckedInBy).HasMaxLength(50);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Session)
                    .WithMany(p => p.TblAttendances)
                    .HasForeignKey(d => d.SessionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblAttend__Sessi__160F4887");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.TblAttendances)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblAttend__Stude__17036CC0");
            });

            modelBuilder.Entity<TblCourse>(entity =>
            {
                entity.ToTable("tblCourse");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.TotalHours).HasColumnName("totalHours");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.TblCourses)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TblDepartment>(entity =>
            {
                entity.ToTable("tblDepartments");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetimeoffset");
            });

            modelBuilder.Entity<TblProgram>(entity =>
            {
                entity.ToTable("tblProgram");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.Department)
                    .WithMany(p => p.TblPrograms)
                    .HasForeignKey(d => d.DepartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TblGroup>(entity =>
            {
                entity.ToTable("tblGroup");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.TblGroups)
                    .HasForeignKey(d => d.AcademicYearId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblGroup__Academ__76969D2E");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.TblGroups)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<TblGroupStudent>(entity =>
            {
                entity.ToTable("tblGroupStudents");

                entity.HasIndex(e => new { e.GroupId, e.StudentId }, "UQ_GroupStudent")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.TblGroupStudents)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblGroupS__Group__7B5B524B");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.TblGroupStudents)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblGroupS__Stude__7C4F7684");
            });

            modelBuilder.Entity<TblRoom>(entity =>
            {
                entity.ToTable("tblRooms");

                entity.HasIndex(e => e.Name, "UQ__tblRooms__737584F6DBA15A66")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<TblSchedule>(entity =>
            {
                entity.ToTable("tblSchedule");

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.TblSchedules)
                    .HasForeignKey(d => d.CourseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblSchedule__Course");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.TblSchedules)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblSchedu__Group__08B54D69");

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.TblSchedules)
                    .HasForeignKey(d => d.AcademicYearId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblSchedu__Acade__0C85DE4D");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.TblSchedules)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblSchedu__RoomI__0B91BA14");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TblSchedules)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblSchedu__Teach__0A9D95DB");
            });

            modelBuilder.Entity<TblSession>(entity =>
            {
                entity.ToTable("tblSessions");

                entity.HasIndex(e => new { e.ScheduleId, e.Date }, "UQ_SessionPerDay")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.IsOpen)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Otp)
                    .HasMaxLength(10)
                    .HasColumnName("OTP");

                entity.Property(e => e.OtpcreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("OTPCreatedAt");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(45);

                entity.HasOne(d => d.Schedule)
                    .WithMany(p => p.TblSessions)
                    .HasForeignKey(d => d.ScheduleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblSessio__Sched__114A936A");
            });

            modelBuilder.Entity<TblStudentCard>(entity =>
            {
                entity.ToTable("tblStudentCard");

                entity.HasIndex(e => e.UserId, "UQ__tblStude__1788CC4DD72A7233")
                    .IsUnique();

                entity.HasIndex(e => e.StudentCardCode, "UQ__tblStude__BAE7DC185150D945")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.StudentCardCode).HasMaxLength(100);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetimeoffset");

                entity.Property(e => e.DisabledDate)
                    .HasColumnType("datetimeoffset");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.AcademicYear)
                    .WithMany(p => p.TblStudentCards)
                    .HasForeignKey(d => d.AcademicYearId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblStuden__Acade__71D1E811");

                entity.HasOne(d => d.Program)
                    .WithMany(p => p.TblStudentCards)
                    .HasForeignKey(d => d.ProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblStuden__Progr__70DDC3D8");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.TblStudentCard)
                    .HasForeignKey<TblStudentCard>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblStuden__UserI__6FE99F9F");
            });

            modelBuilder.Entity<TblTeacher>(entity =>
            {
                entity.ToTable("tblTeachers");

                entity.HasIndex(e => e.UserId, "UQ__tblTeach__1788CC4D8C7DA083")
                    .IsUnique();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.User)
                    .WithOne(p => p.TblTeacher)
                    .HasForeignKey<TblTeacher>(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__tblTeache__UserI__01142BA1");
            });

            modelBuilder.Entity<TblUser>(entity =>
            {
                entity.ToTable("tblUser");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");

                entity.Property(e => e.Birthday)
                     .HasColumnType("datetimeoffset");

                entity.Property(e => e.CreatedDate)
                     .HasColumnType("datetimeoffset");

                entity.Property(e => e.LastModifiedDate)
                    .HasColumnType("datetimeoffset");

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Password).IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserType)
                    .WithMany(p => p.TblUsers)
                    .HasForeignKey(d => d.UserTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_userType");
            });

            modelBuilder.Entity<TblUserType>(entity =>
            {
                entity.ToTable("tblUserType");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

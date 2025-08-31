using AutoMapper;
using DTO;
using DTO.HistoryDTO;
using DTO.PackageDTO;
using DTO.PostOfficeDTO;
using DTO.ShipmentDTO;
using DTO.TransportDTO;
using DTO.UserDTO;
using DTO.UserTypeDTO;
using Entities.Models;
using Newtonsoft.Json;
using System.Linq;

namespace Domain.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region users
            CreateMap<TblUser, UserGetDTO>().ReverseMap();
            CreateMap<TblUser, UserPostDTO>().ReverseMap();
            CreateMap<TblUser, UserPutDTO>().ReverseMap();
            CreateMap<TblUser, UserSimpleDTO>().ReverseMap();
            CreateMap<TblUser, UserPatchDTO>().ReverseMap();
            #endregion
            #region
            CreateMap<TblUserType, UserTypeDTO>().ReverseMap();
            CreateMap<TblUserType, UserTypePostDto>().ReverseMap();
            CreateMap<TblUserType, UserTypeGetDTO>().ReverseMap();
            #endregion
            #region departmants
            CreateMap<TblDepartment, DepartmantDTO>().ReverseMap();
            CreateMap<TblDepartment, DepartmantPostDTO>().ReverseMap();
            #endregion
            #region programs
            CreateMap<TblProgram, ProgramDTO>()
                .ForMember(dest => dest.Departmant, opt => opt.MapFrom(src => src.Department))
                .ReverseMap()
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Departmant));

            CreateMap<TblProgram, ProgramPostDTO>()
                .ForMember(dest => dest.DepartmantId, opt => opt.MapFrom(src => src.DepartmentId))
                .ReverseMap()
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmantId));

            CreateMap<ProgramPostDTO, ProgramDTO>().ReverseMap();
            #endregion
            #region course
            CreateMap<TblCourse, CourseDTO>()
                .ForMember(dest => dest.Program, opt => opt.MapFrom(src => src.Program))
                .ReverseMap()
                .ForMember(dest => dest.Program, opt => opt.MapFrom(src => src.Program));

            CreateMap<TblCourse, CoursePostDTO>()
                .ForMember(dest => dest.ProgramId, opt => opt.MapFrom(src => src.ProgramId))
                .ReverseMap()
                .ForMember(dest => dest.ProgramId, opt => opt.MapFrom(src => src.ProgramId));

            CreateMap<CoursePostDTO, CourseDTO>().ReverseMap();
            #endregion
            #region rooms
            CreateMap<TblRoom, RoomDTO>().ReverseMap();
            CreateMap<TblRoom, RoomPostDTO>().ReverseMap();
            #endregion
            #region academicYear
            CreateMap<TblAcademicYear, AcademicYearDTO>().ReverseMap();
            CreateMap<TblAcademicYear, AcademicYearPostDTO>().ReverseMap();
            #endregion
            #region attendance
            CreateMap<TblAttendance, AttendanceDTO>().ReverseMap();
            CreateMap<TblAttendance, AttendanceCheckInDTO>().ReverseMap();
            #endregion
            #region studentCard
            CreateMap<TblStudentCard, StudentCardDTO>().ReverseMap();
            CreateMap<TblStudentCard, StudentCardPostDTO>().ReverseMap();
            #endregion
            #region academicYear
            CreateMap<TblAcademicYear, AcademicYearDTO>().ReverseMap();
            CreateMap<TblAcademicYear, AcademicYearPostDTO>().ReverseMap();
            #endregion
            #region attendance
            CreateMap<TblAttendance, AttendanceDTO>().ReverseMap();
            CreateMap<TblAttendance, AttendanceCheckInDTO>().ReverseMap();
            #endregion
            #region teachers
            CreateMap<TblTeacher, TeacherDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ReverseMap()
                .ForMember(dest => dest.User, opt => opt.Ignore());
            CreateMap<TblTeacher, TeacherPostDTO>().ReverseMap();
            #endregion
            #region groups
            CreateMap<TblGroup, GroupDTO>()
                .ForMember(dest => dest.Program, opt => opt.MapFrom(src => src.Program))
                .ForMember(dest => dest.AcademicYear, opt => opt.MapFrom(src => src.AcademicYear))
                .ForMember(dest => dest.StudentIds, opt => opt.MapFrom(src => src.TblGroupStudents.Select(gs => gs.StudentId)))
                .ForMember(dest => dest.StudentsLength, opt => opt.MapFrom(src => src.TblGroupStudents.Count))
                .ReverseMap()
                .ForMember(dest => dest.Program, opt => opt.Ignore())
                .ForMember(dest => dest.AcademicYear, opt => opt.Ignore())
                .ForMember(dest => dest.TblGroupStudents, opt => opt.Ignore());
            CreateMap<TblGroup, GroupPostDTO>().ReverseMap();
            #endregion
            #region schedules
            CreateMap<TblSchedule, ScheduleDTO>()
                .ForMember(dest => dest.Group, opt => opt.MapFrom(src => src.Group))
                .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course))
                .ForMember(dest => dest.Teacher, opt => opt.MapFrom(src => src.Teacher))
                .ForMember(dest => dest.Room, opt => opt.MapFrom(src => src.Room))
                .ForMember(dest => dest.AcademicYear, opt => opt.MapFrom(src => src.AcademicYear))
                .ReverseMap()
                .ForMember(dest => dest.Group, opt => opt.Ignore())
                .ForMember(dest => dest.Course, opt => opt.Ignore())
                .ForMember(dest => dest.Teacher, opt => opt.Ignore())
                .ForMember(dest => dest.Room, opt => opt.Ignore())
                .ForMember(dest => dest.AcademicYear, opt => opt.Ignore());
            CreateMap<TblSchedule, SchedulePostDTO>().ReverseMap();
            #endregion
            #region sessions
            CreateMap<TblSession, SessionDTO>()
                .ForMember(dest => dest.Schedule, opt => opt.MapFrom(src => src.Schedule))
                .ReverseMap()
                .ForMember(dest => dest.Schedule, opt => opt.Ignore());
            #endregion

            #region absenceWarnings
            CreateMap<TblAbsenceWarning, AbsenceWarningDTO>().ReverseMap();
            #endregion
        }



    }
}

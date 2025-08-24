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

namespace Domain.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region users
            CreateMap<TblUser, UserGetDTO>().ReverseMap();
            CreateMap<TblUser, UserPostDTO>().ReverseMap();
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
            #region course
            CreateMap<TblCourse, CourseDTO>().ReverseMap();
            CreateMap<TblCourse, CoursePostDTO>().ReverseMap();
            CreateMap<CoursePostDTO, CourseDTO>().ReverseMap();
            #endregion
            #region academicYear
            CreateMap<TblAcademicYear, AcademicYearDTO>().ReverseMap();
            CreateMap<TblAcademicYear, AcademicYearPostDTO>().ReverseMap();
            #endregion
            #region attendance
            CreateMap<TblAttendance, AttendanceDTO>().ReverseMap();
            CreateMap<TblAttendance, AttendanceCheckInDTO>().ReverseMap();
            #endregion
            #region academicYear
            CreateMap<TblAcademicYear, AcademicYearDTO>().ReverseMap();
            CreateMap<TblAcademicYear, AcademicYearPostDTO>().ReverseMap();
            #endregion
            #region attendance
            CreateMap<TblAttendance, AttendanceDTO>().ReverseMap();
            CreateMap<TblAttendance, AttendanceCheckInDTO>().ReverseMap();
            #endregion
        }



    }
}

using AutoMapper;
using DTO.HistoryDTO;
using DTO.PackageDTO;
using DTO.PostOfficeDTO;
using DTO.ShipmentDTO;
using DTO.TransportDTO;
using DTO.UserDTO;
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
            #region shipment
            CreateMap<TblShipment, ShipmentDetailsDTO>().ReverseMap();
            CreateMap<ShipmentPostDTO, TblShipment>()
                 .ForMember(dest => dest.ReceiverUserDetails, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.ReceiverUserDetails)));

            CreateMap<ShipmentGetDTO, TblShipment>()
                .ForMember(dest => dest.Barcode, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Barcode)));
            CreateMap<TblShipment, ShipmentGetDTO>()
               .ForMember(dest => dest.ReceiverUserDetails, opt => opt.MapFrom(src => JsonConvert.DeserializeObject<ReceiverDetails>(src.ReceiverUserDetails)));
            #endregion

            #region postoffice
            CreateMap<TblPostOffice, PostOfficeDTO>().ReverseMap();
            CreateMap<PostOfficePostDTO, TblPostOffice>();
            #endregion

            #region package
            
            CreateMap<TblPackage, PackageDetailsDTO>().ReverseMap();
            CreateMap<TblPackage, PackageDTO>();
            CreateMap<PackageDTO, TblPackage>()
               .ForMember(dest => dest.Barcode, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.Barcode)));

            CreateMap<PackagePostDTO, TblPackage>();
            #endregion

            #region
            CreateMap<TblTransportation, TransportGetDTO>().ReverseMap();
            CreateMap<TblTransportation, TransportPostDTO>().ReverseMap();
            #endregion

            CreateMap<TblHistory, HistoryPostDTO>();
        }



    }
}

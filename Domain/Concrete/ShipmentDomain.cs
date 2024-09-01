using AutoMapper;
using BitMiracle.LibTiff.Classic;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO.HistoryDTO;
using DTO.ShipmentDTO;
using Entities.Models;
using Helpers;
using Helpers.Barcode;
using Helpers.Email;
using Helpers.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZXing;

namespace Domain.Concrete
{
    internal class ShipmentDomain : DomainBase, IShipmentDomain
    {
        #region repository
        private IShipmentRepository ShipmentRepository => _unitOfWork.GetRepository<IShipmentRepository>();
        #endregion
        #region domain
        private IHistoryDomain HistoryDomain => _unitOfWork.GetRepository<IHistoryDomain>();

        #endregion

        public ShipmentDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        public void AddNewShipment(ShipmentPostDTO shipmentPostDTO)
        {
            var mapper = _mapper.Map<TblShipment>(shipmentPostDTO);
            mapper.ShipmentId = Guid.NewGuid();
            //mapper.SendUserId = GetUserId();
            mapper.Status = (int)ShipmentStatus.New;
            mapper.CreationDate = DateTimeOffset.Now;
            // generate barcode
            var data = GenerateBarcode.GenerateNewBarcode();
            // convert to base64
            mapper.TrackNumber = data.Item1;
            mapper.Barcode = Convert.ToBase64String(data.Item2);
            ShipmentRepository.Add(mapper);
            _unitOfWork.Save();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CreatedBy", GetUserId().ToString());
            dic.Add("Description", $"{GetUserId()} added new shippment with {mapper.ShipmentId} on {mapper.CreationDate}");
            
            HistoryDomain.AddLogs(dic, (int)HistoryStatus.AddShipment);

        }
        

        public ShipmentGetDTO GetById(Guid shipmentId)
        {
            var currentData = ShipmentRepository.GetById(shipmentId);
            return _mapper.Map<ShipmentGetDTO>(currentData);

        }

        public void UpdateShipment(List<Guid> shipmentIds, Guid packageId)
        {
            foreach (var item in shipmentIds)
            {
                var data = ShipmentRepository.GetById(item);
                if(data != null)
                {
                    data.PackageId = packageId;
                    ShipmentRepository.Update(data);
                    _unitOfWork.Save();
                }
            }
        }
        public Pagination<ShipmentGetDTO> GetAll(QueryParameters queryParameters, string? searchValue)
        {
            var data = ShipmentRepository.GetAll(queryParameters, searchValue, GetUserId());
            return Pagination<ShipmentGetDTO>.ToPagedList(data, _mapper.Map<List<ShipmentGetDTO>>);
        }
        public void CancelShipment(Guid shipmentId, int status)
        {
            var currentData = ShipmentRepository.GetById(shipmentId);

            if(currentData.Status == (int)ShipmentStatus.InRoute)
            {
                throw new Exception("Porosia eshte derguar per transport!");
            }
            

            currentData.Status = status;
            ShipmentRepository.Update(currentData);
            _unitOfWork.Save();

            if(currentData.Status == (int)ShipmentStatus.InRoute)
            {
                
            }
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("CreatedBy", GetUserId().ToString());
            dic.Add("Description", $"{GetUserId()} has cancelled this {shipmentId}");

            HistoryDomain.AddLogs(dic, (int)HistoryStatus.UpdateShipmentStatus);

        }
    }
}

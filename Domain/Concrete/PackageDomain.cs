using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO.PackageDTO;
using DTO.PostOfficeDTO;
using Entities.Models;
using Helpers;
using Helpers.Barcode;
using Helpers.Pagination;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    internal class PackageDomain : DomainBase, IPackageDomain
    {
        private IPackageRepository PackageRepository => _unitOfWork.GetRepository<IPackageRepository>();

        public PackageDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        public void Add(PackagePostDTO packagePostDTO)
        {
            var mapper = _mapper.Map<TblPackage>(packagePostDTO);
           var data = GenerateBarcode.GenerateNewBarcode();
            mapper.PackageId = Guid.NewGuid();
            mapper.Barcode = Convert.ToBase64String(data.Item2);
            mapper.DateCreated = DateTimeOffset.Now;
            mapper.Status = (int)ShipmentStatus.New;
            // mapper.UserCreatedId = GetUserId();
            PackageRepository.Add(mapper);
            _unitOfWork.Save();

        }

        public PackageDetailsDTO GetById(Guid packageId)
        {
            var data = PackageRepository.GetDetailsById(packageId);
            var mapper = _mapper.Map<PackageDetailsDTO>(data);
            return mapper;

        }

        public List<PackageDTO> GetAll()
        {
            var data = PackageRepository.GetAll();
            return _mapper.Map<List<PackageDTO>>(data);
        }

        public void UpdateStatusShipment(Guid packageId, int status)
        {
            var currentData = PackageRepository.GetDetailsById(packageId);
            currentData.Status = status;
            PackageRepository.Update(currentData);
            _unitOfWork.Save();
        }
    }
}

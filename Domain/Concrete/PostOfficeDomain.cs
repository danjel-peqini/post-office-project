using AutoMapper;
using DAL.Contracts;
using DAL.UoW;
using Domain.Contracts;
using DTO.PostOfficeDTO;
using Entities.Models;
using Helpers.Pagination;
using Microsoft.AspNetCore.Http;

namespace Domain.Concrete
{
    internal class PostOfficeDomain : DomainBase, IPostOfficeDomain
    {
        private IPostOfficeRepository PostOfficeRepository => _unitOfWork.GetRepository<IPostOfficeRepository>();

        public PostOfficeDomain(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        public Pagination<PostOfficeDTO> GetAll(QueryParameters queryParameters, string? searchValue)
        {
            var data = PostOfficeRepository.GetAll(queryParameters, searchValue);
            return Pagination<PostOfficeDTO>.ToPagedList(data, _mapper.Map<List<PostOfficeDTO>>);
        }


        public void Add(PostOfficePostDTO postOfficeDTO)
        {
            var check = PostOfficeRepository.CheckUniquePostOfficeName(postOfficeDTO.Name);
            if (check == null)
            {
                var mapper = _mapper.Map<TblPostOffice>(postOfficeDTO);
                mapper.PostOfficeId = Guid.NewGuid();
                mapper.IsActive = true;
                PostOfficeRepository.Add(mapper);
                _unitOfWork.Save();
            }
            else
            {
                throw new Exception("This post office exist");
            }
        }

        public void Update(PostOfficeDTO postOfficeDTO)
        {
            var check = PostOfficeRepository.GetById(postOfficeDTO.PostOfficeId);
            if (check.Name != postOfficeDTO.Name)
            {
                var checkUniqueName = PostOfficeRepository.CheckUniquePostOfficeName(postOfficeDTO.Name);
                if (checkUniqueName != null && postOfficeDTO.PostOfficeId != check.PostOfficeId)
                {
                    throw new Exception("This post office exist");

                }
            }

            var mapper = _mapper.Map<TblPostOffice>(postOfficeDTO);
            PostOfficeRepository.Update(mapper);
            _unitOfWork.Save();
        }
    }
}

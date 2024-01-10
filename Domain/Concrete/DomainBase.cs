using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DAL.Authentication;
using DAL.UoW;
using JasperFx.Core;
using Microsoft.AspNetCore.Http;

namespace Domain.Concrete
{
    internal class DomainBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly IHttpContextAccessor _httpContextAccessor;

        public DomainBase(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid GetUserId()
        {
            UserContext userContext = new UserContext(_httpContextAccessor);
            return userContext.GetCurrentUserId().IsEmpty() ? Guid.Empty : Guid.Parse(userContext.GetCurrentUserId());
        }
    }
}

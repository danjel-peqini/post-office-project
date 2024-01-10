using DTO.PostOfficeDTO;
using Helpers.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IPostOfficeDomain
    {
        Pagination<PostOfficeDTO> GetAll(QueryParameters queryParameters, string? searchValue);
        void Add(PostOfficePostDTO postOfficeDTO);
        void Update(PostOfficeDTO postOfficeDTO);
    }
}

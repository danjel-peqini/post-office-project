using Domain.Contracts;
using DTO.PackageDTO;
using DTO.PostOfficeDTO;
using Helpers.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace PostOfficeProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PackageController : ControllerBase
    {

        private readonly IPackageDomain _packageDomain;

        public PackageController(IPackageDomain packageDomain)
        {
            _packageDomain = packageDomain;
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add([FromBody] PackagePostDTO postOfficeDTO)
        {
            _packageDomain.Add(postOfficeDTO);
            return Ok();
        }
       
        [HttpGet]
        [Route("getAll")]
        public IActionResult GetAll()
                => Ok(_packageDomain.GetAll());
        [HttpGet]
        [Route("{packageId}")]
        public IActionResult GetById(Guid packageId)
               => Ok(_packageDomain.GetById(packageId));
        [HttpPut]
        [Route("updateStatus")]
        public IActionResult UpdateStatus(Guid packageId, int status)
        {
            _packageDomain.UpdateStatusShipment(packageId, status);
            return Ok();
        }
    }
}

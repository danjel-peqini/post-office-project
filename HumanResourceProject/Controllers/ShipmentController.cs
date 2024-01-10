using Domain.Contracts;
using DTO.PackageDTO;
using DTO.ShipmentDTO;
using DTO.UserDTO;
using Helpers.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace PostOfficeProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentDomain _shipmentDomain;

        public ShipmentController(IShipmentDomain shipmentDomain)
        {
            _shipmentDomain = shipmentDomain;
        }
        [HttpPost]
        [Route("add")]
        public IActionResult AddNewShipment([FromBody] ShipmentPostDTO shipmentPostDTO)
        {
            _shipmentDomain.AddNewShipment(shipmentPostDTO);
            return Ok();
        }
        [HttpPut]
        [Route("update")]
        public IActionResult Update([FromBody] List<Guid> shipmentIds, Guid packageId)
        {
            _shipmentDomain.UpdateShipment(shipmentIds, packageId);
            return Ok();
        }
        [HttpGet]
        [Route("{shipmentID}")]
        public IActionResult GetById(Guid shipmentID)
            => Ok(_shipmentDomain.GetById(shipmentID));

        [HttpPost]
        [Route("getAll")]
        public IActionResult GetById([FromQuery] QueryParameters queryParameters, [FromQuery] string? searchValue)
           => Ok(_shipmentDomain.GetAll(queryParameters, searchValue));
        [HttpPut]
        [Route("updateStatus")]
        public IActionResult UpdateStatus(Guid shipmentID, int status)
        {
            _shipmentDomain.CancelShipment(shipmentID, status);
            return Ok();

        }
    }
}

using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Domain;
using System.Net.Mail;
using System;

namespace ResidentContactApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/contacts")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class ResidentContactApiController : BaseController
    {
        private IGetAllUseCase _getAllUseCase;
        private IGetByIdUseCase _getByIdUseCase;
        private ICreateContactDetailsUseCase _createContactDetails;
        private IUpdateContactDetailsUseCase _updateContactDetails;
        public ResidentContactApiController(IGetAllUseCase getAllUseCase, IGetByIdUseCase getByIdUseCase,
            ICreateContactDetailsUseCase createContactDetails, IUpdateContactDetailsUseCase updateContactDetails)
        {
            _getAllUseCase = getAllUseCase;
            _getByIdUseCase = getByIdUseCase;
            _createContactDetails = createContactDetails;
            _updateContactDetails = updateContactDetails;
        }
        /// <summary>
        /// ...
        /// </summary>
        /// <response code="200">Successful response</response>
        /// <response code="400">Invalid Query Parameter.</response>
        /// <response code="500">There was an error processing your request, please try again.</response>
        [ProducesResponseType(typeof(ResidentResponseList), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult ListContacts([FromQuery] ResidentQueryParam rqp)
        {
            return Ok(_getAllUseCase.Execute(rqp));

        }

        /// <summary>
        /// ...
        /// </summary>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Contact not found for specified ID</response>
        /// <response code = "400">Please enter a valide request.</response>
        [ProducesResponseType(typeof(ResidentResponse), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("{id}")]
        public IActionResult ViewRecord(int id)
        {
            try
            {
                return Ok(_getByIdUseCase.Execute(id));
            }
            catch (ResidentNotFoundException e)
            {
                return NotFound(e.Message);
            }
        }


        /// <summary>
        /// Create a new contact record for a resident
        /// </summary>
        /// <response code="201">Successful operation</response>
        /// <response code="400">Contact not found for specified ID</response>
        /// <response code = "500">Please enter a valide request.</response>
        [ProducesResponseType(typeof(ResidentResponse), StatusCodes.Status201Created)]
        [HttpPost]
        public IActionResult CreateContactRecord([FromBody] ResidentContactParam rcp)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update a contact record for a resident
        /// </summary>
        /// <response code="204">Successful operation</response>
        /// <response code="400">Contact not found for specified ID</response>
        /// <response code = "500">Please enter a valide request.</response>
        [ProducesResponseType(204)]
        [HttpPut]
        public IActionResult UpdateContactRecord([FromBody] ResidentContactParam rcp)
        {
            _updateContactDetails.Execute(rcp);
            return NoContent();
        }
    }
}

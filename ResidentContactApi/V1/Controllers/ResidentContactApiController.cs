using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Domain;
using System.Net.Mail;
using System;
using ResidentContactApi.V1.Boundary;

namespace ResidentContactApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class ResidentContactApiController : BaseController
    {
        private IGetAllUseCase _getAllUseCase;
        private IGetByIdUseCase _getByIdUseCase;
        private ICreateContactDetailsUseCase _createContactDetails;
        private IInsertResidentRecordUseCase _insertResidentRecordUseCase;
        private IInsertExternalReferenceRecordUseCase _insertExternalReferenceRecordUseCase;
        public ResidentContactApiController(IGetAllUseCase getAllUseCase, IGetByIdUseCase getByIdUseCase,
            ICreateContactDetailsUseCase createContactDetails, IInsertResidentRecordUseCase insertResidentRecordUseCase, IInsertExternalReferenceRecordUseCase insertExternalReferenceRecordUseCase)
        {
            _getAllUseCase = getAllUseCase;
            _getByIdUseCase = getByIdUseCase;
            _createContactDetails = createContactDetails;
            _insertResidentRecordUseCase = insertResidentRecordUseCase;
            _insertExternalReferenceRecordUseCase = insertExternalReferenceRecordUseCase;
        }
        /// <summary>
        /// ...
        /// </summary>
        /// <response code="200">Successful response</response>
        /// <response code="400">Invalid Query Parameter.</response>
        /// <response code="500">There was an error processing your request, please try again.</response>
        [ProducesResponseType(typeof(ResidentResponseList), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("contacts")]
        public IActionResult ListContacts([FromQuery] ResidentQueryParam rqp)
        {
            return Ok(_getAllUseCase.Execute(rqp));

        }

        /// <summary>
        /// ...
        /// </summary>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Contact not found for specified ID</response>
        /// <response code = "400">Please enter a valid request.</response>
        [ProducesResponseType(typeof(ResidentResponse), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("contacts/{id}")]
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
        [ProducesResponseType(typeof(ResidentResponse), StatusCodes.Status201Created)]
        [HttpPost]
        [Route("contact-details")]
        public IActionResult CreateContactRecord([FromBody] ResidentContact rcp)
        {
            try
            {
                var resident = _createContactDetails.Execute(rcp);
                return CreatedAtAction("ViewRecord", resident);
            }
            catch (NoIdentifierException)
            {
                return BadRequest(
                    "Request must include either the residents ID or the contact ID for the resident from NCC");
            }
            catch (ResidentNotFoundException)
            {
                return BadRequest("Resident ID and/or NCC Contact ID do not link to a resident record");
            }
        }

        /// <summary>
        /// Create a new resident record
        /// </summary>
        /// <response code="201">Successful operation</response>
        /// <response code="400">Contact not found for specified ID</response>
        [ProducesResponseType(typeof(ResidentResponse), StatusCodes.Status201Created)]
        [HttpPost]
        [Route("residents")]
        public IActionResult InsertResident([FromBody] InsertResidentRequest request)
        {
            try
            {
                var resident = _insertResidentRecordUseCase.Execute(request);
                if (resident.ResidentRecordAlreadyPresent) return Ok(resident);

                return CreatedAtAction("ViewResidentRecord", resident);
            }
            catch (ResidentNotInsertedException ex)
            {
                return StatusCode(500, $"Resident could not be inserted - {ex.Message}");
            }
            catch (ExternalReferenceNotInsertedException ex)
            {
                return StatusCode(500, $"External reference could not be inserted - {ex.Message}");
            }
        }

        /// <summary>
        /// Create a new external resident record
        /// </summary>
        /// <response code="201">Successful operation</response>
        /// <response code="400">Contact not found for specified ID</response>
        [ProducesResponseType(typeof(ResidentResponse), StatusCodes.Status201Created)]
        [HttpPost]
        [Route("/residents/{residentId}/externalReferences")]
        public IActionResult InsertExternalReference([FromBody] InsertResidentRequest request)
        {
            try
            {
                var resident = _insertExternalReferenceRecordUseCase.Execute(request);
                if (resident.ResidentRecordAlreadyPresent) return Ok(resident);

                return CreatedAtAction("ViewResidentRecord", resident);
            }
            catch (ResidentNotInsertedException ex)
            {
                return StatusCode(500, $"Resident could not be inserted - {ex.Message}");
            }
            catch (ExternalReferenceNotInsertedException ex)
            {
                return StatusCode(500, $"External reference could not be inserted - {ex.Message}");
            }
        }
    }
}

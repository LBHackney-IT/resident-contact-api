using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResidentContactApi.V1.Boundary.Response.ContactDetails;
using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Boundary.Response.Residents;

namespace ResidentContactApi.V1.Controllers
{
    [ApiController]
    [Route("api/v1/residents-contacts")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    public class ResidentContactApiController : BaseController
    {
        private IGetAllUseCase _getAllUseCase;
        private IGetByIdUseCase _getByIdUseCase;
        public ResidentContactApiController(IGetAllUseCase getAllUseCase, IGetByIdUseCase getByIdUseCase)
        {
            _getAllUseCase = getAllUseCase;
            _getByIdUseCase = getByIdUseCase;
        }
        /// <summary>
        /// ...
        /// </summary>
        /// <response code="200">Successful response</response>
        /// <response code="400">Please provide a valid input parameter.</response>
        /// <response code="500">There was an error processing your request, please try again.</response>
        [ProducesResponseType(typeof(ResidentResponseList), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult ListContacts([FromQuery] ResidentQueryParam rqp)
        {
            try
            {
                return Ok(_getAllUseCase.Execute(rqp));

            }
            catch (InvalidQueryParameterException)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }

        /// <summary>
        /// ...
        /// </summary>
        /// <response code="200">Successful operation</response>
        /// <response code="404">Contact not found for specified ID</response>
        /// <response code = "400">Please enter a valide request.</response>
        [ProducesResponseType(typeof(ResidentResponse), StatusCodes.Status200OK)]
        [HttpGet]
        //TODO: rename to match the identifier that will be used
        [Route("{yourId}")]
        public IActionResult ViewRecord(int yourId)
        {
            return Ok(_getByIdUseCase.Execute(yourId));
        }
    }
}

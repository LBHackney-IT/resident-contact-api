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
        /// <response code="200">...</response>
        /// <response code="400">Invalid Query Parameter.</response>
        [ProducesResponseType(typeof(ResidentResponseList), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult ListContacts([FromQuery] ResidentQueryParam rqp)
        {
            try
            {
                return Ok(_getAllUseCase.Execute(rqp));

            }
            catch (InvalidQueryParameterException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// ...
        /// </summary>
        /// <response code="200">...</response>
        /// <response code="404">No ? found for the specified ID</response>
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

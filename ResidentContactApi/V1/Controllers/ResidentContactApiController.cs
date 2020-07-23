using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.UseCase.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResidentContactApi.V1.Boundary.Response.ContactDetails;
using ResidentContactApi.V1.Boundary.Requests;

namespace ResidentContactApi.V1.Controllers
{
    [ApiController]
    //TODO: Rename to match the APIs endpoint
    [Route("api/v1/residents-contacts")]
    [Produces("application/json")]
    [ApiVersion("1.0")]
    //TODO: rename class to match the API name
    public class ResidentContactApiController : BaseController
    {
        private readonly IGetAllUseCase _getAllUseCase;
        private readonly IGetByIdUseCase _getByIdUseCase;
        public ResidentContactApiController(IGetAllUseCase getAllUseCase, IGetByIdUseCase getByIdUseCase)
        {
            _getAllUseCase = getAllUseCase;
            _getByIdUseCase = getByIdUseCase;
        }

        //TODO: add xml comments containing information that will be included in the auto generated swagger docs (https://github.com/LBHackney-IT/lbh-base-api/wiki/Controllers-and-Response-Objects)
        /// <summary>
        /// ...
        /// </summary>
        /// <response code="200">...</response>
        /// <response code="400">Invalid Query Parameter.</response>
        [ProducesResponseType(typeof(ContactDetailsResponseList), StatusCodes.Status200OK)]
        [HttpGet]
        public IActionResult ListContacts(ResidentQueryParam rqp)
        {
            return Ok(_getAllUseCase.Execute(rqp));
        }

        /// <summary>
        /// ...
        /// </summary>
        /// <response code="200">...</response>
        /// <response code="404">No ? found for the specified ID</response>
        [ProducesResponseType(typeof(ContactDetailsResponse), StatusCodes.Status200OK)]
        [HttpGet]
        //TODO: rename to match the identifier that will be used
        [Route("{yourId}")]
        public IActionResult ViewRecord(int yourId)
        {
            return Ok(_getByIdUseCase.Execute(yourId));
        }
    }
}

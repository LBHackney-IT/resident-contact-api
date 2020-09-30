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
    public class ContactDetailsApiController : BaseController
    {
        private IUpdateContactDetailsIsDefaultUseCase _updateContactDetailsIsDefaultUseCase;
        public ContactDetailsApiController(IUpdateContactDetailsIsDefaultUseCase updateContactDetailsIsDefaultUseCase)
        {
            _updateContactDetailsIsDefaultUseCase = updateContactDetailsIsDefaultUseCase;
        }

        /// <summary>
        /// Create a new contact record for a resident
        /// </summary>
        /// <response code="204">The ID has been set as the default contact method</response>
        /// <response code="400">Contact not found for specified ID</response>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [HttpPost]
        [Route("contact-details/{id}/isDefault")]
        public IActionResult UpdateContactIsDefault(int id, [FromBody] ContactDetails request)
        {
            try
            {
                _updateContactDetailsIsDefaultUseCase.Execute(id, request);
                return Ok();
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
    }
}
